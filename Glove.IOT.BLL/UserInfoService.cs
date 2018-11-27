using Glove.IOT.DALFactory;
using Glove.IOT.EFDAL;
using Glove.IOT.IBLL;
using Glove.IOT.IDAL;
using Glove.IOT.Model;
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

    }
}
