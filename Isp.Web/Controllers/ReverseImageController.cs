using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Isp.Core.Configs;
using Isp.Core.Entities;
using Isp.Core.ReverseImageFetchers;
using Isp.Web.Serializers;

namespace Isp.Web.Controllers
{
    public class ReverseImageController : Controller
    {
        private readonly BingReverseImageFetch _bingReverseImageFetch;

        public ReverseImageController(
            BingReverseImageFetch bingReverseImageFetch
        )
        {
            _bingReverseImageFetch = bingReverseImageFetch;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                throw new Exception(nameof(file));
            }

            var fileSize = file.ContentLength;
            if (fileSize == 0 || fileSize > 1024 * 1024)
            {
                throw new Exception("File size validation failure");
            }

            try
            {
                var uploadPath = Server.MapPath(AppSetting.UploadFolder);
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(uploadPath, fileName));

                return new JsonNetResult(new ImageUploadResult
                {
                    Name = fileName,
                    Size = fileSize
                });
            }
            catch (Exception ex)
            {
                throw new Exception("File not uploaded", ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetReverseImages(ImageFetchHandler handler, string query, string fileName,
            long? skip, long? take)
        {
            var uploadPath = Server.MapPath(AppSetting.UploadFolder);
            var model = new ReverseImageFetchQuery
            {
                Query = query,
                FileName = fileName,
                Skip = skip,
                Take = take
            };

            ReverseImageFetchResult result;
            switch (handler)
            {
                case ImageFetchHandler.Bing:
                    result = await _bingReverseImageFetch.Execute(model, uploadPath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new JsonNetResult(result);
        }
    }
}