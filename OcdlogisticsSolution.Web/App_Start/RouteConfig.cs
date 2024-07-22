using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OcdlogisticsSolution
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new string[] { "OcdlogisticsSolution.Web.Controllers" }
            );

            routes.MapRoute(
            name: "AdminRoute",
            url: "Admin/{controller}/{action}/{id}",
            defaults: new { controller = "Panel", action = "Index", id = UrlParameter.Optional }, new string[] { "OcdlogisticsSolution.Web.Areas.AdminDashboard" }
            );
        }
    }
}

