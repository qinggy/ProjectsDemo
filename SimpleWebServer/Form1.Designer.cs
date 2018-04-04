namespace SimpleWebServer
{
    partial class WebServer
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebServer));
            this.IP = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Location = new System.Drawing.Point(13, 13);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(29, 12);
            this.IP.TabIndex = 0;
            this.IP.Text = "IP：";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(48, 10);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(185, 21);
            this.txtIP.TabIndex = 1;
            this.txtIP.Text = "192.168.0.106";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(295, 10);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(63, 21);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "50001";
            // 
            // port
            // 
            this.port.AutoSize = true;
            this.port.Location = new System.Drawing.Point(248, 13);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(41, 12);
            this.port.TabIndex = 2;
            this.port.Text = "Port：";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(477, 8);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(587, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(15, 38);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.Size = new System.Drawing.Size(647, 321);
            this.txtMsg.TabIndex = 6;
            // 
            // WebServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 371);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.port);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.IP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WebServer";
            this.Text = "SimpleWebServer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IP;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label port;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtMsg;
    }
}

