using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface ITeamInfoService : IBaseService<TeamInfo>
    {
        /// <summary>
        /// 获取班信息
        /// </summary>
        /// <param name="teamQueryParam"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetTeamInfo(TeamQueryParam teamQueryParam);
    }
}
