using Infrastructure.Dtos;

namespace Infrastructure.BusinessLogic.Interfaces
{
    public interface ISubscriptionService
    {
        ResultDto<bool> IsUserSubscribedTo(string publisherId, string subscriberId);
    }
}
