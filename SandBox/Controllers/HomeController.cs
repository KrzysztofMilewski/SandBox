using SandBox.ViewModels;
using System.Web.Mvc;

namespace SandBox.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "LoggedIn");

            ViewBag.ReturnUrl = TempData["ReturnUrl"];

            return View(new StartPageViewModel()
            {
                LoginViewModel = new Models.LoginViewModel(),
                RegisterViewModel = new Models.RegisterViewModel()
            });
        }
    }
}