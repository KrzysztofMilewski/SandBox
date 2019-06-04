using Infrastructure.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public object Posts { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            EFConfig.ConfigureEF(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}