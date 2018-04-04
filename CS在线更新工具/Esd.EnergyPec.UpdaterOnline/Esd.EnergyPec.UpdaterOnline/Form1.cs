using Esd.EnergyPec.CommonImp;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esd.EnergyPec.UpdaterOnline
{
    public partial class Form1 : Form
    {
        #region 属性

        private bool isMouseDown = false;
        private Point FormLocation;     //form的location
        private Point mouseOffset;      //鼠标的按下位置
        private string SourceFile = "";
        public string ServerPath = "";
        public int FileLength = 0;
        public string DestinationFile = "";

        public string GetFileName
        {
            get
            {
                if (string.IsNullOrEmpty(ServerPath))
                {
                    return "";
                }

                return ServerPath.Substring(ServerPath.LastIndexOf(@"\"));
            }
        } 
        #endregion

        #region 事件

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
        }

        //下载的过程
        void Form1_Load(object sender, EventArgs e)
        {
            using (SettingHelper setting = new SettingHelper())
            {
                ServerPath = setting.ServerPath;
                FileLength = setting.FileLength;
                DestinationFile = setting.DestinationFile;
            }

            Task.Factory.StartNew(new Action(() =>
            {
                this.Invoke((EventHandler)delegate
                {
                    this.prompt.Text = "下载中，请稍后 ......";
                    this.installer.Enabled = false;
                    this.progressBar.Maximum = FileLength;
                });

                try
                {
                    SourceFile = Path.GetTempPath();
                    SourceFile += GetFileName;
                    if (!File.Exists(SourceFile))
                    {
                        File.Create(SourceFile);
                    }

                    var factory = ServiceFactory.CreateService<ICheckUpdateService>();
                    Stream stream = factory.DownLoadFile(ServerPath);
                    if (stream != null && stream.CanRead)
                    {
                        using (FileStream fs = new FileStream(SourceFile, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            const int bufferLength = 4096;
                            byte[] myBuffer = new byte[bufferLength];
                            int count;
                            while ((count = stream.Read(myBuffer, 0, bufferLength)) > 0)
                            {
                                fs.Write(myBuffer, 0, count);

                                this.Invoke((EventHandler)delegate
                                {
                                    this.progressBar.Value += count;
                                    this.progress.Text = "下载进度：" + Math.Round((double)this.progressBar.Value / FileLength, 2) * 100 + "%";
                                });
                            }
                            fs.Close();
                            stream.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((EventHandler)delegate
                    {
                        this.prompt.Location = new Point(23, 148);
                        this.prompt.Text = "网络异常，更新包下载失败！";
                    });
                    throw ex;
                }

                this.Invoke((EventHandler)delegate
                {
                    this.prompt.Text = "下载完成，请点击安装！";
                    this.installer.Enabled = true;
                });
            }));


        }

        #region Form Move
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
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

        #endregion

        //解压缩安装的过程
        private void installer_Click(object sender, EventArgs e)
        {
            string applicationName = ConfigurationManager.AppSettings.Get("ApplicationName").ToString();
            this.progress.Text = "安装进度：0%";
            this.prompt.Text = "安装进行中,请稍后 ......";
            this.progressBar.Value = 0;

            Task.Factory.StartNew(new Action(() =>
            {
                ZipInputStream zipStream = null;
                FileStream fileStream = null;
                ZipEntry entry = null;
                string fileName = "";

                try
                {
                    //判断当前程序是否运行，如果运行则关闭进程。
                    List<Process> currentProcess = Process.GetProcessesByName(applicationName).ToList();
                    if (currentProcess.Count > 0)
                    {
                        currentProcess.ForEach(a => a.Kill());
                    }

                    //ZipOrDeCompressHelper.DeCompressFile(SourceFile, DestinationFile);
                    if (!File.Exists(SourceFile)) throw new FileNotFoundException();
                    if (!Directory.Exists(DestinationFile))
                    {
                        Directory.CreateDirectory(DestinationFile);
                    }

                    zipStream = new ZipInputStream(File.OpenRead(SourceFile));
                    while ((entry = zipStream.GetNextEntry()) != null)
                    {
                        if (entry.Name != string.Empty)
                        {
                            fileName = Path.Combine(DestinationFile, entry.Name);

                            //判断文件是否存在
                            if (fileName.EndsWith("/") || fileName.EndsWith("\\"))
                            {
                                Directory.CreateDirectory(fileName);
                                continue;
                            }

                            fileStream = File.Create(fileName);
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = zipStream.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    fileStream.Write(data, 0, size);
                                    this.Invoke((EventHandler)delegate
                                    {
                                        this.progressBar.Value += size;
                                        this.progress.Text = "安装进度：" + Math.Round((double)this.progressBar.Value / FileLength, 2) * 100 + "%";
                                    });
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((EventHandler)delegate
                    {
                        this.prompt.Text = "网络异常，升级失败，请稍后重试！";
                    });
                    throw ex;
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream = null;
                    }
                    if (entry != null)
                    {
                        entry = null;
                    }
                    if (zipStream != null)
                    {
                        zipStream.Close();
                        zipStream = null;
                    }

                    this.Invoke((EventHandler)delegate
                    {
                        this.progress.Text = "安装完成！";
                        this.progressBar.Value = FileLength;
                        this.prompt.Text = "能源管理平台，升级成功！";
                        this.installer.Visible = false;
                        this.closebtn.Visible = true;
                    });

                    GC.Collect();
                    GC.Collect(1);
                }
            }));

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
