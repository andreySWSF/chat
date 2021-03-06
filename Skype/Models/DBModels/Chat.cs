﻿using Skype.Models;
using Skype.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models
{
    public class Chat : BaseModel, IBaseModel
    {
     
        public string ChatName { get; set; }
        public List<Message> Messages { get; set; }
        //  public List<User> Users { get; set; }
        public List<UserChat> UserChats { get; set; }

        public Chat()
        {
            UserChats = new List<UserChat>();
        }
    }
}
