using CenterRegisterCard.DAL.Interfaces;
using CenterRegisterCard.Domain.Enum;
using CenterRegisterCard.Domain.Models;
using CenterRegisterCard.Domain.Response;
using CenterRegisterCard.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;

        public async Task<IBaseResponse<User>> CreateUser(User useradd)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = new User()
                {
                    PassportSeriesNumber = useradd.PassportSeriesNumber,
                    CategoryBeneficiaryId = useradd.CategoryBeneficiaryId,
                    Email = useradd.Email,
                    Inn = useradd.Inn,
                    Login = useradd.Login,
                    Password = useradd.Password,
                    Patronymic = useradd.Patronymic,
                    Name = useradd.Name,
                    PhoneNumber = useradd.PhoneNumber,
                    Surname = useradd.Surname
                };
                await _userRepository.Create(user);
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteUser(string passport)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x=>x.PassportSeriesNumber==passport);
                if (user == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                await _userRepository.Delete(user);
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<User>> Edit(string filename,User useraccept)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                /*var user = await _userRepository.GetAll().FirstOrDefaultAsync(x=>x.PassportSeriesNumber== useraccept.PassportSeriesNumber);
                if(user == null)
                {
                    baseResponse.StatusCode=StatusCode.UserNotFound;
                    baseResponse.Description = "User not found";
                    return baseResponse;
                }*/
                User user = useraccept;
                user.DocumentImgName = "gggggg";
                await _userRepository.Update(user);
                return  baseResponse;
            }
            catch(Exception ex) 
            {
                return new BaseResponse<User>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            
        }

            public async Task<IBaseResponse<User>> GetByPassport(string passport)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x=>x.PassportSeriesNumber==passport);
                if(user == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound; 
                    return baseResponse;
                }
                baseResponse.Data = user;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<User>>> GetUsers()
        {
            var baseResponse = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await _userRepository.GetAll().ToListAsync();
                if(users.Count() > 0)
                {
                    baseResponse.Description = "Найдено 0";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = users;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
