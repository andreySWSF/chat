using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Controllers
{
    public class MessageController
    {
        private readonly SkypeContext db;

        public MessageController(SkypeContext context)
        {           
            db = context;
        }

    }
}
