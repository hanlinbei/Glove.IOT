
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
        public ActionResult SetActions(int rId,string[] Data)
        {

            List<int> idList = new List<int>();
            for (var i = 0; i < Data.Count(); i++)
            {
                var actionName = Data[i];
                var actionId = ActionInfoService.GetEntities(a => a.ActionName == actionName).Select(a => a.Id).ToList().FirstOrDefault();
                idList.Add(actionId);
            }
            //删除之前存在的关联信息
            R_RoleInfo_ActionInfoService.Delete(r => r.RoleInfoId == rId);
            //添加勾选的权限
            R_RoleInfo_ActionInfoService.AddSelectActions(rId, idList);
            
 
            return Content("oK");

        }


           public ActionResult Userpower()
           {
            return View();
           }

    }
}