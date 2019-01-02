using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IOperationLogService : IBaseService<OperationLog>
    {
        IQueryable<OperationLog> LoagOperationLogPageData(OperationLogQueryParam operationLogQueryParam);
        IQueryable<OperationLog> SearchOperationLogPageData(OperationLogQueryParam operationLogQueryParam);
        OperationLog Add(string axtionName, string actionType, OperationLog loginInfo, string schCode, string schRoleName);
    }
}
