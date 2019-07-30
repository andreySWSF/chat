using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Database.Contracts
{
    public interface IChatRepository<T> : IRepository<T> where T : class
    {
    }
}
