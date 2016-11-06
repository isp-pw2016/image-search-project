using System;
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
        private readonly FlickrImageFetch _flickrImageFetch;
        private readonly ShutterstockImageFetch _shutterstockImageFetch;

        public HomeController(
            GoogleImageFetch googleImageFetch,
            BingImageFetch bingImageFetch,
            InstagramImageFetch instagramImageFetch,
            FlickrImageFetch flickrImageFetch,
            ShutterstockImageFetch shutterstockImageFetch
        )
        {
            _googleImageFetch = googleImageFetch;
            _bingImageFetch = bingImageFetch;
            _instagramImageFetch = instagramImageFetch;
            _flickrImageFetch = flickrImageFetch;
            _shutterstockImageFetch = shutterstockImageFetch;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetImages(ImageFetchHandler handler, string query, long? skip, long? take)
        {
            var model = new ImageFetchQuery
            {
                Query = query,
                Skip = skip,
                Take = take
            };

            BenchmarkResult result;
            switch (handler)
            {
                case ImageFetchHandler.Google:
                    result = await _googleImageFetch.Execute(model);
                    break;
                case ImageFetchHandler.Bing:
                    result = await _bingImageFetch.Execute(model);
                    break;
                case ImageFetchHandler.Instagram:
                    result = await _instagramImageFetch.Execute(model);
                    break;
                case ImageFetchHandler.Flickr:
                    result = await _flickrImageFetch.Execute(model);
                    break;
                case ImageFetchHandler.Shutterstock:
                    result = await _shutterstockImageFetch.Execute(model);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new JsonNetResult(result);
        }
    }
}