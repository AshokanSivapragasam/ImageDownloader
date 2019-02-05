using MvcApp01.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcApp01
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoggingFilterAttribute());
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle(virtualPath: "~/bundles/otf").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/MicrosoftAjax.js",
                "~/Scripts/MicrosoftMvcAjax.js",
                "~/Scripts/MicrosoftMvcValidation.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/otf.js"
                ));

            bundles.Add(new ScriptBundle(virtualPath: "~/bundles/modernizr").Include(
                "~/Scripts/modernizr-{version}.js"
                ));

            bundles.Add(new StyleBundle(virtualPath: "~/bundles/styles").Include(
                "~/Content/Site.css",
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectable.css",
                "~/Content/themes/base/jquery.ui.accordion.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css"));
        }

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

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterBundles(BundleTable.Bundles);

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}