 

namespace Glove.IOT.IDAL
{
    public partial interface IDbSession
    {
	 
		IActionInfoDal ActionInfoDal { get; }
	 
		IDeviceCmdDal DeviceCmdDal { get; }
	 
		IDeviceGroupInfoDal DeviceGroupInfoDal { get; }
	 
		IDeviceHistoryDataDal DeviceHistoryDataDal { get; }
	 
		IDeviceHistoryWarningDal DeviceHistoryWarningDal { get; }
	 
		IDeviceInfoDal DeviceInfoDal { get; }
	 
		IDeviceRealtimeDataDal DeviceRealtimeDataDal { get; }
	 
		IDeviceRealtimeWarningDal DeviceRealtimeWarningDal { get; }
	 
		IOperationLogDal OperationLogDal { get; }
	 
		IR_DeviceInfo_DeviceGroupInfoDal R_DeviceInfo_DeviceGroupInfoDal { get; }
	 
		IR_RoleInfo_ActionInfoDal R_RoleInfo_ActionInfoDal { get; }
	 
		IR_UserInfo_RoleInfoDal R_UserInfo_RoleInfoDal { get; }
	 
		IRoleInfoDal RoleInfoDal { get; }
	 
		ITeamInfoDal TeamInfoDal { get; }
	 
		IUserInfoDal UserInfoDal { get; }
	
	}


}