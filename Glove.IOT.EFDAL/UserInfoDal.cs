using Glove.IOT.IDAL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.EFDAL
{
    public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {
        public string Name { get; set; }
        //    DataModelContainer db = new DataModelContainer();
        //    //查询所有用户
        //    public IQueryable<UserInfo> GetUsers(Expression<Func<UserInfo, bool>> whereLambda)
        //    {

        //        return db.UserInfo.Where(whereLambda).AsQueryable();

        //    }

        //    public IQueryable<UserInfo> GetPageUsers<S>(int pageSize, int pageIndex, out int total,
        //                                           Expression<Func<UserInfo, bool>> whereLambda,
        //                                           Expression<Func<UserInfo, S>> orderByLambda,
        //                                           bool isAsc)
        //    {
        //        total = db.UserInfo.Where(whereLambda).Count();
        //        if (isAsc)
        //        {
        //            var temp = db.UserInfo.Where(whereLambda)
        //                        .OrderBy<UserInfo, S>(orderByLambda)
        //                        .Skip(pageSize * (pageIndex - 1))
        //                        .Take(pageSize).AsQueryable();
        //            return temp;

        //        }
        //        else
        //        {
        //            var temp = db.UserInfo.Where(whereLambda)
        //                       .OrderByDescending<UserInfo, S>(orderByLambda)
        //                       .Skip(pageSize * (pageIndex - 1))
        //                       .Take(pageSize).AsQueryable();
        //            return temp;


        //        }


        //    }
        //    //添加用户
        //    public UserInfo Add(UserInfo userInfo)
        //    {
        //        db.UserInfo.Add(userInfo);
        //        db.SaveChanges();
        //        return userInfo;

        //    }
        //    //更新用户数据
        //    public bool Update(UserInfo userInfo)
        //    {
        //        db.Entry(userInfo).State = EntityState.Modified;
        //        return db.SaveChanges() > 0;
        //    }

        //    //删除数据
        //    public bool Delete(UserInfo userInfo)
        //    {
        //        db.Entry(userInfo).State = EntityState.Deleted;
        //        return db.SaveChanges() > 0;
        //    }


    }
}
