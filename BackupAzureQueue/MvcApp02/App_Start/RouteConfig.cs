using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApp02
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Review",
                "Reviews/{action}/{restaurantId}/{id}",
                new { controller = "Review", action = "List", restaurantId = UrlParameter.Optional, id = UrlParameter.Optional });

            routes.MapRoute(
                "Restaurant",
                "Restaurants/{action}",
                new { controller = "Restaurant", action = "Index" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}