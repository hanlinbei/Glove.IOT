using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化容器
            IApplicationContext ctx = ContextRegistry.GetContext();

            IUserInfoDal dal = ctx.GetObject("UserInfoDal")as IUserInfoDal;
            dal.show();
            Console.ReadKey();

        }
    }
}
