﻿ 
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
	
		 public IDeviceCmdDal DeviceCmdDal
		 {
			get { return StaticDalFactory.GetDeviceCmdDal(); }
		 }
	
		 public IDeviceGroupInfoDal DeviceGroupInfoDal
		 {
			get { return StaticDalFactory.GetDeviceGroupInfoDal(); }
		 }
	
		 public IDeviceHistoryDataDal DeviceHistoryDataDal
		 {
			get { return StaticDalFactory.GetDeviceHistoryDataDal(); }
		 }
	
		 public IDeviceHistoryWarningDal DeviceHistoryWarningDal
		 {
			get { return StaticDalFactory.GetDeviceHistoryWarningDal(); }
		 }
	
		 public IDeviceInfoDal DeviceInfoDal
		 {
			get { return StaticDalFactory.GetDeviceInfoDal(); }
		 }
	
		 public IDeviceRealtimeDataDal DeviceRealtimeDataDal
		 {
			get { return StaticDalFactory.GetDeviceRealtimeDataDal(); }
		 }
	
		 public IDeviceRealtimeWarningDal DeviceRealtimeWarningDal
		 {
			get { return StaticDalFactory.GetDeviceRealtimeWarningDal(); }
		 }
	
		 public IOperationLogDal OperationLogDal
		 {
			get { return StaticDalFactory.GetOperationLogDal(); }
		 }
	
		 public IR_DeviceInfo_DeviceGroupInfoDal R_DeviceInfo_DeviceGroupInfoDal
		 {
			get { return StaticDalFactory.GetR_DeviceInfo_DeviceGroupInfoDal(); }
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
	
		 public ITeamInfoDal TeamInfoDal
		 {
			get { return StaticDalFactory.GetTeamInfoDal(); }
		 }
	
		 public IUserInfoDal UserInfoDal
		 {
			get { return StaticDalFactory.GetUserInfoDal(); }
		 }

	}

}