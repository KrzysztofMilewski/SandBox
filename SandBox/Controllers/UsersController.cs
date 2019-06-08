using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.Configuration;
using Microsoft.AspNet.Identity;
using SandBox.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SandBox.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationUserManager _manager;
        private readonly ISubscriptionService _subscriptionService;

        public UsersController(ApplicationUserManager manager, ISubscriptionService subscriptionService)
        {
            _manager = manager;
            _subscriptionService = subscriptionService;
        }

        public ActionResult FindUsers()
        {
            ViewBag.Id = User.Identity.GetUserId();
            return View();
        }

        public async Task<ActionResult> ProfilePage(string id)
        {
            var user = await _manager.GetUserDtoAsync(id);
            var currentUserId = User.Identity.GetUserId();

            return View(new ProfilePageViewModel()
            {
                CurrentUserId = currentUserId,
                UserDto = user,
                IsCurrentSubscribed = _subscriptionService.IsUserSubscribedTo(user.Id, currentUserId).Data
            });
        }
    }
}