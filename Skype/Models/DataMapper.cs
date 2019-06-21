using Skype.Models.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Skype.Database;
using Microsoft.EntityFrameworkCore;

namespace Skype.Models
{
    public class DataMapper : IRepository<User>
    {

        private SkypeContext db;

        //public DataMapper()
        //{
        //    this.db = new SkypeContext();
        //}
        //public DataMapper()
        //{

        //}

                     

        public void Map()
        {
            Mapper.Initialize(u => u.CreateMap<User, UserVM>());

            //var users =
            //    Mapper.Map<IEnumerable<User>, List<IndexUserViewModel>>(repo.GetAll());
        }

        public IEnumerable<User> GetBookList()
        {
            return db.Users;
        }

        public User GetUser(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User book = db.Users.Find(id);
            if (book != null)
                db.Users.Remove(book);
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

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}


