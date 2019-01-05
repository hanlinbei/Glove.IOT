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
        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="userQueryParam">查询条件</param>
        /// <returns>查询结果</returns>
        IQueryable<OperationLog> LoagOperationLogPageData(OperationLogQueryParam operationLogQueryParam);
        /// <summary>
        /// 查找指定时段的日志信息
        /// </summary>
        /// <param name="operationLogQueryParam"></param>
        /// <returns></returns>
        IQueryable<OperationLog> SearchOperationLogPageData(OperationLogQueryParam operationLogQueryParam);
        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="actionType"></param>
        /// <param name="loginInfo"></param>
        /// <param name="schCode"></param>
        /// <param name="schRoleName"></param>
        /// <returns></returns>
        OperationLog Add(string axtionName, string actionType, OperationLog loginInfo, string schCode, string schRoleName);
    }
}
