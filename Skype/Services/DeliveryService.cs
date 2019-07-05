using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.ServiceModels
{
    public class DeliveryService
    {
        public SkypeContext _skypeContext;

        public DeliveryService(SkypeContext skypeContext)
        {
            _skypeContext = skypeContext;
        }
    }
}
