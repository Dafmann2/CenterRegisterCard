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
    public interface IEmployeeService
    {
        Task<BaseResponse<bool>> ChangeActivateUser(string passport,string path);
        Task<BaseResponse<bool>> ChangeBlockUser(string passport);
        Task<BaseResponse<bool>> CreateCard(string passport);
    }
}
