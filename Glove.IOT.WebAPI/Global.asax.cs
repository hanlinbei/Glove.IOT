using Glove.IOT.WebAPI.Models;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace Glove.IOT.WebAPI
{
    public class WebApiApplication : SpringMvcApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //从配置文件读取log4net的配置，然后进行一个初始化的工作
            log4net.Config.XmlConfigurator.Configure();
            GlobalConfiguration.Configuration.Filters.Add(new WebApiExceptionFilterAttribute());
        }
        public override void Init()
        {
            this.PostAuthenticateRequest += (sender, e) => HttpContext.Current.SetSessionStateBehavior(
                  SessionStateBehavior.Required);
            base.Init();
        }
    }
}
