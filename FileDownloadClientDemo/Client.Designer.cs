namespace FileDownloadClientDemo
{
    partial class Client
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
            this.IpAdressBox = new System.Windows.Forms.TextBox();
            this.ComBox = new System.Windows.Forms.TextBox();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.SelectFileBtn = new System.Windows.Forms.Button();
            this.SendBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.IpLab = new System.Windows.Forms.Label();
            this.Comlab = new System.Windows.Forms.Label();
            this.SendBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IpAdressBox
            // 
            this.IpAdressBox.Location = new System.Drawing.Point(45, 13);
            this.IpAdressBox.Name = "IpAdressBox";
            this.IpAdressBox.Size = new System.Drawing.Size(100, 21);
            this.IpAdressBox.TabIndex = 0;
            this.IpAdressBox.Text = "36.23.72.47";
            // 
            // ComBox
            // 
            this.ComBox.Location = new System.Drawing.Point(194, 15);
            this.ComBox.Name = "ComBox";
            this.ComBox.Size = new System.Drawing.Size(62, 21);
            this.ComBox.TabIndex = 1;
            this.ComBox.Text = "36101";
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(276, 11);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnectBtn.TabIndex = 2;
            this.ConnectBtn.Text = "连接";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(14, 334);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(153, 21);
            this.FilePath.TabIndex = 3;
            // 
            // SelectFileBtn
            // 
            this.SelectFileBtn.Location = new System.Drawing.Point(181, 334);
            this.SelectFileBtn.Name = "SelectFileBtn";
            this.SelectFileBtn.Size = new System.Drawing.Size(75, 23);
            this.SelectFileBtn.TabIndex = 4;
            this.SelectFileBtn.Text = "选择文件";
            this.SelectFileBtn.UseVisualStyleBackColor = true;
            this.SelectFileBtn.Click += new System.EventHandler(this.SelectFileBtn_Click);
            // 
            // SendBox
            // 
            this.SendBox.Location = new System.Drawing.Point(35, 52);
            this.SendBox.Multiline = true;
            this.SendBox.Name = "SendBox";
            this.SendBox.Size = new System.Drawing.Size(316, 134);
            this.SendBox.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(35, 204);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(316, 110);
            this.textBox1.TabIndex = 6;
            // 
            // IpLab
            // 
            this.IpLab.AutoSize = true;
            this.IpLab.Location = new System.Drawing.Point(12, 18);
            this.IpLab.Name = "IpLab";
            this.IpLab.Size = new System.Drawing.Size(23, 12);
            this.IpLab.TabIndex = 7;
            this.IpLab.Text = "Ip:";
            // 
            // Comlab
            // 
            this.Comlab.AutoSize = true;
            this.Comlab.Location = new System.Drawing.Point(165, 18);
            this.Comlab.Name = "Comlab";
            this.Comlab.Size = new System.Drawing.Size(23, 12);
            this.Comlab.TabIndex = 8;
            this.Comlab.Text = "Com";
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(276, 334);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(75, 23);
            this.SendBtn.TabIndex = 9;
            this.SendBtn.Text = "发送";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "发送区";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "接收区";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 411);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.Comlab);
            this.Controls.Add(this.IpLab);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SendBox);
            this.Controls.Add(this.SelectFileBtn);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.ComBox);
            this.Controls.Add(this.IpAdressBox);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IpAdressBox;
        private System.Windows.Forms.TextBox ComBox;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Button SelectFileBtn;
        private System.Windows.Forms.TextBox SendBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label IpLab;
        private System.Windows.Forms.Label Comlab;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

