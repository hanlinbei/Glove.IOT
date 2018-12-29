using Glove.IOT.Common;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using Microsoft.SqlServer.Server;
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
        /// <summary>
        /// 获取设备的实时数据
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public IQueryable<DeviceParameter> GetDeviceParameter(string deviceId)
        {
            var deviceParameterInfo = DbSession.DeviceParameterInfoDal.GetEntities(d => true);
            var deviceInfo = DbSession.DeviceInfoDal.GetEntities(d => d.IsDeleted==false);
            //内连接查询最新参数信息
            var query = from t1 in deviceParameterInfo
                        from t2 in deviceParameterInfo.GroupBy(m => m.DeviceInfoId).Select(p => new
                        {
                            newestTime = p.Max(q => q.SubTime),
                            deviceInfoId = p.Key
                        })
                        join t3 in deviceInfo on t1.DeviceInfoId equals t3.Id
                        where t1.DeviceInfoId==t2.deviceInfoId&&t1.SubTime==t2.newestTime&&t3.DeviceId==deviceId
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
        
         public IQueryable<DeviceParameter> GetHistoryParameter(string deviceId)
         {
            var deviceParameterInfo = DbSession.DeviceParameterInfoDal.GetEntities(d => true);
            var deviceInfoId = DbSession.DeviceInfoDal.GetEntities(d => (d.IsDeleted==false&&d.DeviceId==deviceId))
                .Select(u=>u.Id).FirstOrDefault();
            //内连接查询最新参数信息
            var query = from t1 in deviceParameterInfo
                       where t1.DeviceInfoId == deviceInfoId
                       select new DeviceParameter
                       {
                           NowOutput = t1.NowOutput,
                           SingleProgress = t1.SingleProgress,
                           StartTime = t1.StartTime,
                           StopTime = t1.StopTime,
                           SubTime = t1.SubTime
                       };

            return query.OrderByDescending(q => q.SubTime)
                   .Take(5);

        }
    }
}
