using Glove.IOT.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;

namespace Glove.IOT.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //跨域配置
            var allowedMethods = ConfigurationManager.AppSettings["cors:allowedMethods"];
            var allowedOrigin = ConfigurationManager.AppSettings["cors:allowedOrigin"];
            var allowedHeaders = ConfigurationManager.AppSettings["cors:allowedHeaders"];
            var geduCors = new EnableCorsAttribute(allowedOrigin, allowedHeaders, allowedMethods)
            {
                SupportsCredentials = true
            };
            config.EnableCors(geduCors);
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            //默认路由
            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
            //自定义路由：匹配到action
         //   config.Routes.MapHttpRoute(
         //       name: "ActionApi",
         //       routeTemplate: "api/{controller}/{action}/{id}",
         //       defaults: new { id = RouteParameter.Optional }
         //   );
         //   //自定义路由二
         //   config.Routes.MapHttpRoute(
         //    name: "TestApi",
         //    routeTemplate: "api/{controller}/{ordertype}/{id}",
         //    defaults: new { id = RouteParameter.Optional }
         //);

            config.Filters.Add(new WebApiExceptionFilterAttribute());
        }
    }
}
