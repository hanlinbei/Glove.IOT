namespace ServerDemo
{
    partial class Server
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ListenBtn = new System.Windows.Forms.Button();
            this.SendBox = new System.Windows.Forms.TextBox();
            this.RecieveBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comUsers = new System.Windows.Forms.ComboBox();
            this.SendBtn = new System.Windows.Forms.Button();
            this.SelectFileBtn = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.deviceIdBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sendDatabutton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.deviceId2Box = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timeLab = new System.Windows.Forms.Label();
            this.clrSendBtn = new System.Windows.Forms.Button();
            this.crlReciveBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(200, 13);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(100, 21);
            this.PortBox.TabIndex = 1;
            this.PortBox.Text = "36101";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // ListenBtn
            // 
            this.ListenBtn.Location = new System.Drawing.Point(14, 43);
            this.ListenBtn.Name = "ListenBtn";
            this.ListenBtn.Size = new System.Drawing.Size(75, 23);
            this.ListenBtn.TabIndex = 4;
            this.ListenBtn.Text = "开始侦听";
            this.ListenBtn.UseVisualStyleBackColor = true;
            this.ListenBtn.Click += new System.EventHandler(this.ListenBtn_Click);
            // 
            // SendBox
            // 
            this.SendBox.Location = new System.Drawing.Point(66, 76);
            this.SendBox.Multiline = true;
            this.SendBox.Name = "SendBox";
            this.SendBox.Size = new System.Drawing.Size(385, 126);
            this.SendBox.TabIndex = 5;
            // 
            // RecieveBox
            // 
            this.RecieveBox.Location = new System.Drawing.Point(66, 231);
            this.RecieveBox.Multiline = true;
            this.RecieveBox.Name = "RecieveBox";
            this.RecieveBox.Size = new System.Drawing.Size(385, 120);
            this.RecieveBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "发送区";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "接收区";
            // 
            // comUsers
            // 
            this.comUsers.FormattingEnabled = true;
            this.comUsers.Location = new System.Drawing.Point(330, 16);
            this.comUsers.Name = "comUsers";
            this.comUsers.Size = new System.Drawing.Size(121, 20);
            this.comUsers.TabIndex = 9;
            this.comUsers.SelectedIndexChanged += new System.EventHandler(this.comUsers_SelectedIndexChanged);
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(268, 387);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(75, 23);
            this.SendBtn.TabIndex = 12;
            this.SendBtn.Text = "发送文件";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // SelectFileBtn
            // 
            this.SelectFileBtn.Location = new System.Drawing.Point(173, 387);
            this.SelectFileBtn.Name = "SelectFileBtn";
            this.SelectFileBtn.Size = new System.Drawing.Size(75, 23);
            this.SelectFileBtn.TabIndex = 11;
            this.SelectFileBtn.Text = "选择文件";
            this.SelectFileBtn.UseVisualStyleBackColor = true;
            this.SelectFileBtn.Click += new System.EventHandler(this.SelectFileBtn_Click);
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(14, 389);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(153, 21);
            this.FilePath.TabIndex = 10;
            // 
            // deviceIdBox
            // 
            this.deviceIdBox.Location = new System.Drawing.Point(161, 43);
            this.deviceIdBox.Name = "deviceIdBox";
            this.deviceIdBox.Size = new System.Drawing.Size(90, 21);
            this.deviceIdBox.TabIndex = 13;
            this.deviceIdBox.Text = "00 00 00 00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "设备1ID";
            // 
            // sendDatabutton
            // 
            this.sendDatabutton.Location = new System.Drawing.Point(286, 206);
            this.sendDatabutton.Name = "sendDatabutton";
            this.sendDatabutton.Size = new System.Drawing.Size(75, 23);
            this.sendDatabutton.TabIndex = 15;
            this.sendDatabutton.Text = "发送数据";
            this.sendDatabutton.UseVisualStyleBackColor = true;
            this.sendDatabutton.Click += new System.EventHandler(this.sendDatabutton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(278, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "设备2ID";
            // 
            // deviceId2Box
            // 
            this.deviceId2Box.Location = new System.Drawing.Point(336, 45);
            this.deviceId2Box.Name = "deviceId2Box";
            this.deviceId2Box.Size = new System.Drawing.Size(90, 21);
            this.deviceId2Box.TabIndex = 16;
            this.deviceId2Box.Text = "00 01 02 03";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(393, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "两个同时发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timeLab
            // 
            this.timeLab.AutoSize = true;
            this.timeLab.Location = new System.Drawing.Point(284, 445);
            this.timeLab.Name = "timeLab";
            this.timeLab.Size = new System.Drawing.Size(41, 12);
            this.timeLab.TabIndex = 19;
            this.timeLab.Text = "label1";
            // 
            // clrSendBtn
            // 
            this.clrSendBtn.Location = new System.Drawing.Point(393, 206);
            this.clrSendBtn.Name = "clrSendBtn";
            this.clrSendBtn.Size = new System.Drawing.Size(75, 23);
            this.clrSendBtn.TabIndex = 20;
            this.clrSendBtn.Text = "清除发送区";
            this.clrSendBtn.UseVisualStyleBackColor = true;
            this.clrSendBtn.Click += new System.EventHandler(this.clrSendBtn_Click);
            // 
            // crlReciveBtn
            // 
            this.crlReciveBtn.Location = new System.Drawing.Point(351, 357);
            this.crlReciveBtn.Name = "crlReciveBtn";
            this.crlReciveBtn.Size = new System.Drawing.Size(75, 23);
            this.crlReciveBtn.TabIndex = 21;
            this.crlReciveBtn.Text = "清除接收区";
            this.crlReciveBtn.UseVisualStyleBackColor = true;
            this.crlReciveBtn.Click += new System.EventHandler(this.crlReciveBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 466);
            this.Controls.Add(this.crlReciveBtn);
            this.Controls.Add(this.clrSendBtn);
            this.Controls.Add(this.timeLab);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.deviceId2Box);
            this.Controls.Add(this.sendDatabutton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.deviceIdBox);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.SelectFileBtn);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.comUsers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RecieveBox);
            this.Controls.Add(this.SendBox);
            this.Controls.Add(this.ListenBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PortBox);
            this.Name = "Server";
            this.Text = "Server";
            this.Activated += new System.EventHandler(this.Server_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ListenBtn;
        private System.Windows.Forms.TextBox SendBox;
        private System.Windows.Forms.TextBox RecieveBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comUsers;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Button SelectFileBtn;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.TextBox deviceIdBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button sendDatabutton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox deviceId2Box;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label timeLab;
        private System.Windows.Forms.Button clrSendBtn;
        private System.Windows.Forms.Button crlReciveBtn;
        private System.Windows.Forms.Timer timer1;
    }
}

