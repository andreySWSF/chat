using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Database
{
    public interface IUserRepository<T> : IRepository<T> where T: class
    {
    }
}
