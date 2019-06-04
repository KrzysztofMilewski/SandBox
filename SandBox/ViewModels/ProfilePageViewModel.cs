using Infrastructure.Dtos;

namespace SandBox.ViewModels
{
    public class ProfilePageViewModel
    {
        public ApplicationUserDto UserDto { get; set; }
        public string CurrentUserId { get; set; }
    }
}