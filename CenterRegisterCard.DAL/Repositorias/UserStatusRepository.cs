using CenterRegisterCard.DAL.Interfaces;
using CenterRegisterCard.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.DAL.Repositorias
{
    public class UserStatusRepository : IBaseRepository<UserStatus>
    {
        private readonly CenterRegisterCardContext _context;

        public UserStatusRepository(CenterRegisterCardContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<bool> Create(UserStatus entity)
        {
            _context.UserStatuses.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(UserStatus entity)
        {
            _context.UserStatuses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<UserStatus> DeleteByPassport(string passport)
        {
            throw new NotImplementedException();
        }

        public async Task<UserStatus> Update(UserStatus entity)
        {
            _context.UserStatuses.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<UserStatus> GetAll()
        {
            return _context.UserStatuses;
        }
    }
}
