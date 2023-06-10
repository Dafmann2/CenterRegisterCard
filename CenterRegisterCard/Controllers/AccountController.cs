using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using CenterRegisterCard.Service.Interfaces;
using CenterRegisterCard.Domain.ViewModels.Account;
using CenterRegisterCard.DAL;
using CenterRegisterCard.FormatsData;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using CenterRegisterCard.Domain.Models;
using CenterRegisterCard.EmailFunctions;
using CenterRegisterCard.Domain.Response;
using CenterRegisterCard.Service;

namespace CenterRegisterCard.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly CenterRegisterCardContext _context;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AccountController(IAccountService accountService, CenterRegisterCardContext context, IWebHostEnvironment webHostEnvironment, IUserService userService)
        {
            _accountService = accountService;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult PersonalAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChatTechnicalSupportUser()
        {
            return View();
        }

        //public ActionResult Upload(ImageUploadViewModel model)

        public async Task<IActionResult> Upload(ImageUploadViewModel model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                if (fileExtension.ToLower() == ".pdf")
                {
                    string fileName = Path.GetFileName(model.ImageFile.FileName);
                    string wwwRootPath = @"" + _webHostEnvironment.WebRootPath;
                    string filePath = Path.Combine(@$"{wwwRootPath}\DocumentUserData", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ImageFile.CopyTo(stream);
                    }
                    CenterRegisterCardContext.userAccount.DocumentImgName = model.ImageFile.FileName;
                    CenterRegisterCardContext.userAccount.UserStatusId = 1;
                    _context.SaveChanges();
                    var response = _accountService.ChangeImage((User)CenterRegisterCardContext.userAccount);
                    // var response = _userService.Edit(model.ImageFile.FileName, CenterRegisterCardContext.userAccount);
                }
                ModelState.AddModelError("ImageFile", "Разрешено загружать только файлы в формате PDF.");
            }
            return RedirectToAction("PersonalAccount"); ;

            /*if (ModelState.IsValid)
            {
                var response = await _accountService.ChangePassword(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);*/
        }


        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.category = _context.CategoryBeneficiaries.ToList();
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model, string filename, DateTime date)
        {
            if (ModelState.IsValid)
            {
               CenterRegisterCardContext.filename = filename;
                model.BirthdayDate = DateOnly.FromDateTime(date);
               CenterRegisterCardContext.userAccountreg = model.PassportSeriesNumber;
                string wwwRootPath = @"" + _webHostEnvironment.WebRootPath;
                CenterRegisterCardContext.categoryBeneficiaryreg = _context.CategoryBeneficiaries.FirstOrDefault(x => x.CategoryBeneficiaryId == model.CategoryBeneficiaryID);
                var helper = new WordDocumentation(@$"{wwwRootPath}\WordStatement\UserStatement.docx");
                var items = new Dictionary<string, string>
            {
                {"<surname>",model.Surname},
                {"<name>",model.Name },
                {"<partonomic>",model.Patronymic },
                {"<passport>",model.PassportSeriesNumber },
                {"<birthday>",model.BirthdayDate.ToString("dd.MM.yyyy") },
                {"<numberphone>",model.PhoneNumber },
                {"<inn>",model.INN},
                {"<category>",CenterRegisterCardContext.categoryBeneficiaryreg.CategoryName},
                {"<datenow>",DateTime.Now.ToString("dd.MM.yyyy")}
            };
                helper.Process(items,wwwRootPath);

                


                model.BirthdayDate = DateOnly.FromDateTime(date);
                var response = await _accountService.Register(model);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);

        }


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            CenterRegisterCardContext.userAccount = null;
            CenterRegisterCardContext.employeeAccount = null;
            return RedirectToAction("Index", "Home");
        }

        /*[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ApplicationDbContext.userAccount = null;
            return RedirectToAction("Index", "Home");
        }*/

        [HttpGet]
        public IActionResult ChangePassword()
        {
                return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            /*if (ModelState.IsValid)
            {
                var response = await _accountService.ChangePassword(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            var modelError = ModelState.Values.SelectMany(v => v.Errors);

            return StatusCode(StatusCodes.Status500InternalServerError, new { modelError.FirstOrDefault().ErrorMessage });*/
            if (ModelState.IsValid)
            {
                var response = await _accountService.ChangePassword(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangeData() => View();
        [HttpPost]
        public async Task<IActionResult> ChangeData(ChangeDataViewModel model, DateTime date)
        {
            if (ModelState.IsValid)
            {
                
                model.BirthdayDate = DateOnly.FromDateTime(date);
                var response = await _accountService.ChangeData(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        public IActionResult PINcodeActivate(ForgotPasswordViewModels models)
        {
            if (models.PINcode == CenterRegisterCardContext.EmailMessageRecovery)
            {
                return RedirectToAction("ChangePassword", "Account");
            }
            else
            {
                return RedirectToAction("ForgotPassword", "Account");
            }
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModels model)
        {
            if (model != null)
            {
                User user = _context.Users.FirstOrDefault(x=>x.Email==model.Email);
                if (ModelState.IsValid & user != null)
                {
                    string wwwRootPath = @"" + _webHostEnvironment.WebRootPath;
                    CenterRegisterCardContext.userEmailChangePassword = user;
                    EmailDataFunctions.SendEmail(user, wwwRootPath);
                }
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return View();
        }
    }
}
