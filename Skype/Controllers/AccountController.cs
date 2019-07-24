using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Skype.Authorization;
using Skype.Models;
using Skype.Models.VModels;
using Skype.Services;
using Skype.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
            if (isValid)
                return Json(model);
            else return Json(null);
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

        }
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //Watch on GenericRepository
       
            //if (ModelState.IsValid)
            // [HttpPost]

        [EnableCors("AllowAllOrigin")]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserVM model)
        {
            var isValid = _userService.IsUserExist(model);
            if(isValid == false)
            {
                _userService.RegisterUser(model);
                User user = _userService.MapModels(model);
                user.UserName = user.NickName;
                var result = await _userManager.CreateAsync(user, model.Password);
                
                await _signInManager.SignInAsync(user, false);
                
               
                return Json(false);
            }         

            else return Json(isValid);
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
        [EnableCors("AllowAllOrigin")]
        [Route("LoginByToken")]
        [HttpPost]
        public async Task<IActionResult> LoginByToken([FromBody]UserVM user)
        {
            var username = user.NickName;
            //var password = Request.Form["password"];

            var identity = GetIdentity(username);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return Json("ass");
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt); 
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
             return Json(response);
            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private ClaimsIdentity GetIdentity(string username)
        {
            User person = _userService.GetUser(username);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.NickName),
                    new Claim(ClaimTypes.NameIdentifier, person.NickName)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }

        //[EnableCors("AllowAllOrigin")]
        //[Route("Register")]
        //public IActionResult GetUsers()
        //{
        //    return _userService.
        //}


        //private async Task Authenticate(string userName)
        //{
        //    // создаем один claim
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
        //    };
        //    // создаем объект ClaimsIdentity
        //    ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        //    // установка аутентификационных куки
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        //}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
