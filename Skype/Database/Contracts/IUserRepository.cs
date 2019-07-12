using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Database
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserExist(string name);
        User GetByName(string name);
    }
}
