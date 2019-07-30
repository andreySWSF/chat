using Skype.Models.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Skype.Database;
using Microsoft.EntityFrameworkCore;

namespace Skype.Models
{
    public class DataMapper : Profile
    {

        public DataMapper()
        {
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<Chat, ChatVM>().ReverseMap();
            CreateMap<Message, MessageVM>().ReverseMap();
            //CreateMap<UserVM, User>().ReverseMap();
        }

    }

       
}


