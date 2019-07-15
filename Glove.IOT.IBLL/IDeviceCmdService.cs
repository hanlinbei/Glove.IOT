using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IDeviceCmdService : IBaseService<DeviceCmd>
    {
        /// <summary>
        /// 加载设备命令表
        /// </summary>
        /// <param name="deviceCmdQueryParam"></param>
        /// <returns></returns>
        IQueryable<DeviceCmd> LoagPageData(DeviceCmdQueryParam deviceCmdQueryParam);
        /// <summary>
        /// 向控制指令表添加数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="deviceNames"></param>
        void AddDeviceCmd(string path, int[] deviceNames);
    }
}
