using AutoMapper;
using Microsoft.AspNetCore.Http;
using Skype.Database;
using Skype.Models;
using Skype.Models.VModels;
using Skype.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Services
{
    public class UserService: IUserService
    {
       // private readonly RequestDelegate _next;
        private IUserRepository _repository;
        readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)//, RequestDelegate next)
        {
            this._repository = repository;
            this._mapper = mapper;
         //   this._next= next;
        }
        //public bool CheckUser(UserVM user)
        //{
        //    User checkUser = _repository.GetByName(user.NickName);
            
        //    return true;
        //}
        public void SetFiendUser(string userFromId, string userToId)
        {
            _repository.SetUserConnection(userFromId, userToId);
            
        }
        
        
         public User GetUser(string name)
        {
           return _repository.GetByName(name);
        }
        public bool IsUserExist(UserVM user)
        {
            return _repository.IsUserExist(user.NickName);           
        }

        public User MapModels(UserVM user)
        {
            User userModel = new User() { NickName = user.NickName, Password = user.Password };
            return userModel;
        }

        public void RegisterUser(UserVM user)
        {
            var userToAdd = MapModels(user);
            _repository.Create(userToAdd);
            _repository.Save();
        }

        public bool HasChats(string userId)
        {
            var user = this._repository.Get(userId);
           return user != null? user.UserChats.Any(): false;
        }
    }
}
