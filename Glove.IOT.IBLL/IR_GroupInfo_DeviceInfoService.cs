using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IR_GroupInfo_DeviceInfoService : IBaseService<R_GroupInfo_DeviceInfo>
    {
        /// <summary>
        /// 为组添加已选中的设备
        /// </summary>
        /// <param name="gId"></param>
        /// <param name="idList"></param>
        void AddSelectDevices(int gId, List<int> idList);
    }
}
