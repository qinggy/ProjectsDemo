namespace WinForm.MessageBoxForm
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
            this.showBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // showBtn
            // 
            this.showBtn.Location = new System.Drawing.Point(197, 111);
            this.showBtn.Name = "showBtn";
            this.showBtn.Size = new System.Drawing.Size(75, 23);
            this.showBtn.TabIndex = 0;
            this.showBtn.Text = "点击";
            this.showBtn.UseVisualStyleBackColor = true;
            this.showBtn.Click += new System.EventHandler(this.showBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 310);
            this.Controls.Add(this.showBtn);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button showBtn;
    }
}

