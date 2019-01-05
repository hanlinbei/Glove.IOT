using Glove.IOT.EFDAL;
using Glove.IOT.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.DALFactory
{
    public partial class DbSession:IDbSession
    {
        #region 简单工厂或者抽象工厂部分 用模版自动生成
        //public IUserInfoDal UserInfoDal
        //{
        //    get { return StaticDalFactory.GetUserInfoDal(); }
        //}
        #endregion
        /// <summary>
        /// 拿到当前的上下文，把修改的实体做一个整体的提交
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();                                

        }

    }
}
