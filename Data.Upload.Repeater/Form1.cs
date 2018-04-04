using Data.Upload.Repeater.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Data.Upload.Repeater
{
    public partial class mainForm : Form
    {
        private bool isMouseDown = false;
        private Point FormLocation;     //form的location
        private Point mouseOffset;      //鼠标的按下位置
        private int currentStep = 1;
        private bool ifLoading = false;

        public mainForm()
        {
            InitializeComponent();
            btnPrevious.Visible = false;
            mapPanel.Visible = false;
            donepanel.Visible = false;
        }

        private void menuPanel_MouseMove(object sender, MouseEventArgs e)
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

        private void menuPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void menuPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentStep >= 1)
            {
                switch (currentStep)
                {
                    case 1:
                        picScan.Image = Resources.scan;
                        btnPrevious.Visible = false;
                        scanbtn.Visible = true;
                        mapPanel.Visible = false;
                        break;
                    case 2:
                        picMap.Image = Resources.map;
                        nextBtn.Text = "下一步";
                        mapPanel.Visible = true;
                        donepanel.Visible = false;
                        //进入map模式
                        LoadTreeViewData();
                        break;
                    case 3:
                        donepanel.Visible = true;
                        picDone.Image = Resources.done;
                        nextBtn.Text = "下一步";
                        succesBtn.Text = "上传中。。。";
                        succesBtn.Image = null;
                        break;
                    default:
                        break;
                }

                if (currentStep > 1)
                {
                    currentStep--;
                }

            }

        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (currentStep <= 3)
            {
                switch (currentStep)
                {
                    case 1:
                        picScan.Image = Resources.scanDone;
                        btnPrevious.Visible = true;
                        scanbtn.Visible = false;
                        mapPanel.Visible = true;
                        //进入map模式
                        LoadTreeViewData();
                        break;
                    case 2:
                        picMap.Image = Resources.mapdone;
                        nextBtn.Text = "完成";
                        mapPanel.Visible = false;
                        this.donepanel.Visible = true;
                        nextBtn.Enabled = false;
                        setInterval();
                        break;
                    case 3:
                        //picDone.Image = Resources.doneyello;
                        //succesBtn.Text = "完成";
                        //succesBtn.Image = Resources.done1;
                        this.Close();
                        break;
                    default:
                        break;
                }
                if (currentStep < 3)
                {
                    currentStep++;
                }

            }

        }

        private void setInterval()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.AutoReset = false;
            timer.Interval = 5000;//执行间隔时间,单位为毫秒  
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed);
        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke((Action)delegate()
            {
                picDone.Image = Resources.doneyello;
                succesBtn.Text = "完成";
                nextBtn.Enabled = true;
                succesBtn.Image = Resources.done1;
            });
        }

        private void scanbtn_Click(object sender, EventArgs e)
        {
            //loading效果
            scanbtn.Visible = false;
            currentStep = 2;
            picScan.Image = Resources.scanDone;
            btnPrevious.Visible = true;
            mapPanel.Visible = true;
            //进入map模式
            LoadTreeViewData();

        }

        private void LoadTreeViewData()
        {
            if (!ifLoading)
            {
                //TreeNode node;

                var node = dataTree.Nodes.Add("大型商业酒店");
                var children1 = node.Nodes.Add("空调系统");
                children1.Nodes.Add("大厅中央空调");
                children1.Nodes.Add("Vip室中央空调");
                children1.Nodes.Add("厨房中央空调");
                children1.Nodes.Add("普通客房中央空调");
                var children2 = node.Nodes.Add("电力监控");
                children2.Nodes.Add("大厅总电表");
                children2.Nodes.Add("Vip室总电表");
                children2.Nodes.Add("厨房总电表");
                children2.Nodes.Add("普通客房总电表");
                node.Nodes.Add("抄表系统");
                node.Nodes.Add("风机盘管");

                ifLoading = true;
            }
        }

    }
}
