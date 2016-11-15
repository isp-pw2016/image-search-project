using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Isp.Core.ImageFetchers;
using Isp.Core.ReverseImageFetchers;
using Isp.Web.Serializers;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace Isp.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // MVC: Initialize
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // MVC: Use JSON.NET as the default deserializer
            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories
                .OfType<JsonValueProviderFactory>()
                .FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonNetValueProviderFactory());

            // IoC: Simple Injector
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            RegisterTypes(container);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();
            container.Verify();

            // IoC: Resolve dependencies
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void RegisterTypes(Container container)
        {
            // Register classes which inherit ImageFetchBase
            container.RegisterCollection<ImageFetchBase>(
                from type in typeof(ImageFetchBase).Assembly.GetTypes()
                where type.IsSubclassOf(typeof(ImageFetchBase))
                select type);

            // Register classes which inherit ReverseImageFetchBase
            container.RegisterCollection<ReverseImageFetchBase>(
                from type in typeof(ReverseImageFetchBase).Assembly.GetTypes()
                where type.IsSubclassOf(typeof(ReverseImageFetchBase))
                select type);
        }
    }
}