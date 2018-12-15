using Glove.IOT.Common;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IDeviceParameterInfoService : IBaseService<DeviceParameterInfo>
    {
        IQueryable<DeviceParameter> GetDeviceParameter(int deviceId);
    }
}
