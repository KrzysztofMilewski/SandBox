using Infrastructure.Dtos;
using System.Collections.Generic;

namespace Infrastructure.BusinessLogic.Interfaces
{
    public interface ISubscriptionService
    {
        ResultDto<bool> IsUserSubscribedTo(string publisherId, string subscriberId);
        ResultDto CreateSubscrition(string publisherId, string subscriberId);
        ResultDto DeleteSubscription(string publisherId, string subscriberId);
        ResultDto<IEnumerable<SubscriptionDto>> GetUserSubscriptions(string userId);
        ResultDto<IEnumerable<ApplicationUserDto>> GetUserSubscriptionsAsUsers(string askingUserId, string subscriberId);
        ResultDto<IEnumerable<ApplicationUserDto>> GetFollowersAsUsers(string userId);
        ResultDto<int> GetNumberOfFollowers(string userId);
    }
}
