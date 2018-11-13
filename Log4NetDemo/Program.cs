using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //从配置文件读取log4net的配置，然后进行一个初始化的工作
            log4net.Config.XmlConfigurator.Configure();

            //怎么写日志
            ILog logWriter = log4net.LogManager.GetLogger("DemoWriter");
            logWriter.Debug("ssss调试级别消息");
            logWriter.Error("ssds错误级别的消息");
        }
    }
}
