using System;
using System.IO;
using System.Xml;

/// <summary> 
/// 服务安装配置帮助类 
/// </summary> 
internal class SettingHelper : IDisposable
{
    #region 私有成员
    private string _ServiceName;
    private string _DisplayName;
    private string _Description;
    private string _ServicesDependedOn;
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
    /// 系统用于标志此服务的名称 
    /// </summary> 
    public string ServiceName
    {
        get { return _ServiceName; }
    }
    /// <summary> 
    /// 向用户标志服务的友好名称 
    /// </summary> 
    public string DisplayName
    {
        get { return _DisplayName; }
    }
    /// <summary> 
    /// 服务的说明 
    /// </summary> 
    public string Description
    {
        get { return _Description; }
    }

    /// <summary>
    /// 依赖服务
    /// </summary>
    public string ServicesDependedOn
    {
        get { return _ServicesDependedOn; }
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
        string xmlfile = root.Remove(root.LastIndexOf('\\') + 1) + "ServiceSetting.xml";
        if (File.Exists(xmlfile))
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlfile);
                XmlNode xn = doc.SelectSingleNode("Settings/ServiceName");
                _ServiceName = xn.InnerText;
                xn = doc.SelectSingleNode("Settings/DisplayName");
                _DisplayName = xn.InnerText;
                xn = doc.SelectSingleNode("Settings/Description");
                _Description = xn.InnerText;
                xn = doc.SelectSingleNode("Settings/ServicesDependedOn");
                _ServicesDependedOn = string.IsNullOrWhiteSpace(xn.InnerText) ? "MSSQLSERVER" : xn.InnerText;
                doc = null;
            }
            catch (Exception ex)
            {
                throw new FileLoadException("服务名称配置文件 ServiceSetting.xml加载读取失败\n\r" + ex.Message);
            }
        }
        else
        {
            throw new FileNotFoundException("未能找到服务名称配置文件 ServiceSetting.xml！");
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
                _ServiceName = null;
                _DisplayName = null;
                _Description = null;
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