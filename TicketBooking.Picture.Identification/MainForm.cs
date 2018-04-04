//using Angus.Qing.WinForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TicketBooking.Picture.Identification
{
    public partial class MainForm : Form
    {
        private bool isMouseDown = false;
        private Point FormLocation;     //form的location
        private Point mouseOffset;      //鼠标的按下位置
        private string CODE_URL = "http://aux.szhr.com/pasr/verify-code.action";

        public MainForm()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.tbUserName.Select();
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

        private void barcode_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(CODE_URL);
            request.Timeout = 60000;
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Bitmap bitmap = new System.Drawing.Bitmap(resp.GetResponseStream());
            barcode.Image = bitmap;
            imgdo(bitmap);
            //TesseractProcessor processor = new TesseractProcessor();
            //processor.SetPageSegMode(ePageSegMode.PSM_SINGLE_LINE);
            ////注意路径最后一个“\”是必须的！！！！否则init方法崩溃
            ////语言的版本一定要跟dll对应，我用的是3.01
            //processor.Init(Application.StartupPath + @"\tessdata301", "eng", (int)eOcrEngineMode.OEM_DEFAULT);
            //string result = processor.Recognize(bitmap);
        }


        public void imgdo(Bitmap img)
        {
            //去色
            Bitmap btp = img;
            Color c = new Color();
            int rr, gg, bb;
            for (int i = 0; i < btp.Width; i++)
            {
                for (int j = 0; j < btp.Height; j++)
                {
                    //取图片当前的像素点
                    c = btp.GetPixel(i, j);
                    rr = c.R; gg = c.G; bb = c.B;
                    //改变颜色
                    if (rr == 102 && gg == 0 && bb == 0)
                    {
                        //重新设置当前的像素点
                        btp.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                    }
                    if (rr == 153 && gg == 0 && bb == 0)
                    {
                        //重新设置当前的像素点
                        btp.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                    } if (rr == 153 && gg == 0 && bb == 51)
                    {
                        //重新设置当前的像素点
                        btp.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                    } if (rr == 153 && gg == 43 && bb == 51)
                    {
                        //重新设置当前的像素点
                        btp.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                    }
                    if (rr == 255 && gg == 255 && bb == 0)
                    {
                        //重新设置当前的像素点
                        btp.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                    }
                    if (rr == 255 && gg == 255 && bb == 51)
                    {
                        //重新设置当前的像素点
                        btp.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                    }
                }
            }
            btp.Save(@"d:\去除相关颜色.png");

            //pictureBox2.Image = Image.FromFile("d:\去除相关颜色.png");


            //灰度
            Bitmap bmphd = btp;
            for (int i = 0; i < bmphd.Width; i++)
            {
                for (int j = 0; j < bmphd.Height; j++)
                {
                    //取图片当前的像素点
                    var color = bmphd.GetPixel(i, j);

                    var gray = (int)(color.R * 0.001 + color.G * 0.700 + color.B * 0.250);

                    //重新设置当前的像素点
                    bmphd.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            bmphd.Save(@"d:\灰度.png");
            //pictureBox27.Image = Image.FromFile("d:\灰度.png");


            //二值化
            Bitmap erzhi = bmphd;
            Bitmap orcbmp;
            int nn = 3;
            int w = erzhi.Width;
            int h = erzhi.Height;
            BitmapData data = erzhi.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* p = (byte*)data.Scan0;
                byte[,] vSource = new byte[w, h];
                int offset = data.Stride - w * nn;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        vSource[x, y] = (byte)(((int)p[0] + (int)p[1] + (int)p[2]) / 3);
                        p += nn;
                    }
                    p += offset;
                }
                erzhi.UnlockBits(data);

                Bitmap bmpDest = new Bitmap(w, h, PixelFormat.Format24bppRgb);
                BitmapData dataDest = bmpDest.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                p = (byte*)dataDest.Scan0;
                offset = dataDest.Stride - w * nn;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        p[0] = p[1] = p[2] = (int)vSource[x, y] > 161 ? (byte)255 : (byte)0;
                        //p[0] = p[1] = p[2] = (int)GetAverageColor(vSource, x, y, w, h) > 50 ? (byte)255 : (byte)0;
                        p += nn;

                    }
                    p += offset;
                }
                bmpDest.UnlockBits(dataDest);

                orcbmp = bmpDest;
                orcbmp.Save(@"d:\二值化.png");
                //pictureBox29.Image = Image.FromFile("d:\二值化.png");
            }

            //OCR的值
            if (orcbmp != null)
            {
                //string result = Ocr(orcbmp);
                //label32.Text = result.Replace("n","rn").Replace("","");
            }

        }
    }
}
