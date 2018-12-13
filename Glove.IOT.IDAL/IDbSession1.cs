 

namespace Glove.IOT.IDAL
{
    public partial interface IDbSession
    {
	 
		IActionInfoDal ActionInfoDal { get; }
	 
		IDeviceInfoDal DeviceInfoDal { get; }
	 
		IDeviceParameterInfoDal DeviceParameterInfoDal { get; }
	 
		IR_RoleInfo_ActionInfoDal R_RoleInfo_ActionInfoDal { get; }
	 
		IR_UserInfo_RoleInfoDal R_UserInfo_RoleInfoDal { get; }
	 
		IRoleInfoDal RoleInfoDal { get; }
	 
		IUserInfoDal UserInfoDal { get; }
	
	}


}