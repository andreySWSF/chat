using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models
{
    public class SkypeContext : DbContext
    {
        public SkypeContext(DbContextOptions<SkypeContext> options)
          : base(options)
        { _ = Database.EnsureCreated(); }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Deliver> Delivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          //  modelBuilder.Entity<User>().HasData(new User { NickName = "Vasia", Password = "134455"});
        }
    }
}
