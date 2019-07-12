using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IDeviceHistoryWarningService : IBaseService<DeviceHistoryWarning>
    {
        /// <summary>
        /// 加载设备历史报警信息表
        /// </summary>
        /// <param name="warningQueryParam"></param>
        /// <returns></returns>
        IQueryable<DeviceHistoryWarning> GetHistoryWarningInfo(WarningQueryParam warningQueryParam);
    }
}
