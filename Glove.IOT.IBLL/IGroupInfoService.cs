using Glove.IOT.Common;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IGroupInfoService : IBaseService<GroupInfo>
    {
        /// <summary>
        /// 获取所有组
        /// </summary>
        /// <param name="groupQueryParam"></param>
        /// <returns></returns>
        IQueryable<dynamic> GetGroupInfo();
        /// <summary>
        /// 加载单个组对应的所有设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IQueryable<dynamic> GetGroupDevices(int id, BaseParam baseParam);
        /// <summary>
        /// 获取所以设备，并勾选已经存在组内的设备
        /// </summary>
        /// <param name="deviceQueryParam"></param>
        /// <returns></returns>
        IQueryable<Device> LoagDevicePageData(DeviceQueryParam deviceQueryParam,int gId);
    }
}
