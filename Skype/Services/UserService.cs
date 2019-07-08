using Skype.Database;
using Skype.Models;
using Skype.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Services
{
    public class UserService: IUserService
    {
        private IUserRepository<User> _repository;
        public UserService(IUserRepository<User> repository)
        {
            this._repository = repository;
        }

        public bool HasChats(string userId)
        {
            var user = this._repository.Get(userId);
           return user != null? user.UserChats.Any(): false;
        }
    }
}
