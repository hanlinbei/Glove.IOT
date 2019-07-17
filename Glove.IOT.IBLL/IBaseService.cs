using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public interface IBaseService<T> where T :class,new()
    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);
        DbSet<T> GetEntities();
        IQueryable<T> GetEntitiesNoTracking(Expression<Func<T, bool>> whereLambda);

        IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                               Expression<Func<T, bool>> whereLambda,
                                               Expression<Func<T, S>> orderByLambda,
                                               bool isAsc);

        //添加用户
        T Add(T entity);

        //更新用户数据
        bool Update(T entity);
        /// <summary>
        /// 拓展的更新方法
        /// </summary>
        /// <param name="filterExpression">查询的条件</param>
        /// <param name="updateExpression">要更新的字段内容</param>
        /// <returns></returns>
        bool Update(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression);

        //删除数据
        bool Delete(T entity);
        bool Delete(Expression<Func<T, bool>> filterExpression);
        bool Delete(int id);

        int DeleteList(List<int> ids);

        int DeleteListByLogical<S>(List<S> ids);



    }
}
