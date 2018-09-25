using Microsoft.AspNet.Identity;
using SandBox.Models;
using System.Linq;
using System.Web.Http;

namespace SandBox.Controllers.Api
{
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

        [Authorize]
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
    }
}
