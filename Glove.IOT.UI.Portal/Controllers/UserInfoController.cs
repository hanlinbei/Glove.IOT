using Glove.IOT.BLL;
using Glove.IOT.Common;
using Glove.IOT.Common.Md5;
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
        short delflagNormal = (short)Glove.IOT.Model.Enum.DelFlagEnum.Normal;
        // GET: UserInfo
        //UserInfoService UserInfoService = new UserInfoService();
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }
        public IMd5Helper Md5Helper { get; set; }
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
            //int total = 0;
            //过滤的用户名 过滤备注schName schRemark

           
            var queryParam = new UserQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0,
            };

            var pageData = UserInfoService.LoagPageData(queryParam);

            var temp= pageData.Select( u =>new
            {   u.Id,
                u.UName,
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
            //MD5加密
            userInfo.UCode = "2";
            userInfo.Remark = "3";
            userInfo.Pwd = Md5Helper.GetMd5(userInfo.Pwd);
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

        #region 设置角色
        public ActionResult SetRole(int id)
        {
            //当前要设置角色的用户
            int userId = id;
            UserInfo user = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            //把所有的角色发送到前台
            ViewBag.AllRoles = RoleInfoService.GetEntities(u => u.DelFlag == delflagNormal).ToList();
            //用户已经关联的角色发送到前台
            ViewBag.ExitsRoles = (from r in user.RoleInfo
                                  select r.Id).ToList();
            return View(user);

        }
        //给用户设置角色
        public ActionResult ProcessSetRole(int UId)
        {
            //第一：当前用户的id ----uid
            //第二：所有打上对勾的角色 ----list
            List<int> setRoleIdList = new List<int>();
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    setRoleIdList.Add(roleId);
                }
            }
            UserInfoService.SetRole(UId, setRoleIdList);
            return Content("Ok");

        }
        #endregion

        #region 设置特殊权限
        public ActionResult SetAction(int id)
        {
            ViewBag.User = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewData.Model = ActionInfoService.GetEntities(a => a.DelFlag == delflagNormal).ToList();
            return View();


        }
        //做一个删除特殊权限
        public ActionResult DeleteUserAction(int UId,int ActionId)
        {
            R_UserInfo_ActionInfo rUserAction = R_UserInfo_ActionInfoService.GetEntities(r => r.ActionInfoId == ActionId && r.UserInfoId == UId).FirstOrDefault();
            if (rUserAction != null)
            {
                //rUserAction.DelFlag = (short)Glove.IOT.Model.Enum.DelFlagEnum.Deleted;
                R_UserInfo_ActionInfoService.DeleteListByLogical(new List<int>() { rUserAction.Id });
            }
            return Content("Ok");
        }
        //设置当前用户的特殊权限
        public ActionResult SetUserAction(int UId, int ActionId, int Value)
        {
            R_UserInfo_ActionInfo rUserAction = R_UserInfo_ActionInfoService.GetEntities(r =>
            r.ActionInfoId == ActionId && r.UserInfoId == UId && r.DelFlag == delflagNormal).FirstOrDefault();
            if (rUserAction != null)
            {
                rUserAction.HasPermission = Value == 1 ? true : false;
                R_UserInfo_ActionInfoService.Update(rUserAction);
            }
            else
            {
                R_UserInfo_ActionInfo rUserInfoActionInfo = new R_UserInfo_ActionInfo();
                rUserInfoActionInfo.ActionInfoId = ActionId;
                rUserInfoActionInfo.UserInfoId = UId;
                rUserInfoActionInfo.HasPermission = Value == 1 ? true : false;
                rUserInfoActionInfo.DelFlag = delflagNormal;
                R_UserInfo_ActionInfoService.Add(rUserInfoActionInfo);
            }
            return Content("Ok");
        }

        #endregion
    }
}