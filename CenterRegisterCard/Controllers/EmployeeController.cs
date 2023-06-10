using Microsoft.AspNetCore.Mvc;
using CenterRegisterCard.DAL;
using CenterRegisterCard.DAL.Repositorias;
using CenterRegisterCard.Domain.ViewModels.Account;
using CenterRegisterCard.FormatsData;
using CenterRegisterCard.Service.Implementations;
using CenterRegisterCard.Service.Interfaces;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CenterRegisterCard.DAL.Interfaces;
using CenterRegisterCard.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using CenterRegisterCard.Service;
using CenterRegisterCard.EmailFunctions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CenterRegisterCard.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<SocialCard> _socialCardRepository;
        private readonly CenterRegisterCardContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IUserService userService, CenterRegisterCardContext context, IBaseRepository<User> userRepository, IEmployeeService employeeService,IBaseRepository<SocialCard> socialCardRepository, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _context = context;
            _userRepository = userRepository;
            _employeeService = employeeService;
            _socialCardRepository = socialCardRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        /*public  IActionResult UsersListActivate()
        {
            var response = _userService.GetUsers();
            return View(response);
        }*/


        /*[HttpGet]
        public Task<IActionResult> ViewUserInfo(User user)
        {
            
            var response =  _context.Users.Select(x=>x.PassportSeriesNumber==user.PassportSeriesNumber);
            *//*if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {*//*
            return View(response);
            *//*}
            return RedirectToAction("Error");*//*
        }*/

        [HttpGet]
        public IActionResult EventsAdd()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UsersListActivate()
        {
            ViewBag.Users = await _context.Users.ToArrayAsync();
            /*if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {*/
            return View();
            /*}
            return RedirectToAction("Error");*/
        }

        /*        public async Task<IActionResult> StatusActivateUser(string passport)
                {
                    var user = _context.Users.ToList();
                    //user.UserStatusID = 3;
                    //_userRepository.Update(user);
                    return RedirectToAction("UsersListActivate");
                }*/


        public async Task<IActionResult> StatusActivateUser(string passport)
        {
            string wwwRootPath = @"" + _webHostEnvironment.WebRootPath;
            User user = _context.Users.FirstOrDefault(x=>x.PassportSeriesNumber == passport);
                var response = await _employeeService.ChangeActivateUser(passport,wwwRootPath);
                var response2 = await _employeeService.CreateCard(passport);
            EmailDataFunctions.SendEmailActivateUserAccount(user, wwwRootPath);
            //await DocumantaionCreate(user);
            if (response.StatusCode == Domain.Enum.StatusCode.OK & response2.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    EmailDataFunctions.SendEmailActivateUserAccount(user, wwwRootPath);
                    return RedirectToAction("UsersListActivate", "Employee");
                }
                ModelState.AddModelError("", response.Description);
            return RedirectToAction("UsersListActivate", "Employee");
        }

        public async Task<IActionResult> StatusBlockUser(string passport)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.ChangeBlockUser(passport);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("UsersListActivate", "Employee");
                }
                ModelState.AddModelError("", response.Description);
            }
            return RedirectToAction("UsersListActivate", "Employee");
        }


        public ActionResult Download(string fileName)
        {
            // Получение пути к файлу на сервере
            string wwwRootPath = @"" + _webHostEnvironment.WebRootPath;
            string filePath = Path.Combine(@$"{wwwRootPath}\UserDocumantation\", fileName);

            if (!System.IO.File.Exists(filePath))
            {

            }

            // Чтение содержимого файла в виде массива байтов
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Возврат файла для скачивания
            return File(fileBytes, "application/octet-stream", fileName);
        }

        public ActionResult DownloadPDFDocument(string fileName)
        {
            // Получение пути к файлу на сервере
            string wwwRootPath = @"" + _webHostEnvironment.WebRootPath;
            string filePath = Path.Combine(@$"{wwwRootPath}\DocumentUserData\", fileName);

            if (!System.IO.File.Exists(filePath))
            {

            }

            // Чтение содержимого файла в виде массива байтов
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Возврат файла для скачивания
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpGet]
        public async Task<IActionResult> EventEdit(int eventId)
        {
            Event eventedit = _context.Events.Where(x=>x.EventId==eventId).FirstOrDefault();
            return View(eventedit);
        }

        [HttpPost]
        public async Task<IActionResult> EventEdit(Event eventeditmodel)
        {
            if (ModelState.IsValid)
            {
                /*var response = await _accountService.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);*/
            }
            return View();
        }

        public async Task<IActionResult> Upload(IFormFile model)
        {
            /*if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                string fileName = Path.GetFileName(model.ImageFile.FileName);
                string wwwRootPath = @"" + _webHostEnvironment.WebRootPath;
                string filePath = Path.Combine(@$"{wwwRootPath}\ImagesUsers", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }
                CenterRegisterCardContext.userAccount.DocumentImgName = model.ImageFile.FileName;
                var response = _accountService.ChangeImage(model);
                // var response = _userService.Edit(model.ImageFile.FileName, CenterRegisterCardContext.userAccount);
            }*/
            return RedirectToAction("PersonalAccount"); ;
        }



        public async Task<IActionResult> DocumantaionCreate(User model)
        {              
                DateOnly dateTime = DateOnly.FromDateTime(DateTime.Now);
                Random rnd = new Random();
                SocialCard socialCard = new SocialCard
                {
                    NumberCard = "80802343" + rnd.Next(1000, 9999).ToString() + rnd.Next(1000, 9999),
                    Cvc = rnd.Next(100, 999),
                    DateEndActive = dateTime,
                    DateFormalization=dateTime.AddYears(10),
                    Balance=0,
                    PassportSeriesNumberUser=model.PassportSeriesNumber,
                    StatusCardId=2
                };
                _context.SocialCards.Add(socialCard);
                _context.SaveChanges();
                string wwwRootPath = @"" + _webHostEnvironment.WebRootPath;
                var helper = new WordDocumentation(@$"{wwwRootPath}\WordStatement\EmployeeStatement.docx");
                CenterRegisterCardContext.userActiveView = model;
                var items = new Dictionary<string, string>
                {
                {"<surname>",model.Surname},
                {"<name>",model.Name },
                {"<partonomic>",model.Patronymic },
                {"<birthday>",model.BirthdayDate.ToString() },
                {"<employeesurname>",CenterRegisterCardContext.employeeAccount.Surename },
                {"<employeename>",CenterRegisterCardContext.employeeAccount.Surename },
                {"<employeepartonomic>",CenterRegisterCardContext.employeeAccount.Surename },
                {"<datenow>",dateTime.ToString("yyyy-MM-dd")},
                {"<datestart>",dateTime.ToString("yyyy.MM.dd")},
                {"<dateend>",dateTime.AddYears(10).ToString("yyyy.MM.dd")}
                };
                helper.Process(items,wwwRootPath);
                CenterRegisterCardContext.userActiveView = null;
                return RedirectToAction("UsersListActivate", "Employee");
        }
    }
}
