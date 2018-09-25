using SandBox.Models;
using SandBox.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SandBox.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private ApplicationDbContext _context;

        public CommentsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentViewModel viewModel, int id)
        {
            var commentedPost = _context.Posts.FirstOrDefault(p => p.Id == id);
            var commentingUser = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            var comment = new Comment()
            {
                CommentingUserId = commentingUser.Id,
                PostId = commentedPost.Id,
                Contents = viewModel.Contents,
                DateAdded = System.DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            if (commentedPost.PublisherId == commentingUser.Id)
                return RedirectToAction("MyPosts", "Posts");
            else
                return RedirectToAction("Index", "LoggedIn");
        }
    }
}