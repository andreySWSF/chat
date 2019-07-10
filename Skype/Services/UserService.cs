using AutoMapper;
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
        private IUserRepository _repository;
        readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        //public bool CheckUser(UserVM user)
        //{
        //    User checkUser = _repository.GetByName(user.NickName);
            
        //    return true;
        //}
        public User MapModels(UserVM user)
        {
            User userModel = new User() { NickName = user.NickName, Password = user.Password };
            return userModel;
        }
        public void RegisterUser(UserVM user)
        {
            var userToAdd = MapModels(user);
            _repository.Create(userToAdd);            
        }
        public bool HasChats(string userId)
        {
            var user = this._repository.Get(userId);
           return user != null? user.UserChats.Any(): false;
        }
    }
}
