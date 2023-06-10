using CenterRegisterCard.DAL;
using CenterRegisterCard.DAL.Interfaces;
using CenterRegisterCard.Domain.Enum;
using CenterRegisterCard.Domain.Response;
using CenterRegisterCard.Domain.ViewModels.Account;
using CenterRegisterCard.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CenterRegisterCard;
using Microsoft.AspNetCore.Mvc;
using CenterRegisterCard.Domain.Models;

namespace CenterRegisterCard.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<SocialCard> _socialcardRepository;
        private readonly IBaseRepository<Employee> _employeeRepository;
        private readonly IBaseRepository<CategoryBeneficiary> _categoryRepository;
        private readonly ILogger<AccountService> _logger;
        private readonly CenterRegisterCardContext _applicationDbContext;
        private ClaimsIdentity result;
        public static User userchange;
        public static User userforgot;


        public AccountService(IBaseRepository<User> userRepository, IBaseRepository<SocialCard> socialcardRepository,IBaseRepository<CategoryBeneficiary> categoryRepository,
          IBaseRepository<Employee> employeeRepository, CenterRegisterCardContext applicationDbContext, ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
            _socialcardRepository=socialcardRepository;
            _categoryRepository=categoryRepository;
            _employeeRepository=employeeRepository;
            _applicationDbContext=applicationDbContext;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login &
                x.PassportSeriesNumber==model.PassportSeriesNumber &
                x.Inn==model.INN &
                x.PhoneNumber==model.PhoneNumber);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                    };
                }

                user = new User()
                {
                    PassportSeriesNumber = model.PassportSeriesNumber,
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    Inn = model.INN,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    CategoryBeneficiaryId = model.CategoryBeneficiaryID,
                    Login = model.Login,
                    Password = model.Password,

                    DocumentImgName = CenterRegisterCardContext.filename,
                    BirthdayDate = model.BirthdayDate,
                    UserStatusId=4
                };

                await _userRepository.Create(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            { 
                Employee employee = await _applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Login == model.Login & x.Password==model.Password);
                User user =  await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Login == model.Login & x.Password==model.Password);
                if (user == null & employee == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }
                if (user !=null & employee != null)
                {
                    if (user.Password != model.Password & employee.Password != model.Password)
                    {
                        return new BaseResponse<ClaimsIdentity>()
                        {
                            Description = "Неверный пароль или логин"
                        };
                    }
                }
                if (user != null)
                {
                    result = Authenticate(user);
                    CenterRegisterCardContext.userAccount = user;
                    CenterRegisterCardContext.socialCard = _socialcardRepository.GetAll().FirstOrDefault(x => x.PassportSeriesNumberUser == user.PassportSeriesNumber);
                    CenterRegisterCardContext.categoryBeneficiary = _categoryRepository.GetAll().FirstOrDefault(x => x.CategoryBeneficiaryId == user.CategoryBeneficiaryId);
                }
                else if (employee != null)
                {
                    result = AuthenticateEmployee(employee);
                    CenterRegisterCardContext.employeeAccount = employee;
                }
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (CenterRegisterCardContext.userAccount != null)
                {
                    userchange = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.PassportSeriesNumber == CenterRegisterCardContext.userAccount.PassportSeriesNumber);
                }
                if (CenterRegisterCardContext.userEmailChangePassword != null)
                {
                    userforgot = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.PassportSeriesNumber == CenterRegisterCardContext.userEmailChangePassword.PassportSeriesNumber);
                }
                if (userchange == null & userforgot == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользователь не найден"
                    };
                }
                if (userchange != null)
                {
                    userchange.Password = model.Password;
                    await _userRepository.Update(userchange);
                }
                if(userforgot != null)
                {
                    userforgot.Password = model.Password;
                    await _userRepository.Update(userforgot);
                    CenterRegisterCardContext.userEmailChangePassword = null;
                }
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Пароль обновлен"
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


        public async Task<BaseResponse<bool>> ChangeImage(User user)
        {
            try
            {
                /*User user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.PassportSeriesNumber == CenterRegisterCardContext.userAccount.PassportSeriesNumber);*/
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользователь не найден"
                    };
                }

/*                user.DocumentImgName = model.ImageFile.FileName;*/
                await _userRepository.Update(user);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Пользователь обновлен"
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


            public async Task<BaseResponse<bool>> ChangeData(ChangeDataViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.PassportSeriesNumber == CenterRegisterCardContext.userAccount.PassportSeriesNumber);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользователь не найден"
                    };
                }

                user.PassportSeriesNumber = model.PassportSeriesNumber;
                user.Email = model.Email;
                user.Surname = model.Surname;
                user.Patronymic = model.Patronymic;
                user.PhoneNumber = model.PhoneNumber;
                user.Inn = model.INN;
                user.CategoryBeneficiaryId = model.CategoryBeneficiaryID;
                user.Login = model.Login;
                user.Name = model.Name;
                user.BirthdayDate = model.BirthdayDate;
                await _userRepository.Update(user);
                CenterRegisterCardContext.userAccount= user;

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Пароль обновлен"
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


        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        private ClaimsIdentity AuthenticateEmployee(Employee employee)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, employee.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "Employee")
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        /*private ClaimsIdentity Authenticate(Employer employer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }*/
    }
}
