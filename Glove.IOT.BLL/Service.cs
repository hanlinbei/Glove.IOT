
using Glove.IOT.DALFactory;
using Glove.IOT.EFDAL;
using Glove.IOT.IBLL;
using Glove.IOT.IDAL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
	
	    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
        {
		  // public override void SetCurrentDal()
		  // {
				//CurrentDal = this.DbSession.ActionInfoDal;
		  // }
	    
		}
	
	    public partial class R_UserInfo_ActionInfoService : BaseService<R_UserInfo_ActionInfo>, IR_UserInfo_ActionInfoService
        {
		  // public override void SetCurrentDal()
		  // {
				//CurrentDal = this.DbSession.R_UserInfo_ActionInfoDal;
		  // }
	    
		}
	
	    public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
        {
		  // public override void SetCurrentDal()
		  // {
				//CurrentDal = this.DbSession.RoleInfoDal;
		  // }
	    
		}

    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
       
    }

    public partial class UserInfoExtService : BaseService<UserInfoExt>, IUserInfoExtService
        {
		  // public override void SetCurrentDal()
		  // {
				//CurrentDal = this.DbSession.UserInfoExtDal;
		  // }
	    
		}



}