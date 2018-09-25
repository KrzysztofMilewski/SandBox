using Microsoft.AspNet.Identity;
using SandBox.Models;
using SandBox.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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
            return View("PostForm", new PostFormViewModel() { ActionName = "Create", PageHeading = "Opublikuj nowy wpis"});
        }


        [Route("EditPost/{id}")]
        public ActionResult EditPost(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return HttpNotFound();

            if (post.PublisherId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new PostFormViewModel()
            {
                Contents = post.Contents,
                Id = post.Id,
                Title = post.Title,
                ActionName = "Edit",
                PageHeading = "Edytuj swój post"
            };

            return View("PostForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(PostFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("PostForm", viewModel);

            var currentUserId = User.Identity.GetUserId();
            var newPost = new Post()
            {
                Contents = viewModel.Contents,
                Title = viewModel.Title,
                DatePublished = System.DateTime.Now,
                PublisherId = currentUserId,
                NumberOfEdits = 0
            };

            _context.Posts.Add(newPost);
            _context.SaveChanges();

            return RedirectToAction("MyPosts");
        }


        public ActionResult Edit(PostFormViewModel viewModel)
        {
            var currentUserId = User.Identity.GetUserId();
            var post = _context.Posts.FirstOrDefault(p => p.Id == viewModel.Id && p.PublisherId == currentUserId);

            if (post == null)
                return new HttpUnauthorizedResult();

            post.Edit(viewModel.Title, viewModel.Contents);
            _context.SaveChanges();

            return RedirectToAction("MyPosts");
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
                    Comments = _context.Comments.Include(c => c.CommentingUser).Where(c => c.PostId == post.Id).ToList(),
                    ViewingOwnPosts = true
                });
            }
            
            return View(postsViewModel.OrderByDescending(vm=>vm.Post.DatePublished));
        }
    }
}