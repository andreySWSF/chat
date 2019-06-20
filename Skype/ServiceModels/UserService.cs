using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.ServiceModels
{
    public class UserService
    {
        public SkypeContext _skypeContext;

        public UserService(SkypeContext skypeContext)
        {
            _skypeContext = skypeContext;
        }
    }
}
