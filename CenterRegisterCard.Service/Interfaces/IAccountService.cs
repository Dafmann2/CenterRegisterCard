using CenterRegisterCard.Domain.Models;
using CenterRegisterCard.Domain.Response;
using CenterRegisterCard.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
        Task<BaseResponse<bool>> ChangeData(ChangeDataViewModel model);
        Task<BaseResponse<bool>> ChangeImage(User model);
    }
}
