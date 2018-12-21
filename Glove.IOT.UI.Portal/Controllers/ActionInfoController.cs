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
    public class ActionInfoController : BaseController
    {
        // GET: ActionInfo
        readonly short statusNormal = (short)Glove.IOT.Model.Enum.StatusFlagEnum.Normal;
        public IActionInfoService ActionInfoService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        public IR_UserInfo_RoleInfoService R_UserInfo_RoleInfoService { get; set; }
        public IR_RoleInfo_ActionInfoService R_RoleInfo_ActionInfoService { get; set; }
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
            var userRole = R_UserInfo_RoleInfoService.GetEntities(u => (u.UserInfoId == LoginUser.Id && u.StatusFlag == statusNormal));
            var rRoleAction = R_RoleInfo_ActionInfoService.GetEntities(r => r.StatusFlag == statusNormal);
            var action = ActionInfoService.GetEntities(a => true);
            //查找该用户角色对应的权限
            var roleAction = from r in userRole
                              from a in rRoleAction
                              where r.RoleInfoId == a.RoleInfoId
                              select a;
            //查找对应权限的名称
            var actionName = (from r in roleAction
                              from a in action
                              where r.ActionInfoId == a.Id
                              select a.ActionName).ToList();

            return Json(actionName, JsonRequestBehavior.AllowGet);

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