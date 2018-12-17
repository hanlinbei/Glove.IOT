
using Glove.IOT.Tcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Glove.IOT.UI.Portal
{
    public class MvcApplication : Spring.Web.Mvc.SpringMvcApplication//System.Web.HttpApplication
    {   
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //从配置文件读取log4net的配置，然后进行一个初始化的工作
            log4net.Config.XmlConfigurator.Configure();

            //开启一个TCP线程
            TcpHelper tcpHelper = new TcpHelper();
            Thread th = new Thread(tcpHelper.SocketInit)
            {
                IsBackground = true
            };
            th.Start();
        }
    }
}
