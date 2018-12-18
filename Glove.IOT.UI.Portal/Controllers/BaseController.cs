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
                string httpMethod = Request.HttpMethod.ToLower();
                //通过容器创建一个对象
                IApplicationContext ctx = ContextRegistry.GetContext();
                IActionInfoService ActionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;
                IUserInfoService UserInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;
                IR_UserInfo_RoleInfoService r_UserInfo_RoleInfoService = ctx.GetObject("R_UserInfo_RoleInfoService") as IR_UserInfo_RoleInfoService;
                IR_RoleInfo_ActionInfoService r_RoleInfo_ActionInfoService = ctx.GetObject("R_RoleInfo_ActionInfoService") as IR_RoleInfo_ActionInfoService;

                var actionInfo = ActionInfoService.GetEntities(a => a.Url.ToLower() == url && a.HttpMethod.ToLower() == httpMethod)
                    .FirstOrDefault();
                if (actionInfo == null)
                {
                    Response.Redirect("/Error.html");

                }

                
                var userRole = r_UserInfo_RoleInfoService.GetEntities(u => u.UserInfoId == LoginUser.Id).FirstOrDefault();
                var roleAction = r_RoleInfo_ActionInfoService.GetEntities(r => r.RoleInfoId == userRole.RoleInfoId).FirstOrDefault();
                var actions = ActionInfoService.GetEntities(a => a.Id == roleAction.ActionInfoId);
                var temp = (from a in actions
                            where a.Id == actionInfo.Id
                            select a).Count();
                if (temp <= 0)
                {
                    Response.Redirect("/Error.html");
                }
                
            }
        }
    }
}