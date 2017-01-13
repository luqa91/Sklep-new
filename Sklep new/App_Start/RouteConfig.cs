using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sklep_new
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "KursySzczegoly",
             url: "Kurs-{id}.html",
           defaults: new { controller = "Kursy", action = "MoreInfo" }

);

            routes.MapRoute(
                name: "KursyList",
                url: "Kategoria/{nazwaKategorii}",
                defaults: new { controller = "Kursy", action = "Lista" }

                );

            routes.MapRoute(
                name: "StronyStatyczne",
                url: "strony/{nazwa}.html",
                defaults: new { Controller = "Home", Action = "StronyStatyczne" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
