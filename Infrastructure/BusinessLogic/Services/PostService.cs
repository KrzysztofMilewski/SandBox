using AutoMapper;
using Infrastructure.DataAccess;
using Infrastructure.Dtos;
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

        public void CreatePost(PostDto postDto)
        {
            var post = Mapper.Map<Post>(postDto);
            _postsRepository.Create(post);
        }

        public void EditPost(PostDto postDto)
        {
            var post = Mapper.Map<Post>(postDto);
            _postsRepository.Update(post);
        }
    }
}
