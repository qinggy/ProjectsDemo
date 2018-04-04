namespace Esd.EnergyPec.DataUpdaterApp
{
    partial class DataUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataUpdater));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.filePath = new System.Windows.Forms.TextBox();
            this.findBtn = new System.Windows.Forms.Button();
            this.boxpanel = new System.Windows.Forms.Panel();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.prompt = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.boxpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 63);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(105, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "大数据迁移工具 V1.0.0";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(28, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据库文件";
            // 
            // filePath
            // 
            this.filePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.filePath.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePath.Location = new System.Drawing.Point(3, 6);
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            this.filePath.Size = new System.Drawing.Size(348, 19);
            this.filePath.TabIndex = 2;
            // 
            // findBtn
            // 
            this.findBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.findBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.findBtn.Location = new System.Drawing.Point(379, 161);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(111, 37);
            this.findBtn.TabIndex = 3;
            this.findBtn.Text = "浏览 . .";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // boxpanel
            // 
            this.boxpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boxpanel.Controls.Add(this.filePath);
            this.boxpanel.Location = new System.Drawing.Point(134, 100);
            this.boxpanel.Name = "boxpanel";
            this.boxpanel.Size = new System.Drawing.Size(356, 32);
            this.boxpanel.TabIndex = 4;
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.UpdateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UpdateBtn.Location = new System.Drawing.Point(379, 161);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(111, 37);
            this.UpdateBtn.TabIndex = 5;
            this.UpdateBtn.Text = "迁移";
            this.UpdateBtn.UseVisualStyleBackColor = true;
            this.UpdateBtn.Visible = false;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.closeBtn.Location = new System.Drawing.Point(379, 161);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(111, 37);
            this.closeBtn.TabIndex = 6;
            this.closeBtn.Text = "关闭";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Visible = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(32, 100);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(458, 32);
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;
            // 
            // prompt
            // 
            this.prompt.AutoSize = true;
            this.prompt.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prompt.ForeColor = System.Drawing.Color.Red;
            this.prompt.Location = new System.Drawing.Point(158, 169);
            this.prompt.Name = "prompt";
            this.prompt.Size = new System.Drawing.Size(189, 22);
            this.prompt.TabIndex = 8;
            this.prompt.Text = "数据迁移中，请稍后 ......";
            this.prompt.Visible = false;
            // 
            // progress
            // 
            this.progress.AutoSize = true;
            this.progress.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.progress.ForeColor = System.Drawing.Color.Red;
            this.progress.Location = new System.Drawing.Point(32, 80);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(90, 17);
            this.progress.TabIndex = 9;
            this.progress.Text = "迁移进度：0 %";
            this.progress.Visible = false;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(248, 161);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 37);
            this.button1.TabIndex = 10;
            this.button1.Text = "上一步";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 219);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.prompt);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.boxpanel);
            this.Controls.Add(this.findBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.UpdateBtn);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataUpdater_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataUpdater_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DataUpdater_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.boxpanel.ResumeLayout(false);
            this.boxpanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.Panel boxpanel;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label prompt;
        private System.Windows.Forms.Label progress;
        private System.Windows.Forms.Button button1;
    }
}

