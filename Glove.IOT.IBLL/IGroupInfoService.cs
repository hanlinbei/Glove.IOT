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
        IQueryable<dynamic> GetGroupInfo(GroupQueryParam groupQueryParam);
        IQueryable<dynamic> GetGroupDevices(int id);
    }
}
