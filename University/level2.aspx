<%@ Page Language="C#" MasterPageFile="~/MasterPager/Level2.Master" AutoEventWireup="True"
    CodeBehind="level2.aspx.cs" Inherits="colleges.level2" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function() {
            $(".Level2MainBannerNoDisplay").eq(0).attr("class", "Level2MainBanner")
        });
    </script>

</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div id="Level2Main">
        <div id="Level2MainLeft">
            <div id="Level2MainLeftTop">
                <div class="Level2MainTitle">
                    <div class="Level2MainTitleTxt">
                        中经推荐</div>
                    <img src="images/Level2Hot.png" />
                </div>
                <div class="Level2Commend">
                    <asp:ListView ID="CommendList" runat="server">
                        <ItemTemplate>
                            <li class="Level2CommendTitle">
                                <img class="Level2CommendDot" src="images/CommendDot.png" />
                                <span><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>"
                                    target="_blank">
                                    <%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),14) %></a></span><a
                                        href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>"
                                        target="_blank">
                                        <img class="Level2CommendPlay" src="images/CommendPlay.png" /></a> </li>
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
            <%--<div id="Level2MainLeftBottom">
                <div class="Level2MainTitle">
                    <div class="Level2MainTitleTxt">
                        点击排行</div>
                    <img src="images/Level2Top.png" />
                    <div class="Level2MainTitleMore">
                        <a href=""></a>
                    </div>
                </div>
                <div class="Level2Top">
                    <asp:ListView ID="HotList" runat="server">
                        <ItemTemplate>
                            <li class="Level2CommendTitle"><span class="Level2TopNumBg1"><%#Eval("Seq")%></span> 
                            <span><a href="ShowVideo.aspx?ID=<%# colleges.DataProcessing.CutHotListUrl(Eval("URLPath").ToString())%>"><%# colleges.DataProcessing.SubstringText(Eval("VisitName").ToString(), 15)%></a></span> </li>
                            <li class="Level2TopInfo">点击次数:<%#Eval("Count")%></li>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <ul>
                                <asp:Panel ID="itemPlaceholder" runat="server">
                                </asp:Panel>
                            </ul>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </div>--%>
            <asp:ListView ID="LeftPicList" runat="server">
                <ItemTemplate>
                    <div class ="Level2LeftPicBox">
                        <a href="Search.aspx?act=lv2h&ID=<%# Eval("CategoryGuid") %>" target="_blank">
                            <img class="Level2LeftPic" src="ShowBytePic.aspx?Title=<%# colleges.DataProcessing.UrlEnCode(Eval("CategoryName").ToString()) %>&Alias=<%=LeftPicAliasText%>" /></a>
                        <div class="Level2LeftPicText">
                            <a href="Search.aspx?act=lv2h&ID=<%# Eval("CategoryGuid") %>" target="_blank" title="<%#GetLeftPicTextByTitle(Eval("CategoryName").ToString(),false)%>">
                                <%#colleges.DataProcessing.SubstringText(GetLeftPicTextByTitle(Eval("CategoryName").ToString(),false),54)%></a>
                            <span class="Level2LeftPicTextEnd">
                                <%#GetLeftPicTextByTitle(Eval("CategoryName").ToString(),true)%>
                            </span>
                        </div>
<%--                        <div class="Level2LeftPicTextEnd">
                            <%#GetLeftPicTextByTitle(Eval("CategoryName").ToString(),true)%>
                        </div>--%>
                    </div>
                </ItemTemplate>
                <LayoutTemplate>
                    <asp:Panel ID="itemPlaceholder" runat="server">
                    </asp:Panel>
                </LayoutTemplate>
            </asp:ListView>
            <div class ="Level2LeftPicBox"><asp:Image ID="Level2LeftPicBot" CssClass="Level2LeftPic" runat="server" /></div>
        </div>
        <asp:ListView ID="Level2MainRight" GroupItemCount="4" OnItemDataBound="Level2MainRight_ItemDataBound"
            runat="server">
            <LayoutTemplate>
                <div id="Level2MainRight">
                    <asp:Panel runat="server" ID="groupPlaceholder">
                    </asp:Panel>
                </div>
            </LayoutTemplate>
            <GroupTemplate>
                <asp:Panel runat="server" ID="itemPlaceholder">
                </asp:Panel>
                <div class="Level2MainBannerNoDisplay">
                    <asp:Image ID="Level2MainBanner" runat="server" />
                </div>
            </GroupTemplate>
            <ItemTemplate>
                <asp:Label ID="CurrentLv2Alias" CssClass="HideInfoLable" runat="server" Text='<%# Eval("CategoryAlias") %>'></asp:Label>
                <!--[if IE 6]><div class="IE6Level2Space"></div><![endif]-->
                <div class="Level2MainCourse">
                    <div class="Level2MainTitle">
                        <div class="Level2MainTitleTxt">
                            <asp:Label ID="Lv2ListTitle" runat="server"></asp:Label>
                        </div>
                        <div class="Level2MainTitleMore">
                            <asp:HyperLink ID="Lv2ListLink" runat="server">更多>></asp:HyperLink>
                        </div>
                    </div>
                    <asp:ListView ID="Lv2ListPic" runat="server">
                        <ItemTemplate>
                            <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank"><img class="Level2MainCourseDetailPic" src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a>
                            <div class="Level2MainCourseDetailInfo">
                                <div class="Level2MainCourseDetailTitle">
                                    <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                        <%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),15) %></a>
                                </div>
                                <div class="Level2MainCourseDetailLine">
                                    <span class="Level2MainCourseDetailSpeaker">主讲人:<%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),15) %></span>
                                    <span class="Level2MainCourseDetailDate">
                                        <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %></span>
                                </div>
                                <div class="Level2MainCourseDetailLine">
                                    职&nbsp;&nbsp;务:<%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),15)%></div>
                            </div>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <div class="Level2MainCourseDetail">
                                <asp:Panel ID="itemPlaceholder" runat="server">
                                </asp:Panel>
                            </div>
                        </LayoutTemplate>
                    </asp:ListView>
                    <asp:ListView ID="Lv2ListText" runat="server">
                        <ItemTemplate>
                            <div class="Level2MainBrief">
                                <span class="Level2MainCourseBriefTitle"><a a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                    title="<%#Eval("Title")%>" target="_blank">
                                    <%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),15) %></a></span>
                                <span class="Level2MainCourseBriefDate">
                                    <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %></span>
                            </div>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <asp:Panel ID="itemPlaceholder" runat="server">
                            </asp:Panel>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
