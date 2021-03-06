﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser, IBaseModel
    {

        public override string Id { get; set; }
        public string NickName { get; set; }
        public List<Message> Messages { get; set; }
        //public string ConnectionId { get; set; }
        public List<Chat> Chats { get; set; }
        public List<UserConnection> FromUserToUser { get; set; }
        // public int ChatId { get; set; }
        public string Password { get; set; }     
        public List<UserChat> UserChats { get; set; }
        

        public User()
        {
            UserName = NickName;
            UserChats = new List<UserChat>();
        }
    }
}
