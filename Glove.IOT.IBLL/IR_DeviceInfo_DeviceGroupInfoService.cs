using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IR_DeviceInfo_DeviceGroupInfoService : IBaseService<R_DeviceInfo_DeviceGroupInfo>
    {
        /// <summary>
        /// 为组添加已选中的设备
        /// </summary>
        /// <param name="gId"></param>
        /// <param name="idList"></param>
        void AddSelectDevices(string gId, List<string> idList);
    }
}
