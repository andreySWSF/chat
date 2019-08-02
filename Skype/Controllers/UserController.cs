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

        [EnableCors("AllowAllOrigin")]
        [Route("SearchUser")]
        [HttpPost]
        public IActionResult SearchUser([FromBody] Search body)
        {
           
            List<User> usersToResponce = new List<User>();
            IQueryable<User> users = _userService.GetMatchedUsers(body.query);
           
            foreach(var u in users)
            {
                User buffUser = new User() { Id = u.Id, NickName = u.NickName };
                usersToResponce.Add(buffUser);
            }

            return Json(usersToResponce);

        }




        //[EnableCors("AllowAllOrigin")]
        //[Route("SendInviteToUser")]
        //[HttpPost]
        //public IActionResult SendInviteToUser([FromBody] Models.DBModels.BaseModel body)
        //{

        //    return Json("");

        //}



    }

}

