using Glove.IOT.Common;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IDeviceInfoService:IBaseService<DeviceInfo>
    {
        /// <summary>
        /// 查询设备信息
        /// </summary>
        /// <param name="deviceQueryParam"></param>
        /// <returns>分页设备数据</returns>
        IQueryable<Device> LoagDevicePageData(Model.Param.DeviceQueryParam deviceQueryParam);
        IQueryable<dynamic> ApiGetSumOutput();
    }
}
