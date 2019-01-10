
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
        public IQueryable<dynamic> GetGroupInfo(GroupQueryParam groupQueryParam)
        {     
            //获取班信息表实体
            var query = DbSession.GroupInfoDal.GetEntities(g => g.IsDeleted == false)
                .Select(g=>new
                {
                    g.GName,
                    g.Id
                });

            //总条数
            groupQueryParam.Total = query.Count();
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
        /// 查询分页已存在的设备Id
        /// </summary>
        /// <param name="gId"></param>
        /// <param name="alldIds"></param>
        /// <returns></returns>
        public IQueryable<int> GetExitDevices(int gId, List<int> alldIds)
        {

            var query = DbSession.R_GroupInfo_DeviceInfoDal.GetEntities(g => g.IsDeleted == false && g.GroupInfoId == gId&&alldIds.Contains(g.DeviceInfoId))
                        .Select(g=>g.DeviceInfoId);
            return query;
        }
    }
}
