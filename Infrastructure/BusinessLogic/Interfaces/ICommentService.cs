using Infrastructure.Dtos;
using Infrastructure.Models;
using System.Collections.Generic;

namespace Infrastructure.BusinessLogic.Interfaces
{
    public interface ICommentService
    {
        ResultDto<Comment> AddCommentToPost(CommentDto commentDto);
        ResultDto DeleteComment(int commentId, string currentUserId);
        ResultDto EditComment(CommentDto commentDto, string currentUserId);
        ResultDto<IEnumerable<CommentDto>> GetCommentsForPost(int postId, string currentUserId);
    }
}
