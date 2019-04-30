using Infrastructure.Models;
using System.Linq;

namespace Infrastructure.DataAccess.Interfaces
{
    public interface IPostRepository
    {
        Post GetPostById(int id);
        IQueryable<Post> GetPostsFromUser(string userId);
        void AddPost(Post postToAdd);
        void EditPost(Post postToEdit);
        void DeletePost(int postId);
    }
}
