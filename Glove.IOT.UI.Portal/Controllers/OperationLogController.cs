using Glove.IOT.IBLL;
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
    public class OperationLogController : BaseController
    {
        public IOperationLogService OperationLogService { get; set; }
 
        /// <summary>
        /// 获取所有操作日志信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllOperationLogs(string limit, string page)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");


            var queryParam = new OperationLogQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0
            };

            var pageData = OperationLogService.LoagOperationLogPageData(queryParam);
            var temp = pageData.Select(o => new
            {
                o.UName,
                o.ActionType,
                o.ActionName,
                o.OperationObj,
                o.Id,
                o.SubTime
            });
            var data = new { code = 0, msg = "", count = queryParam.Total, data = temp.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        public ActionResult Operationlog()
        {
            return View();
        }
        public ActionResult LayerSearcholog()
        {
            return View();
        }
    }
}