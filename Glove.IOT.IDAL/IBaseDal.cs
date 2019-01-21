using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IDAL
{
    public interface IBaseDal<T>where T:class,new()
    {
        /// <summary>
        /// 追终查询
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 不查询得到指定实体
        /// </summary>
        /// <returns></returns>
        DbSet<T> GetEntities();
        /// <summary>
        /// 不追终查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> GetEntitiesNoTracking(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 分页操作
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                               Expression<Func<T, bool>> whereLambda,
                                               Expression<Func<T, S>> orderByLambda,
                                               bool isAsc);
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add(T entity);

        /// <summary>
        /// 修改一条记录全部
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);
        /// <summary>
        /// 拓展的更新方法
        /// </summary>
        /// <param name="filterExpression">查询的条件</param>
        /// <param name="updateExpression">要更新的字段数据</param>
        /// <returns></returns>
        bool Update(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression);
        /// <summary>
        /// 逻辑删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> filterExpression);
        /// <summary>
        /// 根据id单个逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        int DeleteListByLogical(List<int> ids);

       
    }
}
