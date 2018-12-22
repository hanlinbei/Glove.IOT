using Glove.IOT.Common;
using Glove.IOT.DALFactory;
using Glove.IOT.EFDAL;
using Glove.IOT.IBLL;
using Glove.IOT.IDAL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 内连接查询
        /// </summary>
        /// <param name="userQueryParam">查询条件</param>
        /// <returns>查询结果</returns>
        public IQueryable<UserInfoRoleInfo> LoagUserPageData(UserQueryParam userQueryParam)
        {
            var userInfo = DbSession.UserInfoDal.GetEntities(u => u.IsDeleted==false);
            var r_UserInfo_RoleInfo = DbSession.R_UserInfo_RoleInfoDal.GetEntities(r => r.IsDeleted == false);
            var roleInfo = DbSession.RoleInfoDal.GetEntities(r => r.IsDeleted == false);

            //内连接查询 查询人员信息
            var query = from t1 in userInfo
                        join t2 in r_UserInfo_RoleInfo on t1.Id equals t2.UserInfoId
                        join t3 in roleInfo on t2.RoleInfoId equals t3.Id
                        select new UserInfoRoleInfo
                        {
                            UId = t1.Id,
                            RId = t3.Id,
                            UCode = t1.UCode,
                            UName = t1.UName,
                            RoleName = t3.RoleName,
                            StatusFlag = t1.StatusFlag
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

            return query.OrderBy(u=>u.UId)
                  .Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                  .Take(userQueryParam.PageSize).AsQueryable();

        }



        //设置角色
        public bool SetRole(int userId, List<int> roleIds)
        {
            //var user = DbSession.UserInfoDal.GetEntities(u => u.Id == userId).FirstOrDefault();
            //user.R_UserInfo_RoleInfo.Clear();//全剁掉
            //var allRoles = DbSession.R_UserInfo_RoleInfoDal.GetEntities(r => roleIds.Contains(r.Id));
            //foreach (var roleInfo in allRoles)
            //{
            //    user.R_UserInfo_RoleInfo.Add(roleInfo);//加最新的角色
            //}
            //DbSession.SaveChanges();
            return true;
        }

    }
}
