using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoManagerThreeLayer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "DirectLinkAlbumAccess",
                url: "{titleSlug}",
                defaults: new { controller = "Album", action = "DirectLinkAlbumAccess" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
