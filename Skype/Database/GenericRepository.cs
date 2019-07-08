using Microsoft.EntityFrameworkCore;
using Skype.Database;
using Skype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models.VModels
{
    public class GenericRepository<T> : IRepository<T> where T: class, IBaseModel
    {
        SkypeContext _context;
        DbSet<T> table;
       // EnvironmentVariableTarget v;

        public GenericRepository(SkypeContext context)
        {
            this._context = context;
            table = this._context.Set<T>();
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

        public IEnumerable<T> GetAll()
        {            
            return table;
        }

        public T Get(string id) //where T : class, IBaseModel
        {            
           // return table.SingleOrDefault(el=>el.Id == id);
            return table.SingleOrDefault(el=>el.Id == id);
        }

        public void Create(T item)
        {
            table.Add(item);
            Save();



        }

        public void Update(T item)// where T : class, IBaseModel
        {
            //table.Where<T>(t=>t.Id==item.id);
            //Save();
        }

        public void Delete(string id)
        {
            
        }

        public void Save()
        {
           _context.SaveChanges();
        }
    }
}
