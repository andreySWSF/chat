using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skype.Models;

namespace Skype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly SkypeContext db;
     //   ChatService chatService;

        public UserController( SkypeContext context)
        {
            db = context;
        }

    }
}
