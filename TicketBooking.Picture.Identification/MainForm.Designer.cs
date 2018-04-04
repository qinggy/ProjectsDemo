namespace TicketBooking.Picture.Identification
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbPsd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.btnBook = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbarcode = new System.Windows.Forms.TextBox();
            this.barcode = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barcode)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(57, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.closeBtn);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 70);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuPanel_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.menuPanel_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.menuPanel_MouseUp);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Location = new System.Drawing.Point(721, 0);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(27, 33);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "X";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(164, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = " V 1.0.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(38, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 39);
            this.label2.TabIndex = 0;
            this.label2.Text = "抢票软件";
            // 
            // tbUserName
            // 
            this.tbUserName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbUserName.Location = new System.Drawing.Point(121, 101);
            this.tbUserName.Multiline = true;
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(201, 27);
            this.tbUserName.TabIndex = 1;
            // 
            // tbPsd
            // 
            this.tbPsd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPsd.Location = new System.Drawing.Point(121, 157);
            this.tbPsd.Multiline = true;
            this.tbPsd.Name = "tbPsd";
            this.tbPsd.PasswordChar = '*';
            this.tbPsd.Size = new System.Drawing.Size(201, 27);
            this.tbPsd.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(57, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "密   码";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(57, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 22);
            this.label5.TabIndex = 5;
            this.label5.Text = "记住密码";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(225, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 22);
            this.label6.TabIndex = 6;
            this.label6.Text = "自动抢票";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(137, 239);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(305, 239);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // btnBook
            // 
            this.btnBook.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnBook.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBook.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnBook.Location = new System.Drawing.Point(273, 310);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(244, 51);
            this.btnBook.TabIndex = 10;
            this.btnBook.Text = "手动刷票";
            this.btnBook.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.Location = new System.Drawing.Point(391, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(328, 196);
            this.panel2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tbarcode);
            this.panel3.Controls.Add(this.barcode);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(392, 73);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(339, 227);
            this.panel3.TabIndex = 0;
            // 
            // tbarcode
            // 
            this.tbarcode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbarcode.Location = new System.Drawing.Point(130, 156);
            this.tbarcode.Multiline = true;
            this.tbarcode.Name = "tbarcode";
            this.tbarcode.PasswordChar = '*';
            this.tbarcode.Size = new System.Drawing.Size(157, 27);
            this.tbarcode.TabIndex = 13;
            // 
            // barcode
            // 
            this.barcode.Image = ((System.Drawing.Image)(resources.GetObject("barcode.Image")));
            this.barcode.Location = new System.Drawing.Point(93, 28);
            this.barcode.Name = "barcode";
            this.barcode.Size = new System.Drawing.Size(194, 78);
            this.barcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.barcode.TabIndex = 0;
            this.barcode.TabStop = false;
            this.barcode.Click += new System.EventHandler(this.barcode_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(65, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 22);
            this.label7.TabIndex = 12;
            this.label7.Text = "验证码";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 382);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnBook);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbPsd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barcode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPsd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox barcode;
        private System.Windows.Forms.TextBox tbarcode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button closeBtn;
    }
}

