using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.Configuration;
using Microsoft.AspNet.Identity;
using SandBox.Infrastructure;
using System.Web.Http;

namespace SandBox.Controllers.Api
{
    [Authorize]
    public class UsersController : ApiController
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ISubscriptionService _subscriptionService;

        public UsersController(ApplicationUserManager applicationUserManager, ISubscriptionService subscriptionService)
        {
            _applicationUserManager = applicationUserManager;
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public IHttpActionResult GetUsers(string nameQuery)
        {
            var result = _applicationUserManager.FindUsersByNicknames(nameQuery);
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult GetSubscriptions(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var result = _subscriptionService.GetUserSubscriptionsAsUsers(currentUserId, id);

            return this.ReturnHttpResponse(result);
        }

        [HttpGet]
        public IHttpActionResult GetMyFollowers()
        {
            var followers = _subscriptionService.GetFollowersAsUsers(User.Identity.GetUserId());

            return this.ReturnHttpResponse(followers);
        }
    }
}
