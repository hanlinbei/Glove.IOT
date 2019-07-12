using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Enum;
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
        public IDeviceRealtimeDataService DeviceRealtimeDataService { get; set; }
        public IDeviceRealtimeWarningService DeviceRealtimeWarningService { get; set; }
        public IDeviceHistoryDataService DeviceHistoryDataService { get; set; }

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
            var runningStatus = StatusFlagEnum.运行中.ToString();
            //当前设备运行总数
            var runingDeviceCount = DeviceRealtimeDataService.GetEntities(u => u.StatusFlag == runningStatus).Count();
            //获取设备总数
            var allDeviceCount = DeviceInfoService.GetEntities(u => true).Count();
            //当前上班的总人数
            var onworkUserCount = UserInfoService.GetNowOnWorkUserCount();
            //员工总人数
            var allUserCount = UserInfoService.GetEntities(u => u.StatusFlag == true).Count();
            //近一周车间每天总产量
            var weekEachdayOutput = DeviceHistoryDataService.GetWeekEachDayData(); 
            //当期报警设备数
            var warningNum = DeviceRealtimeWarningService.GetEntities(u => true).Count();

            var data = new { RuningDeviceCount = runingDeviceCount, AllDeviceCount = allDeviceCount, OnworkUserCount = onworkUserCount,
                AllUseCountr = allUserCount, WeekEachDayOutput= weekEachdayOutput, WarningNum = warningNum };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Home()
        {
            return View();
        }

    }
}