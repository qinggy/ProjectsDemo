using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ExcelHelpTaskPane
{
    public partial class ExcelHelp : UserControl
    {
        // 指定一个主页，这里指定Excel帮助页面为主页
        string homeURL = "http://office.microsoft.com/client/helphome14.aspx?NS=EXCEL&VERSION=14&LCID=2052&SYSLCID=2052&UILCID=2052&AD=1&tl=2";
        
        public ExcelHelp(string url)
        {
            InitializeComponent();

            if (url == string.Empty)
            {         
                helpPageWb.Navigate(homeURL);
            }
            else
            {
                helpPageWb.Navigate(url);
            }
        }

        #region 基本功能

        // 后退
        private void toolStripBackbtn_Click(object sender, EventArgs e)
        {
            helpPageWb.GoBack();
        }

        // 前进
        private void toolStripForwardbtn_Click(object sender, EventArgs e)
        {
            helpPageWb.GoForward();
        }

        private void toolStripRefreshbtn_Click(object sender, EventArgs e)
        {
            helpPageWb.Refresh();
        }

        private void toolStripHomebtn_Click(object sender, EventArgs e)
        {
            helpPageWb.Navigate(homeURL);
        }

        #endregion 

        #region WebBrowser事件
        // 单击程序中某个链接后会打开新窗口，此时就会执行NewWinow事件中的代码
        // 通过此事件中的代码将打开新窗口中内容添加到本软件的webBrowser控件中显示
        // 实现网页用我们自定义的浏览器显示
        private void helpPageWb_NewWindow(object sender, CancelEventArgs e)
        {
            string newUrl = helpPageWb.StatusText;
            ExcelHelp newwebform = new ExcelHelp(newUrl);
            newwebform.Show();

            // 使其他浏览器无法捕获此事件
            // 阻止了其他浏览器显示网页，而是采用我们自定义的浏览器来显示
            e.Cancel = true;
        }
        
        #endregion
    }
}
