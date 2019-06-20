using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerDemo
{
    public partial class Server : Form
    {
        //将远程连接的客户端的IP地址和Socket存入集合中
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
        string ip;//从窗体获取的客户端IP
        public Server()
        {
            InitializeComponent();
  
        }
        void showTime()
        {
            if (timeLab.InvokeRequired)
            {

                timeLab.Invoke(new Action(() => { timeLab.Text = DateTime.Now + "  " + DateTime.Now.DayOfWeek; }));
            }
            else
            {
                timeLab.Invoke(new Action(() => { timeLab.Text = DateTime.Now + "  " + DateTime.Now.DayOfWeek; }));
            }

        }

        private void ListenBtn_Click(object sender, EventArgs e)
        {
           
                //当点击开始监听的时候 在服务器端创建一个负责监IP地址跟端口号的Socket
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;
                //创建端口号对象
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(PortBox.Text.Trim()));
                //监听
                socketWatch.Bind(point);
                ShowMsg("监听成功");
                socketWatch.Listen(10);

                Thread th = new Thread(Listen);
                th.IsBackground = true;
                th.Start(socketWatch);
            


        }

        Socket socketSend;
        void Listen(object o)
        {
            Socket socketWatch = o as Socket;
            //等待客户端的连接 并且创建一个负责通信的Socket
            while (true)
            {
                //负责跟客户端通信的Socket
                socketSend = socketWatch.Accept();
                //将远程连接的客户端的IP地址和Socket存入集合中
                dicSocket.Add(socketSend.RemoteEndPoint.ToString(), socketSend);
                //将远程连接的客户端的IP地址和端口号存储下拉框中   
                if (comUsers.InvokeRequired)
                {

                    comUsers.Invoke(new Action<string>(s => { this.comUsers.Items.Add(s); }), socketSend.RemoteEndPoint.ToString());
                }
                else
                {
                    comUsers.Invoke(new Action<string>(s => { this.comUsers.Items.Add(s); }), socketSend.RemoteEndPoint.ToString());
                }
                ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功");
                    //开启 一个新线程不停的接受客户端发送过来的消息
                    Thread th = new Thread(Recive);
                    th.IsBackground = true;
                    th.Start(socketSend);
              
            }


        }

        /// <summary>
        /// 服务器端不停的接受客户端发送过来的消息
        /// </summary>
        /// <param name="o"></param>
        void Recive(object o)
        {
            Socket socketSend = o as Socket;
            while (true)
            {

                //客户端连接成功后，服务器应该接受客户端发来的消息
                byte[] buffer = new byte[1024 * 1024 * 2];
                //实际接受到的有效字节数
                int r = socketSend.Receive(buffer);
                if (r == 0)
                {
                    break;
                }
                string str = Encoding.UTF8.GetString(buffer, 0, r);
                ShowMsg(socketSend.RemoteEndPoint + ":" + str);


            }
        }

        /// <summary>
        /// 提示信息显示
        /// </summary>
        /// <param name="str"></param>
        void ShowMsg(string str)
        {
            if (RecieveBox.InvokeRequired)
            {

                RecieveBox.Invoke(new Action<string>(s => { this.RecieveBox.AppendText(s); }), str + "\r\n");
            }
            else
            {
                RecieveBox.Invoke(new Action<string>(s => { this.RecieveBox.AppendText(s); }), str + "\r\n");
            }
      
        }

        /// <summary>
        /// 提示信息显示
        /// </summary>
        /// <param name="str"></param>
        void ShowSendMsg(string str)
        {
            if (SendBox.InvokeRequired)
            {

                SendBox.Invoke(new Action<string>(s => { this.SendBox.AppendText(s); }), str + "\r\n");
            }
            else
            {
                SendBox.Invoke(new Action<string>(s => { this.SendBox.AppendText(s); }), str + "\r\n");
            }

        }

        /// <summary>
        /// 选择要发送的文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "请选择要发送的文件",
                Filter = "bin文件|*.bin"
            };
            ofd.ShowDialog();

            FilePath.Text = ofd.FileName;
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendBtn_Click(object sender, EventArgs e)
        {
            //获取要发送文件的路径
            string path = FilePath.Text;
            using (FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[194];
                List<byte> list = new List<byte>();
                byte[] newBuffer = new byte[200];
                int r = fsRead.Read(buffer, 0, buffer.Length);
                //匹配十六进制格式
                MatchCollection mc = Regex.Matches(deviceIdBox.Text.Trim(), @"\b[\da-fA-F]{2}");
                if (mc.Count != 4)
                {
                    MessageBox.Show("请输入4个16进制数");
                }
                else
                {
                    while (r > 0)
                    {
                        foreach (Match m in mc)
                        {
                            list.Add(byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber));//获取输入的设备ID
                        }
                        list.Add(200);
                        list.Add(0x01);
                        list.AddRange(buffer);
                        //将泛型集合转换为数组
                        newBuffer = list.ToArray();
                        if (ip != null)
                        {
                            dicSocket[ip].Send(newBuffer, 0, r + 6, SocketFlags.None);
                            list.Clear();
                        }
                        else
                        {
                            MessageBox.Show("请选择客户端IP");
                        }


                        ShowSendMsg("发送中");
                        r = fsRead.Read(buffer, 0, buffer.Length);

                    }
                }
                ShowSendMsg("发送完成");

            }
        }


        /// <summary>
        /// 发送发送区的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendDatabutton_Click(object sender, EventArgs e)
        {
            string str = SendBox.Text;
            List<byte> list = new List<byte>();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            var s = deviceIdBox.Text.Trim();
            MatchCollection mc = Regex.Matches(deviceIdBox.Text.Trim(), @"\b[\da-fA-F]{2}");
            if (mc.Count != 4)
            {
                MessageBox.Show("请输入4个16进制数");
            }
            else
            {            
                foreach (Match m in mc)
                {
                    list.Add(byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber));//获取输入的设备ID
                }
                list.Add(byte.Parse(buffer.Length.ToString()));
                list.AddRange(buffer);
                //将泛型集合转换为数组
                byte[] newBuffer = list.ToArray();
                //获得用户在下拉框中选中的IP地址

                
                if (ip != null)
                {
                    dicSocket[ip].Send(newBuffer);
                    
                }
                else
                {
                    MessageBox.Show("请选择客户端IP");
                }
                
            }
          
        }
        /// <summary>
        /// 获取选中的客户端IP 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ip = comUsers.SelectedItem.ToString();
        }

        /// <summary>
        /// 同时给俩个设备发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string str = SendBox.Text;
            List<byte> list = new List<byte>();
            List<byte> list2 = new List<byte>();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            
            MatchCollection mc1 = Regex.Matches(deviceIdBox.Text.Trim(), @"\b[\da-fA-F]{2}");
            MatchCollection mc2 = Regex.Matches(deviceId2Box.Text.Trim(), @"\b[\da-fA-F]{2}");
            if (mc1.Count != 4&&mc2.Count!=4)
            {
                MessageBox.Show("请输入4个16进制数");
            }
            else
            {
                foreach (Match m in mc1)
                {
                    list.Add(byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber));//获取输入的设备ID
                }
                list.Add(byte.Parse(buffer.Length.ToString()));
                list.AddRange(buffer);
                foreach (Match m in mc2)
                {
                    list2.Add(byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber));//获取输入的设备ID
                }
                list2.Add(byte.Parse(buffer.Length.ToString()));
                list2.AddRange(buffer);
                //将泛型集合转换为数组
                byte[] newBuffer = list.ToArray();
                byte[] newBuffer2 = list2.ToArray();
                //获得用户在下拉框中选中的IP地址

                
                if (ip != null)
                {
                    dicSocket[ip].Send(newBuffer);
                    dicSocket[ip].Send(newBuffer2);
                    
                }
                else
                {
                    MessageBox.Show("请选择客户端IP");
                }
                
            }

        }

        private void Server_Activated(object sender, EventArgs e)
        {
            //Thread th = new Thread(showTime);
            //th.IsBackground = true;
            //th.Start();
            timeLab.Text = DateTime.Now + "  " + DateTime.Now.DayOfWeek;
        }

        private void clrSendBtn_Click(object sender, EventArgs e)
        {
            SendBox.Clear();
        }

        private void crlReciveBtn_Click(object sender, EventArgs e)
        {
            RecieveBox.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLab.Text = DateTime.Now + "  " + DateTime.Now.DayOfWeek;
        }
    }
}
