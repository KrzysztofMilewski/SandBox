using Infrastructure.BusinessLogic.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SandBox.Infrastructure;
using System.Web;
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

            return this.ReturnHttpResponse(result);
        }

        //temporary
        [HttpGet]
        public IHttpActionResult GetMySubscribers()
        {
            return Ok("Temporarily disabled");
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

        public IHttpActionResult GetUsers()
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();


            return null;
        }
    }
}
