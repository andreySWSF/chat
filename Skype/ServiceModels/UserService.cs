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
    public class UserService : GenericService<User>
    {
        public SkypeContext _skypeContext;
        private readonly IMapper _mapper;

        //public UserService(SkypeContext skypeContext)
        //{
        //    _skypeContext = skypeContext;
        //}
        public UserService(SkypeContext skypeContext, DbSet<User> set, IMapper mapper) : base(skypeContext, set)
        {
            _mapper = mapper;
        }
    }
}
