using Glove.IOT.Common;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class BaseController : Controller
    {

        //在当前的控制器里面所有的方法执行之前，都先执行此代码
        public bool IsCheckuserLogin = true;
        public UserInfo LoginUser { get; set; }
        public OperationLog LoginInfo { get; set; }
        public string ActionParameters {get;set;}
        public string ActionName { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //TDO
            //用户信息处理
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity as FormsIdentity;
                LoginInfo = new OperationLog
                {
                    Id = Convert.ToInt32(user.Ticket.UserData),
                    UName = User.Identity.Name,
                    Ip = WebHelper.GetClientIp(),
                    //Mac = WebHelper.GetClientMACAddress()
                };

            }
        }
    }
}