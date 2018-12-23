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
    public partial class OperationLogService : BaseService<OperationLog>, IOperationLogService
    {
        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="userQueryParam">查询条件</param>
        /// <returns>查询结果</returns>
        public IQueryable<OperationLog> LoagOperationLogPageData(OperationLogQueryParam operationLogQueryParam)
        {
            var temp = DbSession.OperationLogDal.GetEntities(u => u.IsDeleted == false);
            operationLogQueryParam.Total = temp.Count();
            //分页
            return temp.OrderBy(u => u.Id)
                .Skip(operationLogQueryParam.PageSize * (operationLogQueryParam.PageIndex - 1))
                .Take(operationLogQueryParam.PageSize).AsQueryable();
        }

    }
}
