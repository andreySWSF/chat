﻿using Skype.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models
{
    public class Message : DBModels.IBaseModel
    {
       
        public string Text { get; set; }
      //  public int UserId { get; set; }       
        public int UserDeliverId { get; set; }       
        public int ChatId { get; set; }
       // public List<UserMessage> UserMessages { get; set; }

        //public Message()
        //{
        //    UserMessages = new List<UserMessage>();
        //}

    }
}
