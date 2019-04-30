using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Subscribers = new List<ApplicationUser>();
            Subscriptions = new List<ApplicationUser>();
            Subscriptionss = new Collection<Subscription>();
            Subscriberss = new Collection<Subscription>();
        }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public System.DateTime BirthDate { get; set; }

        public List<ApplicationUser> Subscriptions { get; set; }
        public List<ApplicationUser> Subscribers { get; set; }

        public ICollection<Subscription> Subscriptionss { get; private set; }
        public ICollection<Subscription> Subscriberss { get; private set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            userIdentity.AddClaim(new Claim("Nickname", Nickname));
            return userIdentity;
        }

        public bool IsSubscribedTo(string userId)
        {
            return Subscriptionss.Any(s => s.PublisherId == userId && s.SubscriberId == Id);
        }
    }
}