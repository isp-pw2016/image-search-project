using System.Web.Mvc;
using Isp.Core.ImageFetchers;

namespace Isp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GoogleImageFetch _googleImageFetch;

        public HomeController(GoogleImageFetch googleImageFetch)
        {
            _googleImageFetch = googleImageFetch;
        }

        public ActionResult Index()
        {
            ViewBag.Xd = Url.Action("Index", "Home");
            return View();
        }
    }
}