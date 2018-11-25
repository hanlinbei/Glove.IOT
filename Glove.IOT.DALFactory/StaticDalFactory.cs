using Glove.IOT.EFDAL;
using Glove.IOT.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.DALFactory
{
    /// <summary>
    /// 由简单工厂转变成了抽象工厂
    /// </summary>
    public partial class StaticDalFactory
    {
        public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
        #region 由模版自动生成
        //public static IUserInfoDal GetUserInfoDal()
        //{
        //    // return new UserInfoDal();
            
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName+".UserInfoDal")
        //         as IUserInfoDal;
        //}
        #endregion
    }
}
