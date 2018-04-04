using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Esd.EnergyPec.UpdaterOnline
{
    public class SettingHelper : IDisposable
    {

        #region 私有成员
        private string _ServerPath;
        private int _FileLength = 0;
        private string _DestinationFile;
        #endregion

        #region 构造函数
        /// <summary> 
        /// 初始化服务配置帮助类 
        /// </summary> 
        public SettingHelper()
        {
            InitSettings();
        }
        #endregion

        #region 属性
        /// <summary> 
        /// 更新包服务器路径
        /// </summary> 
        public string ServerPath
        {
            get { return _ServerPath; }
        }
        /// <summary> 
        /// 更新包大小 字节
        /// </summary> 
        public int FileLength
        {
            get { return _FileLength; }
        }
        /// <summary> 
        /// 当前程序安装目录 
        /// </summary> 
        public string DestinationFile
        {
            get { return _DestinationFile; }
        }

        #endregion

        #region 私有方法

        #region 初始化服务配置信息
        /// <summary> 
        /// 初始化服务配置信息 
        /// </summary> 
        private void InitSettings()
        {
            string root = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string xmlfile = root.Remove(root.LastIndexOf('\\') + 1) + "Setting.xml";
            if (File.Exists(xmlfile))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(xmlfile);
                    XmlNode xn = doc.SelectSingleNode("Settings/ServerPath");
                    _ServerPath = xn.InnerText;
                    xn = doc.SelectSingleNode("Settings/FileLength");
                    _FileLength = int.Parse(xn.InnerText);
                    xn = doc.SelectSingleNode("Settings/DestinationFile");
                    _DestinationFile = xn.InnerText;
                    doc = null;
                }
                catch (Exception ex)
                {
                    throw new FileLoadException("服务名称配置文件 Setting.xml加载读取失败\n\r" + ex.Message);
                }
            }
            else
            {
                throw new FileNotFoundException("未能找到服务名称配置文件 Setting.xml！");
            }
        }
        #endregion

        #endregion

        #region IDisposable 成员
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //managed dispose 
                    _ServerPath = null;
                    _FileLength = 0;
                    _DestinationFile = null;
                }
                //unmanaged dispose 
            }
            disposed = true;
        }

        ~SettingHelper()
        {
            Dispose(false);
        }
        #endregion
    }
}
