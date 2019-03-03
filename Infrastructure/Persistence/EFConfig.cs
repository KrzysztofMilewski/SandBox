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
                WithMany(u => u.Subscriptionss).
                WillCascadeOnDelete(false);

            //publisher has many subscribers

            modelBuilder.Entity<Subscription>().
                HasRequired(s => s.Publisher).
                WithMany(u => u.Subscriberss).
                WillCascadeOnDelete(false);
        }
    }
}
