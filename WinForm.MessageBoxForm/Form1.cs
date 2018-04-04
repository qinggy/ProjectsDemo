using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForm.MessageBoxForm
{
    public partial class Form1 : BorderNoneForm
    {
        //private const int WM_NCHITTEST = 0x84;
        //private const int HTCLIENT = 0x1;
        //private const int HTCAPTION = 0x2;

        public Form1()
        {
            InitializeComponent();
            SetShadow();
        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            MessageBoxForm form = new MessageBoxForm();
            form.ShowDialog();
        }

        //首先必须了解Windows的消息传递机制，当有鼠标活动消息时， 
        //系统发送WM_NCHITTEST 消息给窗体作为判断消息发生地的根据。 nchittest
        //假如你点击的是标题栏，窗体收到的消息值就是 HTCAPTION ， 
        //同样地，若接受到的消息是 HTCLIENT，说明用户点击的是客户区，也就是鼠标消息发生在客户区。 

        //重写窗体，使窗体可以不通过自带标题栏实现移动
        //protected override void WndProc(ref Message m)
        //{
        //    //当重载窗体的 WndProc 方法时，可以截获 WM_NCHITTEST 消息并改写该消息， 
        //    //当判断鼠标事件发生在客户区时，改写该消息，发送 HTCAPTION 给窗体， 
        //    //这样，窗体收到的消息就时 HTCAPTION ，在客户区通过鼠标来拖动窗体就如同通过标题栏来拖动一样。 
        //    //注意：当你重载 WndProc 并改写鼠标事件后，整个窗体的鼠标事件也就随之改变了。 
        //    switch (m.Msg)
        //    {
        //        case WM_NCHITTEST:
        //            base.WndProc(ref m);
        //            if ((int)m.Result == HTCLIENT)
        //                m.Result = (IntPtr)HTCAPTION;
        //            return;
        //    }
        //    base.WndProc(ref m);
        //}


    }
}
