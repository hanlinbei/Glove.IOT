using Glove.IOT.Common;
using Glove.IOT.Common.Extention;
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
        public IQueryable<dynamic> ApiGetSumOutput()
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
                        select new
                        {
                            Day=t1.Key,
                            SumOutput=t1.sumOutput
                        };

            return query; 
        }

        //public IQueryable<dynamic> GetEvdEvdSumOutput()
        //{
        //    var deviceInfo = DbSession.DeviceInfoDal.GetEntities(d => d.IsDeleted == false);
        //    var deviceParameterInfo = DbSession.DeviceInfoDal.GetEntities(d => true);
        //    //按天按设备分组查询
        //    var query=from t1 in deviceInfo
        //              from t2 in deviceParameterInfo.GroupBy


        //}



    }
}
