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
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public PostService(IPostRepository postRepository, ISubscriptionRepository subscriptionRepository)
        {
            _postRepository = postRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public ResultDto CreatePost(PostDto postDto)
        {
            //add some time related logic (not allowing a post within 10 min...)
            if (string.IsNullOrEmpty(postDto.Contents) || string.IsNullOrEmpty(postDto.Title))
                return new ResultDto() { Message = "Title and contents cannot be empty", RequestStatus = RequestStatus.Error };

            var post = new Post()
            {
                Contents = postDto.Contents,
                Title = postDto.Title,
                PublisherId = postDto.Publisher.Id,
                DatePublished = DateTime.Now,
                NumberOfEdits = 0
            };

            _postRepository.AddPost(post);
            return new ResultDto() { Message = "Post has been added", RequestStatus = RequestStatus.Success };
        }

        public ResultDto EditPost(PostDto postDto)
        {
            var post = _postRepository.GetPostById(postDto.Id);

            if (post == null)
                return new ResultDto() { Message = "Cannot find specified post", RequestStatus = RequestStatus.NotFound };

            if (post.PublisherId != postDto.Publisher.Id)
                return new ResultDto() { Message = "You don't have permission to edit this post", RequestStatus = RequestStatus.NotAuthorized };

            post.Contents = postDto.Contents;
            post.Title = postDto.Title;
            post.NumberOfEdits++;
            post.LastTimeEdited = DateTime.Now;

            _postRepository.EditPost(post);
            return new ResultDto() { Message = "Post edited successfully", RequestStatus = RequestStatus.Success };
        }

        public ResultDto DeletePost(int postId, string currentUserId)
        {
            var post = _postRepository.GetPostById(postId);

            if (post == null)
                return new ResultDto() { Message = "Cannot find specified post", RequestStatus = RequestStatus.NotFound };

            if (post.PublisherId != currentUserId)
                return new ResultDto() { Message = "You don't have permission to edit this post", RequestStatus = RequestStatus.NotAuthorized };

            _postRepository.DeletePost(post);

            return new ResultDto() { Message = "Post deleted successfully", RequestStatus = RequestStatus.Success };
        }

        public ResultDto<IEnumerable<PostDto>> GetUsersPosts(string userId)
        {
            var posts = _postRepository.GetPostsFromUser(userId).OrderByDescending(p => p.DatePublished);
            return new ResultDto<IEnumerable<PostDto>>() { RequestStatus = RequestStatus.Success, Data = Mapper.Map<IEnumerable<PostDto>>(posts), Message = "Posts successfully retrieved" };
        }

        public ResultDto<IEnumerable<PostDto>> GetPostFromSubscriptions(string subscriberId)
        {
            var subscriptions = _subscriptionRepository.GetUserSubscriptions(subscriberId);
            var posts = new List<Post>();

            foreach (var sub in subscriptions)
                posts.AddRange(_postRepository.GetPostsFromUser(sub.PublisherId));

            return new ResultDto<IEnumerable<PostDto>>() { Data = Mapper.Map<IEnumerable<PostDto>>(posts), RequestStatus = RequestStatus.Success, Message = "Posts successfully retrieved" };
        }
    }
}
