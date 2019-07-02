using Microsoft.EntityFrameworkCore;
using Skype.Database;
using Skype.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models.VModels
{
    public class GenericRepository : IRepository
    {
        SkypeContext _context;

        public GenericRepository(SkypeContext context)
        {
            this._context = context;
        }
       
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<T> GetAll<T>() where T : BaseModel
        {
            var table = this._context.Set<T>();
            return table;
        }

        public T Get<T>(int id) where T : BaseModel
        {
            var table = this._context.Set<T>();
            return table.SingleOrDefault(el=>el.Id == id);
        }

        public void Create<T>(T item)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
