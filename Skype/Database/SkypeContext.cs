using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models
{
    public class SkypeContext : IdentityDbContext<User>
    {
        public SkypeContext()
        {
        }

        public SkypeContext(DbContextOptions<SkypeContext> options)
          : base(options)
        { _ = Database.EnsureCreated(); }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }       
        public DbSet<Delivery> Deliverys { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<UserMessage>()
            //    .HasKey(t => new { t.UserId, t.MessageId });
            //modelBuilder.Entity<UserMessage>()
            //    .HasOne(um => um.User)
            //    .WithMany(s => s.UserMessages)
            //    .HasForeignKey(sc => sc.UserId);
            //modelBuilder.Entity<UserMessage>()
            //    .HasOne(sc => sc.Message)
            //    .WithMany(c => c.UserMessages)
            //    .HasForeignKey(sc => sc.MessageId);

            modelBuilder.Entity<UserChat>()
               .HasKey(t => new { t.UserId, t.ChatId });
            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.User)
                .WithMany(s => s.UserChats)
                .HasForeignKey(sc => sc.UserId);
            modelBuilder.Entity<UserChat>()
                .HasOne(sc => sc.Chat)
                .WithMany(c => c.UserChats)
                .HasForeignKey(sc => sc.ChatId);

            modelBuilder.Entity<UserConnection>()
                .HasKey(u => new { u.UserFromId, u.UserToId });
            //modelBuilder.Entity<UserMessage>()
            //   .HasOne(um => um.User)
            //   .WithMany(s => s.UserMessages)
            //   .HasForeignKey(sc => sc.UserId);
            //modelBuilder.Entity<UserMessage>()
            //    .HasOne(sc => sc.Message)
            //    .WithMany(c => c.UserMessages)
            //    .HasForeignKey(sc => sc.MessageId);


            //modelBuilder.Entity<UserChat>()
            //    .HasKey(t => new { t.UserId, t.ChatId });
            //modelBuilder.Entity<UserMessage>()
            //   .HasOne(um => um.User)
            //   .WithMany(s => s.UserMessages)
            //   .HasForeignKey(sc => sc.UserId);
            //modelBuilder.Entity<UserMessage>()
            //    .HasOne(sc => sc.Message)
            //    .WithMany(c => c.UserMessages)
            //    .HasForeignKey(sc => sc.MessageId);

            //modelBuilder.Entity<UserConnection>()
            //   .HasKey(t => new { t.UserFromId, t.UserToId });
            //modelBuilder.Entity<UserMessage>()
            //   .HasOne(um => um.User)
            //   .WithMany(s => s.UserMessages)
            //   .HasForeignKey(sc => sc.UserId);
            //modelBuilder.Entity<UserMessage>()
            //    .HasOne(sc => sc.Message)
            //    .WithMany(c => c.UserMessages)
            //    .HasForeignKey(sc => sc.MessageId);



        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);


        //  //  modelBuilder.Entity<User>().HasData(new User { NickName = "Vasia", Password = "134455"});
        //}
    }
}
