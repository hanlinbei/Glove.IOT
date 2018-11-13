using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glove.IOT.Common
{
    //public delegate void WriteLogDel(string str);
    public class LogHelper
    {
        public static Queue<string> ExceptionStringQueue = new Queue<string>();
        public static List<ILogWriter> LogWriterList = new List<ILogWriter>();
        //public static WriteLogDel WriteLogDelFunc;

        static LogHelper()
        {
            //WriteLogDelFunc = new WriteLogDel(WriteLogToFile);
            //WriteLogDelFunc += WriteLogToMongodb;

            //LogWriterList.Add(new TextFileWrieter());
            //LogWriterList.Add(new SqlServerWriter());
            LogWriterList.Add(new Log4NetWriter());

            //把从队列中获取的错误信息写到日志文件里面去
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    lock (ExceptionStringQueue)
                    {
                        if (ExceptionStringQueue.Count > 0)
                        {
                            string str = ExceptionStringQueue.Dequeue();
                            //把异常信息写到日志文件里去

                            //执行委托代码，把异常信息写到委托里面去
                            //WriteLogDelFunc(str);

                            foreach (var logWriter in LogWriterList)
                            {
                                logWriter.WriteLogInfo(str);
                            }
                            //log4net已经帮助我们处理好了观察者模式
                        }
                        else
                        {
                            Thread.Sleep(30);
                        }
                    }
                }



            });



        }

        //public static void WriteLogToFile(string txt)
        //{




        //}
        //public static void WriteLogToMongodb(string txt)
        //{


        //}
        public static void WriteLog(string exceptionText)
        {
            lock (ExceptionStringQueue)
            {
                ExceptionStringQueue.Enqueue(exceptionText);

            }


        }
    }
}
