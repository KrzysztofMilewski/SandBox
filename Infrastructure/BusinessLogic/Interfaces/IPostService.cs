using Infrastructure.Dtos;
using System.Collections.Generic;

namespace Infrastructure.BusinessLogic.Interfaces
{
    public interface IPostService
    {
        ResultDto CreatePost(PostDto postDto);
        ResultDto EditPost(PostDto postDto);
        ResultDto DeletePost(int postId, string currentUserId);
        ResultDto<IEnumerable<PostDto>> GetUsersPosts(string userId);
        ResultDto<IEnumerable<PostDto>> GetPostFromSubscriptions(string subscriberId);
        ResultDto<PostDto> GetSinglePost(int postId, string requestingUserId);
    }
}
