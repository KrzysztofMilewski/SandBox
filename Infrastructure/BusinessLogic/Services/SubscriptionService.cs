using AutoMapper;
using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Dtos;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.BusinessLogic.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public ResultDto<bool> IsUserSubscribedTo(string publisherId, string subscriberId)
        {
            var subscription = _subscriptionRepository.GetUserSubscriptions(subscriberId).SingleOrDefault(s => s.PublisherId == publisherId);

            if (subscription == null)
                return new ResultDto<bool>() { Message = "You don't subscribe to this user", RequestStatus = RequestStatus.Success, Data = false };
            else
                return new ResultDto<bool>() { Message = "You subscribe to this user", RequestStatus = RequestStatus.Success, Data = true };
        }

        public ResultDto CreateSubscrition(string publisherId, string subscriberId)
        {
            var subscription = _subscriptionRepository.GetUserSubscriptions(subscriberId).SingleOrDefault(s => s.PublisherId == publisherId);

            if (subscription != null)
                return new ResultDto() { Message = "You already subscribed to this user", RequestStatus = RequestStatus.Error };

            var sub = new Subscription()
            {
                PublisherId = publisherId,
                SubscriberId = subscriberId
            };
            _subscriptionRepository.AddSubscription(sub);

            return new ResultDto() { Message = "Subscription added", RequestStatus = RequestStatus.Success };
        }

        public ResultDto DeleteSubscription(string publisherId, string subscriberId)
        {
            var subscriptionToDelete = _subscriptionRepository.GetUserSubscriptions(subscriberId).SingleOrDefault(s => s.PublisherId == publisherId);

            if (subscriptionToDelete == null)
                return new ResultDto() { Message = "You haven't subscribed to this user", RequestStatus = RequestStatus.NotFound };

            _subscriptionRepository.DeleteSubscription(subscriptionToDelete);
            return new ResultDto() { Message = "Subscription deleted", RequestStatus = RequestStatus.Success };
        }

        public ResultDto<IEnumerable<SubscriptionDto>> GetUserSubscriptions(string userId)
        {
            var subscriptions = _subscriptionRepository.GetUserSubscriptions(userId);
            return new ResultDto<IEnumerable<SubscriptionDto>>() { Message = "Your subscriptions", RequestStatus = RequestStatus.Success, Data = Mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions) };
        }

        public ResultDto<IEnumerable<ApplicationUserDto>> GetUserSubscriptionsAsUsers(string requestingUserId, string subscriberId)
        {
            if (requestingUserId == subscriberId)
            {
                var subscriptions = _subscriptionRepository.GetUserSubscriptionsAsUsers(subscriberId);
                return new ResultDto<IEnumerable<ApplicationUserDto>>()
                {
                    RequestStatus = RequestStatus.Success,
                    Data = Mapper.Map<IEnumerable<ApplicationUserDto>>(subscriptions)
                };
            }
            //TEMPORARY (wait for implementation of publicly visible subs)
            else
            {
                if (true)
                {
                    var subscriptions = _subscriptionRepository.GetUserSubscriptionsAsUsers(subscriberId);
                    return new ResultDto<IEnumerable<ApplicationUserDto>>()
                    {
                        RequestStatus = RequestStatus.Success,
                        Data = Mapper.Map<IEnumerable<ApplicationUserDto>>(subscriptions)
                    };
                }
            }
        }

        public ResultDto<IEnumerable<ApplicationUserDto>> GetFollowersAsUsers(string userId)
        {
            var followers = _subscriptionRepository.GetUserFollowersAsUsers(userId);

            return new ResultDto<IEnumerable<ApplicationUserDto>>() { RequestStatus = RequestStatus.Success, Data = Mapper.Map<IEnumerable<ApplicationUserDto>>(followers) };
        }
    }
}
