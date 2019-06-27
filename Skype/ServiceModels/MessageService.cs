using AutoMapper;
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
        private readonly IMapper _mapper;
        public MessageService(SkypeContext skypeContext, DbSet<Message> set, IMapper mapper) : base(skypeContext, set)
        {
            _mapper = mapper;
        }
    }
}
