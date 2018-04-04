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
    public partial class MessageBoxForm : BorderNoneForm
    {
        public MessageBoxForm()
        {
            InitializeComponent();
        }

        private void MessageBoxForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.SteelBlue, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}
