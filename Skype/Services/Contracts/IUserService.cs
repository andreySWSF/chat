using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Services.Contracts
{
    public interface IUserService
    {
        bool HasChats(int userId);
    }
}
