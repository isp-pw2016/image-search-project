using System.Threading.Tasks;
using System.Web.Mvc;
using Isp.Core.Entities;
using Isp.Core.ImageFetchers;
using Isp.Web.Serializers;

namespace Isp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GoogleImageFetch _googleImageFetch;
        private readonly BingImageFetch _bingImageFetch;

        public HomeController(
            GoogleImageFetch googleImageFetch,
            BingImageFetch bingImageFetch
        )
        {
            _googleImageFetch = googleImageFetch;
            _bingImageFetch = bingImageFetch;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetGoogleImages(ImageFetchQuery model)
        {
            var result = await _googleImageFetch.Execute(model);

            return new JsonNetResult(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetBingImages(ImageFetchQuery model)
        {
            var result = await _bingImageFetch.Execute(model);

            return new JsonNetResult(result);
        }
    }
}