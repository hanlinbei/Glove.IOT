 
using Glove.IOT.IDAL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.EFDAL
{  
	
	public partial class ActionInfoDal:BaseDal<ActionInfo>,IActionInfoDal
    {

	}
	
	public partial class DeviceInfoDal:BaseDal<DeviceInfo>,IDeviceInfoDal
    {

	}
	
	public partial class DeviceParameterInfoDal:BaseDal<DeviceParameterInfo>,IDeviceParameterInfoDal
    {

	}
	
	public partial class OperationLogDal:BaseDal<OperationLog>,IOperationLogDal
    {

	}
	
	public partial class R_RoleInfo_ActionInfoDal:BaseDal<R_RoleInfo_ActionInfo>,IR_RoleInfo_ActionInfoDal
    {

	}
	
	public partial class R_UserInfo_RoleInfoDal:BaseDal<R_UserInfo_RoleInfo>,IR_UserInfo_RoleInfoDal
    {

	}
	
	public partial class RoleInfoDal:BaseDal<RoleInfo>,IRoleInfoDal
    {

	}
	
	public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {

	}
	
	public partial class WarningInfoDal:BaseDal<WarningInfo>,IWarningInfoDal
    {

	}


}