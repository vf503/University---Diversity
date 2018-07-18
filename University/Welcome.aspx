<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="colleges.Welcome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body, form
        {
            margin: 0;
            padding: 0;
        }
        .TopLogin
        {
            height: 30px;
            line-height: 30px;
            overflow: hidden;
            color: #595959;
            padding: 0px 0 0 0;
            margin: 0 0 0 0;
            font-size: 12px;
            font-family: 宋体,SimSum;
            border-bottom: 1px solid #ededed;
        }
        .LoginInput
        {
            width: 105px;
            height: 16px;
            display: inline;
            border: solid #d4d4d4 1px;
            vertical-align: middle;
            position: relative;
            margin: 0 15px 0 0;
            padding: 0 0px 0 0;
            -margin: 3px 15px 0 0;
        }
        .TopLoginLeft
        {
            float: left;
            position: relative;
            height: 30px;
            width: 500px;
        }
        .TopLoginLeft a:link
        {
            text-decoration: none;
            color: #000;
            font-size: 12px;
            margin: 0 15px 0 0;
        }
        .TopLoginLeft a:visited
        {
            text-decoration: none;
            color: #000;
            font-size: 12px;
            margin: 0 15px 0 0;
        }
        .TopLoginLeft a:hover
        {
            text-decoration: underline;
            color: #000;
            font-size: 12px;
            margin: 0 15px 0 0;
        }
        .TopLoginLeft a:active
        {
            text-decoration: none;
            color: #000;
            font-size: 12px;
            margin: 0 15px 0 0;
        }
        .TopLoginBox
        {
            border: 0px;
            margin: 0px;
            padding: 0px;
            width: 430px;
            height: 26px;
        }
        #trLogin
        {
            padding: 0px 0 0 10px;
            font-size: 12px;
            font-family: SimSun;
        }
        .LoginBtn
        {
            height: 18px;
            margin: 0 10px 0 0;
            display: inline;
            border: solid #d4d4d4 1px;
        }
        #trExit
        {
            font-size: 12px;
            font-family: SimSun;
        }
        .TopLoginRight
        {
            float: right;
            position: relative;
            padding: 0 15px 0 0;
        }
        .TopLoginRight a:link
        {
            text-decoration: none;
            color: #595959;
        }
        .TopLoginRight a:visited
        {
            text-decoration: none;
            color: #595959;
        }
        .TopLoginRight a:hover
        {
            text-decoration: underline;
            color: #595959;
        }
        .TopLoginRight a:active
        {
            text-decoration: none;
            color: #595959;
        }
        .TopImage
        {
            width: 400px;
            float: left;
            position: relative;
        }
        .TopSearch
        {
            width: 430px;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            font-size: 12px;
            color: #999;
            float: right;
            position: relative;
        }
        .TopLoginBox
        {
        }
        .TopLoginLink
        {
            display: inline-block;
            float: left;
            position: relative;
            margin: 0px 0 0px 10px;
            line-height: 26px;
        }
        * + html .TopLoginLink
        {
            margin-left: 10px;
        }
        .TopLoginLink a:link
        {
            text-decoration: none;
            color: #666666;
        }
        .TopLoginLink a:visited
        {
            text-decoration: none;
            color: #666666;
        }
        .TopLoginLink a:hover
        {
            text-decoration: underline;
            color: #666666;
        }
        .TopSearchKeyWord a:active
        {
            text-decoration: none;
            color: #666666;
        }
        .TopLoginLinkHome
        {
            margin-left: 257px;
            -webkit-margin-start: 263px;
            font-weight: bold;
        }
        * + html .TopLoginLinkHome
        {
            margin-left: 257px;
        }
        .TopLoginLinkHome a:link
        {
            text-decoration: none;
            color: #ba2636;
        }
        .TopLoginLinkHome a:visited
        {
            text-decoration: none;
            color: #ba2636;
        }
        .TopLoginLinkHome a:hover
        {
            text-decoration: underline;
            color: #ba2636;
        }
        .TopLoginLinkHome a:active
        {
            text-decoration: none;
            color: #ba2636;
        }
        .TopLoginLinkHomeExit
        {
            font-weight: bold;
        }
        .TopLoginLinkHomeExit a:link
        {
            text-decoration: none;
            color: #ba2636;
        }
        .TopLoginLinkHomeExit a:visited
        {
            text-decoration: none;
            color: #ba2636;
        }
        .TopLoginLinkHomeExit a:hover
        {
            text-decoration: underline;
            color: #ba2636;
        }
        .TopLoginLinkHomeExit a:active
        {
            text-decoration: none;
            color: #ba2636;
        }
        .TopLoginLinkFavoriteExit
        {
            margin-right: 20px;
        }
        .TopLoginBtn
        {
            width: 36px;
            height: 16px;
            font-size: 12px;
            line-height: 18px;
            text-indent: 3px;
            font-weight: bold;
            margin: 5px 0 5px 12px;
            background: #ba2636;
            color: #fff;
            cursor: pointer;
            float: left;
            position: relative;
        }
        .TopLoginBtnHover
        {
            background: #c5d0d4;
        }
        .trExit
        {
            padding: 2px 6px 0 0px;
            float: right;
            position: relative;
        }
        .trExit
        {
            line-height: 26px;
        }
    </style>

    <script type="text/javascript">
        function AddFavorite(sURL, sTitle) {
            try {
                window.external.addFavorite(sURL, sTitle);
            }
            catch (e) {
                try {
                    window.sidebar.addPanel(sTitle, sURL, "");
                }
                catch (e) {
                    alert("加入收藏失败，请使用Ctrl+D进行添加");
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="trLogin" runat="server">
        <span class="TopLoginLink TopLoginLinkHome"><a href="">设为首页</a></span> <span class="TopLoginLink">
            |</span> <span class="TopLoginLink"><a href="#" onclick="AddFavorite(window.location,document.title);"
                target="_self">收藏</a></span>
        <div class="TopLoginBtn" id="LoginClicker">
            登 录</div>
    </div>
    <div id="trExit" class="trExit" runat="server">
        <span class="TopLoginLink TopLoginLinkHomeExit"><a href="">设为首页</a></span> <span
            class="TopLoginLink">|</span> <span class="TopLoginLink TopLoginLinkFavoriteExit"><a
                href="#" onclick="AddFavorite(window.location,document.title);" target="_self">收藏</a></span>
        <asp:Label ID="lblUser" runat="server"></asp:Label>
        <asp:Label ID="lblWelcome" runat="server"></asp:Label>
        <asp:Button ID="btnExit" runat="server" Text="注销" OnClick="btnExit_Click" CssClass="LoginBtn">
        </asp:Button>
    </div>
    </form>
</body>
</html>
