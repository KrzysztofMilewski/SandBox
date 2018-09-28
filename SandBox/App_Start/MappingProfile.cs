using AutoMapper;
using SandBox.Dtos;
using SandBox.Models;

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