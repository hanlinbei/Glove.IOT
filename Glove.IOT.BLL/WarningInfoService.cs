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
    public partial class WarningInfoService : BaseService<WarningInfo>, IWarningInfoService
    {
        /// <summary>
        /// /获取报警信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<dynamic> GetWarningInfo(WarningQueryParam warningQueryParam)
        {
            var firstTime = Convert.ToDateTime(warningQueryParam.FirstTime);
            var lastTime = Convert.ToDateTime(warningQueryParam.LastTime);
            //获取报警信息表实体
            var warningInfo = DbSession.WarningInfoDal.GetEntities(w => w.IsDeleted == false);

            var query = from t1 in warningInfo
                        from t2 in warningInfo.GroupBy(w => w.DeviceId).Select(p => new
                        {
                            NewestSubTime = p.Max(q => q.SubTime),
                            WarningStartTime = p.Min(q => q.SubTime),
                            deviceId = p.Key
                        })
                        where t1.DeviceId == t2.deviceId && t1.SubTime == t2.NewestSubTime
                        select new
                        {
                            t1.DeviceId,
                            t1.WarningMessage,
                            t2.WarningStartTime,
                            minute=EntityFunctions.DiffMinutes(t2.WarningStartTime, t2.NewestSubTime),
                        };
            //按设备ID编号筛选
            if (!string.IsNullOrEmpty(warningQueryParam.SchDeviceId))
            {
                query = query.Where(w => w.DeviceId.Contains(warningQueryParam.SchDeviceId)).AsQueryable();
            }
            //按报警信息筛选
            if (!string.IsNullOrEmpty(warningQueryParam.SchMessage))
            {
                query = query.Where(w => w.WarningMessage.Contains(warningQueryParam.SchMessage)).AsQueryable();
            }
            //按报警时间查询
            if (!string.IsNullOrEmpty(warningQueryParam.FirstTime)&& !string.IsNullOrEmpty(warningQueryParam.LastTime))
            {
                query = query.Where(w=>(w.WarningStartTime>firstTime&&w.WarningStartTime<lastTime)).AsQueryable();
            }
            //总条数
            warningQueryParam.Total = query.Count();
            return query;

        }
    }
}
