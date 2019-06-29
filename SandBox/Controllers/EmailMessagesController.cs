using System.Web.Mvc;

namespace SandBox.Controllers
{
    [Authorize]
    public class EmailMessagesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewMessage()
        {
            return View();
        }

        public ActionResult Outbox()
        {
            return View();
        }
    }
}