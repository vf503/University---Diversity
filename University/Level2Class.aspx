<%@ Page Language="C#" MasterPageFile="~/MasterPager/Level2.Master" AutoEventWireup="true" CodeBehind="Level2Class.aspx.cs" Inherits="colleges.Level2Class" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div id="Level2Main">
        <div id="Level2MainLeft">
            <div id="Level2FameLeftTop">
                <div class="Lv2NotableShowLeft">
                    <div class="Level2MainTitleTxt">
                        国内大学</div>
                    <div class="Level2MainTitleMore">
                        <asp:HyperLink ID="Lv2ClassLeftLink1" runat="server">更多>></asp:HyperLink>
                    </div>
                </div>
                <asp:ListView ID="Lv2ClassLeftList1" runat="server">
                    <ItemTemplate>
                        <div class="Lv2ClassDadgeItem">
                            <a href="Level3Class.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=1" target="_blank">
                                <img class="Lv2ClassDadge" src="images/<%# Eval("CategoryAlias") %>.png" /></a> <a href="Level3Class.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=1"
                                    target="_blank" class="Lv2ClassDadgeLink">
                                    <%# colleges.DataProcessing.SubstringText(Eval("CategoryName").ToString(),10) %></a>
                        </div>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <asp:Panel ID="itemPlaceholder" runat="server">
                        </asp:Panel>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
            <div id="Level2FameLeftBottom">
                <div class="Lv2NotableShowLeft">
                    <div class="Level2MainTitleTxt">
                        国外大学</div>
                    <div class="Level2MainTitleMore">
                        <asp:HyperLink ID="Lv2ClassLeftLink2" runat="server">更多>></asp:HyperLink>
                    </div>
                </div>
                <asp:ListView ID="Lv2ClassLeftList2" runat="server">
                    <ItemTemplate>
                        <div class="Lv2ClassDadgeItem">
                            <a href="Level3Class.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=1" target="_blank">
                                <img class="Lv2ClassDadge" src="images/<%# Eval("CategoryAlias") %>.png" /></a> <a href="Level3Class.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=1"
                                    target="_blank" class="Lv2ClassDadgeLink">
                                    <%# colleges.DataProcessing.SubstringText(Eval("CategoryName").ToString(),10) %></a>
                        </div>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <asp:Panel ID="itemPlaceholder" runat="server">
                        </asp:Panel>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>
        <div id="Level2MainMiddleClass">
            <div class="Level2FameMainList">
                <div class="Level2MainTitle">
                    <div class="Level2MainTitleTxt">
                        精品课程</div>
                    <div class="Level2MainTitleMore">
                        <asp:HyperLink ID="Lv2ClassMidLink" runat="server">更多>></asp:HyperLink>
                    </div>
                </div>
                <asp:ListView ID="Lv2ClassMidList" runat="server">
                    <ItemTemplate>
                        <li>
                            <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank"><img class="Level2MainListPreview" src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a>
                            <div class="Level2MainListInfo">
                                <div class="Level2MainListLeft">
                                    <div class="Level2MainListTitle">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),16)%></a>
                                        <table class="Level2MainListInfoTable">
                                            <tr>
                                                <td>
                                                    <span>主讲人:</span><span class="SearchResultInfoTxt"><%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),11) %></span>
                                                </td>
                                                <td>
                                                    <span>职务:</span><span class="SearchResultInfoTxt"><%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),12)%></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="Level2MainListSummary">
                                        简介: <span>
                                            <%# colleges.DataProcessing.SubstringText(Eval("Summary").ToString(), 54)%></span>
                                    </div>
                                </div>
                        </li>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <ul>
                            <asp:Panel ID="itemPlaceholder" runat="server">
                            </asp:Panel>
                        </ul>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>
        <div id="Level2MainSlid">
            <div id="Level2MainSlidTop">
                <div class="Level2MainTitle">
                    <div class="Level2MainTitleTxt">
                        中经推荐</div>
                </div>
                 <div class="Level2ClassCommend">
                    <asp:ListView ID="CommendList" runat="server">
                        <ItemTemplate>
                            <li class="Level2CommendTitle">
                                <img class="Level2CommendDot" src="images/CommendDot.png" />
                                <span><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank"><%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),15) %></a></span></li>
                            <li class="Level2CommendInfo">主讲人:<%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),20) %></li>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <ul>
                                <asp:Panel ID="itemPlaceholder" runat="server">
                                </asp:Panel>
                            </ul>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </div>
            <%--<div id="Level2MainSlidBottom">
                <div class="Level2MainTitle">
                    <div class="Level2MainTitleTxt">
                        热播排行</div>
                </div>
                <asp:ListView ID="HotList" runat="server">
                    <ItemTemplate>
                        <li><span class="Level2TopNumBg1">
                            <%#Eval("Seq")%></span></span> <span><a href="ShowVideo.aspx?ID=<%# colleges.DataProcessing.CutHotListUrl(Eval("URLPath").ToString())%>" target="_blank">
                                <%# colleges.DataProcessing.SubstringText(Eval("VisitName").ToString(), 15)%></a></span>
                        </li>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <ul class="Level2FameTop">
                            <asp:Panel ID="itemPlaceholder" runat="server">
                            </asp:Panel>
                        </ul>
                    </LayoutTemplate>
                </asp:ListView>
            </div>--%>
        </div>
    </div>
</asp:Content>