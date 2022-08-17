using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Article.WebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx");//this for image handler
            routes.IgnoreRoute("RestaurantsImages/ArticleImg/{resource}.ashx");
            routes.IgnoreRoute("RestaurantsImages/Notify/{resource}.ashx");
            routes.IgnoreRoute("RestaurantsImages/Products/{resource}.ashx");
            routes.IgnoreRoute("RestaurantsImages/Restaurants/{resource}.ashx");
            routes.IgnoreRoute("ArticleImg/{resource}.ashx");
            routes.IgnoreRoute("PaidAds/PaidAds_img/{resource}.ashx");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
