using Glove.IOT.BLL;
using Glove.IOT.IBLL;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace Glove.IOT.WebAPI.Models
{
    /// <summary>
    /// 自定义此特性用于接口的身份验证
    /// </summary>
    public class RequestAuthorizeAttribute:AuthorizeAttribute
    {
        //重写基类的验证方式，加入我们自定义的ticket验证
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Parameter != null))
            {
                //解密用户ticket，并校验用户名密码是否匹配
                var encryptTicket = authorization.Parameter;
                if (ValidateTicket(encryptTicket))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()
                    .OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous) base.OnAuthorization(actionContext);
                else HandleUnauthorizedRequest(actionContext);
            }
        }

        //校验用户名密码
        private bool ValidateTicket(string encryptTicket)
        {
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;
            //从TIcket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string strUser = strTicket.Substring(0, index);
            string strPwd = strTicket.Substring(index + 1);

            //通过容器创建一个对象
            IApplicationContext ctx = ContextRegistry.GetContext();
            IUserInfoService UserInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;
            var userInfo =
            UserInfoService.GetEntities(u => (u.UName == strUser && u.Pwd == strPwd))
            .FirstOrDefault();
            if (userInfo != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}