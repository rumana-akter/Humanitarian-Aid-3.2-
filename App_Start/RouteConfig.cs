using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HumanitarianAid
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.Map




           /* routes.MapRoute(
             name: "DonorDetails",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "DonorDetails", action = "Index", id = UrlParameter.Optional }
         );*/





              
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



            routes.MapRoute(
            name: "DonorDetails",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "DonorDetails", action = "Activation", id = UrlParameter.Optional }
        );






            routes.MapRoute(
        name: "NeedyDetails",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "NeedyDetails", action = "Activation", id = UrlParameter.Optional }
    );


        }
    }
}
