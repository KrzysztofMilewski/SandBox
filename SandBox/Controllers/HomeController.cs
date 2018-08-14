using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SandBox.ViewModels;

namespace SandBox.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "LoggedIn");

            return View(new StartPageViewModel()
            {
                LoginViewModel = new Models.LoginViewModel(),
                RegisterViewModel= new Models.RegisterViewModel()
            });
        }
    }
}