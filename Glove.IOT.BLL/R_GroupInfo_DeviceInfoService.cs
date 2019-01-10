using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    public partial class R_GroupInfo_DeviceInfoService : BaseService<R_GroupInfo_DeviceInfo>, IR_GroupInfo_DeviceInfoService
    {
        /// <summary>
        /// 为组添加选中的设备
        /// </summary>
        /// <param name="gId"></param>
        /// <param name="idList"></param>
        public void AddSelectDevices(int gId, List<int> idList)
        {
            for (int i = 0; i < idList.Count; i++)
            {
                int deviceInfoId = Convert.ToInt32(idList[i]);
                R_GroupInfo_DeviceInfo r_GroupInfo_DeviceInfo = new R_GroupInfo_DeviceInfo
                {
                    GroupInfoId = gId,
                    DeviceInfoId = deviceInfoId,
                    IsDeleted = false
                };
                DbSession.R_GroupInfo_DeviceInfoDal.Add(r_GroupInfo_DeviceInfo);
            }
            DbSession.SaveChanges();

        }
    }
}
