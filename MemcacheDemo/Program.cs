using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemcacheDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //分布Memcachedf服务IP 端口
            string[] servers = { "10.12.11.44:11211" };

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.EnableCompression = false;

            mc.Add("keyddd", "sssssssss");

            mc.Add("ssss", "dddd", DateTime.Now.AddDays(1));

            //mc.Delete()
            //mc.Set()

        }
    }
}
