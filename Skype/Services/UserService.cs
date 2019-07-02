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
        private IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            this._repository = repository;
        }

        public bool HasChats(int userId)
        {
            var user = this._repository.Get<User>(userId);
           return user != null? user.UserChats.Any(): false;
        }
    }
}
