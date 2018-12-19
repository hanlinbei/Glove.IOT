using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Models
{
    public class ActionCheckFilterAttribute : ActionFilterAttribute
    {
        readonly short statusNormal = (short)Glove.IOT.Model.Enum.StatusFlagEnum.Normal;
        //在当前的控制器里面所有的方法执行之前，都先执行此代码
        public bool IsCheckuserLogin  { get; set; }
        public UserInfo LoginUser { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //MVC请求来了之后，根据请求地址，创建控制器工厂（spring.Net），控制器工厂创建控制器，执行方法
            //Spring.Net
            //base.OnActionExecuting(filterContext);
            //var items = filterContext.RouteData.Values;


            if (IsCheckuserLogin)
            {
                //使用mm+cookies代替session
                //校验用户是否已经登录
                //从缓存中拿到当前的登录的用户信息
                if (filterContext.HttpContext.Request.Cookies["userLoginId"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }
                string userGuid = filterContext.HttpContext.Request.Cookies["userLoginId"].Value;
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


                //string url = filterContext.HttpContext.Request.Url.AbsolutePath.ToLower();
                //string httpMethod = filterContext.HttpContext.Request.HttpMethod.ToLower();
                //if (LoginUser.UName == "admin" )
                //{
                //    return;
                //}
                ////通过容器创建一个对象
                //IApplicationContext ctx = ContextRegistry.GetContext();
                //IActionInfoService ActionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;
                //IUserInfoService UserInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;
                //IR_UserInfo_RoleInfoService r_UserInfo_RoleInfoService = ctx.GetObject("R_UserInfo_RoleInfoService") as IR_UserInfo_RoleInfoService;
                //IR_RoleInfo_ActionInfoService r_RoleInfo_ActionInfoService = ctx.GetObject("R_RoleInfo_ActionInfoService") as IR_RoleInfo_ActionInfoService;

                //var actionInfo = ActionInfoService.GetEntities(a => a.Url.ToLower() == url && a.HttpMethod.ToLower() == httpMethod)
                //    .FirstOrDefault();

                //if (actionInfo == null)
                //{
                //    filterContext.Result = new RedirectResult("/Error.html");
                //}

                //var userRole = r_UserInfo_RoleInfoService.GetEntities(u => (u.UserInfoId == LoginUser.Id && u.StatusFlag == statusNormal));
                //var rRoleAction = r_RoleInfo_ActionInfoService.GetEntities(r => r.StatusFlag == statusNormal);
                //var roleAction = from r in userRole
                //                 from a in rRoleAction
                //                 where r.RoleInfoId == a.RoleInfoId
                //                 select a;
                //var temp = (from a in roleAction
                //            where a.ActionInfoId == actionInfo.Id
                //            select a).Count();

                //if (temp <= 0)
                //{
                //    //filterContext.Result = new HttpNotFoundResult();
                //    filterContext.Result = new RedirectResult("/Error.html");



                //}

            }
          

        }

    }
}