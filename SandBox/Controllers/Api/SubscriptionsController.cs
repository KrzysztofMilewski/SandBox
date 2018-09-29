using Microsoft.AspNet.Identity;
using SandBox.Dtos;
using SandBox.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace SandBox.Controllers.Api
{
    [Authorize]
    public class SubscriptionsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionsController()
        {
            _context = ApplicationDbContext.Create();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }

        //temporary
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = _context.Users.Include(u=>u.Subscriptionss).First(u => u.Id == currentUserId);

            var otherUsers = _context.Users.Where(u => u.Id != currentUser.Id).AsEnumerable();

            var otherUsersDto = otherUsers.
                Select(
                u => new UserDto()
                {
                    Id = u.Id,
                    Nickname = u.Nickname,
                    SubscribedTo = currentUser.IsSubscribedTo(u.Id)
                });

            return Ok(otherUsersDto);
        }

        [HttpPost]
        public IHttpActionResult CreateSubscription(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            if (id == currentUserId)
                return BadRequest();

            if (_context.Subscriptions.Any(s => s.PublisherId == id && s.SubscriberId == currentUserId))
                return BadRequest();

            var subscription = new Subscription()
            {
                SubscriberId = User.Identity.GetUserId(),
                PublisherId = id
            };

            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteSubscription(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var subscription = _context.Subscriptions.SingleOrDefault(s => s.PublisherId == id && s.SubscriberId == currentUserId);

            if (subscription == null)
                return NotFound();

            _context.Subscriptions.Remove(subscription);
            _context.SaveChanges();

            return Ok();
        }
    }
}
