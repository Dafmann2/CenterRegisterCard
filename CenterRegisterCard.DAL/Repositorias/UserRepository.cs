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
    public class UserRepository : IBaseRepository<User>
    {
        private readonly CenterRegisterCardContext _context;

        public UserRepository(CenterRegisterCardContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<bool> Create(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> Get(int id)
        {
            //return await _context.Users.FirstOrDefaultAsync(x=>x.INN==id);
            return await _context.Users.FirstOrDefaultAsync(); ;
        }

        public async Task<List<User>> Select()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByPassport(string passport)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.PassportSeriesNumber == passport);
        }

        public Task<User> DeleteByPassport(string passport)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Update(User entity)
        {
            try {
            _context.Users.Update(entity);
            _context.SaveChanges();
            return entity;
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                return entity;
            }
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }
    }
}
