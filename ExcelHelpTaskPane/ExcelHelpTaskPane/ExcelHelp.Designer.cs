namespace ExcelHelpTaskPane
{
    partial class ExcelHelp
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.helpPageWb = new System.Windows.Forms.WebBrowser();
            this.toolStripBackbtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripForwardbtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripRefreshbtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripHomebtn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBackbtn,
            this.toolStripForwardbtn,
            this.toolStripRefreshbtn,
            this.toolStripHomebtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(315, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // helpPageWb
            // 
            this.helpPageWb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpPageWb.Location = new System.Drawing.Point(0, 25);
            this.helpPageWb.MinimumSize = new System.Drawing.Size(20, 20);
            this.helpPageWb.Name = "helpPageWb";
            this.helpPageWb.Size = new System.Drawing.Size(315, 300);
            this.helpPageWb.TabIndex = 2;
            this.helpPageWb.Url = new System.Uri("", System.UriKind.Relative);
            this.helpPageWb.NewWindow += new System.ComponentModel.CancelEventHandler(this.helpPageWb_NewWindow);
            // 
            // toolStripBackbtn
            // 
            this.toolStripBackbtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBackbtn.Image = global::ExcelHelpTaskPane.Properties.Resources.back;
            this.toolStripBackbtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBackbtn.Name = "toolStripBackbtn";
            this.toolStripBackbtn.Size = new System.Drawing.Size(23, 22);
            this.toolStripBackbtn.Text = "toolStripButton1";
            this.toolStripBackbtn.Click += new System.EventHandler(this.toolStripBackbtn_Click);
            // 
            // toolStripForwardbtn
            // 
            this.toolStripForwardbtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripForwardbtn.Image = global::ExcelHelpTaskPane.Properties.Resources.next;
            this.toolStripForwardbtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripForwardbtn.Name = "toolStripForwardbtn";
            this.toolStripForwardbtn.Size = new System.Drawing.Size(23, 22);
            this.toolStripForwardbtn.Text = "toolStripButton2";
            this.toolStripForwardbtn.Click += new System.EventHandler(this.toolStripForwardbtn_Click);
            // 
            // toolStripRefreshbtn
            // 
            this.toolStripRefreshbtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRefreshbtn.Image = global::ExcelHelpTaskPane.Properties.Resources.refresh;
            this.toolStripRefreshbtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRefreshbtn.Name = "toolStripRefreshbtn";
            this.toolStripRefreshbtn.Size = new System.Drawing.Size(23, 22);
            this.toolStripRefreshbtn.Text = "toolStripButton3";
            this.toolStripRefreshbtn.Click += new System.EventHandler(this.toolStripRefreshbtn_Click);
            // 
            // toolStripHomebtn
            // 
            this.toolStripHomebtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripHomebtn.Image = global::ExcelHelpTaskPane.Properties.Resources.home;
            this.toolStripHomebtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripHomebtn.Name = "toolStripHomebtn";
            this.toolStripHomebtn.Size = new System.Drawing.Size(23, 22);
            this.toolStripHomebtn.Text = "toolStripButton4";
            this.toolStripHomebtn.Click += new System.EventHandler(this.toolStripHomebtn_Click);
            // 
            // ExcelHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.helpPageWb);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ExcelHelp";
            this.Size = new System.Drawing.Size(315, 325);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBackbtn;
        private System.Windows.Forms.ToolStripButton toolStripForwardbtn;
        private System.Windows.Forms.ToolStripButton toolStripRefreshbtn;
        private System.Windows.Forms.ToolStripButton toolStripHomebtn;
        private System.Windows.Forms.WebBrowser helpPageWb;
    }
}
