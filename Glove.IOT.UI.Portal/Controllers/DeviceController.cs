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


        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int[] ids)
        {
            

            return Content("ok");
        }

        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllDeviceInfos(string limit,string page,string deviceId, string statusFlag)
        {
            

            if (!string.IsNullOrEmpty(deviceId) || !string.IsNullOrEmpty(statusFlag))
            {
                //写操作日志
                OperationLogService.Add("查找设备", "设备管理", LoginInfo, deviceId, statusFlag);
            }
            return Json(0, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 获取设备参数详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDeviceParameterInfo(string deviceId)
        {

            
          
            //写操作日志
            OperationLogService.Add("查看设备", "设备管理", LoginInfo, deviceId, "");
            return Json(0, JsonRequestBehavior.AllowGet);

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