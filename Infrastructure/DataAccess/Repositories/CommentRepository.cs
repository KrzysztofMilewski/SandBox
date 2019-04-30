using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Models;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Comment> _comments;

        public CommentRepository(DbContext context)
        {
            _context = context;
            _comments = _context.Set<Comment>();
        }

        public Comment GetCommentById(int id)
        {
            var comment = _comments.SingleOrDefault(c => c.Id == id);
            return comment;
        }

        public IQueryable<Comment> GetCommentsForPost(int postId)
        {
            var comments = _comments.Where(c => c.PostId == postId);
            return comments;
        }

        public void AddCommentToPost(Comment comment)
        {
            _comments.Add(comment);
            _context.SaveChanges();
        }

        public void EditComment(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteComment(int commentId)
        {
            var commentToDelete = _comments.SingleOrDefault(c => c.Id == commentId);
            _comments.Remove(commentToDelete);
            _context.SaveChanges();
        }
    }
}
