using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skype.Database;
using Skype.Models;
using Skype.Models.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.ServiceModels
{
    public class ChatService : GenericService<Chat>
    {


        public void Map()
        {
            SkypeContext context = new SkypeContext();
            Mapper.Initialize(u => u.CreateMap<Chat, ChatVM>());
            var g = new GenericService<User>(
            var users = Mapper.Map<IEnumerable<User>, List<UserVM>>(repo.GetAll());
        }
        public ChatService(SkypeContext skypeContext, DbSet<Chat> set) : base(skypeContext, set)
        {

        }
    }
}

