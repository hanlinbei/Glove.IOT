using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
        bool SetRole(int userId, List<int> roleIds);
    }
}
