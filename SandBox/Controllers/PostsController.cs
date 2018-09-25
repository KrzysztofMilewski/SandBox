using Microsoft.AspNet.Identity.Owin;
using SandBox.Models;
using SandBox.ViewModels;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace SandBox.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("NewPost")]
        public ActionResult NewPost()
        {
            return View("PostForm", new PostFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePost(PostFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("PostForm", viewModel);

            if(viewModel.Id == 0) //New post
            {
                var currentUser = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

                var newPost = new Post()
                {
                    Contents = viewModel.Contents,
                    Title = viewModel.Title,
                    DatePublished = System.DateTime.Now,
                    PublisherId = currentUser.Id,
                    NumberOfEdits = 0
                };

                _context.Posts.Add(newPost);
            }
            else //edit existing
            {
                var post = _context.Posts.FirstOrDefault(p => p.Id == viewModel.Id);
                post.Title = viewModel.Title;
                post.Contents = viewModel.Contents;
                post.NumberOfEdits++;
                post.LastTimeEdited = System.DateTime.Now;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "LoggedIn");
        }


        [Route("EditPost/{id}")]
        public ActionResult EditPost(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return HttpNotFound();

            var viewModel = new PostFormViewModel()
            {
                Contents = post.Contents,
                Id = post.Id,
                Title = post.Title
            };

            return View("PostForm", viewModel);
        }

        [Route("MyPosts")]
        public ActionResult MyPosts()
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            var currentUserPosts = _context.Posts.Where(p => p.PublisherId == currentUser.Id).ToList();

            var postsViewModel = new List<PostWithCommentsViewModel>();

            foreach (var post in currentUserPosts)
            {
                postsViewModel.Add(new PostWithCommentsViewModel()
                {
                    Post = post,
                    Comments = _context.Comments.Include(c => c.CommentingUser).Where(c => c.PostId == post.Id).ToList()
                });
            }
            
            return View(postsViewModel.OrderByDescending(vm=>vm.Post.DatePublished));
        }

        [Route("DeletePost/{id}")]
        public ActionResult DeletePost(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            _context.Posts.Remove(post);
            _context.SaveChanges();

            return RedirectToAction("MyPosts");
        }
    }
}