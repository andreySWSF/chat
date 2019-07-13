using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skype.Models;
using Skype.Models.VModels;
using Skype.Services;
using Skype.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skype.Controllers
{
   // [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        UserService _userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, UserService userService)//
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        [EnableCors("AllowAllOrigin")]
        [HttpPost]
        [Route("Login")]
        //[ValidateAntiForgeryToken]
        public IActionResult Login([FromBody]UserVM model)
        {
            var isValid = _userService.IsUserExist(model);

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
            return Json(isValid);
        }
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //Watch on GenericRepository
        [HttpPost]
        [EnableCors("AllowAllOrigin")]
        [Route("Register")]
        public IActionResult Register([FromBody] UserVM model)
        {
            var isValid = _userService.IsUserExist(model);
            if(isValid == false)
            {
                _userService.RegisterUser(model);
                return Json(false);
            }         

            else return Json(isValid);
            //if (ModelState.IsValid)
            //
            //    User user = await db.Users.FirstOrDefaultAsync(u => u.NickName == model.NickName);
            //    if (user == null)
            //    {
            //        // добавляем пользователя в бд

            //        //db.Users.Add(new User { NickName = model.NickName, Password = model.Password });
            //        //await db.SaveChangesAsync();

            //        //await Authenticate(model.NickName); // аутентификация

            //        //return RedirectToAction("Index", "Home");
            //    }
            //    else
            //        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            //}

        }


        //[EnableCors("AllowAllOrigin")]
        //[HttpPost]
        //[Route("PostUserResult")]
        //public IActionResult UserValidation([FromBody]User user)
        //{
        //   //
        //    // return new PhysicalFileResult(Path.Combine(env.WebRootPath, "index.html"), "text/html");

        //    //var checkResult = CheckUser(user);

        //    return Json(true);

        //}

       

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
