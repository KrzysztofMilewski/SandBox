using AutoMapper;
using Infrastructure.Dtos;
using Infrastructure.Persistence;

namespace Infrastructure.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Post, PostDto>();
        }

        public static void Initialize()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }
    }
}