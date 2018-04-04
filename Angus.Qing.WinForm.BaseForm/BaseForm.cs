using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Angus.Qing.WinForm
{
    public class BaseForm : Form
    {
        /*---None下的拖动*/
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        /*---None下的边框阴影效果*/
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        public BaseForm()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            SetShadow();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= ClassStyles.CS_DROPSHADOW;
                return cp;
            }
        }

        protected void SetShadow()
        {
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
        }

        //首先必须了解Windows的消息传递机制，当有鼠标活动消息时， 
        //系统发送WM_NCHITTEST 消息给窗体作为判断消息发生地的根据。 nchittest
        //假如你点击的是标题栏，窗体收到的消息值就是 HTCAPTION ， 
        //同样地，若接受到的消息是 HTCLIENT，说明用户点击的是客户区，也就是鼠标消息发生在客户区。 

        //重写窗体，使窗体可以不通过自带标题栏实现移动
        protected override void WndProc(ref Message m)
        {
            //当重载窗体的 WndProc 方法时，可以截获 WM_NCHITTEST 消息并改写该消息， 
            //当判断鼠标事件发生在客户区时，改写该消息，发送 HTCAPTION 给窗体， 
            //这样，窗体收到的消息就时 HTCAPTION ，在客户区通过鼠标来拖动窗体就如同通过标题栏来拖动一样。 
            //注意：当你重载 WndProc 并改写鼠标事件后，整个窗体的鼠标事件也就随之改变了。 
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if ((int)m.Result == HTCLIENT)
                        m.Result = (IntPtr)HTCAPTION;
                    return;
            }

            base.WndProc(ref m);
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //base.OnPaint(e);
        //    //e.Graphics.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);
        //    //base.OnPaint(e);
        //}
    }

    public static class ClassStyles
    {
        public static readonly Int32
        CS_BYTEALIGNCLIENT = 0x1000,
        CS_BYTEALIGNWINDOW = 0x2000,
        CS_CLASSDC = 0x0040,
        CS_DBLCLKS = 0x0008,
        CS_DROPSHADOW = 0x00020000,
        CS_GLOBALCLASS = 0x4000,
        CS_HREDRAW = 0x0002,
        CS_NOCLOSE = 0x0200,
        CS_OWNDC = 0x0020,
        CS_PARENTDC = 0x0080,
        CS_SAVEBITS = 0x0800,
        CS_VREDRAW = 0x0001;
    }
}
