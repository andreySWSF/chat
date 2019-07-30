using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Database
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User>GetUserListBySymb(string symbols);
        bool IsUserExist(string name);       
        User GetByName(string name);
        User GetById(string id);
        void SetUserConnection(string userIdFrom, string userIdTo);
    }
}
