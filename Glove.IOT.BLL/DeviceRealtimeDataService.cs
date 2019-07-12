using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Enum;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    public partial class DeviceRealtimeDataService : BaseService<DeviceRealtimeData>, IDeviceRealtimeDataService
    {
        /// <summary>
        /// 加载设备实时数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IQueryable<DeviceRealtimeData> LoadDeviceRealtimePageData(DeviceRealtimeQueryParam deviceRealtimeQueryParam)
        {
            //所有设备实时数据
            var query = DbSession.DeviceRealtimeDataDal.GetEntities(u=>true);
            //按设备名查找
            if (deviceRealtimeQueryParam.SchDeviceName != 0)
            {
                query = query.Where(u => u.DeviceName == deviceRealtimeQueryParam.SchDeviceName).AsQueryable();
            }
            //按设备状态筛选
            if (!string.IsNullOrEmpty(deviceRealtimeQueryParam.SchStatusFlag))
            {
                query = query.Where(u => u.StatusFlag == deviceRealtimeQueryParam.SchStatusFlag).AsQueryable();
            }
            deviceRealtimeQueryParam.Total = query.Count();

            return query.OrderBy(d => d.DeviceName)
                .Skip(deviceRealtimeQueryParam.PageSize * (deviceRealtimeQueryParam.PageIndex - 1))
                .Take(deviceRealtimeQueryParam.PageSize).AsQueryable();
        }

    
    }
}
