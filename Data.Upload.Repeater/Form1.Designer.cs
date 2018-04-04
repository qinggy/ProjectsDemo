namespace Data.Upload.Repeater
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.menuPanel = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            this.nextBtn = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.mapPanel = new System.Windows.Forms.Panel();
            this.txtdetail = new System.Windows.Forms.TextBox();
            this.detail = new System.Windows.Forms.Label();
            this.txtSystemName = new System.Windows.Forms.TextBox();
            this.systemName = new System.Windows.Forms.Label();
            this.dataTree = new System.Windows.Forms.TreeView();
            this.donepanel = new System.Windows.Forms.Panel();
            this.succesBtn = new System.Windows.Forms.Button();
            this.scanbtn = new System.Windows.Forms.Button();
            this.picDone = new System.Windows.Forms.PictureBox();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.picScan = new System.Windows.Forms.PictureBox();
            this.menuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.mapPanel.SuspendLayout();
            this.donepanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScan)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.menuPanel.Controls.Add(this.btnClose);
            this.menuPanel.Controls.Add(this.logo);
            this.menuPanel.Controls.Add(this.label);
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Margin = new System.Windows.Forms.Padding(0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(774, 65);
            this.menuPanel.TabIndex = 0;
            this.menuPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuPanel_MouseDown);
            this.menuPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.menuPanel_MouseMove);
            this.menuPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.menuPanel_MouseUp);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.Color.Red;
            this.btnClose.Location = new System.Drawing.Point(751, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Tag = "";
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // logo
            // 
            this.logo.Image = global::Data.Upload.Repeater.Properties.Resources.newlogo;
            this.logo.Location = new System.Drawing.Point(12, 9);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(97, 45);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 1;
            this.logo.TabStop = false;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("微软雅黑", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label.Location = new System.Drawing.Point(263, 4);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(244, 50);
            this.label.TabIndex = 0;
            this.label.Text = "数据集成助手";
            // 
            // nextBtn
            // 
            this.nextBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.nextBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nextBtn.ForeColor = System.Drawing.SystemColors.Highlight;
            this.nextBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.nextBtn.Location = new System.Drawing.Point(659, 566);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(103, 38);
            this.nextBtn.TabIndex = 1;
            this.nextBtn.Text = "下一步";
            this.nextBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrevious.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnPrevious.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrevious.Location = new System.Drawing.Point(541, 566);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(103, 38);
            this.btnPrevious.TabIndex = 5;
            this.btnPrevious.Text = "上一步";
            this.btnPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // mapPanel
            // 
            this.mapPanel.Controls.Add(this.txtdetail);
            this.mapPanel.Controls.Add(this.detail);
            this.mapPanel.Controls.Add(this.txtSystemName);
            this.mapPanel.Controls.Add(this.systemName);
            this.mapPanel.Controls.Add(this.dataTree);
            this.mapPanel.Location = new System.Drawing.Point(13, 165);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(749, 383);
            this.mapPanel.TabIndex = 7;
            // 
            // txtdetail
            // 
            this.txtdetail.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtdetail.Location = new System.Drawing.Point(335, 126);
            this.txtdetail.Multiline = true;
            this.txtdetail.Name = "txtdetail";
            this.txtdetail.Size = new System.Drawing.Size(411, 254);
            this.txtdetail.TabIndex = 4;
            // 
            // detail
            // 
            this.detail.AutoSize = true;
            this.detail.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.detail.Location = new System.Drawing.Point(332, 106);
            this.detail.Name = "detail";
            this.detail.Size = new System.Drawing.Size(32, 17);
            this.detail.TabIndex = 3;
            this.detail.Text = "描述";
            // 
            // txtSystemName
            // 
            this.txtSystemName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSystemName.Location = new System.Drawing.Point(335, 37);
            this.txtSystemName.Multiline = true;
            this.txtSystemName.Name = "txtSystemName";
            this.txtSystemName.Size = new System.Drawing.Size(411, 29);
            this.txtSystemName.TabIndex = 2;
            // 
            // systemName
            // 
            this.systemName.AutoSize = true;
            this.systemName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.systemName.Location = new System.Drawing.Point(332, 16);
            this.systemName.Name = "systemName";
            this.systemName.Size = new System.Drawing.Size(56, 17);
            this.systemName.TabIndex = 1;
            this.systemName.Text = "系统名称";
            // 
            // dataTree
            // 
            this.dataTree.CheckBoxes = true;
            this.dataTree.Location = new System.Drawing.Point(3, 3);
            this.dataTree.Name = "dataTree";
            this.dataTree.Size = new System.Drawing.Size(306, 377);
            this.dataTree.TabIndex = 0;
            // 
            // donepanel
            // 
            this.donepanel.Controls.Add(this.succesBtn);
            this.donepanel.Location = new System.Drawing.Point(3, 3);
            this.donepanel.Name = "donepanel";
            this.donepanel.Size = new System.Drawing.Size(746, 380);
            this.donepanel.TabIndex = 0;
            // 
            // succesBtn
            // 
            this.succesBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.succesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.succesBtn.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.succesBtn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.succesBtn.Location = new System.Drawing.Point(254, 282);
            this.succesBtn.Name = "succesBtn";
            this.succesBtn.Size = new System.Drawing.Size(268, 88);
            this.succesBtn.TabIndex = 10;
            this.succesBtn.Text = "上传中。。。";
            this.succesBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.succesBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.succesBtn.UseVisualStyleBackColor = true;
            // 
            // scanbtn
            // 
            this.scanbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.scanbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scanbtn.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scanbtn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.scanbtn.Image = global::Data.Upload.Repeater.Properties.Resources.scanMeter;
            this.scanbtn.Location = new System.Drawing.Point(258, 271);
            this.scanbtn.Name = "scanbtn";
            this.scanbtn.Size = new System.Drawing.Size(270, 88);
            this.scanbtn.TabIndex = 6;
            this.scanbtn.Text = "扫描";
            this.scanbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.scanbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.scanbtn.UseVisualStyleBackColor = true;
            this.scanbtn.Click += new System.EventHandler(this.scanbtn_Click);
            // 
            // picDone
            // 
            this.picDone.Image = global::Data.Upload.Repeater.Properties.Resources.done;
            this.picDone.Location = new System.Drawing.Point(462, 78);
            this.picDone.Name = "picDone";
            this.picDone.Size = new System.Drawing.Size(164, 64);
            this.picDone.TabIndex = 4;
            this.picDone.TabStop = false;
            // 
            // picMap
            // 
            this.picMap.Image = global::Data.Upload.Repeater.Properties.Resources.map;
            this.picMap.Location = new System.Drawing.Point(298, 78);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(164, 64);
            this.picMap.TabIndex = 3;
            this.picMap.TabStop = false;
            // 
            // picScan
            // 
            this.picScan.Image = global::Data.Upload.Repeater.Properties.Resources.scan;
            this.picScan.Location = new System.Drawing.Point(134, 78);
            this.picScan.Name = "picScan";
            this.picScan.Size = new System.Drawing.Size(164, 64);
            this.picScan.TabIndex = 2;
            this.picScan.TabStop = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 616);
            this.Controls.Add(this.mapPanel);
            this.Controls.Add(this.scanbtn);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.picDone);
            this.Controls.Add(this.picMap);
            this.Controls.Add(this.picScan);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.menuPanel);
            this.Controls.Add(this.donepanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.Text = "mainForm";
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.mapPanel.ResumeLayout(false);
            this.mapPanel.PerformLayout();
            this.donepanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.PictureBox picScan;
        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.PictureBox picDone;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button scanbtn;
        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.Label systemName;
        private System.Windows.Forms.TreeView dataTree;
        private System.Windows.Forms.TextBox txtSystemName;
        private System.Windows.Forms.Panel donepanel;
        private System.Windows.Forms.Label detail;
        private System.Windows.Forms.TextBox txtdetail;
        private System.Windows.Forms.Button succesBtn;
    }
}

