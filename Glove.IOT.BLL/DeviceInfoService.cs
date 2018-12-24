using Glove.IOT.Common;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    public partial class DeviceInfoService : BaseService<DeviceInfo>, IDeviceInfoService
    {
        public IQueryable<Device> LoagDevicePageData(DeviceQueryParam deviceQueryParam)
        {
           
            var deviceParameterInfo = DbSession.DeviceParameterInfoDal.GetEntities(t => true);
            var deviceInfo = DbSession.DeviceInfoDal.GetEntities(d => d.IsDeleted == false);
            //查询每台机器的最新一条数据（分组查询）
            var query = from t1 in deviceParameterInfo
                        from t2 in deviceParameterInfo.GroupBy(m => m.DeviceInfoId).Select(p => new
                        {
                            newestTime = p.Max(q => q.SubTime),
                            deviceInfoId = p.Key
                        })
                        join t3 in deviceInfo on t1.DeviceInfoId equals t3.Id
                        where t1.DeviceInfoId==t2.deviceInfoId&&t1.SubTime==t2.newestTime
                        select new Device
                        {
                            Id = t3.Id,
                            DeviceId = t3.DeviceId,                     
                            StatusFlag = t1.StatusFlag,
                        };
            //按设备ID查找
            if (!string.IsNullOrEmpty(deviceQueryParam.SchDeviceId))
            {
                query = query.Where(u => u.DeviceId.Contains(deviceQueryParam.SchDeviceId)).AsQueryable();
            }
            //按设备状态筛选
            if (!string.IsNullOrEmpty(deviceQueryParam.SchStatusFlag))
            {
                query = query.Where(u => u.StatusFlag.Contains(deviceQueryParam.SchStatusFlag)).AsQueryable();
            }

            deviceQueryParam.Total = query.Count();

            return query.OrderBy(d =>d.DeviceId)
                  .Skip(deviceQueryParam.PageSize * (deviceQueryParam.PageIndex - 1))
                  .Take(deviceQueryParam.PageSize).AsQueryable();

        }
    }
}
