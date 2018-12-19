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
    [ActionCheckFilter(IsCheckuserLogin = false)]
    public class RoleInfoController : Controller
    {
        // GET: RoleInfo
        public IRoleInfoService RoleInfoService { get; set; }
        public IR_RoleInfo_ActionInfoService R_RoleInfo_ActionInfoService { get; set; }
        readonly short statusNormal = (short)Glove.IOT.Model.Enum.StatusFlagEnum.Normal;
        readonly short delFlag = (short)Glove.IOT.Model.Enum.StatusFlagEnum.Deleted;

        /// <summary>
        /// 角色起始视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 所有已选的权限发送给前台
        /// </summary>
        /// <param name="rId"></param>
        /// <returns></returns>
        public ActionResult GetExitsActions(int rId)
        {
            RoleInfo role = RoleInfoService.GetEntities(r => r.Id == rId).FirstOrDefault();
            var allActionInfoIds = (from r in role.R_RoleInfo_ActionInfo
                                  where r.RoleInfoId == rId&&r.StatusFlag==statusNormal
                                  select r.ActionInfoId).ToList();
            return Json(allActionInfoIds, JsonRequestBehavior.AllowGet);

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
            foreach (var strId in strIds)
            {
                idList.Add(int.Parse(strId));

            }

            //第一：当前角色的id ----rid
            int rId = idList[0];
            //第二：当前用户在角色关联表中的ID
            RoleInfo role = RoleInfoService.GetEntities(r => r.Id == rId).FirstOrDefault();
            var allRoleInfoActionInfoIds = (from r in role.R_RoleInfo_ActionInfo
                                    where r.RoleInfoId == rId
                                    select r.Id).ToList();

            for (int i = 0; i < allRoleInfoActionInfoIds.Count(); i++)
            {
                int userInfoId = Convert.ToInt32(allRoleInfoActionInfoIds[i]);
                var rUserRole = R_RoleInfo_ActionInfoService.GetEntities(r =>
                r.Id == userInfoId).FirstOrDefault();
                R_RoleInfo_ActionInfoService.Delete(rUserRole);
            }



            for (int i = 1; i < idList.Count; i++)
            {
                int actionId = Convert.ToInt32(idList[i]);
                R_RoleInfo_ActionInfo rRoleInfoActionInfo = new R_RoleInfo_ActionInfo();
                rRoleInfoActionInfo.RoleInfoId = rId;
                rRoleInfoActionInfo.ActionInfoId = actionId;
                rRoleInfoActionInfo.StatusFlag = statusNormal;
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