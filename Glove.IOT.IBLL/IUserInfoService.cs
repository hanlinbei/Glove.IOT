using Glove.IOT.Common;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="userQueryParam">查询条件</param>
        /// <returns>查询结果</returns>
        IQueryable<UserInfo>  LoagPageData(Model.Param.UserQueryParam userQueryParam);
        /// <summary>
        /// 内连接查询
        /// </summary>
        /// <param name="userQueryParam">查询条件</param>
        /// <returns>查询结果</returns>
        IQueryable<UserInfoRoleInfo> LoagUserPageData(Model.Param.UserQueryParam userQueryParam);
        /// <summary>
        /// 内连接查询
        /// </summary>
        /// <param name="userQueryParam">查询条件</param>
        /// <returns>查询结果</returns>
        UserInfoRoleInfo GetUserDetailInfo(string Uname);
        /// <summary>
        /// 用户注销
        /// </summary>
        void Logout();
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <param name="uId">登录用户ID</param>
        /// <returns></returns>
        string EditPwd(string oldpwd, string newPwd, int uId);

    }
}
