 
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
	
		public static IDeviceInfoDal GetDeviceInfoDal()
        {                
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".DeviceInfoDal")
                 as IDeviceInfoDal;
        }
	
		public static IDeviceParameterInfoDal GetDeviceParameterInfoDal()
        {                
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".DeviceParameterInfoDal")
                 as IDeviceParameterInfoDal;
        }
	
		public static IR_RoleInfo_ActionInfoDal GetR_RoleInfo_ActionInfoDal()
        {                
            return Assembly.Load(assemblyName).CreateInstance(assemblyName+".R_RoleInfo_ActionInfoDal")
                 as IR_RoleInfo_ActionInfoDal;
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