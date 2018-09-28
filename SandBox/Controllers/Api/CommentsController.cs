using AutoMapper;
using Microsoft.AspNet.Identity;
using SandBox.Dtos;
using SandBox.Models;
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

            return Ok();
        }
    }
}
