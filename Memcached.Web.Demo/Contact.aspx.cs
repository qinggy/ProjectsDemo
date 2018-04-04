using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Memcached.Web.Demo
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Text = Session["Name"].ToString();
        }
    }
}