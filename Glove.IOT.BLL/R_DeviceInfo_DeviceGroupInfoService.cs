using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    public partial class R_DeviceInfo_DeviceGroupInfoService : BaseService<R_DeviceInfo_DeviceGroupInfo>, IR_DeviceInfo_DeviceGroupInfoService
    {
        /// <summary>
        /// 为组添加选中的设备
        /// </summary>
        /// <param name="gId"></param>
        /// <param name="idList"></param>
        public void AddSelectDevices(string gId, List<string> idList)
        {
            for (int i = 0; i < idList.Count; i++)
            {
                string deviceInfoId = idList[i];
                R_DeviceInfo_DeviceGroupInfo r_DeviceInfo_DeviceGroupInfo = new R_DeviceInfo_DeviceGroupInfo
                {
                    Id=Guid.NewGuid().ToString(),
                    DeviceGroupInfoId = gId,
                    DeviceInfoId = deviceInfoId,
                };
                DbSession.R_DeviceInfo_DeviceGroupInfoDal.Add(r_DeviceInfo_DeviceGroupInfo);
            }
            DbSession.SaveChanges();

        }
    }
}
