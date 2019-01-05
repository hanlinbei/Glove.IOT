using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IWarningInfoService : IBaseService<WarningInfo>
    {
        /// <summary>
        /// /获取报警信息
        /// </summary>
        /// <returns></returns>
        IQueryable<dynamic> GetWarningInfo(WarningQueryParam warningqueryParam);
    }
}
