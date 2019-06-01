using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.DataAccess.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Subscription> _subscriptions;

        public SubscriptionRepository(DbContext context)
        {
            _context = context;
            _subscriptions = _context.Set<Subscription>();
        }

        public void AddSubscription(Subscription subscriptionToAdd)
        {
            _subscriptions.Add(subscriptionToAdd);
            _context.SaveChanges();
        }

        public void DeleteSubscription(Subscription subscriptionToDelete)
        {
            _subscriptions.Remove(subscriptionToDelete);
            _context.SaveChanges();
        }

        public IQueryable<Subscription> GetUserSubscriptions(string userId)
        {
            var subscriptions = _subscriptions.Where(s => s.SubscriberId == userId);
            return subscriptions;
        }

        public Subscription GetSubscription(string publisherId, string subscriberId)
        {
            var subscription = _subscriptions.SingleOrDefault(s => s.PublisherId == publisherId && s.SubscriberId == subscriberId);
            return subscription;
        }

        public IQueryable<ApplicationUser> GetUserSubscriptionsAsUsers(string userId)
        {
            var subscriptions = GetUserSubscriptions(userId).ToList();

            var users = new List<ApplicationUser>();
            var usersContext = _context.Set<ApplicationUser>();

            foreach (var subscription in subscriptions)
            {
                var user = usersContext.SingleOrDefault(u => u.Id == subscription.PublisherId);
                users.Add(user);
            }
            return users.AsQueryable();
        }
    }
}
