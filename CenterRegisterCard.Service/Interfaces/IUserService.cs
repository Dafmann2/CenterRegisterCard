using CenterRegisterCard.Domain.Models;
using CenterRegisterCard.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        Task<IBaseResponse<User>> GetByPassport(string passport);
        Task<IBaseResponse<bool>> DeleteUser(string passport);
        Task<IBaseResponse<User>> CreateUser(User user);
        Task<IBaseResponse<User>> Edit(string filename,User user); 
    }
}
