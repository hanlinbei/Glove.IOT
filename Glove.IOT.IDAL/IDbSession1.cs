 

namespace Glove.IOT.IDAL
{
    public partial interface IDbSession
    {
	 
		IActionInfoDal ActionInfoDal { get; }
	 
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal { get; }
	 
		IRoleInfoDal RoleInfoDal { get; }
	 
		IUserInfoDal UserInfoDal { get; }
	 
		IUserInfoExtDal UserInfoExtDal { get; }
	
	}


}