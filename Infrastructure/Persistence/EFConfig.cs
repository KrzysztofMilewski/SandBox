using Infrastructure.Models;
using System.Data.Entity;

namespace Infrastructure.Persistence
{
    class EFConfig
    {
        public static void ConfigureEF(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().
                HasRequired(c => c.CommentingUser).
                WithMany().
                WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>().
                HasRequired(c => c.Post).
                WithMany(p => p.Comments);

            //subscriber can be subsribed to many publishers

            modelBuilder.Entity<Subscription>().
                HasRequired(s => s.Subscriber).
                WithMany(u => u.Subscriptions).
                WillCascadeOnDelete(false);

            //publisher has many subscribers

            modelBuilder.Entity<Subscription>().
                HasRequired(s => s.Publisher).
                WithMany(u => u.Subscribers).
                WillCascadeOnDelete(false);


            modelBuilder.Entity<EmailMessage>().
                HasRequired(em => em.Sender).
                WithMany(s => s.EmailsSent).
                WillCascadeOnDelete(false);

            modelBuilder.Entity<EmailMessage>().
                HasRequired(em => em.Receiver).
                WithMany(r => r.EmailsReceived).
                WillCascadeOnDelete(false);

            modelBuilder.Entity<EmailMessage>().
                Property(em => em.Message).IsRequired();
            modelBuilder.Entity<EmailMessage>().
                Property(em => em.Title).IsRequired();
        }
    }
}
