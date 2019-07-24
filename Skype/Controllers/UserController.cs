using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skype.Models;
using Skype.ServiceModels;
using Skype.Services;
using Skype.Services.Contracts;

namespace Skype.Controllers
{
    // [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        public IActionResult SearchUser([FromBody] string name)
        {
           User user = _userService.GetUser(name);
            return Json("0");

        }

        

    }
}
