using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Database
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByName(string name);
    }
}
