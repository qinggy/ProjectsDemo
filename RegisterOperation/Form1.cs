using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace ReportRegistor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Register();
        }

        private void Register()
        {
            try
            {
                RegistryKey software = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ReportGenerator", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);
                if (software == null)
                {
                    var baseNode = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);
                    software = baseNode.CreateSubKey("ReportGenerator");
                }

                software.SetValue("IsEnable", "1");
                software.SetValue("Expiration", Encryption.EncryptDES(this.dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                software.SetValue("ServiceURL", "localhost");
                MessageBox.Show("注册成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog(string.Empty, e);
                MessageBox.Show("注册失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
