using Infrastructure.Dtos;

namespace SandBox.ViewModels
{
    public class ProfilePageViewModel
    {
        public ApplicationUserDto UserDto { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsCurrentSubscribed { get; set; }

        public string ButtonClass
        {
            get
            {
                if (IsCurrentSubscribed)
                    return "btn-secondary";
                else
                    return "btn-success";
            }
        }

        public string ButtonContent
        {
            get
            {
                if (IsCurrentSubscribed)
                    return "Unsubscribe";
                else
                    return "Subscribe";

            }
        }
    }
}