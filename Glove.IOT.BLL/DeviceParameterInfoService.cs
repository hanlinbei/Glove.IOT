using Glove.IOT.Common;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Enum;
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
        /// <param name="deviceId">设备ID</param>
        /// <returns>设备的具体参数</returns>
        public dynamic GetDeviceParameter(string deviceId)
        {
           
            //外键的值
            var deviceInfoId = DbSession.DeviceInfoDal.GetEntities(d => d.IsDeleted == false && d.DeviceId == deviceId)
                              .Select(d => d.Id).FirstOrDefault();
            //最新上传的一条设备数据信息
            var deviceParameterInfo = DbSession.DeviceParameterInfoDal.GetEntities(d => d.DeviceInfoId==deviceInfoId)
                                       .OrderByDescending(d=>d.SubTime).FirstOrDefault();
            //该设备的最近一次的开机时间
            var latestStartTime = DbSession.DeviceParameterInfoDal.GetEntities(d => (d.DeviceInfoId == deviceInfoId && d.StartTime == d.SubTime))
                                  .OrderByDescending(d=>d.StartTime).Select(d=>d.StartTime).FirstOrDefault();
            //获取设备最近一次正常状态运行的提交时间
            var statusFlag = StatusFlagEnum.运行中.ToString();
            var latestNormalTime= DbSession.DeviceParameterInfoDal.GetEntities(d=>d.DeviceInfoId==deviceInfoId&&d.StatusFlag== statusFlag&&d.SubTime>= latestStartTime)
                                  .OrderByDescending(d => d.SubTime).Select(d => d.SubTime).FirstOrDefault();
            //设备正常运行时长
            var runTime = deviceParameterInfo.SubTime - latestStartTime;
            
          
            var query = new 
            {
                deviceId,
                deviceParameterInfo.StatusFlag,
                //RunTime= runTime.ToString(),
                RunTime = runTime,
                deviceParameterInfo.StopTime,
                deviceParameterInfo.TargetOutput,
                deviceParameterInfo.SingleProgress,
                deviceParameterInfo.NowOutput
            };
            return query;

        }
        /// <summary>
        /// 获取设备历史数据
        /// </summary>
        /// <param name="deviceId">设备Id</param>
        /// <returns>5条设备历史数据</returns>
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

        /// <summary>
        /// 获取设备的实时数据
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>设备的具体参数</returns>
        public IQueryable<dynamic> ApiGetDeviceParameter()
        {
            var statusFlag = StatusFlagEnum.运行中.ToString();
            var deviceParameterInfo = DbSession.DeviceParameterInfoDal.GetEntities(t => true);
            var deviceInfo = DbSession.DeviceInfoDal.GetEntities(d => d.IsDeleted == false);
            //查询每台机器的最新一条数据（分组查询）
            var query = from t1 in deviceParameterInfo 
                        join t3 in deviceInfo on t1.DeviceInfoId equals t3.Id
                        from t2 in deviceParameterInfo.GroupBy(m => m.DeviceInfoId).Select(p => new
                        {
                            newestSubTime = p.Max(q => q.SubTime),
                            latestStartTime = p.Max(q => q.StartTime),
                            deviceInfoId = p.Key
                        })
                        from t4 in deviceParameterInfo
                        .Where(d => d.StatusFlag == statusFlag)
                        .GroupBy(m => m.DeviceInfoId).Select(p => new
                        {
                            newestNormalSubTime = p.Max(q => q.SubTime),
                            deviceInfoId = p.Key
                        })
                        where t1.DeviceInfoId == t2.deviceInfoId && t1.SubTime == t2.newestSubTime&&t1.DeviceInfoId==t4.deviceInfoId
                        select new 
                        {
                            runtime=EntityFunctions.DiffMinutes(t2.latestStartTime,t4.newestNormalSubTime),
                            t3.DeviceId,
                            t1.StatusFlag,
                        };
            return query;
        }
    }
}
