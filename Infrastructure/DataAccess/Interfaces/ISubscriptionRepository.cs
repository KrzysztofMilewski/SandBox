using Infrastructure.Models;
using System.Linq;

namespace Infrastructure.DataAccess.Interfaces
{
    public interface ISubscriptionRepository
    {
        void AddSubscription(Subscription subscriptionToAdd);
        void DeleteSubscription(Subscription subscriptionToDelete);
        IQueryable<Subscription> GetUserSubscriptions(string userId);
        IQueryable<ApplicationUser> GetUserSubscriptionsAsUsers(string userId);
        IQueryable<ApplicationUser> GetUserFollowersAsUsers(string userId);
    }
}
