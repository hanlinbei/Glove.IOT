
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
    public partial class GroupInfoService : BaseService<GroupInfo>, IGroupInfoService
    {
        /// <summary>
        /// 获取所有组
        /// </summary>
        /// <param name="groupQueryParam"></param>
        /// <returns></returns>
        public IQueryable<dynamic> GetGroupInfo()
        {     
            //获取班信息表实体
            var query = DbSession.GroupInfoDal.GetEntities(g => g.IsDeleted == false)
                .Select(g=>new
                {
                    g.GName,
                    g.Id
                });
            return query;

        }
        /// <summary>
        /// 加载单个组对应的所有设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<dynamic> GetGroupDevices(int id,BaseParam baseParam)
        {
            var r_GroupInfo_DeviceInfoDal = DbSession.R_GroupInfo_DeviceInfoDal.GetEntities(g => g.IsDeleted == false && g.GroupInfoId == id);
            var deviceInfo = DbSession.DeviceInfoDal.GetEntities(d => d.IsDeleted == false);
            var query = from t1 in r_GroupInfo_DeviceInfoDal
                        join t2 in deviceInfo on t1.DeviceInfoId equals t2.Id
                        select (new
                        {
                            t2.Id,
                            t2.DeviceId
                        });
            baseParam.Total = query.Count();

            return query.GetPageEntitiesAsc(baseParam.PageSize, baseParam.PageIndex, q => q.Id, true);
        }

        /// <summary>
        /// 获取所以设备，并勾选已经存在组内的设备
        /// </summary>
        /// <param name="deviceQueryParam"></param>
        /// <returns></returns>
        public IQueryable<Device> LoagDevicePageData(DeviceQueryParam deviceQueryParam,int gId)
        {
            //组内已存在的设备Id
            var temp = DbSession.R_GroupInfo_DeviceInfoDal.GetEntities(g => g.IsDeleted == false && g.GroupInfoId == gId)
                        .Select(g => g.DeviceInfoId);

            var deviceParameterInfo = DbSession.DeviceParameterInfoDal.GetEntities(t => true);
            var deviceInfo = DbSession.DeviceInfoDal.GetEntities(d => d.IsDeleted == false);
            //查询每台机器的最新一条数据（分组查询）
            var query = from t1 in deviceParameterInfo
                        from t2 in deviceParameterInfo.GroupBy(m => m.DeviceInfoId).Select(p => new
                        {
                            newestTime = p.Max(q => q.SubTime),
                            deviceInfoId = p.Key
                        })
                        join t3 in deviceInfo on t1.DeviceInfoId equals t3.Id
                        where t1.DeviceInfoId == t2.deviceInfoId && t1.SubTime == t2.newestTime
                        select new Device
                        {
                            Id = t3.Id,
                            DeviceId = t3.DeviceId,
                            StatusFlag = t1.StatusFlag,
                            LAY_CHECKED=temp.Contains(t3.Id)
                        };
            //按设备ID查找
            if (!string.IsNullOrEmpty(deviceQueryParam.SchDeviceId))
            {
                query = query.Where(u => u.DeviceId.Contains(deviceQueryParam.SchDeviceId)).AsQueryable();
            }
            //按设备状态筛选
            if (!string.IsNullOrEmpty(deviceQueryParam.SchStatusFlag))
            {
                query = query.Where(u => u.StatusFlag.Contains(deviceQueryParam.SchStatusFlag)).AsQueryable();
            }

            deviceQueryParam.Total = query.Count();

            return query.GetPageEntitiesAsc(deviceQueryParam.PageSize, deviceQueryParam.PageIndex, q => q.DeviceId, true);

        }
    }
}
