using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esd.EnergyPec.DataUpdaterApp
{
    public partial class DataUpdater : Form
    {
        #region 属性

        private bool isMouseDown = false;
        private Point FormLocation;     //form的location
        private Point mouseOffset;      //鼠标的按下位置
        private string sqlFilePath = "";
        private long fileLength = 0;

        #endregion

        #region 事件

        public DataUpdater()
        {
            InitializeComponent();
        }

        #region 窗体移动

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
                Point pt = Control.MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;

                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
                Point pt = Control.MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;

                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void DataUpdater_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }

        private void DataUpdater_MouseMove(object sender, MouseEventArgs e)
        {
            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
                Point pt = Control.MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;

                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }
        }

        private void DataUpdater_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
        #endregion

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            this.label2.Visible = false;
            this.boxpanel.Visible = false;
            this.filePath.Visible = false;
            this.button1.Visible = false;
            this.progressBar.Visible = true;
            this.prompt.Visible = true;
            this.progress.Visible = true;

            this.progressBar.Maximum = (int)fileLength;
            this.UpdateBtn.Enabled = false;

            Task.Factory.StartNew(new Action(() =>
            {

            }));
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog()
            {
                Filter = "数据库文件(*.sql)|*.sql",
                Multiselect = false,
                RestoreDirectory = true
            };
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sqlFilePath = fileDialog.FileName;
                this.filePath.Text = sqlFilePath;
                FileInfo sqlFile = new FileInfo(sqlFilePath);
                fileLength = sqlFile.Length;

                this.findBtn.Visible = false;
                this.UpdateBtn.Visible = true;
                this.button1.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Visible = false;
            this.UpdateBtn.Visible = false;
            this.findBtn.Visible = true;
            this.filePath.Text = "";
            fileLength = 0;
            sqlFilePath = "";
        }

        #endregion
    }
}
