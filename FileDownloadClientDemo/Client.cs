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
    }
}
