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

    }
}
