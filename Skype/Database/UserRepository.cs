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
    public class UserRepository : GenericRepository<User>, Database.IUserRepository
    {
         SkypeContext _skypeContext;
        //  private readonly IMapper _mapper;
        //  SkypeContext db;
        DbSet<User> _table;
        
        public UserRepository(SkypeContext context) : base(context)
        {
            //_skypeContext = context;
            _table = context.Set<User>();
            //  _mapper = mapper;
            //this.GetAll<Chat>();
        }
      //  public 
        public User GetByName(string name)
        {
            var user = _table.SingleOrDefault(el => el.NickName == name);
            if (user == null) throw new Exception();
            else return user;            
        }

       
    }
}
