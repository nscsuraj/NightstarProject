using System.Web.Mvc;

namespace PartnerPortal.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index(int? pageId)
        {
            return View();
        }

    }
}
