using CenterRegisterCard.DAL.Interfaces;
using CenterRegisterCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterRegisterCard.DAL.Repositorias
{
    public class EventRepository : IBaseRepository<Event>
    {
        private readonly CenterRegisterCardContext _context;

        public EventRepository(CenterRegisterCardContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<bool> Create(Event entity)
        {
            _context.Events.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Event entity)
        {
            _context.Events.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public  IQueryable<Event> GetAll()
        {
            return _context.Events;
        }

        public async Task<Event> Update(Event entity)
        {
            _context.Events.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
