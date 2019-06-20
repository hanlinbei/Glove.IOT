 
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
	
		public partial interface IActionInfoService:IBaseService<ActionInfo>
        {
        }
	
		public partial interface ICmdInfoService:IBaseService<CmdInfo>
        {
        }
	
		public partial interface IDeviceInfoService:IBaseService<DeviceInfo>
        {
        }
	
		public partial interface IDeviceParameterInfoService:IBaseService<DeviceParameterInfo>
        {
        }
	
		public partial interface IGroupInfoService:IBaseService<GroupInfo>
        {
        }
	
		public partial interface IOperationLogService:IBaseService<OperationLog>
        {
        }
	
		public partial interface IR_GroupInfo_DeviceInfoService:IBaseService<R_GroupInfo_DeviceInfo>
        {
        }
	
		public partial interface IR_RoleInfo_ActionInfoService:IBaseService<R_RoleInfo_ActionInfo>
        {
        }
	
		public partial interface IR_UserInfo_RoleInfoService:IBaseService<R_UserInfo_RoleInfo>
        {
        }
	
		public partial interface IRoleInfoService:IBaseService<RoleInfo>
        {
        }
	
		public partial interface ITeamInfoService:IBaseService<TeamInfo>
        {
        }
	
		public partial interface IUserInfoService:IBaseService<UserInfo>
        {
        }
	
		public partial interface IWarningInfoService:IBaseService<WarningInfo>
        {
        }


}