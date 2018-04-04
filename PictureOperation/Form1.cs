using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PicZipDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog()
            {
                Filter = "图片文件(*.jpg,*.bmp,*.png)|*.jpg;*.bmp;*.png",
                Multiselect = false,
                RestoreDirectory = true
            };
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileDialog.FileName);
                if (fileInfo.Length > 10485760)
                {
                    MessageBox.Show("为了保证系统性能，图片大小请不要超过10M");
                    return;
                }

                SetGoodImage(fileDialog.FileName, @"C:\test\1.jpg", 1228, 2482, 60);
            }
        }

        public static void SetGoodImage(string fileName, string newFile, int maxHeight, int maxWidth, long qualitys)
        {
            if (qualitys == 0)
            {
                qualitys = 80;
            }
            using (System.Drawing.Image img = System.Drawing.Image.FromFile(fileName))
            {
                System.Drawing.Imaging.ImageFormat
                thisFormat = img.RawFormat;
                System.Drawing.Size newSize = new System.Drawing.Size(maxWidth, maxHeight);
                Bitmap outBmp = new Bitmap(newSize.Width, newSize.Height);
                Graphics g = Graphics.FromImage(outBmp);
                // 设置画布的描绘质量
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, newSize.Width, newSize.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                g.Dispose();
                // 以下代码为保存图片时,设置压缩质量
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = qualitys;
                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象.
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICI = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x];
                        //设置JPEG编码
                        break;
                    }
                }
                if (jpegICI != null)
                {
                    outBmp.Save(newFile, jpegICI, encoderParams);
                }
                else
                {
                    outBmp.Save(newFile, thisFormat);
                }
                img.Dispose();
                outBmp.Dispose();
            }
        }
    }
}
