using SandBox.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SandBox.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //fetch all available users apart the logged in one
        public ActionResult FindUsers()
        {
            return View();
        }

        public ActionResult SubscribeToUser(string id)
        {
            var subscriber = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            var publisher = _context.Users.FirstOrDefault(u => u.Id == id);

            subscriber.Subscriptions.Add(publisher);
            publisher.Subscribers.Add(subscriber);

            _context.SaveChanges();

            return RedirectToAction("FindUsers");
        }

        public ActionResult UnsubscribeFromUser(string id)
        {
            var subscriber = _context.Users.Include(u=>u.Subscriptions).FirstOrDefault(u => u.UserName == User.Identity.Name);
            var publisher = _context.Users.Include(u=>u.Subscribers).FirstOrDefault(u => u.Id == id);

            subscriber.Subscriptions.Remove(publisher);
            publisher.Subscriptions.Remove(subscriber);

            _context.SaveChanges();

            return RedirectToAction("FindUsers");
        }
    }
}