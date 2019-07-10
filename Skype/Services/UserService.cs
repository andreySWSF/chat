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
        public UserService(IUserRepository repository)
        {
            this._repository = repository;
        }
        public bool CheckUser(UserVM user)
        {
            User checkUser = _repository.GetByName(user.NickName);   //.Users.FirstOrDefaultAsync(u => u.NickName == model.NickName);
            return true;
        }
        public bool HasChats(string userId)
        {
            var user = this._repository.Get(userId);
           return user != null? user.UserChats.Any(): false;
        }
    }
}
