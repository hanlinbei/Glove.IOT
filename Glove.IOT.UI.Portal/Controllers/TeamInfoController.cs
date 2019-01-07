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
        public ActionResult LayerEditclass()
        {
            return View();
        }
        public ActionResult LayerSearchclass()
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

        /// <summary>
        /// 添加班信息
        /// </summary>
        /// <param name="tName"></param>
        /// <param name="startTime"></param>
        /// <param name="stopTime"></param>
        /// <returns></returns>
        public ActionResult AddTeamInfo(string tName, string startTime, string stopTime)
        {
            TeamInfo teamInfo = new TeamInfo
            {
                TName = tName,
                StartTime = Convert.ToDateTime(startTime),
                StopTime = Convert.ToDateTime(stopTime),
                SubTime=DateTime.Now
            };
            TeamInfoService.Add(teamInfo);
            return Content("Ok");
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>del ok</returns>
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
            TeamInfoService.DeleteListByLogical(idList);
            return Content("del ok");
        }

        /// <returns>ok</returns>
        public ActionResult Edit(TeamInfo teamInfo)
        {
            teamInfo.SubTime = DateTime.Now;
            TeamInfoService.Update(teamInfo);
            return Content("ok");
        }

    }
}