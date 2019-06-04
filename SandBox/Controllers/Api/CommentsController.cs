using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.Dtos;
using Microsoft.AspNet.Identity;
using System;
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

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok(result.Data);
        }

        [HttpPost]
        public IHttpActionResult AddComentToPost(CommentDto dto)
        {
            var currentUserId = User.Identity.GetUserId();
            dto.CommentingUserId = currentUserId;

            var result = _commentService.AddCommentToPost(dto);

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Created(new Uri(Request.RequestUri + "/" + result.Data.Id.ToString()), result.Data);
        }

        [HttpDelete]
        public IHttpActionResult DeleteComment(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _commentService.DeleteComment(id, currentUserId);

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok();
        }

        [HttpPut]
        public IHttpActionResult EditComment(CommentDto dto)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _commentService.EditComment(dto, currentUserId);

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok();
        }
    }
}