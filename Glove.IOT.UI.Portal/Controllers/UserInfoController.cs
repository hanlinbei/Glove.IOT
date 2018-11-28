using Glove.IOT.BLL;
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

            //过滤的用户名 过滤备注schName schRemark
            string schName = Request["schName"];
            string schRemark = Request["schRemark"];
            short delflagNormal = (short)Glove.IOT.Model.Enum.DelFlagEnum.Normal;
            var queryParam = new UserQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0,
                SchName = schName,
                SchRemark = schRemark
            };

            var pageData = UserInfoService.LoagPageData(queryParam);

            var temp= pageData.Select( u =>new
            {   u.Id,
                u.UName,
                u.Remark,
                u.ShowName,
                u.SubTime,
                u.ModfiedOn,
                u.Pwd
            });
            //拿到当前页的数据
            //var pageData = UserInfoService.GetPageEntities(pageSize, pageIndex,
            //                                             out total, u => u.DelFlag == delflagNormal, u => u.Id,
            //                                             true)
            //                                            .Select(
            //                                             u =>
            //                                             new { u.Id, u.UName, u.Remark, u.ShowName, u.SubTime, u.ModfiedOn, u.Pwd }
            //                                           );
            var data = new { total = queryParam.Total, rows = temp.ToList() };

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

        #region 修改
        public ActionResult Edit(int id)
        {
            ViewData.Model = UserInfoService.GetEntities(u => u.Id ==id).FirstOrDefault();
            return View();

        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            UserInfoService.Update(userInfo);
            return Content("ok");

        }
        #endregion
        #region 删除
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
            //UserInfoService.DeleteList(idList);
            UserInfoService.DeleteListByLogical(idList);
            return Content("ok");




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