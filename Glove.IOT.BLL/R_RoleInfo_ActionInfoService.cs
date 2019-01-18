using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    public partial class R_RoleInfo_ActionInfoService : BaseService<R_RoleInfo_ActionInfo>, IR_RoleInfo_ActionInfoService
    {
        /// <summary>
        /// 添加选中的权限
        /// </summary>
        /// <param name="rId"></param>
        /// <param name="idList"></param>
        public void AddSelectActions(int rId, List<int> idList)
        {
            for (int i = 0; i < idList.Count; i++)
            {
                int actionId = Convert.ToInt32(idList[i]);
                R_RoleInfo_ActionInfo rRoleInfoActionInfo = new R_RoleInfo_ActionInfo
                {
                    RoleInfoId = rId,
                    ActionInfoId = actionId,
                    IsDeleted = false
                };
                DbSession.R_RoleInfo_ActionInfoDal.Add(rRoleInfoActionInfo);
            }
            DbSession.SaveChanges();

        }
    }
}
