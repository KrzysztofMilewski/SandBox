using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace SandBox.Controllers
{
    [Authorize]
    public class LoggedInController : Controller
    {      
        [Route("Home")]
        public ActionResult Index()
        {
            var currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());

            ViewBag.UserNickname = currentUser.Nickname;
            ViewBag.Id = currentUser.Id;
            return View();
        }
    }
}