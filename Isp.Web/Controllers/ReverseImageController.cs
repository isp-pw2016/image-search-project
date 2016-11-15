using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Isp.Core.Entities;
using Isp.Web.Serializers;

namespace Isp.Web.Controllers
{
    public class ReverseImageController : Controller
    {
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
            if (fileSize == 0 || fileSize > 1024 * 1024 * 10)
            {
                throw new Exception("File size validation failure");
            }

            try
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/Upload"), fileName));

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
    }
}