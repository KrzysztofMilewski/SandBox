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

        public UsersController(ApplicationUserManager manager)
        {
            _manager = manager;
        }

        public ActionResult FindUsers()
        {
            ViewBag.Id = User.Identity.GetUserId();
            return View();
        }

        public async Task<ActionResult> ProfilePage(string id)
        {
            var user = await _manager.GetUserDtoAsync(id);
            return View(new ProfilePageViewModel()
            {
                CurrentUserId = User.Identity.GetUserId(),
                UserDto = user
            });
        }
    }
}