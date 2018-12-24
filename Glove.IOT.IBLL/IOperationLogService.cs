using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IOperationLogService : IBaseService<OperationLog>
    {
        IQueryable<OperationLog> LoagOperationLogPageData(Model.Param.OperationLogQueryParam operationLogQueryParam);
    }
}
