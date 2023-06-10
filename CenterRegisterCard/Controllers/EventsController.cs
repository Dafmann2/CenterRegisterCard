using CenterRegisterCard.DAL.Repositorias;
using CenterRegisterCard.DAL;
using CenterRegisterCard.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CenterRegisterCard.DAL.Interfaces;
using CenterRegisterCard.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CenterRegisterCard.Domain.ViewModels.Account;

namespace CenterRegisterCard.Controllers
{
    public class EventsController : Controller
    {
        private readonly CenterRegisterCardContext _context;

        public EventsController(CenterRegisterCardContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Events()
        {
            ViewBag.EventsList = await _context.Events.ToArrayAsync();
            return View();
        }


        public async Task<IActionResult> EventViewPage(int passport)
        {
            Event eventview = _context.Events.Where(x=>x.EventId==passport).FirstOrDefault();
            return View(eventview);
        }
    }
}
