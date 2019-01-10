using Glove.IOT.IBLL;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class GroupInfoController : Controller
    {
        public IGroupInfoService GroupInfoService { get; set; }
        public IR_GroupInfo_DeviceInfoService R_GroupInfo_DeviceInfoService { get; set; }
        // GET: GroupInfo
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取所有组
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="schGName"></param>
        /// <returns></returns>
        public ActionResult GetAllGroupInfos(string limit, string page)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");
            //过滤条件
            var groupQueryParam = new GroupQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0,
            };
            var pageData = GroupInfoService.GetGroupInfo(groupQueryParam);
            var data = new {group = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 加载单个组对应的所有设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetGroupDevices(int id, string limit, string page)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");

            var baseParam = new BaseParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0,
            };
            var query = GroupInfoService.GetGroupDevices(id, baseParam);
            var data = new { code = 0, msg = "", count = query.Count(), data = query.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 为组添加选定的设备
        /// </summary>
        /// <param name="gId"></param>
        /// <param name="dIds"></param>
        /// <returns></returns>
        public ActionResult SetDevices(int gId, int[] alldIds, int[] dIds)
        {
            List<int> dIdsList = dIds.ToList();
            List<int> alldIdsList = alldIds.ToList();
            //剁掉组里已存在的设备
            R_GroupInfo_DeviceInfoService.Delete(r => (r.GroupInfoId==gId&& alldIdsList.Contains(r.DeviceInfoId)));
            //添加勾选的设备
            R_GroupInfo_DeviceInfoService.AddSelectDevices(gId, dIdsList);

            return Content("OK");

        }
        public ActionResult Groupmanage()
        {
            return View();
        }
        public ActionResult GroupAdddevice()
        {
            return View();
        }
    }
}