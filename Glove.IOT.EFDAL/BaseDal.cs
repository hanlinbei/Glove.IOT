﻿using EntityFramework.Extensions;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.EFDAL
{
    public class BaseDal<T>where T : class, new()
    {
        //DataModelContainer db = new DataModelContainer();
        
        //依赖抽象编程
        public DbContext Db
        {
            get { return DbContextFactory.GetCurrentDbContext(); }
        }
        /// <summary>
        /// 追终查询
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where(whereLambda).AsQueryable();

        }
        /// <summary>
        /// 不查询得到指定实体
        /// </summary>
        /// <returns></returns>
        public DbSet<T> GetEntities()
        {
            return Db.Set<T>();
        }

        /// <summary>
        /// 不追终查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> GetEntitiesNoTracking(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().AsNoTracking().Where(whereLambda).AsQueryable();

        }
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
        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                               Expression<Func<T, bool>> whereLambda,
                                               Expression<Func<T, S>> orderByLambda,
                                               bool isAsc)
        {
            total = Db.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = Db.Set<T>().Where(whereLambda)
                            .OrderBy<T, S>(orderByLambda)
                            .Skip(pageSize * (pageIndex - 1))
                            .Take(pageSize).AsQueryable();
                return temp;

            }
            else
            {
                var temp = Db.Set<T>().Where(whereLambda)
                           .OrderByDescending<T, S>(orderByLambda)
                           .Skip(pageSize * (pageIndex - 1))
                           .Take(pageSize).AsQueryable();
                return temp;


            }


        }
        
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            Db.Set<T>().Add(entity);
            //Db.SaveChanges();
            return entity;

        }
        /// <summary>
        /// 修改一条记录全部
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return true;
        }
        /// <summary>
        /// 扩展更新
        /// </summary>
        /// <param name="filterExpression">要查询的条件</param>
        /// <param name="updateExpression">要更新的字段内容</param>
        /// <returns></returns>
        public bool Update(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression)
        {
            Db.Set<T>().Update(filterExpression, updateExpression);
            return true;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> filterExpression)
        {     
            Db.Set<T>().Delete(filterExpression);
            return true;
        }
        /// <summary>
        /// 逻辑删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            //Db.Entry(entity).State = EntityState.Deleted;
            
            Db.Entry(entity).Property("IsDeleted").CurrentValue = true;
            Db.Entry(entity).Property("IsDeleted").IsModified = true;
             return true;
        }
        /// <summary>
        /// 根据id单个逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var entity = Db.Set<T>().Find(id);
            //Db.Set<T>().Remove(entity);
            Db.Entry(entity).Property("IsDeleted").CurrentValue = true;
            Db.Entry(entity).Property("IsDeleted").IsModified = true;
            return true;
        }
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteListByLogical(List<int> ids)
        {
            foreach (var id in ids)
            {
                var entity = Db.Set<T>().Find(id);
                Db.Entry(entity).Property("IsDeleted").CurrentValue = true;
                Db.Entry(entity).Property("IsDeleted").IsModified = true;
            }
            return ids.Count;

        }

    }
}

