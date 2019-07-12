using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Model.Enum
{
    /// <summary>
    /// 机器状态枚举
    /// </summary>
    public enum StatusFlagEnum
    {
        /// <summary>
        /// 关机状态
        /// </summary>
        关机中 = 5,
        /// <summary>
        /// 运行状态
        /// </summary>
        运行中 = 1,
        /// <summary>
        /// 暂停状态
        /// </summary>
        暂停中 = 0,
        /// <summary>
        /// 故障状态
        /// </summary>
        故障中 = 2,
        /// <summary>
        /// 离线状态
        /// </summary>
        离线中 = 3,
        /// <summary>
        /// 设备注册后显示上线状态
        /// </summary>
        已上线 = 4

    }

    /// <summary>
    /// 报警信息枚举
    /// </summary>
    public enum WarningMessageEnum
    {
        /// <summary>
        /// 线段
        /// </summary>
        线断 = 1,
        /// <summary>
        /// 伺服过载
        /// </summary>
        伺服过载 = 2

    }

    /// <summary>
    /// 设备命令状态
    /// </summary>
    public enum DevieceCmdStateEnum
    {
        /// <summary>
        /// 待更新
        /// </summary>
        待操作 = 1,
        /// <summary>
        /// 更新完成
        /// </summary>
        操作完成 = 2
    }

}
