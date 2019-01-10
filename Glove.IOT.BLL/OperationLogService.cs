using Glove.IOT.Common.Extention;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using Spring.Context;
using Spring.Context.Support;
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
            return temp.GetPageEntitiesAsc(operationLogQueryParam.PageSize, operationLogQueryParam.PageIndex, u => u.SubTime, false);
        }
        /// <summary>
        /// 查找指定时段的日志信息
        /// </summary>
        /// <param name="operationLogQueryParam"></param>
        /// <returns></returns>
        public IQueryable<OperationLog> SearchOperationLogPageData(OperationLogQueryParam operationLogQueryParam)
        {
            var firstTime=Convert.ToDateTime(operationLogQueryParam.FirstTime);
            var lastTime=Convert.ToDateTime(operationLogQueryParam.LastTime);
            var temp = DbSession.OperationLogDal.GetEntities(u => (u.IsDeleted == false&&(u.SubTime>firstTime&&u.SubTime<lastTime)));
            //按操作人员筛选
            if (!string.IsNullOrEmpty(operationLogQueryParam.UName))
            {
                temp = temp.Where(o => o.UName.Contains(operationLogQueryParam.UName)).AsQueryable();
            }
            //按操作类型筛选
            if (!string.IsNullOrEmpty(operationLogQueryParam.ActionType))
            {
                temp = temp.Where(o => o.ActionType.Contains(operationLogQueryParam.ActionType)).AsQueryable();
            }
            //按操作名称查找
            if (!string.IsNullOrEmpty(operationLogQueryParam.ActionName))
            {
                temp = temp.Where(o => o.ActionName.Contains(operationLogQueryParam.ActionName)).AsQueryable();
            }

            operationLogQueryParam.Total = temp.Count();
            //分页
            return temp.GetPageEntitiesAsc(operationLogQueryParam.PageSize, operationLogQueryParam.PageIndex, u => u.SubTime, false);
        }

        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="actionType"></param>
        /// <param name="loginInfo"></param>
        /// <param name="schCode"></param>
        /// <param name="schRoleName"></param>
        /// <returns></returns>
        public OperationLog Add(string actionName, string actionType, OperationLog loginInfo, string schCode, string schRoleName)
        {
           
            //写操作日志
                OperationLog operationLog = new OperationLog
                {
                    ActionName = actionName,
                    ActionType = actionType,
                    Ip = loginInfo.Ip,
                    Mac = loginInfo.Mac,
                    SubTime = DateTime.Now,
                    UName = loginInfo.UName
                };
                if (!string.IsNullOrEmpty(schCode))
                {
                    operationLog.OperationObj = schCode;
                }
                if (!string.IsNullOrEmpty(schRoleName))
                {
                    operationLog.OperationObj = schRoleName;
                }
                  var data=DbSession.OperationLogDal.Add(operationLog);
                  DbSession.SaveChanges();
                  return data;
                     
            
        }
    }
}
