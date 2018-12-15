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
    public class DeviceController : Controller
    {
        readonly short statusNormal = (short)Glove.IOT.Model.Enum.StatusFlagEnum.Normal;
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
            var deviceId = DeviceInfoService.GetEntities(u => (u.DeviceId == deviceInfo.DeviceId&&u.StatusFlag==statusNormal)).FirstOrDefault();
            if (deviceId == null)
            {
                deviceInfo.SubTime = DateTime.Now;
                int id = DeviceInfoService.Add(deviceInfo).Id;
                DeviceParameterInfo deviceParameterInfo = new DeviceParameterInfo
                {
                    DeviceInfoId = id,
                    StatusFlag = 4,
                    SubTime=DateTime.Now,
                };
                DeviceParameterInfoService.Add(deviceParameterInfo);
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
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除的数据！");
            }
            //正常处理
            string[] strIds = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (var strId in strIds)
            {
                idList.Add(int.Parse(strId));

            }
            DeviceInfoService.DeleteListByLogical(idList);
            return Content("del ok");
        }

        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllDeviceInfos()
        {
            int pageSize = int.Parse(Request["limit"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            //过滤的用户名 过滤备注schName schRemark

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
        /// <summary>
        /// 获取设备参数详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDeviceParameterInfo()
        {
            int deviceId = int.Parse(Request["DeviceId"]);
            var deviceParameter = DeviceParameterInfoService.GetDeviceParameter(deviceId);
            var data = deviceParameter.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

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
    }
}