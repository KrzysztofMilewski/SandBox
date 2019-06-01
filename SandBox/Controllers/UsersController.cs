using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace SandBox.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult FindUsers()
        {
            ViewBag.Id = User.Identity.GetUserId();
            return View();
        }
    }
}