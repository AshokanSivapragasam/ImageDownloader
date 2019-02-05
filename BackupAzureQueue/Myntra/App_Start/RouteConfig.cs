﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Myntra
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Mailbox",
                url: "owa/Mailbox/{action}/{id}",
                defaults: new { controller = "Mailbox", action = "Inbox", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Authentication",
                url: "owa/{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "SignIn", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}