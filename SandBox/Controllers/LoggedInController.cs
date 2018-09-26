using SandBox.Models;
using SandBox.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SandBox.Controllers
{
    [Authorize]
    public class LoggedInController : Controller
    {
        private ApplicationDbContext _context;

        public LoggedInController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //fetch all posts from subscribed users
        [Route("Home")]
        public ActionResult Index()
        {
            var currentUser = _context.Users.Include(u=>u.Subscriptions).FirstOrDefault(u => u.UserName == User.Identity.Name);
            var subscriptions = currentUser.Subscriptions;

            List<string> ids = new List<string>();

            foreach (var publisher in subscriptions)
            {
                ids.Add(publisher.Id);
            }

            var postsViewModel = new List<PostWithCommentsViewModel>();

            foreach (var publisherId in ids)
            {
                var posts = _context.Posts.Where(p => p.PublisherId == publisherId).ToList();
                foreach (var post in posts) 
                {
                    postsViewModel.Add(new PostWithCommentsViewModel()
                    {
                        Post = post,
                        Comments = _context.Comments.Include(c => c.CommentingUser).Where(p => p.PostId == post.Id).ToList(),
                        ViewingOwnPosts = false
                    });
                }
            }

            postsViewModel = postsViewModel.OrderByDescending(p => p.Post.DatePublished).ToList();

            return View(postsViewModel.AsEnumerable());
        }
    }
}