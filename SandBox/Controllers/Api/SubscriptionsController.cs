using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.Dtos;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace SandBox.Controllers.Api
{
    [Authorize]
    public class SubscriptionsController : ApiController
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public IHttpActionResult GetSubscriptions(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _subscriptionService.GetUserSubscriptionsAsUsers(currentUserId, id);

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok(result.Data);
        }

        //temporary
        [HttpGet]
        public IHttpActionResult GetMySubscribers()
        {
            return Ok("Temporarily disabled ver. 2");
        }

        [HttpPost]
        public IHttpActionResult CreateSubscription(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _subscriptionService.CreateSubscrition(id, currentUserId);

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok(result.Message);
        }

        [HttpDelete]
        public IHttpActionResult DeleteSubscription(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _subscriptionService.DeleteSubscription(id, currentUserId);

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok(result.Message);
        }
    }
}
