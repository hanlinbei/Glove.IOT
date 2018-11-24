using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Common.Cache
{
    public class CacheHelper
    {
        //Spring.Net直接注入一个Cache的实现过来
        public static ICacheWriter CacheWriter { get; set; }
        static CacheHelper()
        {
            //通过容器创建一个对象
            IApplicationContext ctx = ContextRegistry.GetContext();
            //ctx.GetObject("CacheHelper");
            CacheHelper.CacheWriter = ctx.GetObject("CacheWriter") as ICacheWriter;


        }
        public static void AddCache(string key, object value, DateTime expDate)
        {
            //往缓存写：单机，分布式
            //ICacheWriter cacheWriter = new MemcacheWriter();
            CacheWriter.AddCache(key, value, expDate);
        }
        public static void AddCache(string key, object value)
        {
            CacheWriter.AddCache(key, value);
        }
        public static object GetCache(string key)
        {
            return CacheWriter.GetCache(key);
        }
        public static void SetCache(string key, object value, DateTime extDate)
        {
            CacheWriter.SetCache(key, value, extDate);
        }

        public static void SetCache(string key, object value)
        {
            CacheWriter.SetCache(key, value);
        }
    }
}
