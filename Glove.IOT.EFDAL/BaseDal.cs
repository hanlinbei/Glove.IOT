using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        //查询所有用户
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {

            return Db.Set<T>().Where(whereLambda).AsQueryable();

        }

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
        //添加用户
        public T Add(T entity)
        {
            Db.Set<T>().Add(entity);
            //Db.SaveChanges();
            return entity;

        }
        //更新用户数据
        public bool Update(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            //return Db.SaveChanges() > 0;
            return true;
        }

        //删除数据
        public bool Delete(T entity)
        {
            //Db.Entry(entity).State = EntityState.Deleted;
            //return Db.SaveChanges() > 0;
            Db.Entry(entity).Property("DelFlag").CurrentValue = (short)Glove.IOT.Model.Enum.DelFlagEnum.Deleted;
            Db.Entry(entity).Property("DelFlag").IsModified = true;
            return true;
        }

        public bool Delete(int id)
        {
            var entity = Db.Set<T>().Find(id);
            //Db.Set<T>().Remove(entity);
            Db.Entry(entity).Property("DelFlag").CurrentValue = (short)Glove.IOT.Model.Enum.DelFlagEnum.Deleted;
            Db.Entry(entity).Property("DelFlag").IsModified = true;
            return true;
        }

        public int DeleteListByLogical(List<int> ids)
        {
            foreach (var id in ids)
            {
                var entity = Db.Set<T>().Find(id);
                Db.Entry(entity).Property("DelFlag").CurrentValue = (short)Glove.IOT.Model.Enum.DelFlagEnum.Deleted;
                Db.Entry(entity).Property("DelFlag").IsModified = true;
            }
            return ids.Count;

        }

    }
}

