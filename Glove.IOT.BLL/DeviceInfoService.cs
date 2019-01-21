using Glove.IOT.Common;
using Glove.IOT.Common.Extention;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Enum;
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
        /// <summary>
        /// 查询设备信息
        /// </summary>
        /// <param name="deviceQueryParam"></param>
        /// <returns>分页设备数据</returns>
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
                        where t1.DeviceInfoId == t2.deviceInfoId && t1.SubTime == t2.newestTime
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

            return query.GetPageEntitiesAsc(deviceQueryParam.PageSize, deviceQueryParam.PageIndex,q=>q.DeviceId,true);

        }

        /// <summary>
        /// 汇总每天的车间总产量
        /// </summary>
        /// <returns></returns>
        public IQueryable<Device> GetSumOutput()
        {
            var deviceParameterInfo = DbSession.DeviceParameterInfoDal.GetEntities(t => true);
            //按天分组查询
            var query = from t1 in deviceParameterInfo
                        .GroupBy(s => EntityFunctions.TruncateTime(s.SubTime))
                        .Select(s => new
                        {
                            s.Key,
                            sumOutput = s.Sum(p => p.NowOutput)
                        })
                        select new  Device
                        {
                            Date=t1.Key.Value,
                            SumOutput=t1.sumOutput.Value
                        };

            return query; 
        }

 
        /// <summary>
        /// 查询每台设备每天的产量
        /// </summary>
        /// <returns></returns>
        public IQueryable<dynamic> GetEvdEvdSumOutput()
        {
            var deviceInfo = DbSession.DeviceInfoDal.GetEntities(d => d.IsDeleted == false);
            var deviceParameterInfo = DbSession.DeviceParameterInfoDal.GetEntities(d => true);
            //按天按设备分组查询
            var query = from t1 in deviceParameterInfo.GroupBy(q => new { Date=EntityFunctions.TruncateTime(q.SubTime), q.DeviceInfoId })
                        .Select(s => new
                        {
                            Day = s.Key.Date,
                            EvdEvdSumOutput = s.Sum(p => p.NowOutput),
                            DeviceId = s.Key.DeviceInfoId
                        })
                        join t2 in deviceInfo on t1.DeviceId equals t2.Id
                        select new
                        {
                            t2.DeviceId,
                            t1.Day,
                            t1.EvdEvdSumOutput
                        };
            return query;
        }


        /// <summary>
        /// 统计当前正在运行的设备总数
        /// </summary>
        /// <returns></returns>
        public int GetRunningDeviceCount()
        {
            var statusFlag = StatusFlagEnum.运行中.ToString();
            var deviceParameterInfo = DbSession.DeviceParameterInfoDal.GetEntities(d=>true);
            var query = from t1 in deviceParameterInfo
                        from t2 in deviceParameterInfo.GroupBy(d => d.DeviceInfoId)
                        .Select(s => new
                        {
                            s.Key,
                            newestTime = s.Max(p => p.SubTime)
                        })
                        where t1.SubTime == t2.newestTime && t1.StatusFlag == statusFlag
                        select t1.DeviceInfoId;

            return query.Count();
                        
        }

    }
}
