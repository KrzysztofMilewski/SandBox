using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace SandBox.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
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
                WithMany(u=>u.Subscriptionss).
                WillCascadeOnDelete(false);

            //publisher has many subscribers

            modelBuilder.Entity<Subscription>().
                HasRequired(s=>s.Publisher).
                WithMany(u=>u.Subscriberss).
                WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}