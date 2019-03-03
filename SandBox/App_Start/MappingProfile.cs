using AutoMapper;
using Infrastructure.Dtos;
using Infrastructure.Persistence;
 
namespace SandBox.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Post, PostDto>();
        }
    }
}