using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using Glove.IOT.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
   [LoginCheckFilter]
    public class DeviceController:BaseController
    {
        public IOperationLogService OperationLogService { get; set; }
        public IDeviceInfoService DeviceInfoService { get; set; }
        public IDeviceParameterInfoService DeviceParameterInfoService { get; set; }

        // GET: Device

        /// <summary>
        /// 添加设备信息
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public ActionResult Add(DeviceInfo deviceInfo)
        {
            var deviceId = DeviceInfoService.GetEntities(u => (u.DeviceId == deviceInfo.DeviceId&&u.IsDeleted==false)).FirstOrDefault();
            if (deviceId == null)
            {
                deviceInfo.SubTime = DateTime.Now; 
                int id = DeviceInfoService.Add(deviceInfo).Id;
                DeviceParameterInfo deviceParameterInfo = new DeviceParameterInfo
                {
                    DeviceInfoId = id,
                    StatusFlag = Glove.IOT.Model.Enum.StatusFlagEnum.未连接.ToString(),
                    SubTime=DateTime.Now,
                };
                DeviceParameterInfoService.Add(deviceParameterInfo);
                //写操作日志
                OperationLogService.Add("添加设备", "设备管理", LoginInfo, deviceInfo.DeviceId,"");
                return Content("Ok");
            }
            else
            {
                return Content("fail");
            }
        }
        /// <summary>
        /// 修改设备信息
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public ActionResult Edit(DeviceInfo deviceInfo)
        {
            
            deviceInfo.SubTime = DateTime.Now;
            DeviceInfoService.Update(deviceInfo);
            return Content("ok");
        }
        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int[] ids)
        {
            List<int> idList = ids.ToList();
            if (idList==null)
            {
                return Content("请选中要删除的数据！");
            }   

            DeviceInfoService.DeleteListByLogical(idList);

            foreach (var id in idList)
            {
                var deviceId = DeviceInfoService.GetEntities(d => d.Id == id).Select(d => d.DeviceId).FirstOrDefault();
                //写操作日志
                OperationLogService.Add("删除设备", "设备管理", LoginInfo, deviceId, "");
            }

            return Content("ok");
        }

        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllDeviceInfos(string limit,string page,string deviceId, string statusFlag)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");

            //过滤的设备名 过滤备注schDeviceId schStatusFlag
            var queryParam = new DeviceQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                SchDeviceId=deviceId,
                SchStatusFlag=statusFlag,
                Total = 0
            };
            var pageData = DeviceInfoService.LoagDevicePageData(queryParam).ToList();
            var data = new { code = 0, msg = "", count = queryParam.Total, data = pageData.ToList() };

            if (!string.IsNullOrEmpty(deviceId) || !string.IsNullOrEmpty(statusFlag))
            {
                //写操作日志
                OperationLogService.Add("查找设备", "设备管理", LoginInfo, deviceId, statusFlag);
            }
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 获取设备参数详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDeviceParameterInfo(string deviceId)
        {

            var deviceParameter = DeviceParameterInfoService.GetDeviceParameter(deviceId);
            var parameterHistory = DeviceParameterInfoService.GetHistoryParameter(deviceId);
            var data = new { deviceParameter, parameterHistory = parameterHistory.ToList() };
            //写操作日志
            OperationLogService.Add("查看设备", "设备管理", LoginInfo, deviceId, "");
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Test1()
        {
            int pageSize = 10;
            int pageIndex = 1;

            //过滤的设备名 过滤备注schDeviceId schStatusFlag
            var queryParam = new DeviceQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0
            };
            var pageData = DeviceInfoService.LoagDevicePageData(queryParam).ToList();
            var data = new { code = 0, msg = "", count = queryParam.Total, data = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

     

        public ActionResult IsLayerAdddevice()
        {
            return Content("Ok");
        }

        public ActionResult Devicemanage()
        {
            return View();
        }

        public ActionResult LayerAdddevice()
        {
            return View();
        }

        public ActionResult Devicedetail()
        {
            return View();
        }

        public ActionResult LayerSearchdevice()
        {
            return View();
        }
        public ActionResult LayerAddgroup()
        {
            return View();
        }
        public ActionResult test()
        {
            return View();
        }
        public ActionResult treetest()
        {
            return View();
        }
        public ActionResult eChartsTest()
        {
            return View();
        }
    }
}