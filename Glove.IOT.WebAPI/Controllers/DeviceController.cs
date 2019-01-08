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

        [Route("GetAllDeviceInfos")]
        [HttpGet]
        public IHttpActionResult GetAllDeviceInfos()
        {
            //过滤的设备名 过滤备注schDeviceId schStatusFlag

            var queryParam = new DeviceQueryParam()
            {
                PageSize = 10,
                PageIndex = 1,
                Total = 0
            };
            var pageData = DeviceParameterInfoService.ApiGetDeviceParameter().ToList();
            return Json(pageData);
        }

        [Route("GetEveryDaySumOutput")]
        [HttpGet]
        public IHttpActionResult GetEveryDaySumOutput()
        {
            var pageData = DeviceInfoService.ApiGetSumOutput().ToList();
            return Json(pageData);
        }
    }
}
