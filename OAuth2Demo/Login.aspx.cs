using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace OAuth2Demo
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litOtherLoginInfo.Text = OAuth2.UI.GetHtml();
            if (IsPostBack)
            {
                return;
            }
            OAuth2.OAuth2Base ob = OAuth2.OAuth2Factory.Current;//获取当前的授权类型，如果成功，则会缓存到Session中。
            if (ob != null) //说明用户点击了授权，并跳回登陆界面来
            {
                string account = string.Empty;
                if (ob.Authorize(out account))//检测是否授权成功，并返回绑定的账号（具体是绑定ID还是用户名，你的选择）
                {
                    if (!string.IsNullOrEmpty(account))//已绑定账号，直接用该账号设置登陆。
                    {
                        UserLogin ul = new UserLogin();
                        if (ul.Login(account))
                        {
                            Response.Redirect("/");
                        }
                    }
                    else // 未绑定账号，引导提示用户绑定账号。
                    {
                        Response.Write(ob.nickName + " 首次使用需要绑定网站账号，请登陆或注册新账号");
                    }
                }

            }
            else // 读取授权失败。
            {
                //提示用户重试，或改用其它社区方法登陆。
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserLogin ul = new UserLogin();
            if (ul.Login(txtUserName.Text, txtPassword.Text))
            {
                //登陆成功后，增加两行逻辑，检测是否绑定了账号
                OAuth2.OAuth2Base ob = OAuth2.OAuth2Factory.SessionOAuth;
                if (ob != null && !string.IsNullOrEmpty(ob.openID))
                {
                    ob.SetBindAccount(txtUserName.Text);
                }

                Response.Redirect("/");//登陆后跳转
            }
        }

        protected void btnReg_Click(object sender, EventArgs e)
        {
            UserLogin ul = new UserLogin();
            if (ul.Reg(txtUserName.Text, txtPassword.Text))
            {
                //注册成功后，增加两行逻辑，检测是否绑定了账号 -- 代码和登陆的一样。
                OAuth2.OAuth2Base ob = OAuth2.OAuth2Factory.SessionOAuth;
                if (ob != null && !string.IsNullOrEmpty(ob.openID))
                {
                    ob.SetBindAccount(txtUserName.Text);
                }

                Response.Redirect("/");//登陆后跳转
            }
        }
    }

    public class UserLogin
    {
        /// <summary>
        /// 授权时直接用用户名登陆。
        /// </summary>
        public bool Login(string userName)
        {
            return true;
        }
        public bool Login(string userName, string password)
        {
            return true;
        }
        public bool Reg(string userName, string password)
        {
            return true;
        }
    }
}
