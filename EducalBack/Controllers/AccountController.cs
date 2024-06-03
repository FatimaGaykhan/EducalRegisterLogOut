using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducalBack.Models;
using EducalBack.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EducalBack.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new()
            {
                FullName=request.FullName,
                Email=request.Email,
                UserName=request.UserName,

            };

            IdentityResult result = await _userManager.CreateAsync(user,request.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

                return View();
            }
            await _signInManager.SignInAsync(user,false);

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}

