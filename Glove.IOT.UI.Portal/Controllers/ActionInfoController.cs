using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.UI.Portal.Models;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    [ActionCheckFilter(IsCheckuserLogin =true)]
    public class ActionInfoController : Controller
    {
        // GET: ActionInfo
        readonly short statusNormal = (short)Glove.IOT.Model.Enum.StatusFlagEnum.Normal;

        /// <summary>
        /// 权限起始视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetActions()
        {
            string userGuid = HttpContext.Request.Cookies["userLoginId"].Value;
            UserInfo loginUser = Common.Cache.CacheHelper.GetCache(userGuid) as UserInfo;
            //通过容器创建一个对象
            IApplicationContext ctx = ContextRegistry.GetContext();
            IActionInfoService ActionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;
            IUserInfoService UserInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;
            IR_UserInfo_RoleInfoService r_UserInfo_RoleInfoService = ctx.GetObject("R_UserInfo_RoleInfoService") as IR_UserInfo_RoleInfoService;
            IR_RoleInfo_ActionInfoService r_RoleInfo_ActionInfoService = ctx.GetObject("R_RoleInfo_ActionInfoService") as IR_RoleInfo_ActionInfoService;


            var userRole = r_UserInfo_RoleInfoService.GetEntities(u => (u.UserInfoId == loginUser.Id && u.StatusFlag == statusNormal));
            var rRoleAction = r_RoleInfo_ActionInfoService.GetEntities(r => r.StatusFlag == statusNormal);
            var roleAction = (from r in userRole
                             from a in rRoleAction
                             where r.RoleInfoId == a.RoleInfoId
                             select a.ActionInfoId).ToList();
            return Json(roleAction, JsonRequestBehavior.AllowGet);

        }
    
  
        

        /// <summary>
        /// 上传图片处理
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadImage()
        {
            var file = Request.Files["fileMenuIcon"];

            string path = "/UploadFiles/UploadImgs/" + Guid.NewGuid().ToString() + "-" + file.FileName;

            file.SaveAs(Request.MapPath(path));

            return Content(path);
        }
 
    }
}