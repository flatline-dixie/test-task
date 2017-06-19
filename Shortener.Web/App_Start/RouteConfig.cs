using System.Web.Mvc;
using System.Web.Routing;

namespace Shortener.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Shorten",
                url: "{id}",
                defaults: new { controller = "Shortener", action = "Shorten" });

            routes.MapRoute(
                name: "Other",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Shortener", action = "Shorten", id = UrlParameter.Optional }
            );
        }
    }
}
