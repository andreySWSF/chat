using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public List<Message> Messages { get; set; }
        [NotMapped]
        public List<User> Contacts
        { get; set; }
        public virtual  List<Chat> Chats { get; set; }
    }
}
