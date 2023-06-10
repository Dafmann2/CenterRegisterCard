using CenterRegisterCard.Domain.Models;
using CenterRegisterCard.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.ActiveDirectory;
using System.Threading.Tasks;

namespace CenterRegisterCard.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string passport)
        {
            var response = await _userService.GetByPassport(passport);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [Authorize(Roles ="Employee")]
        public async Task<IActionResult> Delete(string passport)
        {
            var response = await _userService.DeleteUser(passport);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Save(string passport)
        {
            
            if(passport == null)
            {
                return View(); 
            }



            var response = await _userService.GetByPassport(passport);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Save(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.PassportSeriesNumber == null)
                {
                    return View();
                }
                else
                {
                    await _userService.Edit(user.PassportSeriesNumber, user);
                }
            }
            ////////////////Edit///////////////this
            return RedirectToAction("GetUser");
        }
    }
}
