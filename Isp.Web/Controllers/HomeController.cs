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
        private readonly InstagramImageFetch _instagramImageFetch;
        private readonly ShutterstockImageFetch _shutterstockImageFetch;

        public HomeController(
            GoogleImageFetch googleImageFetch,
            BingImageFetch bingImageFetch,
            InstagramImageFetch instagramImageFetch,
            ShutterstockImageFetch shutterstockImageFetch
        )
        {
            _googleImageFetch = googleImageFetch;
            _bingImageFetch = bingImageFetch;
            _instagramImageFetch = instagramImageFetch;
            _shutterstockImageFetch = shutterstockImageFetch;
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

        [HttpGet]
        public async Task<ActionResult> GetInstagramImages(ImageFetchQuery model)
        {
            var result = await _instagramImageFetch.Execute(model);

            return new JsonNetResult(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetShutterstockImages(ImageFetchQuery model)
        {
            var result = await _shutterstockImageFetch.Execute(model);

            return new JsonNetResult(result);
        }
    }
}