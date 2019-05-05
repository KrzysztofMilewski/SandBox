using System.Web.Mvc;

namespace SandBox.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult FindUsers()
        {
            return View();
        }
    }
}