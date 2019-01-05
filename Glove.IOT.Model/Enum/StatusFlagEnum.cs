using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Model.Enum
{
    public enum StatusFlagEnum
    {
        /// <summary>
        /// 无效状态
        /// </summary>
        关机中 = 0,
        /// <summary>
        /// 有效状态
        /// </summary>
        运行中 = 1,
        /// <summary>
        /// 已经删除状态
        /// </summary>
        暂停中=2,
        /// <summary>
        /// 故障
        /// </summary>
        故障中=3,
        /// <summary>
        /// 设备离线状态
        /// </summary>
        未连接=4
            
    }
    
}
