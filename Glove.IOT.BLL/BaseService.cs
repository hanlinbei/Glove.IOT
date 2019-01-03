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

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLambda">表达式条件</param>
        /// <returns>查询结果</returns>
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntities(whereLambda);

        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageSize">一页存放多少条</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="total">数据总条数</param>
        /// <param name="whereLambda">表达式条件</param>
        /// <param name="orderByLambda">排序条件</param>
        /// <param name="isAsc">升序</param>
        /// <returns>查询结果</returns>
        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total,
                                               Expression<Func<T, bool>> whereLambda,
                                               Expression<Func<T, S>> orderByLambda,
                                               bool isAsc)
        {
            return CurrentDal.GetPageEntities(pageSize, pageIndex, out total, whereLambda, orderByLambda, isAsc);

        }
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids">多个od</param>
        /// <returns>true</returns>
        public int DeleteListByLogical(List<int> ids)
        {

            CurrentDal.DeleteListByLogical(ids);
            return DbSession.SaveChanges();

        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">多个id</param>
        /// <returns>true</returns>
        public int DeleteList(List<int> ids)
        {
            foreach (var id in ids)
            {
                CurrentDal.Delete(id);
            }
            return DbSession.SaveChanges();


        }

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true</returns>
        public bool Delete(int id)
        {
            CurrentDal.Delete(id);
            return DbSession.SaveChanges() > 0;

        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体</returns>
        public T Add(T entity)
        {
             CurrentDal.Add(entity);
            //DbSession.SaveChanges();
            return entity;
        }
    


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>true</returns>
        public bool Update(T entity)
        {
             CurrentDal.Update(entity);
            return DbSession.SaveChanges() > 0;
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>true</returns>
        public bool Delete(T entity)
        {
             CurrentDal.Delete(entity);
            return DbSession.SaveChanges() > 0;
        }

    }
}
