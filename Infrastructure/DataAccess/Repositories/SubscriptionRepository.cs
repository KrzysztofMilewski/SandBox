using Infrastructure.Models;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.DataAccess.Repositories
{
    public class SubscriptionRepository
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

        public void DeleteSubscription(string subscriberId, string publisherId)
        {
            var subscriptionToDelete = _subscriptions.SingleOrDefault(s => s.PublisherId == publisherId && s.SubscriberId == subscriberId);
            _subscriptions.Remove(subscriptionToDelete);
            _context.SaveChanges();
        }
    }
}
