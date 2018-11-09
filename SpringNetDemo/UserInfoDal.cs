using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo
{
    public class UserInfoDal : IUserInfoDal
    {
        public string Name { get; set; }

        public void show()
        {
            Console.WriteLine("daffe"+Name);
        }
    }
}
