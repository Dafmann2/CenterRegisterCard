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
    public class SocialCardRepository: IBaseRepository<SocialCard>
    {
        private readonly CenterRegisterCardContext _context;

        public SocialCardRepository(CenterRegisterCardContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<bool> Create(SocialCard entity)
        {
            try
            {
                _context.SocialCards.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(SocialCard entity)
        {
            _context.SocialCards.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SocialCard>> Select()
        {
            return await _context.SocialCards.ToListAsync();
        }

        public async Task<SocialCard> Update(SocialCard entity)
        {
            _context.SocialCards.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<SocialCard> GetAll()
        {
            return _context.SocialCards;
        }
    }
}


