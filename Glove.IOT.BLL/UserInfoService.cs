using EntityFramework.Extensions;
using Glove.IOT.Common;
using Glove.IOT.Common.Extention;
using Glove.IOT.DALFactory;
using Glove.IOT.EFDAL;
using Glove.IOT.IBLL;
using Glove.IOT.IDAL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Glove.IOT.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="userQueryParam">查询条件</param>
        /// <returns>查询结果</returns>
        public IQueryable<UserInfo> LoagPageData(UserQueryParam userQueryParam)
        {

            var temp = DbSession.UserInfoDal.GetEntities(u => u.IsDeleted==false);

            userQueryParam.Total = temp.Count();

            //分页
            return temp.OrderBy(u => u.Id)
                .Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                .Take(userQueryParam.PageSize).AsQueryable();
        }

        /// <summary>
        /// 查询用户详细信息
        /// </summary>
        /// <param name="uName"></param>
        /// <returns></returns>
        public UserInfoRoleInfo GetUserDetailInfo(string uName)
        {

            var temp = DbSession.UserInfoDal.GetEntities((u => u.IsDeleted == false&&u.UName==uName)).FirstOrDefault();
            var roleId = DbSession.R_UserInfo_RoleInfoDal.GetEntities(r => (r.UserInfoId == temp.Id && r.IsDeleted == false)).FirstOrDefault();
            var roleName = DbSession.RoleInfoDal.GetEntities(r => (r.Id == roleId.RoleInfoId && r.IsDeleted == false)).FirstOrDefault();
            var data = new UserInfoRoleInfo
            {
                Email=temp.Email,
                Gender= temp.Gender,
                Phone= temp.Phone,
                Picture=temp.Picture,
                Remark= temp.Remark,
                UCode= temp.UCode,
                UName= temp.UName,
                RoleName= roleName.RoleName,
            };
            return data;

        }

        /// <summary>
        /// 内连接查询
        /// </summary>
        /// <param name="userQueryParam">查询条件</param>
        /// <returns>查询结果</returns>
        public IQueryable<dynamic> LoagUserPageData(UserQueryParam userQueryParam)
        {
            var userInfo = DbSession.UserInfoDal.GetEntities(u => u.IsDeleted == false);
            var r_UserInfo_RoleInfo = DbSession.R_UserInfo_RoleInfoDal.GetEntities(r => r.IsDeleted == false);
            var roleInfo = DbSession.RoleInfoDal.GetEntities(r => r.IsDeleted == false);
            var teamInfo = DbSession.TeamInfoDal.GetEntities(t => t.IsDeleted == false);
            var groupInfo = DbSession.GroupInfoDal.GetEntities(g => g.IsDeleted == false);
            //内连接查询 查询人员信息
            var query = from t1 in userInfo
                        join t2 in r_UserInfo_RoleInfo on t1.Id equals t2.UserInfoId
                        join t3 in roleInfo on t2.RoleInfoId equals t3.Id
                        join t4 in teamInfo on t1.TeamInfoId equals t4.Id
                        join t5 in groupInfo on t1.GroupInfoId equals t5.Id
                        select new
                        {
                            UId = t1.Id,
                            RId = t3.Id,
                            t1.UCode,
                            t1.UName,
                            t3.RoleName,
                            t1.Remark,
                            t1.StatusFlag,
                            TId = t4.Id,
                            GId = t5.Id,
                            t4.TName,
                            t5.GName
                        };

           //按员工编号筛选
            if (!string.IsNullOrEmpty(userQueryParam.SchCode))
            {
                query = query.Where(u => u.UCode.Contains(userQueryParam.SchCode)).AsQueryable();
            }
            //按角色名筛选
            if (!string.IsNullOrEmpty(userQueryParam.SchRoleName))
            {
                query = query.Where(u => u.RoleName.Contains(userQueryParam.SchRoleName)).AsQueryable();
            }

            userQueryParam.Total = query.Count();

            return query.GetPageEntitiesAsc(userQueryParam.PageSize, userQueryParam.PageIndex, u => u.UId, true);

        }
        /// <summary>
        /// 用户注销
        /// </summary>
        public void Logout()
        {
            FormsAuthentication.SignOut();
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <param name="uId">登录用户ID</param>
        /// <returns></returns>
        public string EditPwd(string oldpwd, string newPwd,int uId)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            IUserInfoService userInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;
            var user = DbSession.UserInfoDal.GetEntities(u => u.Id == uId).FirstOrDefault();
            if (user.Pwd != oldpwd.ToMD5())
            {
                return "false";
            }
            else
            {
                user.Pwd = newPwd.ToMD5();
                userInfoService.Update(user);
                return "true";
            }

        }

        /// <summary>
        /// 编辑个人资料
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="user">登录用户信息</param>
        /// <returns></returns>
        public string EditUserDetail(UserInfo userInfo, UserInfo user)
        {
            //校验邮箱格式与可为空
            if (userInfo.Email.IsValidEmail() || userInfo.Email.IsBlank())
            {
                var email = DbSession.UserInfoDal.GetEntities(u => u.Email == userInfo.Email && u.IsDeleted == false && u.Id != user.Id).FirstOrDefault();
                //之前不存在该邮箱则允许更改
                if (email == null)
                {
                    //校验手机号码与可为空
                    if (userInfo.Phone.IsValidMobile() || userInfo.Phone.IsBlank())
                    {
                        var phone = DbSession.UserInfoDal.GetEntities(u => u.Phone == userInfo.Phone && u.IsDeleted == false && u.Id != user.Id).FirstOrDefault();
                        //之前不存在该手机号 则允许添加
                        if (phone == null)
                        {
                            userInfo.Pwd = user.Pwd;
                            userInfo.UCode = user.UCode;
                            userInfo.UName = user.UName;                
                            userInfo.Id = user.Id;
                            DbSession.UserInfoDal.Update(userInfo);
                            DbSession.SaveChanges();
                            return "ok";
                        }
                        else
                        {
                            return "手机号码已存在";
                        }
                    }
                    else
                    {
                        return "手机号码格式不正确";
                    }
                }
                else
                {
                    return "邮箱已存在";
                }


            }
            else
            {
                return "邮箱格式不正确";
            }


        }
        /// <summary>
        /// 获取现在上班的员工数
        /// </summary>
        /// <returns></returns>
        public int GetNowOnWorkUserCount()
        {
            var nowTime = DateTime.Now.Hour;
            //在当前时间点内的班的ID
            List<int> teamInfoIds = new List<int>();
            var teamInfoId1 = DbSession.TeamInfoDal.GetEntities(t => (t.StartTime.Value.Hours <= nowTime 
                                && t.StopTime.Value.Hours > nowTime
                                &&t.StopTime.Value.Hours>=t.StartTime.Value.Hours&&t.IsDeleted==false))
                                .Select(t=>t.Id).ToList();
            var teamInfoId2 = DbSession.TeamInfoDal.GetEntities(t => t.StopTime.Value.Hours <= t.StartTime.Value.Hours 
                                && (nowTime >= t.StartTime.Value.Hours || nowTime < t.StopTime.Value.Hours)&&t.IsDeleted==false)
                                .Select(t => t.Id).ToList();
            teamInfoIds.AddRange(teamInfoId1);
            teamInfoIds.AddRange(teamInfoId2);

            var query = DbSession.UserInfoDal.GetEntities(u => u.IsDeleted == false && u.StatusFlag == true && teamInfoIds.Contains(u.TeamInfoId));
            return query.Count();
        }
    }
}
