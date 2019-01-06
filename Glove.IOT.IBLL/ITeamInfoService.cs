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
        IQueryable<dynamic> GetTeamInfo(TeamQueryParam teamQueryParam);
    }
}
