<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Level3List.aspx.cs" Inherits="colleges.Level3List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ListView ID="Level3MainListPic" runat="server">
        <LayoutTemplate>
            <ul>
                <asp:Panel ID="itemPlaceholder" runat="server">
                </asp:Panel>
            </ul>
        </LayoutTemplate>
        <ItemTemplate>
            <li>
                <img class="Level3MainListContentDetailPreview" src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" />
                <div class="Level3MainListContentDetailInfo">
                    <div class="Level3MainListDetailLeft">
                        <div class="Level3MainListDetailTitle">
                            <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                <%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),20) %></a></div>
                        <table class="Level3MainListDetailInfo">
                            <tr>
                                <td>
                                    <span>主讲人:</span> <span class="Level3MainListInfoTxt">
                                        <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),25) %></span>
                                </td>
                                <td>
                                    <span>职务:</span> <span class="Level3MainListInfoTxt">
                                        <%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),15)%></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span>时长:</span> <span class="Level3MainListInfoTxt">
                                        <%#Eval("Duration")%>分钟</span>
                                </td>
                                <td>
                                    <span>日期:</span> <span class="Level3MainListInfoTxt">
                                        <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %></span>
                                </td>
                            </tr>
                        </table>
                    </div>
            </li>
        </ItemTemplate>
    </asp:ListView>
    <asp:ListView ID="Level3MainListText" runat="server">
        <LayoutTemplate>
            <table class="SearchResultContentTable">
                <thead>
                    <tr>
                        <th>
                            课程名称
                        </th>
                        <th>
                            主讲人
                        </th>
                        <th>
                            时长
                        </th>
                        <th>
                            日期
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Panel ID="itemPlaceholder" runat="server">
                    </asp:Panel>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                        <%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),20) %></a>
                </td>
                <td>
                    <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),25) %></span>
                </td>
                <td>
                    <%# Eval("Duration")%>分钟
                </td>
                <td>
                    <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %></span>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <div id="Div1" class="Level3MainListPagerBox" runat="server">
        <webdiyer:aspnetpager id="Lv3Pager" runat="server" onpagechanged="Lv3Pager_PageChanged"
            alwaysshow="True" urlpaging="False" firstpagetext="首页" lastpagetext="末页" nextpagetext=">>"
            numericbuttoncount="5" pagingbuttonspacing="10px" numericbuttontextformatstring="[{0}]"
            prevpagetext="<<" submitbuttontext="Go" textafterpageindexbox="页" textbeforepageindexbox=""
            showcustominfosection="Right" custominfohtml="第%CurrentPageIndex%页 / 共%PageCount%页"
            cssclass="" currentpagebuttonclass="" custominfoclass="" custominfotextalign="Left">
                                    </webdiyer:aspnetpager>
    </div>
    </form>
</body>
</html>
