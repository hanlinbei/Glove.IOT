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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileDownloadClientDemo
{
    public partial class Client : Form
    {
        Dictionary<int, Socket> dicSocket = new Dictionary<int, Socket>();
        public Client()
        {
            InitializeComponent();
        }
        
        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            //创建负责通信的Socket
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(IpAdressBox.Text.Trim());
            IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(ComBox.Text.Trim()));
            //获得要连接的远程服务应用程序的IP地址和端口
            socketSend.Connect(point);
            ShowMsg("连接成功");
            dicSocket.Add(1, socketSend);

            Thread th = new Thread(Recive);
            th.IsBackground = true;
            th.Start();

        }

        private void Recive()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 3];
                    int r = dicSocket[1].Receive(buffer);
                    //实际接收到的有效字节数
                    if (r == 0)
                    {
                        break;
                    }
                    //表示发送的文字消息
                    if (buffer[0] == 0)
                    {
                        string s = Encoding.UTF8.GetString(buffer, 1, r - 1);
                        ShowMsg(dicSocket[1].RemoteEndPoint + ":" + s);
                    }
                    else if (buffer[0] == 1)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.InitialDirectory = @"C:\Users\SpringRain\Desktop";
                        sfd.Title = "请选择要保存的文件";
                        sfd.Filter = "所有文件|*.*";
                        sfd.ShowDialog(this);
                        string path = sfd.FileName;
                        using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            fsWrite.Write(buffer, 1, r - 1);
                        }
                        MessageBox.Show("保存成功");
                    }


                }
                catch { }
            }
        }

        void ShowMsg(string str)
        {
            SendBox.AppendText(str + "\r\n");
        }

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
                while (r > 0)
                {
                    
                    list.Add(0);
                    list.Add(0);
                    list.Add(0);
                    list.Add(0);
                    list.Add(200);
                    list.Add(0x01);
                    list.AddRange(buffer);
                    //将泛型集合转换为数组
                    newBuffer = list.ToArray();
                    dicSocket[1].Send(newBuffer, 0, r+6 , SocketFlags.None);
                    list.Clear();
                    
                    ShowMsg("发送中");
                    r = fsRead.Read(buffer, 0, buffer.Length);

                }
                ShowMsg("发送完成");

            }


        }

        private void disconbtn_Click(object sender, EventArgs e)
        {
            dicSocket[1].Close();
        }
    }
}
