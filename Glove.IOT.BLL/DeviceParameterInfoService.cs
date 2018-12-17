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
    public partial class DeviceParameterInfoService : BaseService<DeviceParameterInfo>, IDeviceParameterInfoService
    {
        public IQueryable<DeviceParameter> GetDeviceParameter(int deviceId)
        {
            DataModelContainer model = new DataModelContainer();
         //内连接查询最新参数信息
            var query = from t1 in model.DeviceParameterInfo
                        from t2 in model.DeviceParameterInfo.GroupBy(m => m.DeviceInfoId).Select(p => new
                        {
                            newestTime = p.Max(q => q.SubTime),
                            deviceInfoId = p.Key
                        })
                        join t3 in model.DeviceInfo on t1.DeviceInfoId equals t3.Id
                        where t1.DeviceInfoId==t2.deviceInfoId&&t1.SubTime==t2.newestTime&&t3.DeviceId==deviceId&&t3.StatusFlag!=2
                        select new DeviceParameter
                        {
                            DeviceId=t3.DeviceId,
                            StatusFlag=t1.StatusFlag,
                            StartTime=t1.StartTime,
                            StopTime=t1.StopTime,
                            TargetOutput=t1.TargetOutput,
                            SingleProgress=t1.SingleProgress,
                            NowOutput=t1.NowOutput
                        };

            return query;

        }
    }
}
