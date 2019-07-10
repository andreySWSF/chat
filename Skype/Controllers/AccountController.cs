using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skype.Models;
using Skype.Models.VModels;
using Skype.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skype.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        IUserService _userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
           //if (ModelState.IsValid)
            //{
            //    User user = await db.Users.FirstOrDefaultAsync(u => u.NickName == model.NickName && u.Password == model.Password);
            //    if (user != null)
            //    {
            //        await Authenticate(model.NickName); // аутентификация

            //        return RedirectToAction("Index", "Home");
            //    }
            //    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            //}
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Watch on GenericRepository
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            //if (ModelState.IsValid)
            //{
            //    User user = await db.Users.FirstOrDefaultAsync(u => u.NickName == model.NickName);
            //    if (user == null)
            //    {
            //        // добавляем пользователя в бд
            //        db.Users.Add(new User { NickName = model.NickName, Password = model.Password });
            //        await db.SaveChangesAsync();

            //        await Authenticate(model.NickName); // аутентификация

            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            //}
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
