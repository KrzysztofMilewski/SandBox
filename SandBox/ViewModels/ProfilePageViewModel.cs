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
                    return "btn-light";
                else
                    return "btn-primary";
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