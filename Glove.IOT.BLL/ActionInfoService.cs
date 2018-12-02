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
        //设置角色
        public bool SetRole(int userId, List<int> roleIds)
        {
            var actionInfo = DbSession.ActionInfoDal.GetEntities(u => u.Id == userId).FirstOrDefault();
            actionInfo.RoleInfo.Clear();//全剁掉

            var allRoles = DbSession.RoleInfoDal.GetEntities(r => roleIds.Contains(r.Id));
            foreach (var roleInfo in allRoles)
            {
                actionInfo.RoleInfo.Add(roleInfo);//加最新的角色
            }
            DbSession.SaveChanges();
            return true;
        }


    }
}
