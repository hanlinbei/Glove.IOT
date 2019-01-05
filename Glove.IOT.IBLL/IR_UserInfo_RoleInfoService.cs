using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IR_UserInfo_RoleInfoService : IBaseService<R_UserInfo_RoleInfo>
    {
        /// <summary>
        /// 给用户设置角色
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="rId"></param>
        void ProcessSetRole(int uId, int rId);
    }
}
