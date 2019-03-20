using Glove.IOT.BLL;
using Glove.IOT.Common;
using Glove.IOT.Common.Extention;
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
    [LoginCheckFilter]
    public class UserInfoController : BaseController
    {
        // GET: UserInfo
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public IR_UserInfo_RoleInfoService R_UserInfo_RoleInfoService { get; set; }
        public IOperationLogService OperationLogService { get; set; }
        public ITeamInfoService TeamInfoService { get; set; }
        public IGroupInfoService GroupInfoService { get; set; }
        /// <summary>
        /// 用户起始视图
        /// </summary>
        /// <returns>用户视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <returns>Json数据</returns>
        public ActionResult GetAllUserInfos(string limit, string page, string schCode, string schRoleName)
        {
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
            var data = new { code = 0, msg = "", count = queryParam.Total, data = pageData.ToList() };
            if (!string.IsNullOrEmpty(schCode) || !string.IsNullOrEmpty(schRoleName))
            {
                //写操作日志
                OperationLogService.Add("查找员工", "系统管理", LoginInfo, schCode, schRoleName);
            }

            return Json(data, JsonRequestBehavior.AllowGet);

        }
       /// <summary>
       /// 获取用户详细信息
       /// </summary>
       /// <returns></returns>
        public ActionResult GetUserDetail()
        {
            var data = UserInfoService.GetUserDetailInfo(LoginInfo.UName);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑用户详细信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult EditUserDetail( )
        {
            var user = UserInfoService.GetEntitiesNoTracking(u => u.UName == LoginInfo.UName && u.IsDeleted == false).FirstOrDefault();
            UserInfo userInfo = new UserInfo
            {
                SubTime = DateTime.Now
            };
            userInfo.Email = Request["Email"];
            userInfo.Gender = Request["Gender"];
            userInfo.Phone = Request["Phone"];
            userInfo.Remark = Request["Remark"];
            userInfo.UCode = Request["UCode"];
            userInfo.GroupInfoId = user.GroupInfoId;
            userInfo.TeamInfoId = user.TeamInfoId;
            var file = Request.Files["Picture"];
            //如果头像为空 用回原来已存在的
            if (file == null)
            {
                userInfo.Picture = user.Picture;
            }
            //如果是新头像，保存到本地
            else
            {
                string fileName = file.FileName;
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1, fileName.Length - fileName.LastIndexOf("\\") - 1);
                string path = "/UploadFiles/UploadImgs/" + Guid.NewGuid().ToString() + "-" + fileName;
                file.SaveAs(Request.MapPath(path));
                userInfo.Picture = path;
            }
            string msg = UserInfoService.EditUserDetail(userInfo, user);
            return Content(msg);
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
                    UCode = userInfoRoleInfo.UCode,
                    UName = userInfoRoleInfo.UName,
                    Pwd = userInfoRoleInfo.Pwd.ToMD5(), //MD5加密
                    StatusFlag = userInfoRoleInfo.StatusFlag,
                    Remark = userInfoRoleInfo.Remark,
                    TeamInfoId=userInfoRoleInfo.TId,
                    GroupInfoId=userInfoRoleInfo.GId,
                    SubTime = DateTime.Now
                };
                
                int insertedUserId = UserInfoService.Add(userInfo).Id;
                int roleId = userInfoRoleInfo.RId;

                R_UserInfo_RoleInfo rUserInfoRoleInfo = new R_UserInfo_RoleInfo
                {
                    UserInfoId = insertedUserId,
                    RoleInfoId = roleId,
                    IsDeleted = false
                };
                //设置用户角色
                R_UserInfo_RoleInfoService.Add(rUserInfoRoleInfo);
                //写操作日志
                OperationLogService.Add("添加员工", "系统管理", LoginInfo, userInfoRoleInfo.UName,"");
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
        public ActionResult Edit(UserInfoRoleInfo userInfo)
        {
            UserInfoService.Update(t => t.Id == userInfo.UId, t => new UserInfo
            {
                UName = userInfo.UName,
                UCode=userInfo.UCode,
                Remark=userInfo.Remark,
                StatusFlag=userInfo.StatusFlag,
                TeamInfoId=userInfo.TId,
                GroupInfoId=userInfo.GId,
                SubTime=DateTime.Now
            });
            //设置更新用户角色
            R_UserInfo_RoleInfoService.Update(r=>r.UserInfoId==userInfo.UId, r=>new R_UserInfo_RoleInfo
            {
                RoleInfoId=userInfo.RId
            });
            //写操作日志
            OperationLogService.Add("编辑员工", "系统管理", LoginInfo, userInfo.UName, "");
            return Content("ok");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>del ok</returns>
        public ActionResult Delete(int[] ids)
        {
            List<int> idList = ids.ToList();
            if (idList==null)
            {
                return Content("请选中要删除的数据！");
            }
            UserInfoService.DeleteListByLogical(idList);
            foreach (var id in idList)
            {
                var uName = UserInfoService.GetEntities(u => u.Id == id).Select(u => u.UName).FirstOrDefault();
                //写操作日志
                OperationLogService.Add("删除员工", "系统管理", LoginInfo,uName, "");
            }
                return Content("del ok");
        }

        /// <summary>
        /// 获取所有角色
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
        /// 获取所有班
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllTeams()
        {
            //把所有的组发送到前台
            var AllTeams = TeamInfoService.GetEntities(t => t.IsDeleted == false).OrderBy(t => t.Id);
            var temp = AllTeams.Select(t => new
            {
                t.Id,
                t.TName
            });
            return Json(temp.ToList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有组
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllGroups()
        {
            //把所有的班发送到前台
            var AllGroups = GroupInfoService.GetEntities(g => g.IsDeleted == false).OrderBy(g => g.Id);
            var temp = AllGroups.Select(g => new
            {
                g.Id,
                g.GName
            });
            return Json(temp.ToList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取登录用户头像
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserPicture()
        {
            var user = UserInfoService.GetEntities(u => u.Id == LoginInfo.Id).FirstOrDefault();
            var data = new { user.UCode, LoginInfo.UName, user.Picture};
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public ActionResult EditPwd(string oldPwd, string newPwd)
        {
            var msg=UserInfoService.EditPwd(oldPwd, newPwd, LoginInfo.Id);
            return Content(msg);
        }
        /// <summary>
        /// 网络测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            var enterTime = DateTime.Now.ToString();
            var user = UserInfoService.GetEntities(u => u.Id == LoginInfo.Id).FirstOrDefault();
            var outTime = DateTime.Now.ToString();
            var data = new { enterTime, user.UName,outTime };
            return Json(data, JsonRequestBehavior.AllowGet);

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
        public ActionResult Userdetail()
        {
            return View();
        }
        public ActionResult Userpassword()
        {
            return View();
        }
    }
}