using Microsoft.EntityFrameworkCore;
using Skype.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models.VModels
{
    public class GenericService<T> : IRepository<T> where T : class
    {
        public SkypeContext db;
        public DbSet<T> dbSet;

        public GenericService(SkypeContext skypeContext, DbSet<T> set)
        {
            db = skypeContext;
            dbSet = set;
        }
         

        public IEnumerable<T> GetListofItems()
        {           
            return dbSet;
        }

        public T GetChat(int id)
        {
            return dbSet.Find(id);
        }

        public void Create(T item)
        {
            dbSet.Add(item);
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T item = dbSet.Find(id);
            if (item != null)
                dbSet.Remove(item);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
