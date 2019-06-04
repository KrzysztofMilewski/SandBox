using Infrastructure.Models;
using System.Linq;

namespace Infrastructure.DataAccess.Interfaces
{
    public interface ICommentRepository
    {
        Comment GetCommentById(int id);
        IQueryable<Comment> GetCommentsForPost(int postId);
        Comment AddCommentToPost(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(Comment commentToDelete);
    }
}
