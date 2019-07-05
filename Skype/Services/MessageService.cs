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
    public class MessageRepository : GenericRepository
    {
        private readonly IMapper _mapper;
        public MessageRepository(SkypeContext skypeContext, IMapper mapper) : base(skypeContext)
        {
            _mapper = mapper;
        }
    }
}
