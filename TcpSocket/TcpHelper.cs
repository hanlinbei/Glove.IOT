using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glove.IOT.TcpSocket
{
    public class TcpHelper
    {
        public IDeviceParameterInfoService DeviceParameterInfoService { get; set; }
        public void SocketInit()
        {
            try
            {
                //当点击开始监听的时候 在服务器端创建一个负责监IP地址跟端口号的Socket
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;//IPAddress.Parse(txtServer.Text);
                                             //创建端口号对象
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(50000));
                //监听
                socketWatch.Bind(point);

                socketWatch.Listen(10);

                Thread th = new Thread(Listen)
                {
                    IsBackground = true
                };
                th.Start(socketWatch);
            }
            catch
            { }
        }
        /// <summary>
        /// 等待客户端的连接 并且创建与之通信用的Socket
        /// </summary>
        /// 
        Socket socketSend;
        public void Listen(object o)
        {
            Socket socketWatch = o as Socket;
            //等待客户端的连接 并且创建一个负责通信的Socket
            while (true)
            {
                try
                {
                    //负责跟客户端通信的Socket
                    socketSend = socketWatch.Accept();
                    //开启 一个新线程不停的接受客户端发送过来的消息
                    Thread th = new Thread(Recive)
                    {
                        IsBackground = true
                    };
                    th.Start(socketSend);
                }
                catch
                { }
            }
        }

        /// <summary>
        /// 服务器端不停的接受客户端发送过来的消息
        /// </summary>
        /// <param name="o"></param>
        public void Recive(object o)
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                try
                {
                    //客户端连接成功后，服务器应该接受客户端发来的消息
                    byte[] buffer = new byte[1024];
                    //实际接受到的有效字节数
                    int r = socketSend.Receive(buffer);
                    DeviceParameterInfo deviceParameter = new DeviceParameterInfo
                    {
                        DeviceInfoId = buffer[0],
                        NowOutput = buffer[1],
                        TargetOutput = buffer[2],
                        SingleProgress = buffer[3],
                        StatusFlag = 1,
                        StartTime = DateTime.Now,
                        StopTime = DateTime.Now,
                        SubTime = DateTime.Now
                    };
                    DeviceParameterInfoService.Add(deviceParameter);

                    if (r == 0)
                    {
                        break;
                    }


                }
                catch
                { }
            }
        }
    }
}
