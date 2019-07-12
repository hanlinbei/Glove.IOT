using Glove.IOT.Common.Extention;
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
    public partial class DeviceRealtimeWarningService : BaseService<DeviceRealtimeWarning>, IDeviceRealtimeWarningService
    {
        /// <summary>
        /// 加载实时报警信息
        /// </summary>
        /// <param name="warningQueryParam"></param>
        /// <returns></returns>
        public IQueryable<DeviceRealtimeWarning> GetRealTimeWarningInfo(WarningQueryParam warningQueryParam)
        {
            var query = DbSession.DeviceRealtimeWarningDal.GetEntities(u => true).AsQueryable();
            //按设备名查找
            if (warningQueryParam.SchDeviceName != 0)
            {
                query = query.Where(u => u.DeviceName == warningQueryParam.SchDeviceName).AsQueryable();
            }
            //按报警信息查询
            if (!string.IsNullOrEmpty(warningQueryParam.SchMessage))
            {
                query = query.Where(w => w.WarningMessage.Contains(warningQueryParam.SchMessage)).AsQueryable();
            }
            //按报警时间查询
            if (!string.IsNullOrEmpty(warningQueryParam.SchStartTime))
            {
                var startTime = Convert.ToDateTime(warningQueryParam.SchStartTime);
                query = query.Where(w => w.StartTime >= startTime).AsQueryable();
            }
            warningQueryParam.Total = query.Count();
            return query.GetPageEntitiesAsc(warningQueryParam.PageSize, warningQueryParam.PageIndex, q => q.StartTime,false);

        }
    }
}
