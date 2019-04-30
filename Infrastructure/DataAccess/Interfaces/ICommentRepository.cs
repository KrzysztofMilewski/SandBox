using Infrastructure.Models;
using System.Linq;

namespace Infrastructure.DataAccess.Interfaces
{
    interface ICommentRepository
    {
        Comment GetCommentById(int id);
        IQueryable<Comment> GetCommentsForPost(int postId);
        void AddCommentToPost(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(int commentId);
    }
}
