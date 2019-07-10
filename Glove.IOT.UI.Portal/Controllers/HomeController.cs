using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    //[AllowAnonymous]
    public class HomeController : BaseController
    {
        public IDeviceInfoService DeviceInfoService { get; set; }
        public IUserInfoService UserInfoService { get; set; }

        // GET: Home

        /// <summary>
        /// 主页起始视图
        /// </summary>
        /// <returns>主页视图</returns>
        public ActionResult Index(string UserName, string Ticket)
        {
            ViewBag.UserName = UserName;
            ViewBag.Ticket = Ticket;
            return View();

        }

        public ActionResult TreeIndex()
        {
            return View();
        }

        /// <summary>
        /// 获取设备运行数，上班人数，今日产量，报警数
        /// </summary>
        /// <returns>主页视图</returns>
        public ActionResult GetSummation()
        {
            //var date = DateTime.Now.Date;
            ////当前设备运行总数
            //var device = DeviceInfoService.GetRunningDeviceCount();
            ////当前上班的总人数
            //var user=UserInfoService.GetNowOnWorkUserCount();
            ////今日车间总产量
            //var todayOutput = DeviceInfoService.GetSumOutput().Where(d=>d.Date==date).Select(d=>d.SumOutput).FirstOrDefault();
            ////当期报警设备数
            //var warningNum = WarningInfoService.GetWarningNum();

            //var data = new { Device = device, User = user, TodayOutput = todayOutput, WarningNum = warningNum };
            return Json(0, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Home()
        {
            return View();
        }

    }
}