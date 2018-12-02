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


        #region
        //依赖接口编程
        //IUserInfoDal UserInfoDal = new UserInfoDal();

        //private IUserInfoDal UserInfoDal = StaticDalFactory.GetUserInfoDal();
        //DbSession dbSession = new DbSession();
        //IDbSession dbSession = new DbSession();
        //private IDbSession dbSession = DbSessionFactory.GetCurrentDbSession();
        #endregion
        //public UserInfoService(IDbSession dbSession)
        //    :base(dbSession)
        //    {
        //    //this.DbSession = dbSession;

        //    }

        #region 由模版自动生成
        //public override void SetCurrentDal()
        //{
        //    CurrentDal = this.DbSession.UserInfoDal;
        //}
        #endregion
        #region
        //public UserInfo Add(UserInfo userInfo)
        //{

        //    dbSession.UserInfoDal.Add(userInfo);
        //    if (dbSession.SaveChanges() > 0)
        //    {


        //    }
        //    dbSession.UserInfoDal.Add(new UserInfo());

        //    dbSession.SaveChanges();
        //    //return UserInfoDal.Add(userInfo);


        //}
        #endregion

        #region 多条件查询
        public IQueryable<UserInfo> LoagPageData(UserQueryParam userQueryParam)
        {
            short normalFlag = (short)Glove.IOT.Model.Enum.DelFlagEnum.Normal;
            var temp = DbSession.UserInfoDal.GetEntities(u => u.DelFlag == normalFlag);

            //过滤
            if (!string.IsNullOrEmpty(userQueryParam.SchName))
            {
                temp = temp.Where(u => u.UName.Contains(userQueryParam.SchName)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(userQueryParam.SchRemark))
            {
                temp = temp.Where(u => u.Remark.Contains(userQueryParam.SchRemark)).AsQueryable();
            }
            userQueryParam.Total = temp.Count();

            //分页
            return temp.OrderBy(u => u.Id)
                .Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                .Take(userQueryParam.PageSize).AsQueryable();
        }
        #endregion
        //设置角色
        public bool SetRole(int userId, List<int> roleIds)
        {
            var user = DbSession.UserInfoDal.GetEntities(u => u.Id == userId).FirstOrDefault();
            user.RoleInfo.Clear();//全剁掉

            var allRoles = DbSession.RoleInfoDal.GetEntities(r => roleIds.Contains(r.Id));
            foreach (var roleInfo in allRoles)
            {
                user.RoleInfo.Add(roleInfo);//加最新的角色
            }
            DbSession.SaveChanges();
            return true;
        }

    }
}
