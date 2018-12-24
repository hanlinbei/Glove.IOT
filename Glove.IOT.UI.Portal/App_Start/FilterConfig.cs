using Glove.IOT.UI.Portal.Models;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionFilterAttribut());
            filters.Add(new LoginCheckFilterAttribute());
        }
    }
}
