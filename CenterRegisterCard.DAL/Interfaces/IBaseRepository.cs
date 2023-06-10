using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);
        IQueryable<T> GetAll();
        Task<bool> Delete(T entity);
        Task<T> Update(T entity);
    }
}
