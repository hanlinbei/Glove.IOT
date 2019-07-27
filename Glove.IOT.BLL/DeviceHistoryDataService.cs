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
    public partial class DeviceHistoryDataService : BaseService<DeviceHistoryData>, IDeviceHistoryDataService
    {
        /// <summary>
        /// 获取单个设备近一周每天产量
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns></returns>
        public IQueryable<DeviceDayOutput> GetWeekEachDayData(int deviceName)
        {
            var deviceHistoryDatas = DbSession.DeviceHistoryDataDal.GetEntities(u => true);
            var query = from t1 in deviceHistoryDatas.GroupBy(u => EntityFunctions.TruncateTime(u.CreateTime))
                         .Select(p => new
                         {
                             DayTime = p.Key,
                             DayOutput = p.Sum(q => q.NowOutput)
                         })
                        select new DeviceDayOutput
                        {
                            Date = t1.DayTime,
                            SumOutput = t1.DayOutput
                        };

            return query.OrderBy(u=>u.Date).Take(7);

        }

        /// <summary>
        /// 获取车间近一周每天产量
        /// </summary>
        /// <returns></returns>
        public IQueryable<DeviceDayOutput> GetWeekEachDayData()
        {           
            var deviceHistoryDatas = DbSession.DeviceHistoryDataDal.GetEntities(u =>true);
            var query = from t1 in deviceHistoryDatas.GroupBy(u => EntityFunctions.TruncateTime(u.CreateTime))
                        .Select(p => new
                        {
                            DayTime = p.Key,
                            DaOutput = p.Sum(q => q.NowOutput)
                        })
                        select new DeviceDayOutput
                        {
                            Date = t1.DayTime,
                            SumOutput = t1.DaOutput
                        };
            return query.OrderBy(u=>u.Date).Take(7);
        }

        /// <summary>
        /// 获取每台设备今日产量
        /// </summary>
        /// <returns></returns>
        public IQueryable<dynamic> GetTodayOutput(DeviceRealtimeQueryParam queryParam )
        {
            var today = DateTime.Now.Date;
            var deviceHistoryDatas = DbSession.DeviceHistoryDataDal.GetEntities(u => EntityFunctions.TruncateTime(u.CreateTime)==today);
            var query = from t1 in deviceHistoryDatas.GroupBy(u => u.DeviceName)
                         .Select(p => new
                         {
                             DeviceName=p.Key,
                             DayOutput = p.Sum(q => q.NowOutput)
                         })
                        select new 
                        {
                            DeviceName = t1.DeviceName,
                            SumOutput = t1.DayOutput
                        };

            //按设备名查找
            if (queryParam.SchDeviceName != 0)
            {
                query = query.Where(u => u.DeviceName == queryParam.SchDeviceName).AsQueryable();
            }

            queryParam.Total = query.Count();

            return query.GetPageEntitiesAsc(queryParam.PageSize, queryParam.PageIndex, q => q.DeviceName, true);

        }
    }
}
