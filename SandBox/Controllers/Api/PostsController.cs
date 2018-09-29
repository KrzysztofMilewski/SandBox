using AutoMapper;
using Microsoft.AspNet.Identity;
using SandBox.Dtos;
using SandBox.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace SandBox.Controllers.Api
{
    [Authorize]
    public class PostsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public PostsController()
        {
            _context = ApplicationDbContext.Create();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }

        [HttpDelete]
        public IHttpActionResult DeletePost(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var postToDelete = _context.Posts.FirstOrDefault(p => p.Id == id && p.PublisherId == currentUserId);

            if (postToDelete == null)
                return NotFound();

            _context.Posts.Remove(postToDelete);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetPostsFromSubscriptions()
        {
            var currentUserId = User.Identity.GetUserId();
            var subscriptions = _context.Users.Include(u => u.Subscriptionss).SingleOrDefault(u => u.Id == currentUserId).Subscriptionss;

            var posts = new List<Post>();

            subscriptions.ForEach(s => posts.AddRange(_context.Posts.Where(p => p.PublisherId == s.PublisherId).Include(p => p.Publisher).AsEnumerable()));

            return Ok(Mapper.Map<IEnumerable<Post>, IEnumerable<PostDto>>(posts.OrderByDescending(p => p.DatePublished)));
        }

        [HttpGet]
        public IHttpActionResult GetPostsFromUser(string id)
        {
            var usersPosts = _context.Posts.Where(p => p.PublisherId == id);

            return Ok(Mapper.Map<IEnumerable<Post>, IEnumerable<PostDto>>(usersPosts.OrderByDescending(p => p.DatePublished)));
        }
    }
}
