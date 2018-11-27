using Glove.IOT.BLL;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        //UserInfoService UserInfoService = new UserInfoService();
        public IUserInfoService UserInfoService { get; set; }
        #region 获取用户
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllUserInfos()
        {
            //jquery easyui:table:{total:32,row:[{},{}]}
            // easyui:table 在初始化的时候自动发送以下俩个参数值
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;
            short delflagNormal = (short)Glove.IOT.Model.Enum.DelFlagEnum.Normal;
            //拿到当前页的数据
            var pageData = UserInfoService.GetPageEntities(pageSize, pageIndex,
                                                         out total, u => u.DelFlag == delflagNormal, u => u.Id,
                                                         true)
                                                        .Select(
                                                         u =>
                                                         new { u.Id, u.UName, u.Remark, u.ShowName, u.SubTime, u.ModfiedOn, u.Pwd }
                                                       );
           var data = new { total = total, rows = pageData.ToList() };

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 添加用户
        public ActionResult Add(UserInfo userInfo)
        {
            userInfo.ModfiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            userInfo.DelFlag = (short)Glove.IOT.Model.Enum.DelFlagEnum.Normal;

            UserInfoService.Add(userInfo);
            return Content("Ok");


        }
        #endregion


        #region create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                UserInfoService.Add(userInfo);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}