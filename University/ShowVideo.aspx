<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowVideo.aspx.cs" Inherits="colleges.ShowVideo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>中经视频</title>
    <style type="text/css">
        /*ShowBox OP*/
        .VideoShowBox
        {
           width: 1024px;
           margin: 0 auto 0 auto;
        }
        #VideoShowFrame
        {
            width: 1015px;
            height: 750px;
            margin: 0 auto 0 auto;
            border:0px;
        }
        /*ShowBox ED*/</style>
</head>
<body>
    <div id="vframe" runat="server" style="margin: 0 auto; width: 600px;">
    </div>
    <div class="VideoShowBox">
        <iframe id="VideoShowFrame" src="" frameborder="no" runat="server"></iframe>
    </div>
</body>
</html>
