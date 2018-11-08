using System;
using System.Linq;
using Glove.IOT.EFDAL;
using Glove.IOT.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glove.IOT.UnitTest
{
    [TestClass]
    public class UserInfoDalTest
    {
        [TestMethod]
        public void TestGetUsers()
        {
            //测试 获取数据的方法
            UserInfoDal dal = new UserInfoDal();
            for (int i = 0; i < 20; i++)
            {
                dal.Add(new UserInfo()
                {
                    UName = i + "ssss"
                });
            }
            IQueryable<UserInfo> temp = dal.GetUsers(u => u.UName.Contains("ss"));

            //断言
            Assert.AreEqual(true, temp.Count() >= 10);

        }
    }
}
