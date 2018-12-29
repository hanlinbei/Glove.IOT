 
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
	
		 public IActionInfoDal ActionInfoDal
		 {
			get { return StaticDalFactory.GetActionInfoDal(); }
		 }
	
		 public IDeviceInfoDal DeviceInfoDal
		 {
			get { return StaticDalFactory.GetDeviceInfoDal(); }
		 }
	
		 public IDeviceParameterInfoDal DeviceParameterInfoDal
		 {
			get { return StaticDalFactory.GetDeviceParameterInfoDal(); }
		 }
	
		 public IOperationLogDal OperationLogDal
		 {
			get { return StaticDalFactory.GetOperationLogDal(); }
		 }
	
		 public IR_RoleInfo_ActionInfoDal R_RoleInfo_ActionInfoDal
		 {
			get { return StaticDalFactory.GetR_RoleInfo_ActionInfoDal(); }
		 }
	
		 public IR_UserInfo_RoleInfoDal R_UserInfo_RoleInfoDal
		 {
			get { return StaticDalFactory.GetR_UserInfo_RoleInfoDal(); }
		 }
	
		 public IRoleInfoDal RoleInfoDal
		 {
			get { return StaticDalFactory.GetRoleInfoDal(); }
		 }
	
		 public IUserInfoDal UserInfoDal
		 {
			get { return StaticDalFactory.GetUserInfoDal(); }
		 }
	
		 public IWarningInfoDal WarningInfoDal
		 {
			get { return StaticDalFactory.GetWarningInfoDal(); }
		 }

	}

}