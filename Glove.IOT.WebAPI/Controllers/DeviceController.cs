using Glove.IOT.IBLL;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Glove.IOT.WebAPI.Controllers
{
    [RoutePrefix("api/Device")]
    public class DeviceController : BaseController
    {
        public IDeviceInfoService DeviceInfoService { get; set; }
        public IDeviceParameterInfoService DeviceParameterInfoService { get; set; }

        //[Route("GetAllDeviceInfos")]
        //[HttpGet]
        //public IHttpActionResult GetAllDeviceInfos()
        //{
        //    //过滤的设备名 过滤备注schDeviceId schStatusFlag

        //    var queryParam = new DeviceQueryParam()
        //    {
        //        PageSize = 10,
        //        PageIndex = 1,
        //        Total = 0
        //    };
        //    var pageData = DeviceParameterInfoService.ApiGetDeviceParameter().ToList();
        //    return Json(pageData);
        //}
        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns></returns>
        [Route("GetAllDeviceInfos")]
        [HttpGet]
        public IHttpActionResult GetAllDeviceInfos(string limit, string page, string deviceId, string statusFlag)
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
            var pageData = DeviceInfoService.LoagDevicePageData(queryParam).ToList();
            var data = new { code = 0, msg = "", count = queryParam.Total, data = pageData.ToList() };


            return Json(data);

        }
        [Route("GetAllDeviceInfos")]
        [HttpGet]
        public IHttpActionResult GetAllDeviceInfos(string limit, string page)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");

            //过滤的设备名 过滤备注schDeviceId schStatusFlag
            var queryParam = new DeviceQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0
            };
            var pageData = DeviceInfoService.LoagDevicePageData(queryParam).ToList();
            var data = new { code = 0, msg = "", count = queryParam.Total, data = pageData.ToList() };


            return Json(data);

        }
        /// <summary>
        /// 获取设备参数详细信息
        /// </summary>
        /// <returns></returns>\
        [Route("GetDeviceParameterInfo")]
        [HttpGet]
        public IHttpActionResult GetDeviceParameterInfo(string deviceId)
        {
            var deviceParameter = DeviceParameterInfoService.GetDeviceParameter(deviceId);
            var parameterHistory = DeviceParameterInfoService.GetHistoryParameter(deviceId);
            var data = new { deviceParameter, parameterHistory = parameterHistory.ToList() };
                return Json(data);

        }

        [Route("GetEveryDaySumOutput")]
        [HttpGet]
        public IHttpActionResult GetEveryDaySumOutput()
        {
            var pageData = DeviceInfoService.GetSumOutput().Select(d=>new { d.Date,d.SumOutput}).ToList();
            return Json(pageData);
        }
    }
}
