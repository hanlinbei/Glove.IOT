using Glove.IOT.IBLL;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class WarningInfoController : BaseController
    {
        public IOperationLogService OperationLogService { get; set; }
        public IDeviceRealtimeWarningService DeviceRealtimeWarningService { get; set; }
        public IDeviceHistoryWarningService DeviceHistoryWarningService { get; set; }

        /// <summary>
        /// 报警视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Warningmanage()
        {
            return View();
        }
        public ActionResult Warninghistory()
        {
            return View();
        }
        public ActionResult LayerSearchwarning()
        {
            return View();
        }
       
        // GET: Warning
        /// <summary>
        /// 获取设备报警信息
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="firsTime"></param>
        /// <param name="lastTime"></param>
        /// <param name="schMessage"></param>
        /// <param name="schDeviceId"></param>
        /// <returns></returns>
        public ActionResult GetRealTimeWarningInfo(string limit, string page, string schStartTime, string schMessage,string schDeviceName)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");
            int deviceName = int.Parse(schDeviceName ?? "0");
            
            //过滤条件
            var warningQueryParam = new WarningQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                SchStartTime = schStartTime,
                SchMessage = schMessage,
                SchDeviceName= deviceName,
                Total = 0,
            };
            var pageData=DeviceRealtimeWarningService.GetRealTimeWarningInfo(warningQueryParam);

            var data = new { code = 0, msg = "", count = warningQueryParam.Total, data = pageData.ToList() };
            //写操作日志
            OperationLogService.Add("实时报警查看", "报警管理", LoginInfo, "报警", "");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取所有设备历史报警信息
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="schStartTime"></param>
        /// <param name="schMessage"></param>
        /// <param name="schDeviceName"></param>
        /// <returns></returns>
        public ActionResult GetHistoryWarningInfo(string limit, string page, string schStartTime, string schMessage, string schDeviceName)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");
            int deviceName = int.Parse(schDeviceName ?? "0");

            //过滤条件
            var warningQueryParam = new WarningQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                SchStartTime = schStartTime,
                SchMessage = schMessage,
                SchDeviceName = deviceName,
                Total = 0,
            };
            var pageData = DeviceHistoryWarningService.GetHistoryWarningInfo(warningQueryParam);

            var data = new { code = 0, msg = "", count = warningQueryParam.Total, data = pageData.ToList() };
            //写操作日志
            OperationLogService.Add("历史报警查看", "报警管理", LoginInfo, "报警", "");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}