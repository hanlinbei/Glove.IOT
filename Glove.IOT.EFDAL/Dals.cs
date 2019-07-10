 
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
	
	public partial class DeviceCmdDal:BaseDal<DeviceCmd>,IDeviceCmdDal
    {

	}
	
	public partial class DeviceHistoryDataDal:BaseDal<DeviceHistoryData>,IDeviceHistoryDataDal
    {

	}
	
	public partial class DeviceHistoryWarningDal:BaseDal<DeviceHistoryWarning>,IDeviceHistoryWarningDal
    {

	}
	
	public partial class DeviceInfoDal:BaseDal<DeviceInfo>,IDeviceInfoDal
    {

	}
	
	public partial class DeviceRealtimeDataDal:BaseDal<DeviceRealtimeData>,IDeviceRealtimeDataDal
    {

	}
	
	public partial class DeviceRealtimeWarningDal:BaseDal<DeviceRealtimeWarning>,IDeviceRealtimeWarningDal
    {

	}
	
	public partial class GroupInfoDal:BaseDal<GroupInfo>,IGroupInfoDal
    {

	}
	
	public partial class OperationLogDal:BaseDal<OperationLog>,IOperationLogDal
    {

	}
	
	public partial class R_GroupInfo_DeviceInfoDal:BaseDal<R_GroupInfo_DeviceInfo>,IR_GroupInfo_DeviceInfoDal
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
	
	public partial class TeamInfoDal:BaseDal<TeamInfo>,ITeamInfoDal
    {

	}
	
	public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {

	}


}