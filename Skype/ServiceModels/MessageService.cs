using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.ServiceModels
{
    public class MessageService
    {
        public SkypeContext _skypeContext;

        public MessageService(SkypeContext skypeContext)
        {
            _skypeContext = skypeContext;
        }
    }
}
