namespace Esd.EnergyPec.UpdaterOnline
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.installer = new System.Windows.Forms.Button();
            this.prompt = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(25, 79);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(366, 37);
            this.progressBar.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 47);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "能源管理平台在线更新工具 V1.0.0";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_MouseUp);
            // 
            // installer
            // 
            this.installer.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.installer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.installer.Font = new System.Drawing.Font("华文新魏", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.installer.Location = new System.Drawing.Point(265, 141);
            this.installer.Name = "installer";
            this.installer.Size = new System.Drawing.Size(126, 36);
            this.installer.TabIndex = 2;
            this.installer.Text = "安  装";
            this.installer.UseVisualStyleBackColor = false;
            this.installer.Click += new System.EventHandler(this.installer_Click);
            // 
            // prompt
            // 
            this.prompt.AutoSize = true;
            this.prompt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prompt.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prompt.ForeColor = System.Drawing.Color.Red;
            this.prompt.Location = new System.Drawing.Point(64, 148);
            this.prompt.Name = "prompt";
            this.prompt.Size = new System.Drawing.Size(186, 22);
            this.prompt.TabIndex = 3;
            this.prompt.Text = "下载完成，请点击安装！";
            // 
            // progress
            // 
            this.progress.AutoSize = true;
            this.progress.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.progress.ForeColor = System.Drawing.Color.Red;
            this.progress.Location = new System.Drawing.Point(23, 60);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(85, 17);
            this.progress.TabIndex = 4;
            this.progress.Text = "下载进度 : 0%";
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("华文新魏", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.closebtn.Location = new System.Drawing.Point(265, 141);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(126, 36);
            this.closebtn.TabIndex = 5;
            this.closebtn.Text = "关  闭";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Visible = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 193);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.prompt);
            this.Controls.Add(this.installer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "能源管理系统在线更新工具";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button installer;
        private System.Windows.Forms.Label prompt;
        private System.Windows.Forms.Label progress;
        private System.Windows.Forms.Button closebtn;
    }
}

