using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleWebServer
{
    public class TextBoxValueSetting
    {
        private TextBox textBox;

        public TextBox TextBox
        {
            get { return textBox; }
            set { textBox = value; }
        }

        private string textBoxVal;

        public TextBoxValueSetting(TextBox textBox, string textBoxVal)
        {
            this.textBox = textBox;
            this.textBoxVal = textBoxVal;
        }

        public void SetText()
        {
            textBox.Text = this.textBoxVal;
        }


    }
}
