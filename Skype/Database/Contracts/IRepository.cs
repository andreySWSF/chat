using Skype.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Database
{
    public interface IRepository : IDisposable
        //where T : class
    {
        IEnumerable<T> GetAll<T>() where T: BaseModel;
        T Get<T>(int id) where T : BaseModel;
        void Create<T>(T item);
        void Update<T>(T item);
        void Delete<T>(int id);
        void Save();

    }
}
