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
    public class EmployeeRepository : IBaseRepository<Employee>
    {
        private readonly CenterRegisterCardContext _context;

        public EmployeeRepository(CenterRegisterCardContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<bool> Create(Employee entity)
        {
            _context.Employees.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Employee entity)
        {
            _context.Employees.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> Update(Employee entity)
        {
            _context.Employees.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<Employee> GetAll()
        {
            return _context.Employees;
        }
    }
}
