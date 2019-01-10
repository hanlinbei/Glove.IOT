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
        void AddSelectDevices(int gId, List<int> idList);
    }
}
