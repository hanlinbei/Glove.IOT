
using Glove.IOT.Common;
using Glove.IOT.Common.Extention;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    public partial class DeviceGroupInfoService : BaseService<DeviceGroupInfo>, IDeviceGroupInfoService
    {
        /// <summary>
        /// 获取所有组
        /// </summary>
        /// <param name="groupQueryParam"></param>
        /// <returns></returns>
        public IQueryable<dynamic> GetGroupInfo()
        {     
            //获取班信息表实体
            var query = DbSession.DeviceGroupInfoDal.GetEntities(g => g.IsDeleted == false)
                .Select(g=>new
                {
                    g.DeviceGroupName,
                    g.Id
                });
            return query;

        }
        /// <summary>
        /// 加载单个组对应的所有设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<dynamic> GetGroupDevices(string id,BaseParam baseParam)
        {
            var r_GroupInfo_DeviceInfoDal = DbSession.R_DeviceInfo_DeviceGroupInfoDal.GetEntities(g=>g.DeviceGroupInfoId == id);
            var deviceInfo = DbSession.DeviceInfoDal.GetEntities(u=>true);
            var query = from t1 in r_GroupInfo_DeviceInfoDal
                        join t2 in deviceInfo on t1.DeviceInfoId equals t2.Id
                        select (new
                        {
                            t2.Id,
                            t2.DeviceName
                        });
            baseParam.Total = query.Count();

            return query.GetPageEntitiesAsc(baseParam.PageSize, baseParam.PageIndex, q => q.Id, true);
        }

        /// <summary>
        /// 获取所有设备，并勾选已经存在组内的设备
        /// </summary>
        /// <param name="queryParam"></param>
        /// <param name="gId"></param>
        /// <returns></returns>
        public IQueryable<Device> LoagDevicePageData(DeviceRealtimeQueryParam queryParam, string gId)
        {
            //组内已存在的设备ID
            var temp = DbSession.R_DeviceInfo_DeviceGroupInfoDal.GetEntities(g =>g.DeviceGroupInfoId == gId).Select(g => g.DeviceInfoId);
            var deviceInfoDal = DbSession.DeviceInfoDal.GetEntities(u => true);
            var query = from t1 in deviceInfoDal
                        select new Device
                        {
                            Id = t1.Id,
                            DeviceName = t1.DeviceName,
                            LAY_CHECKED = temp.Contains(t1.Id)
                        };
            //按设备名查找
            if (queryParam.SchDeviceName != 0)
            {
                query = query.Where(u => u.DeviceName == queryParam.SchDeviceName).AsQueryable();
            }

            queryParam.Total = query.Count();

            return query.GetPageEntitiesAsc(queryParam.PageSize, queryParam.PageIndex, q => q.DeviceName, true);
        }




    }
}
