using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Glove.IOT.UI.Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //1.可以创建多条路由规则，每条路由规则的name属性不相同
            //2 路由规则是有顺序的，如果被前面的模版匹配了，那么后面的就没有机会了
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "UserLogin", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
