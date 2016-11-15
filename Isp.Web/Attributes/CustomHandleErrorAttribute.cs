using System.Web;
using System.Web.Mvc;
using Isp.Core.Exceptions;
using Isp.Web.Serializers;

namespace Isp.Web.Attributes
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// Custom HandleErrorAttribute to return exceptions in controllers.
        /// 
        /// If the request in an Ajax request, respond with a JSON message.
        /// Otherwise respond with a ViewResult.
        /// </summary>
        /// <param name="filterContext">Context of the exception</param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var title = string.Empty;
                var message = filterContext.Exception?.Message;

                if (filterContext.Exception is ImageFetchException)
                {
                    title = ((ImageFetchException) filterContext.Exception).Title;
                }

                filterContext.Result = new JsonNetResult(new
                {
                    Error = true,
                    Title = !string.IsNullOrWhiteSpace(title)
                        ? title
                        : "Error",
                    Message = !string.IsNullOrWhiteSpace(message)
                        ? message
                        : "An error has occurred"
                });
            }
            else
            {
                var controllerName = (string) filterContext.RouteData.Values["controller"];
                var actionName = (string) filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}