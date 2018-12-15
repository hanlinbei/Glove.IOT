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
        UNormal = 0,
        /// <summary>
        /// 有效状态
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 已经删除状态
        /// </summary>
        Deleted=2,
        /// <summary>
        /// 设备离线状态
        /// </summary>
        Outline=4
            
    }
    
}
