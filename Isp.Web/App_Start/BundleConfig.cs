using System.Web.Optimization;

namespace Isp.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/lodash").Include(
                "~/Scripts/lodash.js"));

            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                "~/Scripts/Chart.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                "~/Scripts/angular.js",
                "~/Scripts/toaster.js",
                "~/Scripts/angular-chart.js",
                "~/Scripts/ng-file-upload-all.js",
                "~/Angular/app.module.js",
                "~/Angular/Controllers/home.controller.js",
                "~/Angular/Controllers/reverse-image.controller.js",
                "~/Angular/Directives/image-fetch.directive.js",
                "~/Angular/Factories/common.factory.js",
                "~/Angular/Services/client.service.js",
                "~/Angular/Services/server.service.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/toaster.css"));
        }
    }
}