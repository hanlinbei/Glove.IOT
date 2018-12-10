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
            short delFlag = (short)Glove.IOT.Model.Enum.StatusFlagEnum.Deleted;

            var temp = DbSession.UserInfoDal.GetEntities(u => u.StatusFlag!=delFlag );

       
            //var date=from t1 in DbSession.UserInfoDal
        

            //过滤
            //if (!string.IsNullOrEmpty(userQueryParam.SchName))
            //{
            //    temp = temp.Where(u => u.UName.Contains(userQueryParam.SchName)).AsQueryable();
            //}

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
            short delFlag = (short)Glove.IOT.Model.Enum.StatusFlagEnum.Deleted;
            DataModelContainer model = new DataModelContainer();
            //内连接查询
            var query = from t1 in model.UserInfo
                        join t2 in model.R_UserInfo_RoleInfo on t1.Id equals t2.UserInfoId
                        join t3 in model.RoleInfo on t2.RoleInfoId equals t3.Id
                        where (t2.StatusFlag !=delFlag && t3.StatusFlag != delFlag)
                        select new UserInfoRoleInfo
                        {
                            UId = t1.Id,
                            RId = t3.Id,
                            UCode = t1.UCode,
                            UName = t1.UName,
                            RoleName = t3.RoleName,
                            StatusFlag=t1.StatusFlag
                        };
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
