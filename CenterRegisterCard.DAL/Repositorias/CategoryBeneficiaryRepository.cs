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
    public class CategoryBeneficiaryRepository:IBaseRepository<CategoryBeneficiary>
    {
        private readonly CenterRegisterCardContext _context;
        public CategoryBeneficiaryRepository(CenterRegisterCardContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<bool> Create(CategoryBeneficiary entity)
        {
            _context.CategoryBeneficiaries.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(CategoryBeneficiary entity)
        {
            _context.CategoryBeneficiaries.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryBeneficiary>> Select()
        {
            return await _context.CategoryBeneficiaries.ToListAsync();
        }

        public async Task<CategoryBeneficiary> Update(CategoryBeneficiary entity)
        {
            _context.CategoryBeneficiaries.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<CategoryBeneficiary> GetAll()
        {
            return _context.CategoryBeneficiaries;
        }
    }
}
