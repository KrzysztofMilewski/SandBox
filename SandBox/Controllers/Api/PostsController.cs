using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.Dtos;
using Microsoft.AspNet.Identity;
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

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetPostsFromSubscriptions()
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _postService.GetPostFromSubscriptions(currentUserId);

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok(result.Data);
        }



        //temporary
        [HttpGet]
        public IHttpActionResult GetPostsFromUser(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _postService.GetUsersPosts(currentUserId);

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok(result.Data);
        }
    }
}
