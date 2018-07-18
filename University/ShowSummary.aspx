<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSummary.aspx.cs" Inherits="colleges.ShowSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=sTitle%></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:80%;margin:0 auto"><%=sTitle%></div>
    <div id ="divSummary" runat="server">
    </div>
    </form>
</body>
</html>
