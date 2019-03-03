using Infrastructure.DataAccess;
using Infrastructure.Persistence;

namespace Infrastructure.BusinessLogic.Services
{
    public class PostService
    {
        private readonly IEntityRepository<Post> _postsRepository;
        public PostService(IEntityRepository<Post> postRepository)
        {
            _postsRepository = postRepository;
        }

        public void 
    }
}
