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
        public IWarningInfoService WarningInfoService { get; set; }
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
        public ActionResult GetWarningInfo(string limit, string page, string firsTime,string lastTime, string schMessage,string schDeviceId)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");
            
            //过滤条件
            var warningQueryParam = new WarningQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                FirstTime = firsTime,
                LastTime=lastTime,
                SchMessage = schMessage,
                SchDeviceId= schDeviceId,
                Total = 0,
            };
            var pageData = WarningInfoService.GetWarningInfo(warningQueryParam);
            var data = new { code = 0, msg = "", count = warningQueryParam.Total, data = pageData.ToList() };
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}