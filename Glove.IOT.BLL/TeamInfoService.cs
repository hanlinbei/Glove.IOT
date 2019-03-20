using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    public partial class TeamInfoService : BaseService<TeamInfo>, ITeamInfoService
    {
        /// <summary>
        /// 获取班信息
        /// </summary>
        /// <param name="teamQueryParam"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetTeamInfo(TeamQueryParam teamQueryParam)
        {
            //获取班信息表实体
            var teamInfo = DbSession.TeamInfoDal.GetEntities(t => t.IsDeleted == false&&t.Id>1);

            var query = from t1 in teamInfo
                        select new
                        {
                            t1.Id,
                            t1.TName,
                            t1.StartTime,
                            t1.StopTime,
                        };
            //按班名编号筛选
            if (!string.IsNullOrEmpty(teamQueryParam.SchTName))
            {
                query = query.Where(t => t.TName.Contains(teamQueryParam.SchTName)).AsQueryable();
            }
            //按班工作时间筛选
            if (!string.IsNullOrEmpty(teamQueryParam.SchTime))
            {
                var schTime = Convert.ToDateTime(teamQueryParam.SchTime).TimeOfDay;
                query = query.Where(t=>t.StartTime.Value<=schTime&&t.StopTime.Value>=schTime).AsQueryable();
            }
            //总条数
            teamQueryParam.Total = query.Count();

            return query.AsEnumerable().Select(q=>new {
                q.Id,
                q.TName,
                StartTime=q.StartTime.ToString(),
                StopTime=q.StopTime.ToString()
            });
             
        }
    }
}
