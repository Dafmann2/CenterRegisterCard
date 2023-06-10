using CenterRegisterCard.DAL;
using CenterRegisterCard.DAL.Interfaces;
using CenterRegisterCard.Models;
using CenterRegisterCard.Service.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace CenterRegisterCard.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IUserService _usersService ;
        //private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment ;
        private readonly CenterRegisterCardContext _context;
        public HomeController(CenterRegisterCardContext context,IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _usersService = userService;
            _webHostEnvironment = webHostEnvironment;
        }



        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
           ViewBag.Users = _context.Users.ToList();
            return View();
        }
        /*[HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("Index");
        }*/

        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
