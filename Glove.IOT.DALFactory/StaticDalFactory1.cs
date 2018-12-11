 
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
	
		public static IActionInfoDal GetActionInfoDal()
        {                
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".ActionInfoDal")
                 as IActionInfoDal;
        }
	
		public static IR_UserInfo_ActionInfoDal GetR_UserInfo_ActionInfoDal()
        {                
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".R_UserInfo_ActionInfoDal")
                 as IR_UserInfo_ActionInfoDal;
        }
	
		public static IR_UserInfo_RoleInfoDal GetR_UserInfo_RoleInfoDal()
        {                
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".R_UserInfo_RoleInfoDal")
                 as IR_UserInfo_RoleInfoDal;
        }
	
		public static IRoleInfoDal GetRoleInfoDal()
        {                
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".RoleInfoDal")
                 as IRoleInfoDal;
        }
	
		public static IUserInfoDal GetUserInfoDal()
        {                
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".UserInfoDal")
                 as IUserInfoDal;
        }

	}

}