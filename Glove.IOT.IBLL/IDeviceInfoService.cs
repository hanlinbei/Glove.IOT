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
        /// <summary>
        /// 汇总每天的车间总产量
        /// </summary>
        /// <returns></returns>
        IQueryable<Device> GetSumOutput();
        /// <summary>
        /// 查询每台设备每天的产量
        /// </summary>
        /// <returns></returns>
        IQueryable<dynamic> GetEvdEvdSumOutput();
        /// <summary>
        /// 统计当前正在运行的设备总数
        /// </summary>
        /// <returns></returns>
        int GetRunningDeviceCount();
        /// <summary>
        /// 获取单个设备今日产量
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        int GetDeviceDayOutput(int deviceInfoId);
    }
}
