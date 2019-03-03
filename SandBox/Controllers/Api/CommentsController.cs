using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace SandBox.Controllers.Api
{
    [Authorize]
    public class CommentsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CommentsController()
        {
            _context = ApplicationDbContext.Create();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        public IHttpActionResult GetCommentsForPost(int id)
        {
            var comments = _context.Comments.Where(c => c.PostId == id).Include(c => c.CommentingUser).OrderBy(c => c.DateAdded).AsEnumerable();

            return Ok(Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments));
        }

        [HttpPost]
        public IHttpActionResult AddComentToPost(CommentDto dto)
        {
            var comment = new Comment()
            {
                CommentingUserId = User.Identity.GetUserId(),
                Contents = dto.Contents,
                DateAdded = DateTime.Now,
                PostId = dto.PostId
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + comment.Id), Mapper.Map<Comment, CommentDto>(comment));
        }

        [HttpDelete]
        public IHttpActionResult DeleteComment(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var commentToDelete = _context.Comments.Include(c => c.CommentingUser).Include(c => c.Post).SingleOrDefault(c => c.Id == id);

            if (commentToDelete == null)
                return NotFound();

            if (currentUserId != commentToDelete.CommentingUserId && commentToDelete.Post.PublisherId != currentUserId)
                return Unauthorized();

            _context.Comments.Remove(commentToDelete);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult EditComment(CommentDto dto)
        {
            var currentUserId = User.Identity.GetUserId();
            var comment = _context.Comments.SingleOrDefault(c => c.Id == dto.Id && c.CommentingUserId == currentUserId);

            if (comment == null)
                return BadRequest();

            comment.Edit(dto.Contents);
            _context.SaveChanges();

            return Ok();
        }
    }
}