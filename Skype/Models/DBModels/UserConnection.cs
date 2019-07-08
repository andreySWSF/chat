using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models
{
    public class UserConnection
    {
        [ForeignKey("User.Id")]
        public string UserFromId { get; set; }
        [ForeignKey("User.Id")]
        public string UserToId { get; set; }
    }
}
