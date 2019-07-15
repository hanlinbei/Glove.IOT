using Glove.IOT.Common.Extention;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Enum;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    public partial class DeviceCmdService : BaseService<DeviceCmd>, IDeviceCmdService
    {
        /// <summary>
        /// 加载设备命令表
        /// </summary>
        /// <param name="deviceCmdQueryParam"></param>
        /// <returns></returns>
        public IQueryable<DeviceCmd> LoagPageData(DeviceCmdQueryParam deviceCmdQueryParam)
        {
            var query = DbSession.DeviceCmdDal.GetEntities(u => true);
            deviceCmdQueryParam.Total = query.Count();
            return query.GetPageEntitiesAsc(deviceCmdQueryParam.PageSize, deviceCmdQueryParam.PageIndex, q => q.CreateTime, false);
        }

        /// <summary>
        /// 向控制指令表添加数据
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        public void AddDeviceCmd(string path, int[]deviceNames)
        {
            foreach (int deviceName in deviceNames)
            {
                DbSession.DeviceCmdDal.Add(new DeviceCmd
                {
                    Id = Guid.NewGuid().ToString(),
                    DeviceName = deviceName,
                    CmdCode = "1",
                    CmdData = path,
                    CmdState = DevieceCmdStateEnum.待操作.ToString(),
                    CreateTime = DateTime.Now
                });
            }
            DbSession.SaveChanges();
        }
    }
}
