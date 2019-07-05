using Skype.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models
{
    public class Delivery : DBModels.IBaseModel
    {
       
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public bool IsDelivered { get; set; }
    }
}
