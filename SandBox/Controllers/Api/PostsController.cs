using Infrastructure.BusinessLogic.Interfaces;
using Microsoft.AspNet.Identity;
using SandBox.Infrastructure;
using System.Web.Http;

namespace SandBox.Controllers.Api
{
    [Authorize]
    public class PostsController : ApiController
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpDelete]
        public IHttpActionResult DeletePost(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _postService.DeletePost(id, currentUserId);

            return this.ReturnHttpResponse(result);
        }

        [HttpGet]
        public IHttpActionResult GetPostsFromSubscriptions()
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _postService.GetPostFromSubscriptions(currentUserId);

            return this.ReturnHttpResponse(result);
        }

        [HttpGet]
        public IHttpActionResult GetPostsFromUser(string id)
        {
            var result = _postService.GetUsersPosts(id, User.Identity.GetUserId());

            return this.ReturnHttpResponse(result);
        }

        [HttpPut]
        public IHttpActionResult ToggleVisibility(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _postService.TogglePostVisibility(id, currentUserId);

            return this.ReturnHttpResponse(result);
        }
    }
}
