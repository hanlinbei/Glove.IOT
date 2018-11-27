using Glove.IOT.DALFactory;
using Glove.IOT.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.BLL
{
    /// <summary>
    /// 父类要逼迫自己给父类的一个属性赋值
    /// 赋值的操作必须在父类的方法调用之前执行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> where T:class,new()
    {
        public IBaseDal<T> CurrentDal { get; set; }

        public IDbSession DbSession
        {
            get;
            set;
            //{
            //    return DbSessionFactory.GetCurrentDbSession();
            //}
        }

        //public BaseService(IDbSession dbSession)//基类的构造函数
        //{
        //    DbSession = dbSession;
        //    SetCurrentDal();//抽象方法
        //}
        //public abstract void SetCurrentDal();//抽象方法要求子类必须实现

        //查询
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntities(whereLambda);

        }


        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                               Expression<Func<T, bool>> whereLambda,
                                               Expression<Func<T, S>> orderByLambda,
                                               bool isAsc)
        {
            return CurrentDal.GetPageEntities(pageSize, pageIndex, out total, whereLambda, orderByLambda, isAsc);

        }

        public int DeleteListByLogical(List<int> ids)
        {

            CurrentDal.DeleteListByLogical(ids);
            return DbSession.SaveChanges();

        }
        //批量删除
        public int DeleteList(List<int> ids)
        {
            foreach (var id in ids)
            {
                CurrentDal.Delete(id);
            }
            return DbSession.SaveChanges();


        }

        public bool Delete(int id)
        {
            CurrentDal.Delete(id);
            return DbSession.SaveChanges() > 0;

        }

        //添加用户
        public T Add(T entity)
        {
             CurrentDal.Add(entity);
            DbSession.SaveChanges();
            return entity;
        }

        //更新用户数据
        public bool Update(T entity)
        {
             CurrentDal.Update(entity);
            return DbSession.SaveChanges() > 0;
        }


        //删除数据
        public bool Delete(T entity)
        {
             CurrentDal.Delete(entity);
            return DbSession.SaveChanges() > 0;
        }

    }
}
