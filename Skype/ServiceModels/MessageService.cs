using Microsoft.EntityFrameworkCore;
using Skype.Models;
using Skype.Models.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.ServiceModels
{
    public class MessageService : GenericService<Message>
    {
        public MessageService(SkypeContext skypeContext, DbSet<Message> set) : base(skypeContext, set)
        {
        }
    }
}
