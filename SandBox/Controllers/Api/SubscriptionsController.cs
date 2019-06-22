using Infrastructure.BusinessLogic.Interfaces;
using Microsoft.AspNet.Identity;
using SandBox.Infrastructure;
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
        public IHttpActionResult GetNumberOfFollowers(string id)
        {
            var result = _subscriptionService.GetNumberOfFollowers(id);
            return this.ReturnHttpResponse(result);
        }

        [HttpPost]
        public IHttpActionResult CreateSubscription(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _subscriptionService.CreateSubscrition(id, currentUserId);

            return this.ReturnHttpResponse(result);
        }

        [HttpDelete]
        public IHttpActionResult DeleteSubscription(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _subscriptionService.DeleteSubscription(id, currentUserId);

            return this.ReturnHttpResponse(result);
        }
    }
}
