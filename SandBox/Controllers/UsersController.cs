using SandBox.Models;
using System.Linq;
using SandBox.ViewModels;
using System.Web.Mvc;
using System.Data.Entity;

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
            var users = _context.Users.
                                    Where(u => u.Email != User.Identity.Name).
                                    Select(u => new UserViewModel()
                                    {
                                        Id = u.Id,
                                        Nickname = u.Nickname
                                    }).ToList();

            var currentUser = _context.Users.Include(u=>u.Subscriptions).Include(u=>u.Subscribers).FirstOrDefault(u => u.Email == User.Identity.Name);

            foreach (var user in users)
            {
                if (currentUser.Subscriptions.FirstOrDefault(s => s.Id == user.Id) != null)
                    user.AlreadySubscribedTo = true;
                else
                    user.AlreadySubscribedTo = false;
            }

            return View(users);
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