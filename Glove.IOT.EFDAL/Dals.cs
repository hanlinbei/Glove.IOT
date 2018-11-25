 
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
	
	public partial class R_UserInfo_ActionInfoDal:BaseDal<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoDal
    {

	}
	
	public partial class RoleInfoDal:BaseDal<RoleInfo>,IRoleInfoDal
    {

	}
	
	public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {

	}
	
	public partial class UserInfoExtDal:BaseDal<UserInfoExt>,IUserInfoExtDal
    {

	}


}