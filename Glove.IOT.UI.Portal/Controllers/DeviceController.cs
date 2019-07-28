﻿using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using Glove.IOT.UI.Portal.Models;
using Newtonsoft.Json;
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
        public IDeviceRealtimeDataService DeviceRealtimeDataService { get; set; }
        public IDeviceHistoryDataService DeviceHistoryDataService { get; set; }
        public IDeviceCmdService DeviceCmdService { get; set; }

 
        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllDeviceRealtimeData(string limit,string page,string schDeviceName, string schStatusFlag)
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
                SchStatusFlag = schStatusFlag,
                Total = 0
            };

            var pageData = DeviceRealtimeDataService.LoadDeviceRealtimePageData(queryParam).ToList();
            var data = new { code = 0, msg = "", count = queryParam.Total, data = pageData };
            
            if (!string.IsNullOrEmpty(schDeviceName) || !string.IsNullOrEmpty(schStatusFlag))
            {
                //写操作日志
                OperationLogService.Add("查找设备", "设备管理", LoginInfo, schDeviceName, schStatusFlag);
            }
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 获取单个设备近7天每天产量详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWeekEachDayData(int deviceName)
        {
            var data = DeviceHistoryDataService.GetWeekEachDayData(deviceName).ToList();
            //写操作日志
            OperationLogService.Add("查看设备", "设备管理", LoginInfo, deviceName.ToString(), "");
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 获取控制命令表
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult GetCmdPageData(string limit, string page)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");

            var queryParam = new DeviceCmdQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0
            };
            var pageData = DeviceCmdService.LoagPageData(queryParam).ToList();
            var data = new { code = 0, msg = "", count = queryParam.Total, data = pageData };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 长传程序更新指令
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadProgramFile()
        {
            var file = Request.Files["file"];
            var names = Request["deviceNames"];
            int[] deviceNames = Array.ConvertAll<string, int>(names.Split(new char[] { ',' }), s => int.Parse(s));
            if (file != null)
            {
                string path = "/UploadFiles/UploadProgramFiles/" + file.FileName;
                string realPath = Request.MapPath(path);
                if (!System.IO.File.Exists(realPath))
                {
                    file.SaveAs(realPath);
                }            
                DeviceCmdService.AddDeviceCmd(realPath, deviceNames);
                return Content("ok");
            }
            return Content("false");
        }

        /// <summary>
        /// 获取每台设备今日产量
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="schDeviceName"></param>
        /// <returns></returns>
        public ActionResult GetTodayOutput(string limit, string page, string schDeviceName)
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
            var pageData = DeviceHistoryDataService.GetTodayOutput(queryParam);
            var data = new { code = 0, msg = "", count = queryParam.Total, data = pageData };

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
        public ActionResult DeviceOutputs()
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
        public ActionResult UploadFile()
        {
            return View();
        }
        public ActionResult UploadFileInfo()
        {
            return View();
        }
        public ActionResult LayerUploadFile()
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