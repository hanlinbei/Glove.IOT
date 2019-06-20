 

namespace Glove.IOT.IDAL
{
    public partial interface IDbSession
    {
	 
		IActionInfoDal ActionInfoDal { get; }
	 
		ICmdInfoDal CmdInfoDal { get; }
	 
		IDeviceInfoDal DeviceInfoDal { get; }
	 
		IDeviceParameterInfoDal DeviceParameterInfoDal { get; }
	 
		IGroupInfoDal GroupInfoDal { get; }
	 
		IOperationLogDal OperationLogDal { get; }
	 
		IR_GroupInfo_DeviceInfoDal R_GroupInfo_DeviceInfoDal { get; }
	 
		IR_RoleInfo_ActionInfoDal R_RoleInfo_ActionInfoDal { get; }
	 
		IR_UserInfo_RoleInfoDal R_UserInfo_RoleInfoDal { get; }
	 
		IRoleInfoDal RoleInfoDal { get; }
	 
		ITeamInfoDal TeamInfoDal { get; }
	 
		IUserInfoDal UserInfoDal { get; }
	 
		IWarningInfoDal WarningInfoDal { get; }
	
	}


}