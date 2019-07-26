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
        public IDeviceGroupInfoService DeviceGroupInfoService { get; set; }
        public IR_DeviceInfo_DeviceGroupInfoService R_DeviceInfo_DeviceGroupInfoService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
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
            var pageData = DeviceGroupInfoService.GetGroupInfo();
            var data = new {group = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 加载单个组对应的所有设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetGroupDevices(string id, string limit, string page)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");

            var baseParam = new BaseParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0,
            };
            var query = DeviceGroupInfoService.GetGroupDevices(id, baseParam);
            var data = new { code = 0, msg = "", count = query.Count(), data = query.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 获取所有设备，并勾选已经存在组内的设备
        /// </summary>
        /// <param name="deviceQueryParam"></param>
        /// <returns></returns>
        public ActionResult GetAllDeviceInfos(string limit, string page, string schDeviceName,string gId)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");
            int deviceName = int.Parse(schDeviceName ?? "0");

            //过滤的设备名 过滤备注schDeviceId schStatusFlag
            var queryParam = new DeviceRealtimeQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                SchDeviceName = deviceName,
                Total = 0
            };
            var pageData = DeviceGroupInfoService.LoagDevicePageData(queryParam, gId).ToList();
            var data = new { code = 0, msg = "", count = queryParam.Total, data = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="groupInfo"></param>
        /// <returns></returns>
        public ActionResult Add(DeviceGroupInfo groupInfo)
        {
            if (DeviceGroupInfoService.GetEntities(u => u.DeviceGroupName == groupInfo.DeviceGroupName & u.IsDeleted == false) != null)
            {
                groupInfo.CreateTime = DateTime.Now;
                groupInfo.Id = Guid.NewGuid().ToString();
                groupInfo.IsDeleted = false;
                DeviceGroupInfoService.Add(groupInfo);
                return Content("OK");
            }
            else
            {
                return Content("false");
            }
        }
        /// <summary>
        /// 编辑组信息
        /// </summary>
        /// <param name="groupInfo"></param>
        /// <returns></returns>
        public ActionResult Edit(DeviceGroupInfo groupInfo)
        {
            if (DeviceGroupInfoService.GetEntities(u => u.DeviceGroupName == groupInfo.DeviceGroupName & u.IsDeleted == false) != null)
            {
                groupInfo.CreateTime = DateTime.Now;
                DeviceGroupInfoService.Update(groupInfo);
                return Content("OK");
            }
            else
            {
                return Content("false");
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>del ok</returns>
        public ActionResult Delete(string[] gIds)
        {
            if (gIds==null)
            {
                return Content("请选中要删除的数据！");
            }
            //正常处理
            List<string> idList = gIds.ToList();

            DeviceGroupInfoService.DeleteListByLogical(idList);
            //原有组删除后 人员表重置为默认组
            UserInfoService.Update(r => idList.Contains(r.DeviceGroupInfoId), r => new UserInfo
            {
                DeviceGroupInfoId = "123"
            });
            return Content("del ok");
        }

        /// <summary>
        /// 为组添加设备
        /// </summary>
        /// <param name="gId"></param>
        /// <param name="alldIds"></param>
        /// <param name="dIds"></param>
        /// <returns></returns>
        public ActionResult SetDevices(string gId, string[]alldIds,string[] dIds)
        {
            List<string> dIdsList = dIds.ToList();

            //剁掉组里已存在的设备
            R_DeviceInfo_DeviceGroupInfoService.Delete(r => (r.DeviceGroupInfoId == gId&&alldIds.Contains(r.DeviceInfoId)));
            //添加勾选的设备
            if (dIds[0] == string.Empty)
            {
                return Content("OK");
            }
            R_DeviceInfo_DeviceGroupInfoService.AddSelectDevices(gId, dIdsList);

            return Content("OK");
        }

        /// <summary>
        /// 为组添加设备重载
        /// </summary>
        /// <param name="gId"></param>
        /// <param name="alldIds"></param>
        /// <returns></returns>
        //public ActionResult SetDevices(int gId)
        //{
        //    //List<int> alldIdsList = alldIds.ToList();
        //    ////剁掉组里已存在的设备
        //    //R_GroupInfo_DeviceInfoService.Delete(r => (r.GroupInfoId == gId && alldIdsList.Contains(r.DeviceInfoId)));
        //    return Content("OK");
        //}


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