using Glove.IOT.IBLL;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class GroupInfoController : Controller
    {
        public IGroupInfoService GroupInfoService { get; set; }
        // GET: GroupInfo
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取所有组
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="schGName"></param>
        /// <returns></returns>
        public ActionResult GetAllGroupInfos(string limit, string page, string schGName)
        {
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");
            //过滤条件
            var groupQueryParam = new GroupQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                SchGName = schGName,
                Total = 0,
            };
            var pageData = GroupInfoService.GetGroupInfo(groupQueryParam);
            //var data = new { code = 0, msg = "", count = groupQueryParam.Total, data = pageData.ToList() };
            var data = new {group = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 加载单个组对应的所有设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetGroupDevices(int id)
        {
            var query = GroupInfoService.GetGroupDevices(id);
            var data = new { code = 0, msg = "", count = query.Count(), data = query.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Groupmanage()
        {
            return View();
        }
        public ActionResult GroupAdddevice()
        {
            return View();
        }
    }
}