using Glove.IOT.Common;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IDeviceParameterInfoService : IBaseService<DeviceParameterInfo>
    {
        /// <summary>
        /// 获取设备的实时数据
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>设备的具体参数</returns>
        dynamic GetDeviceParameter(string deviceId);
        /// <summary>
        /// 获取设备历史数据
        /// </summary>
        /// <param name="deviceId">设备Id</param>
        /// <returns>5条设备历史数据</returns>
        IQueryable<DeviceParameter> GetHistoryParameter(string deviceId);
        /// <summary>
        /// 获取设备的实时数据
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>设备的具体参数</returns>
        IQueryable<dynamic> ApiGetDeviceParameter();
    }
}
