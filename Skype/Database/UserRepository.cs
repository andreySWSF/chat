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
    public class UserRepository : GenericRepository
    {
        // public SkypeContext _skypeContext;
        //  private readonly IMapper _mapper;
        //  SkypeContext db;

        public UserRepository(SkypeContext context) : base(context)
        {
            //  _mapper = mapper;
            //this.GetAll<Chat>();
        }
    }
}
