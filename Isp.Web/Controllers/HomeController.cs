using System;
using System.Web.Mvc;
using Isp.Core.ImageFetchers;
using Isp.Web.Serializers;

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
            return View();
        }

        [HttpGet]
        public ActionResult GetGoogleImages(string query)
        {
            var result = new
            {
                Amount = 12345,
                Message = query
            };

            return new JsonNetResult(result);
        }
    }
}