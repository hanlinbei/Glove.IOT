using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IR_RoleInfo_ActionInfoService : IBaseService<R_RoleInfo_ActionInfo>
    {
        /// <summary>
        /// 添加选中的权限
        /// </summary>
        /// <param name="rId"></param>
        /// <param name="idList"></param>
        void AddSelectActions(int rId, List<int> idList);
    }
}
