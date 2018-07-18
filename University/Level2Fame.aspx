<%@ Page Language="C#" MasterPageFile="~/MasterPager/Level2.Master" AutoEventWireup="true" CodeBehind="Level2Fame.aspx.cs" Inherits="colleges.Level2Fame" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div id="Level2Main">
        <div id="Level2FameLeft">
            <div id="Level2MainSlidTop">
                <div class="Level2MainTitle">
                    <div class="Level2MainTitleTxt">
                        专家名录</div>
                    <div class="Level2MainTitleMore">
                        <asp:HyperLink ID="Lv2FameRightLink1" runat="server">更多>></asp:HyperLink>
                    </div>
                </div>
                <asp:ListView ID="Lv2FameRightList1" runat="server">
                    <ItemTemplate>
                        <li><a href="Level3Fame.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=1" target="_blank">
                            <%# colleges.DataProcessing.SubstringText(Eval("CategoryName").ToString(),10) %></a></li>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <ul class="FamousList">
                            <asp:Panel ID="itemPlaceholder" runat="server">
                            </asp:Panel>
                        </ul>
                    </LayoutTemplate>
                </asp:ListView>
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
            <img class="Level2FameSlidPic" src="images/gxchannel10.jpg" />
        </div>
        <div id="Level2MainMiddle">
            <div class="Level2FameMainList">
                <div class="Level2MainTitle">
                    <div class="Level2MainTitleTxt">
                        名师推荐</div>
                    <div class="Level2MainTitleMore">
                        <asp:HyperLink ID="Lv2FameMidLink" runat="server">更多>></asp:HyperLink>
                    </div>
                </div>
                <asp:ListView ID="Lv2FameMidList" runat="server">
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
        <div id="Level2FameLeftTop">
                <div class="Lv2NotableShowLeft">
                    <div class="Level2MainTitleTxt">
                        名家谈时事</div>
                    <div class="Level2MainTitleMore">
                        <asp:HyperLink ID="Lv2FameLeftLink1" runat="server">更多>></asp:HyperLink>
                    </div>
                </div>
                <asp:ListView ID="Lv2FameLeftList1" runat="server">
                    <ItemTemplate>
                        <div class="Lv2NotableShowLeftItem">
                           <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank"><img class="Lv2NotableShowLeftPic" src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a>
                            <div class="Lv2NotableShowLeftInfo">
                                <div class="Lv2NotableShowLeftInfoTitle">
                                      <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),9)%></a></div>
                                <div class="Lv2NotableShowLeftInfoOther">
                                    <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),11) %></div>
                                <div class="Lv2NotableShowLeftInfoOther">
                                    <%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),9)%></div>
                            </div>
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
                        名家谈管理</div>
                    <div class="Level2MainTitleMore">
                        <asp:HyperLink ID="Lv2FameLeftLink2" runat="server">更多>></asp:HyperLink>
                    </div>
                </div>
                <asp:ListView ID="Lv2FameLeftList2" runat="server">
                    <ItemTemplate>
                        <div class="Lv2NotableShowLeftItem">
                            <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank"><img class="Lv2NotableShowLeftPic" src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a>
                            <div class="Lv2NotableShowLeftInfo">
                                <div class="Lv2NotableShowLeftInfoTitle">
                                      <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),9)%></a></div>
                                <div class="Lv2NotableShowLeftInfoOther">
                                    <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),11) %></div>
                                <div class="Lv2NotableShowLeftInfoOther">
                                    <%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),9)%></div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <asp:Panel ID="itemPlaceholder" runat="server">
                        </asp:Panel>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
            
        </div>
    </div>
</asp:Content>