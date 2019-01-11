using Glove.IOT.IBLL;
using Glove.IOT.Model;
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
        public ActionResult GetAllGroupInfos()
        {
            var pageData = GroupInfoService.GetGroupInfo();
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
        /// 获取所以设备，并勾选已经存在组内的设备
        /// </summary>
        /// <param name="deviceQueryParam"></param>
        /// <returns></returns>
        public ActionResult GetAllDeviceInfos(string limit, string page, string deviceId, string statusFlag,int gId)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");

            //过滤的设备名 过滤备注schDeviceId schStatusFlag
            var queryParam = new DeviceQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                SchDeviceId = deviceId,
                SchStatusFlag = statusFlag,
                Total = 0
            };
            var pageData = GroupInfoService.LoagDevicePageData(queryParam,gId).ToList();
            var data = new { code = 0, msg = "", count = queryParam.Total, data = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="groupInfo"></param>
        /// <returns></returns>
        public ActionResult Add(GroupInfo groupInfo)
        {
            groupInfo.SubTime = DateTime.Now;
            GroupInfoService.Add(groupInfo);
            return Content("OK");
        }
        /// <summary>
        /// 编辑组信息
        /// </summary>
        /// <param name="groupInfo"></param>
        /// <returns></returns>
        public ActionResult Edit(GroupInfo groupInfo)
        {
            groupInfo.SubTime = DateTime.Now;
            GroupInfoService.Update(groupInfo);
            return Content("OK");
        }

     /// <summary>
     /// 为组添加设备
     /// </summary>
     /// <param name="gId"></param>
     /// <param name="alldIds"></param>
     /// <param name="dIds"></param>
     /// <returns></returns>
        public ActionResult SetDevices(int gId, int[] alldIds, int[] dIds)
        {
            
            List<int> dIdsList = dIds.ToList();
            List<int> alldIdsList = alldIds.ToList();
            //剁掉组里已存在的设备
            R_GroupInfo_DeviceInfoService.Delete(r => (r.GroupInfoId==gId&& alldIdsList.Contains(r.DeviceInfoId)));
            if (dIds[0] == 0)
            {
                return Content("OK");
            }
            //添加勾选的设备
            if (dIds[0] == 0){
                return Content("OK");
            }
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
        public ActionResult LayerSearchdevice()
        {
            return View();
        }
    }
}