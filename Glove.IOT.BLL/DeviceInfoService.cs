using Glove.IOT.Common;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    public partial class DeviceInfoService : BaseService<DeviceInfo>, IDeviceInfoService
    {
        public IQueryable<Device> LoagDevicePageData(DeviceQueryParam deviceQueryParam)
        {
            
            DataModelContainer model = new DataModelContainer();
            //内连接查询
            var query = from t1 in model.DeviceParameterInfo
                        join t2 in model.DeviceInfo on t1.DeviceInfoId equals t2.Id
                        select new Device
                        {
                            Id = t2.Id,
                            DeviceId=t2.DeviceId,
                            NowOutput=t1.NowOutput,
                            SingleProgress=t1.SingleProgress,
                            StartTime=t1.StartTime,
                            StatusFlag=t1.StatusFlag,
                            StopTime=t1.StopTime,
                            TargetOutput=t1.TargetOutput
                        };

            deviceQueryParam.Total = query.Count();

            return query.OrderBy(d => d.Id)
                  .Skip(deviceQueryParam.PageSize * (deviceQueryParam.PageIndex - 1))
                  .Take(deviceQueryParam.PageSize).AsQueryable();

        }
    }
}
