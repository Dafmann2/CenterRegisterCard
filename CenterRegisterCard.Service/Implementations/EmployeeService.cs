using CenterRegisterCard.DAL.Interfaces;
using CenterRegisterCard.DAL;
using CenterRegisterCard.DAL.Repositorias;
using CenterRegisterCard.Domain.Models;
using CenterRegisterCard.Domain.Enum;
using CenterRegisterCard.Domain.Response;
using CenterRegisterCard.Domain.ViewModels.Account;
using CenterRegisterCard.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CenterRegisterCard.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<SocialCard> _socialcardRepository;
        private readonly IBaseRepository<Employee> _employeeRepository;
        private readonly IBaseRepository<CategoryBeneficiary> _categoryRepository;
        private readonly ILogger<AccountService> _logger;
        private readonly CenterRegisterCardContext _applicationDbContext;
        private ClaimsIdentity result;

        public EmployeeService(IBaseRepository<User> userRepository, IBaseRepository<SocialCard> socialcardRepository,  IBaseRepository<CategoryBeneficiary> categoryRepository,
          IBaseRepository<Employee> employeeRepository, CenterRegisterCardContext applicationDbContext, ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
            _socialcardRepository = socialcardRepository;
            _categoryRepository = categoryRepository;
            _employeeRepository = employeeRepository;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<BaseResponse<bool>> ChangeActivateUser(string passport,string path)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.PassportSeriesNumber == passport);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользователь не найден"
                    };
                }

                user.UserStatusId = 3;
                await _userRepository.Update(user);
                DocumantCreation(user,path);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Статус был обновлен"
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> ChangeBlockUser(string passport)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.PassportSeriesNumber == passport);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользователь не найден"
                    };
                }

                user.UserStatusId = 2;
                await _userRepository.Update(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Статус был обновлен"
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> CreateCard(string passport)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.PassportSeriesNumber == passport);
                Random rnd = new Random();
                DateTime dateTime = DateTime.Now;
                SocialCard socialCard = new SocialCard
                {
                    NumberCard = "80802343" + rnd.Next(1000, 9999).ToString() + rnd.Next(1000, 9999),
                    Cvc = rnd.Next(100, 999),
                    Balance = 0,
                    PassportSeriesNumberUser = user.PassportSeriesNumber,
                    DateEndActive= DateOnly.FromDateTime(DateTime.Now),
                    DateFormalization= DateOnly.FromDateTime(DateTime.Now),
                    StatusCardId = 2
                };
                await _socialcardRepository.Create(socialCard);
                _applicationDbContext.SocialCards.Add(socialCard);
                _applicationDbContext.SaveChanges();
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользователь не найден"
                    };
                }
                //DocumantCreation(user);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Статус был обновлен"
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public void DocumantCreation(User model,string path)
        {
            DateOnly dateTime = DateOnly.FromDateTime(DateTime.Now);
            var helper = new WordDocumentation(@$"{path}\WordStatement\EmployeeStatement.docx");
            CenterRegisterCardContext.userActiveView = model;
            var items = new Dictionary<string, string>
                {
                {"<surname>",model.Surname},
                {"<name>",model.Name },
                {"<partonomic>",model.Patronymic },
                {"<birthday>",model.BirthdayDate.ToString() },
                {"<employeesurname>",CenterRegisterCardContext.employeeAccount.Surename },
                {"<employeename>",CenterRegisterCardContext.employeeAccount.Surename },
                {"<employeepartonomic>",CenterRegisterCardContext.employeeAccount.Surename },
                {"<datenow>",dateTime.ToString("yyyy-MM-dd")},
                {"<datestart>",dateTime.ToString("yyyy.MM.dd")},
                {"<dateend>",dateTime.AddYears(10).ToString("yyyy.MM.dd")}
                };
            helper.Process(items, path);
            CenterRegisterCardContext.userActiveView = null;
        }
    }
    }
   

