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
            var user = UserInfoService.GetEntities(u => u.UName == LoginInfo.UName&&u.IsDeleted==false).FirstOrDefault();
            
            UserInfo userInfo = new UserInfo
            {
                SubTime = DateTime.Now
            };
            userInfo.Email = Request["Email"];
            userInfo.Gender = Request["Gender"];
            userInfo.Phone = Request["Pnumber"];
            userInfo.Remark = Request["Remark"];
            userInfo.UCode = Request["UCode"];
            var file = Request.Files["Picture"];
            
            //校验邮箱格式与可为空
            if (userInfo.Email.IsValidEmail()|| userInfo.Email.IsBlank())
            {
                var email = UserInfoService.GetEntities(u => u.Email == userInfo.Email&&u.IsDeleted==false&&u.UName!= LoginInfo.UName).FirstOrDefault();
                //之前不存在该邮箱则允许更改
                if (email == null)
                {
                    //校验手机号码与可为空
                    if (userInfo.Phone.IsValidMobile() || userInfo.Phone.IsBlank())
                    {
                        var phone= UserInfoService.GetEntities(u => u.Phone == userInfo.Phone && u.IsDeleted == false&&u.UName != LoginInfo.UName).FirstOrDefault();
                        //之前不存在该手机号 则允许添加
                        if (phone == null)
                        {
                            userInfo.Pwd = user.Pwd;
                            userInfo.UCode = user.UCode;
                            userInfo.UName = user.UName;
                            //如果文件为空 用回原来已存在的图片
                            if (file == null)
                            {
                                userInfo.Picture = user.Picture;
                            }
                            else
                            {
                                string fileName = file.FileName;
                                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1, fileName.Length - fileName.LastIndexOf("\\") - 1);
                                string path = "/UploadFiles/UploadImgs/" + Guid.NewGuid().ToString() + "-" + fileName;
                                file.SaveAs(Request.MapPath(path));
                                userInfo.Picture = path;
                            }
                            userInfo.Id = user.Id;
                            UserInfoService.Update(userInfo);
                            return Content("ok");
                        }
                        else
                        {
                            return Content("手机号码已存在");
                        }
                    }
                    else
                    {
                        return Content("手机号码格式不正确");
                    }
                }
                else
                {
                    return Content("邮箱已存在");
                }
              
               
            }
            else
            {
                return Content("邮箱格式不正确");
            }

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
                    SubTime = DateTime.Now
                };
                
                int insertedUserId = UserInfoService.Add(userInfo).Id;
                int roleId = userInfoRoleInfo.RId;
                //设置用户角色
                R_UserInfo_RoleInfoService.ProcessSetRole(insertedUserId, roleId);
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
        public ActionResult Edit(UserInfo userInfo)
        {
            userInfo.SubTime = DateTime.Now;
            userInfo.Pwd = UserInfoService.GetEntities(u => u.Id == userInfo.Id).Select(u => u.Pwd).FirstOrDefault();
            UserInfoService.Update(userInfo);
            //写操作日志
            OperationLogService.Add("编辑员工", "系统管理", LoginInfo, userInfo.UName, "");
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
                var uName = UserInfoService.GetEntities(u => u.Id == id).Select(u => u.UName).FirstOrDefault();
                //写操作日志
                OperationLogService.Add("删除员工", "系统管理", LoginInfo,uName, "");
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
        /// 获取登录用户头像
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserPicture()
        {
            var user = UserInfoService.GetEntities(u => u.Id == LoginInfo.Id).FirstOrDefault();
            var data = new { user.UCode, LoginInfo.UName, user.Picture};
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
    }
}