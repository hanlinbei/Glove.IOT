using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class BaseController : Controller
    {
        //在当前的控制器里面所有的方法执行之前，都先执行此代码
        public bool IsCheckuserLogin = true;
        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //MVC请求来了之后，根据请求地址，创建控制器工厂（spring.Net），控制器工厂创建控制器，执行方法
            //Spring.Net
            base.OnActionExecuting(filterContext);
            var items = filterContext.RouteData.Values;
          

            if (IsCheckuserLogin)
            {
                //使用mm+cookies代替session
                //校验用户是否已经登录
                //从缓存中拿到当前的登录的用户信息
                if (Request.Cookies["userLoginId"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }
                string userGuid = Request.Cookies["userLoginId"].Value;
                UserInfo userInfo = Common.Cache.CacheHelper.GetCache(userGuid) as UserInfo;
                if (userInfo == null)
                {
                    //用户长时间不操作，超时
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");

                }
                LoginUser = userInfo;
                //滑动窗口机制
                Common.Cache.CacheHelper.SetCache(userGuid, userInfo, DateTime.Now.AddMinutes(20));
                //if (filterContext.HttpContext.Session["loginUser"] == null)
                //{
                //    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                //}
                //else
                //{
                //    LoginUser = filterContext.HttpContext.Session["loginUser"] as UserInfo;
                //}
                string url = Request.Url.AbsolutePath.ToLower();
                
            }
        }
    }
}