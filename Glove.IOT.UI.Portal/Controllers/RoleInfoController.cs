using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class RoleInfoController : Controller
    {
        // GET: RoleInfo
        public IRoleInfoService RoleInfoService { get; set; }
        short delflagNormal = (short)Glove.IOT.Model.Enum.DelFlagEnum.Normal;

        public ActionResult Index()
        {
            return View();
        }
        #region load roleInfos
        public ActionResult GetAllRoleInfos()
        {
            //jquery easyui:table:{total:32,row:[{},{}]}
            // easyui:table 在初始化的时候自动发送以下俩个参数值
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            //拿到当前页的数据
            var temp = RoleInfoService.GetPageEntities(pageSize, pageIndex,
                                                         out total, u => u.DelFlag == delflagNormal, u => u.Id,
                                                         true);
            var tempData = temp.Select(a => new
            {
                a.Id,
                a.RoleName,
                a.Remark,
                a.SubTime,
                a.ModfiedOn,
                a.DelFlag
               
            });

            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Add

        public ActionResult Add(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = delflagNormal;
            roleInfo.ModfiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            RoleInfoService.Add(roleInfo);
            return Content("Ok");

        }
        #endregion
    }
}