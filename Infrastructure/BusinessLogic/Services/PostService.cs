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
        private readonly ISubscriptionService _subscriptionService;

        public PostService(IPostRepository postRepository, ISubscriptionRepository subscriptionRepository, ISubscriptionService subscriptionService)
        {
            _postRepository = postRepository;
            _subscriptionService = subscriptionService;
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
                NumberOfEdits = 0,
                PubliclyVisible = postDto.PubliclyVisible
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
            post.PubliclyVisible = postDto.PubliclyVisible;

            _postRepository.EditPost(post);
            return new ResultDto() { Message = "Post edited successfully", RequestStatus = RequestStatus.Success };
        }

        public ResultDto DeletePost(int postId, string currentUserId)
        {
            var post = _postRepository.GetPostById(postId);

            if (post == null)
                return new ResultDto() { Message = "Cannot find specified post", RequestStatus = RequestStatus.NotFound };

            if (post.PublisherId != currentUserId)
                return new ResultDto() { Message = "You don't have permission to delete this post", RequestStatus = RequestStatus.NotAuthorized };

            _postRepository.DeletePost(post);

            return new ResultDto() { Message = "Post deleted successfully", RequestStatus = RequestStatus.Success };
        }

        public ResultDto<IEnumerable<PostDto>> GetUsersPosts(string userId, string requestingUserId)
        {
            if (userId == requestingUserId)
            {
                var posts = _postRepository.GetPostsFromUser(userId).OrderByDescending(p => p.DatePublished);
                return new ResultDto<IEnumerable<PostDto>>() { RequestStatus = RequestStatus.Success, Data = Mapper.Map<IEnumerable<PostDto>>(posts), Message = "Posts successfully retrieved" };
            }
            else
            {
                var isSubscribed = _subscriptionService.IsUserSubscribedTo(userId, requestingUserId);
                if (!isSubscribed.Data)
                {
                    var publicPosts = _postRepository.GetPostsFromUser(userId).Where(p => p.PubliclyVisible);
                    return new ResultDto<IEnumerable<PostDto>>() { Message = "Showing only public posts by this user", RequestStatus = RequestStatus.Success, Data = Mapper.Map<IEnumerable<PostDto>>(publicPosts) };
                }

                var posts = _postRepository.GetPostsFromUser(userId).OrderByDescending(p => p.DatePublished);
                return new ResultDto<IEnumerable<PostDto>>() { RequestStatus = RequestStatus.Success, Data = Mapper.Map<IEnumerable<PostDto>>(posts), Message = "Posts successfully retrieved" };
            }
        }

        public ResultDto<IEnumerable<PostDto>> GetPostFromSubscriptions(string subscriberId)
        {
            var subscriptions = _subscriptionService.GetUserSubscriptions(subscriberId).Data;
            var posts = new List<Post>();

            foreach (var sub in subscriptions)
                posts.AddRange(_postRepository.GetPostsFromUser(sub.PublisherId));

            return new ResultDto<IEnumerable<PostDto>>() { Data = Mapper.Map<IEnumerable<PostDto>>(posts.OrderByDescending(p => p.DatePublished)), RequestStatus = RequestStatus.Success, Message = "Posts successfully retrieved" };
        }

        public ResultDto<PostDto> GetSinglePost(int postId, string requestingUserId)
        {
            var post = _postRepository.GetPostById(postId);

            if (post == null)
                return new ResultDto<PostDto>() { Message = "Couldn't find post", RequestStatus = RequestStatus.NotFound };

            if (post.PublisherId == requestingUserId)
                return new ResultDto<PostDto>() { Message = "Post retrieved successfully", RequestStatus = RequestStatus.Success, Data = Mapper.Map<PostDto>(post) };

            var isSubscribedTo = _subscriptionService.IsUserSubscribedTo(post.PublisherId, requestingUserId).Data;

            if (!isSubscribedTo)
                return new ResultDto<PostDto>() { Message = "You don't have permission to view this post", RequestStatus = RequestStatus.NotAuthorized };

            return new ResultDto<PostDto>() { Message = "Post retrieved successfully", RequestStatus = RequestStatus.Success, Data = Mapper.Map<PostDto>(post) };
        }

        public ResultDto<PostDto> GetPostForEdition(int postId, string userId)
        {
            var post = _postRepository.GetPostById(postId);

            if (post == null)
                return new ResultDto<PostDto>() { Message = "Couldn't find post", RequestStatus = RequestStatus.NotFound };

            if (post.PublisherId != userId)
                return new ResultDto<PostDto>() { Message = "You don't have permission to edit this post", RequestStatus = RequestStatus.NotAuthorized };

            return new ResultDto<PostDto>() { RequestStatus = RequestStatus.Success, Message = "Post retrieved successfully", Data = Mapper.Map<PostDto>(post) };
        }

        public ResultDto TogglePostVisibility(int postId, string requestingUserId)
        {
            var post = _postRepository.GetPostById(postId);

            if (post.PublisherId != requestingUserId)
                return new ResultDto() { Message = "You cannot edit this post", RequestStatus = RequestStatus.Error };

            post.PubliclyVisible = !post.PubliclyVisible;

            _postRepository.EditPost(post);

            return new ResultDto() { RequestStatus = RequestStatus.Success, Message = ToggleVisibilitySuccessMessage(post.PubliclyVisible) };
        }

        private string ToggleVisibilitySuccessMessage(bool publiclyVisible)
        {
            if (publiclyVisible)
                return "Post is now visible for everyone";
            else
                return "Post is now visible only to your subscribers";
        }
    }
}
