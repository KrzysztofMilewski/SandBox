using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.Dtos;
using Microsoft.AspNet.Identity;
using SandBox.Infrastructure;
using System.Web.Http;

namespace SandBox.Controllers.Api
{
    [Authorize]
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IHttpActionResult GetCommentsForPost(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _commentService.GetCommentsForPost(id, currentUserId);

            return this.ReturnHttpResponse(result);
        }

        [HttpPost]
        public IHttpActionResult AddComentToPost(CommentDto dto)
        {
            var currentUserId = User.Identity.GetUserId();
            dto.CommentingUserId = currentUserId;

            var result = _commentService.AddCommentToPost(dto);
            return this.ReturnHttpResponse(result);
        }

        [HttpDelete]
        public IHttpActionResult DeleteComment(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _commentService.DeleteComment(id, currentUserId);

            return this.ReturnHttpResponse(result);
        }

        [HttpPut]
        public IHttpActionResult EditComment(CommentDto dto)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _commentService.EditComment(dto, currentUserId);

            return this.ReturnHttpResponse(result);
        }
    }
}