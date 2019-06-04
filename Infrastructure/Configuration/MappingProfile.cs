using AutoMapper;
using Infrastructure.Dtos;
using Infrastructure.Models;

namespace Infrastructure.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Post, PostDto>();
            CreateMap<Subscription, SubscriptionDto>();
        }

        public static void Initialize()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }
    }
}