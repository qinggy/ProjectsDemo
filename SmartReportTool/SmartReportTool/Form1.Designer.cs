namespace SmartReportTool
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
            this.closebtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.searchtxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sTime = new System.Windows.Forms.DateTimePicker();
            this.eTime = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.settingbtn = new System.Windows.Forms.Button();
            this.seachbtn = new System.Windows.Forms.Button();
            this.resetbtn = new System.Windows.Forms.Button();
            this.cellmappingGrid = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.filename = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cellmappingGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // closebtn
            // 
            this.closebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closebtn.BackgroundImage")));
            this.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Location = new System.Drawing.Point(1015, 2);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(38, 30);
            this.closebtn.TabIndex = 1;
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询条件";
            // 
            // searchtxt
            // 
            this.searchtxt.Location = new System.Drawing.Point(93, 118);
            this.searchtxt.Name = "searchtxt";
            this.searchtxt.Size = new System.Drawing.Size(294, 21);
            this.searchtxt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(552, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "开始时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(745, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "结束时间";
            // 
            // sTime
            // 
            this.sTime.CustomFormat = "yyyy-MM-dd";
            this.sTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sTime.Location = new System.Drawing.Point(611, 115);
            this.sTime.Name = "sTime";
            this.sTime.Size = new System.Drawing.Size(99, 21);
            this.sTime.TabIndex = 4;
            // 
            // eTime
            // 
            this.eTime.CustomFormat = "yyyy-MM-dd";
            this.eTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.eTime.Location = new System.Drawing.Point(804, 115);
            this.eTime.Name = "eTime";
            this.eTime.Size = new System.Drawing.Size(101, 21);
            this.eTime.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.closebtn);
            this.panel1.Controls.Add(this.settingbtn);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1056, 71);
            this.panel1.TabIndex = 6;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(930, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 29);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(416, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(254, 31);
            this.label4.TabIndex = 2;
            this.label4.Text = "智能报表时间修改工具";
            // 
            // settingbtn
            // 
            this.settingbtn.AutoEllipsis = true;
            this.settingbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("settingbtn.BackgroundImage")));
            this.settingbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.settingbtn.FlatAppearance.BorderSize = 0;
            this.settingbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingbtn.ForeColor = System.Drawing.Color.Transparent;
            this.settingbtn.Location = new System.Drawing.Point(975, 3);
            this.settingbtn.Name = "settingbtn";
            this.settingbtn.Size = new System.Drawing.Size(36, 30);
            this.settingbtn.TabIndex = 0;
            this.settingbtn.UseVisualStyleBackColor = true;
            this.settingbtn.Click += new System.EventHandler(this.settingbtn_Click);
            // 
            // seachbtn
            // 
            this.seachbtn.Location = new System.Drawing.Point(422, 116);
            this.seachbtn.Name = "seachbtn";
            this.seachbtn.Size = new System.Drawing.Size(75, 23);
            this.seachbtn.TabIndex = 7;
            this.seachbtn.Text = "查询";
            this.seachbtn.UseVisualStyleBackColor = true;
            this.seachbtn.Click += new System.EventHandler(this.seachbtn_Click);
            // 
            // resetbtn
            // 
            this.resetbtn.Location = new System.Drawing.Point(936, 116);
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.Size = new System.Drawing.Size(75, 23);
            this.resetbtn.TabIndex = 8;
            this.resetbtn.Text = "替换";
            this.resetbtn.UseVisualStyleBackColor = true;
            this.resetbtn.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // cellmappingGrid
            // 
            this.cellmappingGrid.AllowUserToAddRows = false;
            this.cellmappingGrid.AllowUserToResizeRows = false;
            this.cellmappingGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cellmappingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cellmappingGrid.Location = new System.Drawing.Point(21, 158);
            this.cellmappingGrid.MultiSelect = false;
            this.cellmappingGrid.Name = "cellmappingGrid";
            this.cellmappingGrid.RowTemplate.Height = 23;
            this.cellmappingGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cellmappingGrid.Size = new System.Drawing.Size(1014, 413);
            this.cellmappingGrid.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "文件名";
            // 
            // filename
            // 
            this.filename.Location = new System.Drawing.Point(93, 85);
            this.filename.Name = "filename";
            this.filename.ReadOnly = true;
            this.filename.Size = new System.Drawing.Size(918, 21);
            this.filename.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(19, 577);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "当前版本 V2.0.0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 592);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.filename);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cellmappingGrid);
            this.Controls.Add(this.resetbtn);
            this.Controls.Add(this.seachbtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.eTime);
            this.Controls.Add(this.sTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.searchtxt);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cellmappingGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchtxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker sTime;
        private System.Windows.Forms.DateTimePicker eTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button seachbtn;
        private System.Windows.Forms.Button resetbtn;
        private System.Windows.Forms.DataGridView cellmappingGrid;
        private System.Windows.Forms.Button settingbtn;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox filename;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
    }
}

