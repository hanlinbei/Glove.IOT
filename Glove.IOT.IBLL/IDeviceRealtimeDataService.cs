using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IDeviceRealtimeDataService : IBaseService<DeviceRealtimeData>
    {
        /// <summary>
        /// 加载设备实时表
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        IQueryable<DeviceRealtimeData> LoadDeviceRealtimePageData(DeviceRealtimeQueryParam queryParam);
       
    }
}
