using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Common.Extention
{
    public static class QueryableExtention
    {
        /// <summary>
        /// 分页操作
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public static IQueryable<T> GetPageEntitiesAsc<T,S>(this IQueryable<T> source, int pageSize, int pageIndex,
                                              Expression<Func<T, S>> orderByLambda,
                                              bool isAsc)
        {
            if (isAsc)
            {
                var temp = source
                            .OrderBy<T, S>(orderByLambda)
                            .Skip(pageSize * (pageIndex - 1))
                            .Take(pageSize).AsQueryable();
                return temp;

            }
            else
            {
                var temp = source
                           .OrderByDescending<T, S>(orderByLambda)
                           .Skip(pageSize * (pageIndex - 1))
                           .Take(pageSize).AsQueryable();
                return temp;


            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        private static Expression<Func<T,R>> GetKeySelector<T, R>(PropertyInfo property)
        {
            Type type = typeof(T);
            ParameterExpression param = Expression.Parameter(type);
            Expression propertyAccess = param;
            propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            var keySelector = Expression.Lambda<Func<T, R>>(propertyAccess, param);
            return keySelector;
        }
    }
}
