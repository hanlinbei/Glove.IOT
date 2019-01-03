using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    public partial class R_UserInfo_RoleInfoService : BaseService<R_UserInfo_RoleInfo>, IR_UserInfo_RoleInfoService
    {
        /// <summary>
        /// 给用户设置角色
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="rId"></param>
        public void ProcessSetRole(int uId, int rId)
        {
                //第一：当前用户的id ----uid
                //第二：当前用户在角色关联表中的ID
                UserInfo user = DbSession.UserInfoDal.GetEntities(u => u.Id == uId).FirstOrDefault();
                var allUserInfoIds = (from r in user.R_UserInfo_RoleInfo
                                      where r.UserInfoId == uId && r.IsDeleted == false
                                      select r.Id).ToList();

                for (int i = 0; i < allUserInfoIds.Count(); i++)
                {
                    int userInfoId = Convert.ToInt32(allUserInfoIds[i]);
                    var rUserRole = DbSession.R_UserInfo_RoleInfoDal.GetEntities(r =>
                    r.Id == userInfoId).FirstOrDefault();
                    DbSession.R_UserInfo_RoleInfoDal.Delete(rUserRole);
                }
                //第三：所有打上对勾的角色 ----list
                List<int> setRoleIdList = new List<int>
                {
                    rId
                };

                for (int i = 0; i < setRoleIdList.Count; i++)
                {
                    int roleId = Convert.ToInt32(setRoleIdList[i]);
                    R_UserInfo_RoleInfo rUserInfoRoleInfo = new R_UserInfo_RoleInfo
                    {
                        UserInfoId = uId,
                        RoleInfoId = roleId,
                        IsDeleted = false
                    };
                  DbSession.R_UserInfo_RoleInfoDal.Add(rUserInfoRoleInfo);

                }   

        }

    }
}
