namespace SmartReportTool
{
    partial class about
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(about));
            this.panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel.Controls.Add(this.closebtn);
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(455, 34);
            this.panel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "工具信息";
            // 
            // closebtn
            // 
            this.closebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closebtn.BackgroundImage")));
            this.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Location = new System.Drawing.Point(426, 1);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(29, 25);
            this.closebtn.TabIndex = 2;
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // about
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 323);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "about";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "about";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closebtn;
    }
}