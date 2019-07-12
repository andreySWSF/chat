using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skype.Models;
using Skype.ServiceModels;
using Skype.Services.Contracts;

namespace Skype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    { 
        User ivan = new User { NickName = "Ivan", Password = "sdfg" };
        //IMapper _mapper = new Mapper();
        IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        

        [EnableCors("AllowAllOrigin")]
        [HttpPost]
        [Route("PostUserResult")]
        public IActionResult UserValidation([FromBody]User user)
        {

            // return new PhysicalFileResult(Path.Combine(env.WebRootPath, "index.html"), "text/html");

            var checkResult = CheckUser(user);

            return Json(true);

        }

        public bool CheckUser(User user)
        {
            return user.Equals(ivan);
           
        }

    }
}
