using Glove.IOT.IBLL;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class TeamInfoController : BaseController
    {
        public ITeamInfoService TeamInfoService { get; set;}
        // GET: TeamInfo
        public ActionResult Classmanage()
        {
            return View();
        }
        public ActionResult LayerAddclass()
        {
            return View();
        }
        /// <summary>
        /// 获取班信息
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="schTime">按时间查找条件</param>
        /// <param name="schTName">按名字查找条件</param>
        /// <returns></returns>
        public ActionResult GetTeamInfo(string limit, string page,string schTime,string schTName)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");
            //过滤条件
            var teamQueryParam = new TeamQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                SchTime=schTime,
                SchTName=schTName,
                Total = 0,
            };
            var pageData = TeamInfoService.GetTeamInfo(teamQueryParam);
            var data = new { code = 0, msg = "", count = teamQueryParam.Total, data = pageData.ToList() };

            return Json(data, JsonRequestBehavior.AllowGet);

        }

    }
}