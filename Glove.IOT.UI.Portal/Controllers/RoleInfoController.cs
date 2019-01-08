
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    [LoginCheckFilter]
    public class RoleInfoController : BaseController
    {
        // GET: RoleInfo
        public IRoleInfoService RoleInfoService { get; set; }
        public IR_RoleInfo_ActionInfoService R_RoleInfo_ActionInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }



        /// <summary>
        /// 所有已选的权限发送给前台
        /// </summary>
        /// <param name="rId"></param>
        /// <returns></returns>
        public ActionResult GetExitsActions(int rId)
        {
            var roleAction = R_RoleInfo_ActionInfoService.GetEntities(r => (r.RoleInfoId == rId&&r.IsDeleted==false));
            var action = ActionInfoService.GetEntities(a => true);
            var allActionNames = from r in roleAction
                                   from a in action
                                  where r.ActionInfoId==a.Id
                                  select a.ActionName;
            return Json(allActionNames, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 给角色设置权限
        /// </summary>
        /// <param name="UId">用户Id</param>
        /// <returns>OK</returns>
        public ActionResult SetActions()
        {
            string ids = Request.QueryString["Data"];
            //正常处理
            string[] strIds = ids.Split(',');
            List<int> idList = new List<int>();
            idList.Add(int.Parse(strIds[0]));
            for (var i = 1; i < strIds.Count(); i++)
            {
                var actionName = strIds[i];
                var actionId = ActionInfoService.GetEntities(a => a.ActionName == actionName).Select(a => a.Id).ToList().FirstOrDefault();
                idList.Add(actionId);
            }

            //第一：当前角色的id ----rid
            int rId = idList[0];
            //第二：当前用户在角色关联表中的ID
            RoleInfo role = RoleInfoService.GetEntities(r => r.Id == rId).FirstOrDefault();
            var allRoleInfoActionInfoIds = (from r in role.R_RoleInfo_ActionInfo
                                    where r.RoleInfoId == rId&&r.IsDeleted==false
                                    select r.Id).ToList();
            //全部剁掉
            for (int i = 0; i < allRoleInfoActionInfoIds.Count(); i++)
            {
                int userInfoId = Convert.ToInt32(allRoleInfoActionInfoIds[i]);
                var rUserRole = R_RoleInfo_ActionInfoService.GetEntities(r =>
                r.Id == userInfoId).FirstOrDefault();
                R_RoleInfo_ActionInfoService.Delete(rUserRole);
            }


            //添加勾选的权限
            for (int i = 1; i < idList.Count; i++)
            {
                int actionId = Convert.ToInt32(idList[i]);
                R_RoleInfo_ActionInfo rRoleInfoActionInfo = new R_RoleInfo_ActionInfo
                {
                    RoleInfoId = rId,
                    ActionInfoId = actionId,
                    IsDeleted = false
                };
                R_RoleInfo_ActionInfoService.Add(rRoleInfoActionInfo);

            }
            return Content("oK");

        }


           public ActionResult Userpower()
           {
            return View();
           }

    }
}