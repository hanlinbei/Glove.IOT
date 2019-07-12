using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IDeviceRealtimeWarningService : IBaseService<DeviceRealtimeWarning>
    {
        /// <summary>
        /// 加载设备实时报警信息表
        /// </summary>
        /// <param name="warningQueryParam"></param>
        /// <returns></returns>
        IQueryable<DeviceRealtimeWarning> GetRealTimeWarningInfo(WarningQueryParam warningQueryParam);
    }
}
