using System.Web.Mvc;

namespace Isp.Web.Extensions
{
    public static class RazorExtensions
    {
        /// <summary>
        /// Url extension
        /// 
        /// Should generate an absolute URL to an action method by using
        /// the specified action name, controller name and route values
        /// </summary>
        /// <returns>The (absolute) URL</returns>
        public static string AbsoluteAction(this UrlHelper url, string actionName, string controllerName,
            object routeValues = null)
        {
            var scheme = url.RequestContext.HttpContext.Request.Url?.Scheme;

            return url.Action(actionName, controllerName, routeValues, scheme);
        }
    }
}