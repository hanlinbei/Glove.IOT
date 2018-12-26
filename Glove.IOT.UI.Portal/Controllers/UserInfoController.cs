using Glove.IOT.BLL;
using Glove.IOT.Common;
using Glove.IOT.Common.Md5;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using Glove.IOT.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class UserInfoController : BaseController
    {
        // GET: UserInfo
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public IR_UserInfo_RoleInfoService R_UserInfo_RoleInfoService { get; set; }
        public IOperationLogService OperationLogService { get; set; }
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
        public ActionResult GetAllUserInfos(string limit,string page,string schCode,string schRoleName)
        {
            //jquery easyui:table:{total:32,row:[{},{}]}
            // easyui:table 在初始化的时候自动发送以下俩个参数值
            int pageSize = int.Parse(limit ?? "10");
            int pageIndex = int.Parse(page ?? "1");
            //过滤的用户名 过滤备注schName schRemark

            var queryParam = new UserQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                SchCode = schCode,
                SchRoleName = schRoleName,
                Total = 0,
            };
         
            var pageData = UserInfoService.LoagUserPageData(queryParam).ToList();
            var data = new { code=0,msg="",count = queryParam.Total, data = pageData.ToList() };
            if (!string.IsNullOrEmpty(schCode)||!string.IsNullOrEmpty(schRoleName))
            {
                //写操作日志
                OperationLog operationLog = new OperationLog
                {
                    ActionName = "查找员工",
                    ActionType = "系统管理",
                    Ip = LoginInfo.Ip,
                    Mac = LoginInfo.Mac,
                    SubTime = DateTime.Now,
                    UName = LoginInfo.UName
                };
                if (!string.IsNullOrEmpty(schCode))
                {
                    operationLog.OperationObj = schCode;
                }
                if (!string.IsNullOrEmpty(schRoleName))
                {
                    operationLog.OperationObj = schRoleName;
                }
                OperationLogService.Add(operationLog);

            }
     
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userInfo">前端用户信息</param>
        /// <returns>OK</returns>
        public ActionResult Add(UserInfoRoleInfo userInfoRoleInfo)
        {
            
            var uCode = UserInfoService.GetEntities(u => (u.UCode == userInfoRoleInfo.UCode&&u.IsDeleted==false)).FirstOrDefault();
            if (uCode == null)
            {
                UserInfo userInfo = new UserInfo
                {
                    //MD5加密
                    //userInfo.UCode = "2";
                    //userInfo.Remark = "3";
                    //userInfo.Pwd = Md5Helper.GetMd5(userInfo.Pwd);
                    UCode = userInfoRoleInfo.UCode,
                    UName = userInfoRoleInfo.UName,
                    Pwd = userInfoRoleInfo.Pwd,
                    StatusFlag = userInfoRoleInfo.StatusFlag,
                    Remark = userInfoRoleInfo.Remark,
                    SubTime = DateTime.Now
                };
                int insertedUserId = UserInfoService.Add(userInfo).Id;
                int roleId = userInfoRoleInfo.RId;
                ProcessSetRole(insertedUserId, roleId);
                //写操作日志
                OperationLog operationLog = new OperationLog
                {
                    ActionName = "添加员工",
                    ActionType = "系统管理",
                    Ip = LoginInfo.Ip,
                    Mac = LoginInfo.Mac,
                    OperationObj = userInfoRoleInfo.UName,
                    SubTime = DateTime.Now,
                    UName = LoginInfo.UName

                };
                OperationLogService.Add(operationLog);

                return Content("Ok");
            }
            else
            {
                return Content("fail");

            }

        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>ok</returns>
        public ActionResult Edit(UserInfo userInfo)
        {
            userInfo.SubTime = DateTime.Now;
            UserInfoService.Update(userInfo);
            //写操作日志
            OperationLog operationLog = new OperationLog
            {
                ActionName = "编辑员工",
                ActionType = "系统管理",
                Ip = LoginInfo.Ip,
                Mac = LoginInfo.Mac,
                OperationObj = userInfo.UName,
                SubTime = DateTime.Now,
                UName = LoginInfo.UName

            };
            OperationLogService.Add(operationLog);
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
            UserInfoService.DeleteListByLogical(idList);
            foreach (var id in idList)
            {
                //写操作日志
                OperationLog operationLog = new OperationLog
                {
                    ActionName = "删除员工",
                    ActionType = "系统管理",
                    Ip = LoginInfo.Ip,
                    Mac = LoginInfo.Mac,
                    OperationObj = UserInfoService.GetEntities(u=>u.Id==id).Select(u=>u.UName).FirstOrDefault(),
                    SubTime = DateTime.Now,
                    UName = LoginInfo.UName
                };
                OperationLogService.Add(operationLog);

            }

                return Content("del ok");

        }


        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>当前已存在的角色</returns>
        public ActionResult GetAllRoles()
        {
            //把所有的角色发送到前台
            var AllRoles = RoleInfoService.GetEntities(r=>r.IsDeleted ==false).OrderByDescending(r=>r.Id).ToList();
            var temp = AllRoles.Select(u => new {
                u.Id,
                u.RoleName
            });
            var data = temp.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 给用户设置角色
        /// </summary>
        /// <param name="UId">用户Id</param>
        /// <returns>OK</returns>
         void ProcessSetRole(int uId,int rId)
         {
            //第一：当前用户的id ----uid
            //第二：当前用户在角色关联表中的ID
            UserInfo user = UserInfoService.GetEntities(u => u.Id == uId).FirstOrDefault();
            var allUserInfoIds = (from r in user.R_UserInfo_RoleInfo
                                  where r.UserInfoId == uId && r.IsDeleted ==false
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
                 setRoleIdList.Add(rId);

            for (int i = 0; i < setRoleIdList.Count; i++)
            {
                int roleId = Convert.ToInt32(setRoleIdList[i]);
                R_UserInfo_RoleInfo rUserInfoRoleInfo = new R_UserInfo_RoleInfo
                {
                    UserInfoId = uId,
                    RoleInfoId = roleId,
                    IsDeleted = false
                };
                R_UserInfo_RoleInfoService.Add(rUserInfoRoleInfo);
              
            }

         }


        public ActionResult t()
        {
            return View();
        }
        public ActionResult indexUI()
        {
            return View();
        }
        public ActionResult Usermanage()
        {
            return View();
        }
        public ActionResult LayerEdituser()
        {
            return View();
        }
        public ActionResult LayerAdduser()
        {
            return View();
        }
        public ActionResult LayerSearchuser()
        {
            return View();
        }
     
    }
}