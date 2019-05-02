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

        //temporary
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            return Ok("Temporary disabled");
        }

        [HttpPost]
        public IHttpActionResult CreateSubscription(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _subscriptionService.CreateSubscrition(id, currentUserId);

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteSubscription(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _subscriptionService.DeleteSubscription(id, currentUserId);

            if (result.RequestStatus != RequestStatus.Success)
                return BadRequest();
            else
                return Ok();
        }
    }
}
