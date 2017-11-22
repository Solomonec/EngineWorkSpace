﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace MetroSupport
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}