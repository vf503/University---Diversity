<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="colleges.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="Styles/DefaultLayout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="trLogin" runat="server">
    <%--<div class="InlineDiv" id="timer">
        </div>--%>
        <div class="InlineDiv">
            用户名：<asp:TextBox ID="txtUid" TabIndex="1" runat="server" CssClass="LoginInput"></asp:TextBox></div>
        <div class="InlineDiv">
            密码：<asp:TextBox ID="txtPwd" TabIndex="2" runat="server" CssClass="LoginInput"></asp:TextBox></div>
        <div class="InlineDiv">
            <asp:Button ID="btnOK" runat="server" Text="登录" OnClick="btnOK_Click" CssClass="LoginBtn"></asp:Button></div>
        <div class="InlineDiv">
            <input type="checkbox" value="1" name="chLogAuto" /><span class="LoginCheckText">下次自动登录</span>
        </div>
    </div>
    <div id="trExit" runat="server">
        <asp:Label ID="lblUser" runat="server"></asp:Label>
        <asp:Label ID="lblWelcome" runat="server"></asp:Label>
        <asp:Button ID="btnExit" runat="server" Text="注销" OnClick="btnExit_Click" CssClass="LoginBtn"></asp:Button>
    </div>
    <%--<div class="InlineDiv">
        <a href="#">试用申请</a></div>--%>
    </form>
</body>
</html>
