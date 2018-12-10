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
        readonly short delFlag = (short)Glove.IOT.Model.Enum.StatusFlagEnum.Deleted;
        // GET: UserInfo
        //UserInfoService UserInfoService = new UserInfoService();
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }
        public IR_UserInfo_RoleInfoService R_UserInfo_RoleInfoService { get; set; }
        public IMd5Helper Md5Helper { get; set; }
        /// <summary>
        /// 用户起始视图
        /// </summary>
        /// <returns>用户视图</returns>
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// 获取用户所有信息
        /// </summary>
        /// <returns>Json数据</returns>
        public ActionResult GetAllUserInfos()
        {
            //jquery easyui:table:{total:32,row:[{},{}]}
            // easyui:table 在初始化的时候自动发送以下俩个参数值
            int pageSize = int.Parse(Request["limit"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            //过滤的用户名 过滤备注schName schRemark

            var queryParam = new UserQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0,
            };
         
            var pageData = UserInfoService.LoagUserPageData(queryParam).ToList();
            var data = new { code=0,msg="",count = queryParam.Total, data = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userInfo">前端用户信息</param>
        /// <returns>OK</returns>
        public ActionResult Add(UserInfo userInfo)
        {
            //MD5加密
            //userInfo.UCode = "2";
            //userInfo.Remark = "3";
            //userInfo.Pwd = Md5Helper.GetMd5(userInfo.Pwd);
            userInfo.SubTime = DateTime.Now;
            //userInfo.StatusFlag = (short)Glove.IOT.Model.Enum.DelFlagEnum.Normal;
            int insertedUserId = UserInfoService.Add(userInfo).Id;
            ProcessSetRole(insertedUserId);
            return Content("Ok");


        }


   
        /*public ActionResult Edit(int id)
        {
            ViewData.Model = UserInfoService.GetEntities(u => u.Id ==id).FirstOrDefault();
            return View();

        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            UserInfoService.Update(userInfo);
            return Content("ok");

        }*/

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>ok</returns>
        public ActionResult Edit(UserInfo userInfo)
        {
            userInfo.SubTime = DateTime.Now;
            UserInfoService.Update(userInfo);
            return Content("ok");
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
            //UserInfoService.DeleteList(idList);
            UserInfoService.DeleteListByLogical(idList);
            return Content("del ok");




        }


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

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>当前已存在的角色</returns>
        public ActionResult SetRole()
        {
            //当前要设置角色的用户
            //int userId = id;
            //UserInfo user = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            //把所有的角色发送到前台
            var AllRoles = RoleInfoService.GetEntities(u => u.StatusFlag != delFlag).ToList();
            var temp = AllRoles.Select(u =>
              new {
              u.Id,
              u.RoleName
              }
            );
            var data = temp.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
            //用户已经关联的角色发送到前台
            /*ViewBag.ExitsRoles = (from r in user.R_UserInfo_RoleInfo
                                  where r.StatusFlag==delFlag
                                  select r.RoleInfoId).ToList();
            return View(user);*/

        }

        /// <summary>
        /// 给用户设置角色
        /// </summary>
        /// <param name="UId">用户Id</param>
        /// <returns>OK</returns>
         void ProcessSetRole(int UId)
         {
            //第一：当前用户的id ----uid
            //第二：当前用户在角色关联表中的ID
            UserInfo user = UserInfoService.GetEntities(u => u.Id == UId).FirstOrDefault();
            var allUserInfoIds = (from r in user.R_UserInfo_RoleInfo
                                  where r.UserInfoId == UId && r.StatusFlag == delFlag
                                  select r.Id).ToList();

            for (int i = 0; i < allUserInfoIds.Count(); i++)
            {
                int userInfoId = Convert.ToInt32(allUserInfoIds[i]);
                var rUserRole = R_UserInfo_RoleInfoService.GetEntities(r =>
                r.Id== userInfoId).FirstOrDefault();
                R_UserInfo_RoleInfoService.Delete(rUserRole);
             }

            //第三：所有打上对勾的角色 ----list
            List<int> setRoleIdList = new List<int>();
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    setRoleIdList.Add(roleId);
                }
            }


            for (int i = 0; i < setRoleIdList.Count; i++)
            {
                int roleId = Convert.ToInt32(setRoleIdList[i]);
               
                R_UserInfo_RoleInfo rUserInfoRoleInfo = new R_UserInfo_RoleInfo();
                rUserInfoRoleInfo.UserInfoId = UId;
                rUserInfoRoleInfo.RoleInfoId = roleId;
                rUserInfoRoleInfo.StatusFlag = delFlag;
                R_UserInfo_RoleInfoService.Add(rUserInfoRoleInfo);
              
            }

        }


        #region 设置特殊权限
        public ActionResult SetAction(int id)
        {
            ViewBag.User = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewData.Model = ActionInfoService.GetEntities(a => a.StatusFlag == delFlag).ToList();
            return View();


        }
        //做一个删除特殊权限
        public ActionResult DeleteUserAction(int UId,int ActionId)
        {
            R_UserInfo_ActionInfo rUserAction = R_UserInfo_ActionInfoService.GetEntities(r => r.ActionInfoId == ActionId && r.UserInfoId == UId).FirstOrDefault();
            if (rUserAction != null)
            {
                //rUserAction.StatusFlag = (short)Glove.IOT.Model.Enum.DelFlagEnum.Deleted;
                R_UserInfo_ActionInfoService.DeleteListByLogical(new List<int>() { rUserAction.Id });
            }
            return Content("Ok");
        }
        //设置当前用户的特殊权限
        public ActionResult SetUserAction(int UId, int ActionId, int Value)
        {
            R_UserInfo_ActionInfo rUserAction = R_UserInfo_ActionInfoService.GetEntities(r =>
            r.ActionInfoId == ActionId && r.UserInfoId == UId && r.StatusFlag == delFlag).FirstOrDefault();
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
                rUserInfoActionInfo.StatusFlag = delFlag;
                R_UserInfo_ActionInfoService.Add(rUserInfoActionInfo);
            }
            return Content("Ok");
        }

        #endregion





        public ActionResult t()
        {
            return View();
        }
        public ActionResult indexUI()
        {
            return View();
        }
        public ActionResult Personnel_management()
        {
            return View();
        }
        public ActionResult tck_ry_bj()
        {
            return View();
        }
        public ActionResult tck_ry_tj()
        {
            return View();
        }
    }
}