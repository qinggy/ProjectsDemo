<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OAuth2Demo.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 100px">
                    UserName:</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Password:</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /></td>
                <td style="width: 100px">
                    <asp:Button ID="btnReg" runat="server" Text="Reg" OnClick="btnReg_Click" /></td>
            </tr>
        </table>
        <br />
        <br />
    
        <asp:Literal ID="litOtherLoginInfo" runat="server"></asp:Literal>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
