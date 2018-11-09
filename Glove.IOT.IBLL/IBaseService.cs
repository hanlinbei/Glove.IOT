using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public interface IBaseService<T> where T :class,new()
    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);


        IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                               Expression<Func<T, bool>> whereLambda,
                                               Expression<Func<T, S>> orderByLambda,
                                               bool isAsc);

        //添加用户
        T Add(T entity);

        //更新用户数据
        bool Update(T entity);


        //删除数据
        bool Delete(T entity);
    }
}
