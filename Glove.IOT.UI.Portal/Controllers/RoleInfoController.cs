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
        readonly short statusFlagNormal = (short)Glove.IOT.Model.Enum.StatusFlagEnum.Normal;
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
       /// 加载角色信息
       /// </summary>
       /// <returns>Json数据</returns>
        public ActionResult GetAllRoleInfos()
        {
            //jquery easyui:table:{total:32,row:[{},{}]}
            // easyui:table 在初始化的时候自动发送以下俩个参数值
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            //拿到当前页的数据
            var temp = RoleInfoService.GetPageEntities(pageSize, pageIndex,
                                                         out total, u => u.StatusFlag !=delFlag, u => u.Id,
                                                         true);
            var tempData = temp.Select(a => new
            {
                a.Id,
                a.RoleName,
                a.Remark,
                a.SubTime,
                a.StatusFlag
               
            });

            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="roleInfo">前端传来的角色信息</param>
        /// <returns>OK</returns>
        public ActionResult Add(RoleInfo roleInfo)
        {
            roleInfo.StatusFlag = statusFlagNormal;
            roleInfo.SubTime = DateTime.Now;
            RoleInfoService.Add(roleInfo);
            return Content("Ok");

        }
       
    }
}