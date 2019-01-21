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
        public IWarningInfoService WarningInfoService { get; set; }
        /// <summary>
        /// 报警视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Warningmanage()
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
        public ActionResult GetWarningInfo(string limit, string page, string firsTime, string schMessage,string schDeviceId)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");
            
            //过滤条件
            var warningQueryParam = new WarningQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                FirstTime = firsTime,
                SchMessage = schMessage,
                SchDeviceId= schDeviceId,
                Total = 0,
            };
            var pageData = WarningInfoService.GetWarningInfo(warningQueryParam);
            var data = new { code = 0, msg = "", count = warningQueryParam.Total, data = pageData.ToList() };
            //写操作日志
            OperationLogService.Add("报警查看", "报警管理", LoginInfo, "报警", "");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}