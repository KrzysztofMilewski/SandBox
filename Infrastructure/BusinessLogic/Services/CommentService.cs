using AutoMapper;
using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Dtos;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.BusinessLogic.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private ISubscriptionService _subscriptionService;

        public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, ISubscriptionService subscriptionService)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _subscriptionService = subscriptionService;
        }

        public ResultDto<CommentDto> AddCommentToPost(CommentDto commentDto)
        {
            if (string.IsNullOrWhiteSpace(commentDto.Contents))
                return new ResultDto<CommentDto>() { RequestStatus = RequestStatus.Error, Message = "Comment cannot be empty" };

            var post = _postRepository.GetPostById(commentDto.PostId);

            if (post == null)
                return new ResultDto<CommentDto>() { Message = "Couldn't find post", RequestStatus = RequestStatus.NotFound };

            var isSubscribed = _subscriptionService.IsUserSubscribedTo(post.PublisherId, commentDto.CommentingUserId).Data;

            if (isSubscribed || post.PublisherId == commentDto.CommentingUserId)
            {
                var comment = new Comment()
                {
                    CommentingUserId = commentDto.CommentingUserId,
                    Contents = commentDto.Contents,
                    PostId = commentDto.PostId,
                    DateAdded = DateTime.Now
                };
                var addedComment = _commentRepository.AddCommentToPost(comment);
                return new ResultDto<CommentDto>() { Message = "Comment added", RequestStatus = RequestStatus.Success, Data = Mapper.Map<CommentDto>(comment) };
            }
            else
                return new ResultDto<CommentDto>() { Message = "You haven't subscribed to this user", RequestStatus = RequestStatus.NotAuthorized };
        }

        public ResultDto DeleteComment(int commentId, string currentUserId)
        {
            var comment = _commentRepository.GetCommentById(commentId);

            if (comment == null)
                return new ResultDto() { RequestStatus = RequestStatus.NotFound, Message = "Comment not found" };

            if (comment.CommentingUserId != currentUserId)
                return new ResultDto() { RequestStatus = RequestStatus.NotAuthorized, Message = "You cannot delete another user's comment!" };

            _commentRepository.DeleteComment(comment);
            return new ResultDto() { RequestStatus = RequestStatus.Success, Message = "Comment deleted" };
        }

        public ResultDto EditComment(CommentDto commentDto, string currentUserId)
        {
            var comment = _commentRepository.GetCommentById(commentDto.Id);

            if (comment == null)
                return new ResultDto() { RequestStatus = RequestStatus.NotFound, Message = "Comment not found" };

            if (comment.CommentingUserId != currentUserId)
                return new ResultDto() { RequestStatus = RequestStatus.NotAuthorized, Message = "You cannot edit another user's comment!" };

            comment.Contents = commentDto.Contents;

            _commentRepository.EditComment(comment);
            return new ResultDto() { RequestStatus = RequestStatus.Success, Message = "Comment edited" };
        }

        public ResultDto<IEnumerable<CommentDto>> GetCommentsForPost(int postId, string currentUserId)
        {
            var post = _postRepository.GetPostById(postId);

            if (post == null)
                return new ResultDto<IEnumerable<CommentDto>>() { RequestStatus = RequestStatus.NotFound, Message = "Couldn't find post" };

            if (post.PublisherId == currentUserId)
            {
                var commentsForOwnPost = _commentRepository.GetCommentsForPost(postId).OrderBy(c => c.DateAdded);
                return new ResultDto<IEnumerable<CommentDto>>() { Data = Mapper.Map<IEnumerable<CommentDto>>(commentsForOwnPost), RequestStatus = RequestStatus.Success, Message = "Comments have been loaded" };
            }

            var isSubscribed = _subscriptionService.IsUserSubscribedTo(post.PublisherId, currentUserId).Data;

            if (!isSubscribed)
                return new ResultDto<IEnumerable<CommentDto>>() { Message = "You haven't subscribed to this user", RequestStatus = RequestStatus.Error };

            var comments = _commentRepository.GetCommentsForPost(postId).OrderBy(c => c.DateAdded);
            return new ResultDto<IEnumerable<CommentDto>>() { Message = "Comments have been loaded", RequestStatus = RequestStatus.Success, Data = Mapper.Map<IEnumerable<CommentDto>>(comments) };
        }
    }
}
