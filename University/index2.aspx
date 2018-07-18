<%@ OutputCache Duration="600" VaryByParam="showlogin" VaryByCustom="USSUserID" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPager/Normal.Master" AutoEventWireup="true"
    CodeBehind="index2.aspx.cs" Inherits="colleges.index2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="renderer" content="ie-stand" />

    <script type="text/javascript" src="Scripts/jquery-ui-tabs-rotate.js"></script>

    <script type="text/javascript" src="Scripts/jquery.tabs.js"></script>

    <script type="text/javascript">
        $(function() {
            $("#FocusTabs").tabs({ event: "mouseover" }).tabs("rotate", 3000);
        });

        $(document).ready(function() {
            $(".tab_Index1_menu li:first").addClass("current");
            $("#tab_Index1_box2 div.tab_Index1_box2_item:first").removeClass("hide");
            $("div.tab_Index1_box_item:first").removeClass("hide");
            $("#TabsIndex1").TabsIndex1({
                auto: 3000
            });
            //
            //
            $("#ctl00_IndexMainPlaceHolder_UpdateBtn").click();
            $("#ctl00_IndexMainPlaceHolder_UpdateBtn").hide();
            //
            $(".tab_Index2_menu li:first").addClass("current");
            $("div.tab_Index2_box_item:first").removeClass("hide");
            $("#TabsIndex2").TabsIndex2();
        });	
    </script>

</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div id="" class="UpsideContent">
        <div id="FocusTabs">
            <asp:ListView ID="FocusTabsTxt" runat="server">
                <ItemTemplate>
                    <li><a href="#tabs-<%#Eval("XIndex")%>" onclick="OpenWin('PicFocusTxt.aspx?SubAlias=<%#Eval("CategoryAlias")%>')">
                        <%#Eval("CategoryName")%>
                    </a></li>
                </ItemTemplate>
                <EmptyDataTemplate>
                    未返回数据。
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <ul id="TabsList">
                        <asp:Panel ID="itemPlaceholder" runat="server">
                        </asp:Panel>
                    </ul>
                </LayoutTemplate>
            </asp:ListView>
            <asp:ListView ID="FocusTabsPic" runat="server">
                <ItemTemplate>
                    <div id="tabs-<%#Eval("FocusTabsIndex")%>">
                        <a href="PicFocusPic.aspx?ID=<%#Eval("ArticleGUID")%>" target="_blank">
                            <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"360x240.png")%>" /></a>
                    </div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    未返回数据。
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <div id="TabsPic">
                        <asp:Panel ID="itemPlaceholder" runat="server">
                        </asp:Panel>
                    </div>
                </LayoutTemplate>
            </asp:ListView>
        </div>
        <div id="" class="SpecialFocus">
            <div id="SpecialAttentionTitle">
                <span>特别关注</span>
                <div class="IndexMore">
                    <a href="SpecialIndex.aspx?ID=d46335e1f6dd45368eff799832968c48" target="_blank">
                    更多&gt;&gt;</a>
                </div>
            </div>
            <div id="" class="SpecialFocusContent">
                <asp:ListView ID="SpecialFocusContent" runat="server" GroupItemCount="2">
                    <GroupTemplate>
                        <ul>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        </ul>
                    </GroupTemplate>
                    <LayoutTemplate>
                        <div id="" class="SpecialFocusContent">
                            <asp:PlaceHolder ID="groupPlaceholder" runat="server" />
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li class="SpecialFocusTop"><a href="<%#colleges.DataProcessing.GetSubjectLink(Eval("SubjectID").ToString(),Eval("SubjectTitle").ToString())%>"
                            title="<%#Eval("SubjectTitle")%>" target="_blank">
                            <%#colleges.DataProcessing.SubstringText(Eval("SubjectTitle").ToString(),18)%></a></li>
                        <li class="SpecialFocusLi"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>"
                            target="_blank">
                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),21)%></a></li>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <li class="SpecialFocusLi"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>"
                            target="_blank">
                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),21)%></a></li>
                    </AlternatingItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <div class="DailyVideo">
            <div class="DailyVideoTitle">
                <span>凤凰时政</span>
                <div class="IndexMore">
                    <a href="level3.aspx?alias=gxchannel9_topics_1&IsChild=0" target="_blank">更多&gt;&gt;</a>
                </div>
            </div>
            <div class="DailyVideoPlayer">
                <asp:HyperLink ID="TopVideoLink1a" Target="_blank" runat="server"><img src="images/IndexNews1.png" /></asp:HyperLink>
            </div>
            <div class="DailyVideoInfo">
                <div class="DailyVideoInfoLink">
                    <asp:HyperLink ID="TopVideoLink1b" Target="_blank" runat="server"></asp:HyperLink></div>
                <asp:Label ID="TopVideoText1" runat="server" Text="" CssClass="DailyVideoInfoText"></asp:Label>
            </div>
        </div>
        <div class="DailyVideoBot">
            <div class="DailyVideoTitleBot">
                <span>财经评论</span>
                <div class="IndexMore">
                    <a href="level3.aspx?alias=gxchannel9_topics_6&IsChild=0" target="_blank">更多&gt;&gt;</a>
                </div>
            </div>
            <div class="DailyVideoPlayer">
                <asp:HyperLink ID="TopVideoLink2a" Target="_blank" runat="server"><img src="images/IndexNews2.png" /></asp:HyperLink>
            </div>
            <div class="DailyVideoInfo">
                <div class="DailyVideoInfoLink">
                    <asp:HyperLink ID="TopVideoLink2b" Target="_blank" runat="server"></asp:HyperLink></div>
                <%--<div class="DailyVideoInfoText">--%>
                <%--<asp:ListView ID="DailyVideoInfoTextLines" runat="server">
                        <ItemTemplate>
                            <span class="DailyVideoInfoTextLine">
                                <%# colleges.DataProcessing.SubstringText(Eval("newsTitle").ToString(),9) %></span>
                        </ItemTemplate>
                        <LayoutTemplate>
                                <asp:Panel ID="itemPlaceholder" runat="server">
                                </asp:Panel>
                        </LayoutTemplate>
                    </asp:ListView>--%>
                <asp:Label ID="TopVideoText2" runat="server" Text="" CssClass="DailyVideoInfoText"></asp:Label>
                <%--</div>--%>
            </div>
        </div>
        <div class="MainContentNav">
            <div class="MainContentNavTitle">
                <span>导航</span>
                <div class="IndexMore">
                    <a href="navigate.aspx" target="_blank">更多&gt;&gt;</a>
                </div>
            </div>
            <div class="MainContentNavMain">
                <div class="MainContentNavSort">
                    <div class="MainContentNavSortName">
                        重点学科
                    </div>
                    <asp:ListView ID="NaviList1" runat="server">
                        <ItemTemplate>
                            <li><a href="Level3Navi.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                                <%# colleges.DataProcessing.SubstringText(Eval("CategoryName").ToString(),19) %></a></li>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <div class="MainContentNavItem">
                                <ul>
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </ul>
                            </div>
                        </LayoutTemplate>
                    </asp:ListView>
                    <div class="MainContentNavMore">
                        <a href="Level3Navi.aspx?alias=gxb2_keydiscipline1&IsChild=0" target="_blank">……</a></div>
                </div>
                <div class="MainContentNavSort">
                    <div class="MainContentNavSortName">
                        专业设置
                    </div>
                    <asp:ListView ID="NaviList2" runat="server">
                        <ItemTemplate>
                            <li><a href="Level3Navi.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                                <%# colleges.DataProcessing.SubstringText(Eval("CategoryName").ToString(),10) %></a></li>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <div class="MainContentNavItem">
                                <ul>
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </ul>
                            </div>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
                <div class="MainContentNavSort">
                    <div class="MainContentNavSortName">
                        机构分类
                    </div>
                    <asp:ListView ID="NaviList3" runat="server">
                        <ItemTemplate>
                            <li><a href="Level3Navi.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                                <%# colleges.DataProcessing.SubstringText(Eval("CategoryName").ToString(),10) %></a></li>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <div class="MainContentNavItem">
                                <ul>
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </ul>
                            </div>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="UpdateBtn" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div id="Div1" class="MainContent">
                <div id="Div2" class="MainContentLeft">
                    <div class="ColumnShow">
                        <div class="ColumnShowTitle">
                            <asp:Label ID="ListATitle1" runat="server" Text=""></asp:Label>
                            <div class="IndexMore">
                                <asp:HyperLink ID="ListALink1" runat="server">更多&gt;&gt;</asp:HyperLink>
                            </div>
                            <asp:ListView ID="ColumnShowSort1" runat="server">
                                <ItemTemplate>
                                    <a href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                                        <%# Eval("CategoryName") %></a><%--<%# HomeChannelListALinkSplit(3)%>--%>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnShowSort">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                        <asp:ListView ID="ColumnVideo1" runat="server">
                            <ItemTemplate>
                                <div class="ColumnVideo">
                                    <div class="ColumnVideoPicBox">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a>
                                    </div>
                                    <div class="ColumnVideoLink">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),9)%></a>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <div class="ColumnVideoContent">
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </div>
                            </LayoutTemplate>
                        </asp:ListView>
                        <div>
                            <asp:ListView ID="ColumnOtherL1" runat="server">
                                <ItemTemplate>
                                    <div class="ColumnOtherLink">
                                        <span class="ColumnOtherLeft"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                            title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),18)%></a></span>
                                        <span class="ColumnOtherRight">
                                            <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></span>
                                    </div>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnOther">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                            <asp:ListView ID="ColumnOtherR1" runat="server">
                                <ItemTemplate>
                                    <div class="ColumnOtherLink">
                                        <span class="ColumnOtherLeft"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                            title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),17)%></a></span>
                                        <span class="ColumnOtherRight">
                                            <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></span>
                                    </div>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnOther">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                    <div class="ColumnShow">
                        <div class="ColumnShowTitle">
                            <asp:Label ID="ListATitle2" runat="server" Text=""></asp:Label>
                            <div class="IndexMore">
                                <asp:HyperLink ID="ListALink2" runat="server">更多&gt;&gt;</asp:HyperLink>
                            </div>
                            <asp:ListView ID="ColumnShowSort2" runat="server">
                                <ItemTemplate>
                                    <a href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                                        <%# Eval("CategoryName") %></a><%--<%# HomeChannelListALinkSplit(3)%>--%>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnShowSort">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                        <asp:ListView ID="ColumnVideo2" runat="server">
                            <ItemTemplate>
                                <div class="ColumnVideo">
                                    <div class="ColumnVideoPicBox">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a>
                                    </div>
                                    <div class="ColumnVideoLink">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),9)%></a>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <div class="ColumnVideoContent">
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </div>
                            </LayoutTemplate>
                        </asp:ListView>
                        <div>
                            <asp:ListView ID="ColumnOtherL2" runat="server">
                                <ItemTemplate>
                                    <div class="ColumnOtherLink">
                                        <span class="ColumnOtherLeft"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                            title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),17)%></a></span>
                                        <span class="ColumnOtherRight">
                                            <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></span>
                                    </div>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnOther">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                            <asp:ListView ID="ColumnOtherR2" runat="server">
                                <ItemTemplate>
                                    <div class="ColumnOtherLink">
                                        <span class="ColumnOtherLeft"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                            title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),17)%></a></span>
                                        <span class="ColumnOtherRight">
                                            <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></span>
                                    </div>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnOther">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                    <div class="IndexAdvert">
                        <img src="images/ADHome.gif" />
                    </div>
                    <div class="ColumnShow">
                        <div class="ColumnShowTitle">
                            <asp:Label ID="ListATitle3" runat="server" Text=""></asp:Label>
                            <div class="IndexMore">
                                <asp:HyperLink ID="ListALink3" runat="server">更多&gt;&gt;</asp:HyperLink>
                            </div>
                            <asp:ListView ID="ColumnShowSort3" runat="server">
                                <ItemTemplate>
                                    <a href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                                        <%# Eval("CategoryName") %></a><%--<%# HomeChannelListALinkSplit(3)%>--%>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnShowSort">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                        <asp:ListView ID="ColumnVideo3" runat="server">
                            <ItemTemplate>
                                <div class="ColumnVideo">
                                    <div class="ColumnVideoPicBox">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a>
                                    </div>
                                    <div class="ColumnVideoLink">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),9)%></a>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <div class="ColumnVideoContent">
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </div>
                            </LayoutTemplate>
                        </asp:ListView>
                        <div>
                            <asp:ListView ID="ColumnOtherL3" runat="server">
                                <ItemTemplate>
                                    <div class="ColumnOtherLink">
                                        <span class="ColumnOtherLeft"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                            title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),17)%></a></span>
                                        <span class="ColumnOtherRight">
                                            <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></span>
                                    </div>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnOther">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                            <asp:ListView ID="ColumnOtherR3" runat="server">
                                <ItemTemplate>
                                    <div class="ColumnOtherLink">
                                        <span class="ColumnOtherLeft"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                            title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),18)%></a></span>
                                        <span class="ColumnOtherRight">
                                            <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></span>
                                    </div>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnOther">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                    <div class="ColumnShow">
                        <div class="ColumnShowTitle">
                            <asp:Label ID="ListATitle4" runat="server" Text=""></asp:Label>
                            <div class="IndexMore">
                                <asp:HyperLink ID="ListALink4" runat="server">更多&gt;&gt;</asp:HyperLink>
                            </div>
                            <asp:ListView ID="ColumnShowSort4" runat="server">
                                <ItemTemplate>
                                    <a href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                                        <%# Eval("CategoryName") %></a><%--<%# HomeChannelListALinkSplit(3)%>--%>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnShowSort">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                        <asp:ListView ID="ColumnVideo4" runat="server">
                            <ItemTemplate>
                                <div class="ColumnVideo">
                                    <div class="ColumnVideoPicBox">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a>
                                    </div>
                                    <div class="ColumnVideoLink">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),9)%></a>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <div class="ColumnVideoContent">
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </div>
                            </LayoutTemplate>
                        </asp:ListView>
                        <div>
                            <asp:ListView ID="ColumnOtherL4" runat="server">
                                <ItemTemplate>
                                    <div class="ColumnOtherLink">
                                        <span class="ColumnOtherLeft"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                            title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),18)%></a></span>
                                        <span class="ColumnOtherRight">
                                            <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></span>
                                    </div>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnOther">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                            <asp:ListView ID="ColumnOtherR4" runat="server">
                                <ItemTemplate>
                                    <div class="ColumnOtherLink">
                                        <span class="ColumnOtherLeft"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                            title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),18)%></a></span>
                                        <span class="ColumnOtherRight">
                                            <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></span>
                                    </div>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div class="ColumnOther">
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </div>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
                <div id="Div3" class="MainContentRight">
                    <div class="NotableShow">
                        <div class="NotableShowTitle">
                            <asp:Label ID="ListBTitle1" runat="server" Text=""></asp:Label>
                            <div class="IndexMore">
                                <asp:HyperLink ID="ListBLink1" runat="server">更多&gt;&gt;</asp:HyperLink>
                            </div>
                        </div>
                        <asp:ListView ID="VerticalShowVideo1" runat="server">
                            <ItemTemplate>
                                <div class="MainVerticalShowVideoPic">
                                    <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                        <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a></div>
                                <div class="MainVerticalShowInfo">
                                    <div class="MainVerticalShowInfoTitle">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),9)%></a></div>
                                    <div class="MainVerticalShowInfoOther">
                                        <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),11) %></div>
                                    <div class="MainVerticalShowInfoOther">
                                        <%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),11)%></div>
                                    <div class="MainVerticalShowInfoOther">
                                        <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %></div>
                                </div>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <div class="MainVerticalShowVideo">
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </div>
                            </LayoutTemplate>
                        </asp:ListView>
                    </div>
                    <div class="MainVerticalShow" id="MainVerticalShow1">
                        <div class="NotableShowTitle">
                            <asp:Label ID="ListBTitle2" runat="server" Text=""></asp:Label>
                            <div class="IndexMore">
                                <asp:HyperLink ID="ListBLink2" runat="server">更多&gt;&gt;</asp:HyperLink>
                            </div>
                        </div>
                        <asp:ListView ID="VerticalShowVideo2" runat="server">
                            <ItemTemplate>
                                <div class="MainVerticalShowVideoPic">
                                    <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                        <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a></div>
                                <div class="MainVerticalShowInfo">
                                    <div class="MainVerticalShowInfoTitle">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),9)%></a></div>
                                    <div class="MainVerticalShowInfoOther">
                                        <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),11) %></div>
                                    <div class="MainVerticalShowInfoOther">
                                        <%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),11)%></div>
                                    <div class="MainVerticalShowInfoOther">
                                        <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %></div>
                                </div>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <div class="MainVerticalShowVideo">
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </div>
                            </LayoutTemplate>
                        </asp:ListView>
                        <asp:ListView ID="VerticalShowList2" runat="server">
                            <ItemTemplate>
                                <li><span class="NotableShowOtherLeft"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                    title="<%#Eval("Title")%>" target="_blank">
                                    <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a></span>
                                    <span class="NotableShowOtherRight">
                                        <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></span>
                                </li>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <ul class="MainVerticalShowList">
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </ul>
                            </LayoutTemplate>
                        </asp:ListView>
                    </div>
                    <div class="MainVerticalShow" id="MainVerticalShow2">
                        <div class="NotableShowTitle">
                            <asp:Label ID="ListBTitle3" runat="server" Text=""></asp:Label>
                            <div class="IndexMore">
                                <asp:HyperLink ID="ListBLink3" runat="server">更多&gt;&gt;</asp:HyperLink>
                            </div>
                        </div>
                        <asp:ListView ID="VerticalShowVideo3" runat="server">
                            <ItemTemplate>
                                <div class="MainVerticalShowVideoPic">
                                    <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                        <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a></div>
                                <div class="MainVerticalShowInfo">
                                    <div class="MainVerticalShowInfoTitle">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),9)%></a></div>
                                    <div class="MainVerticalShowInfoOther">
                                        <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),11) %></div>
                                    <div class="MainVerticalShowInfoOther">
                                        <%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),11)%></div>
                                    <div class="MainVerticalShowInfoOther">
                                        <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %></div>
                                </div>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <div class="MainVerticalShowVideo">
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </div>
                            </LayoutTemplate>
                        </asp:ListView>
                        <asp:ListView ID="VerticalShowList3" runat="server">
                            <ItemTemplate>
                                <li><span class="NotableShowOtherLeft"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                    title="<%#Eval("Title")%>" target="_blank">
                                    <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a></span>
                                    <span class="NotableShowOtherRight">
                                        <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></span>
                                </li>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <ul class="MainVerticalShowList">
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </ul>
                            </LayoutTemplate>
                        </asp:ListView>
                    </div>
                    <div class="MainVerticalShow">
                        <div class="MainVerticalShowTitle">
                            <span>公开课</span>
                            <div class="IndexMore">
                                <a href="Level2Class.aspx">更多&gt;&gt;</a></div>
                        </div>
                        <div id="TabsIndex2">
                            <div class="tab_Index2_box">
                                <asp:ListView ID="TabIndex2List1" runat="server">
                                    <ItemTemplate>
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3Class.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/<%# Eval("CategoryAlias") %>.png" /></a>
                                            <a href="Level3Class.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=1" target="_blank"
                                                class="IndexClassDadgeLink">
                                                <%# colleges.DataProcessing.SubstringText(Eval("CategoryName").ToString(),10) %></a>
                                        </div>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <div class="tab_Index2_box_item hide">
                                            <asp:Panel ID="itemPlaceholder" runat="server">
                                            </asp:Panel>
                                        </div>
                                    </LayoutTemplate>
                                </asp:ListView>
                                <asp:ListView ID="TabIndex2List2" runat="server">
                                    <ItemTemplate>
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3Class.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/<%# Eval("CategoryAlias") %>.png" /></a>
                                            <a href="Level3Class.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=1" target="_blank"
                                                class="IndexClassDadgeLink">
                                                <%# colleges.DataProcessing.SubstringText(Eval("CategoryName").ToString(),10) %></a>
                                        </div>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <div class="tab_Index2_box_item hide">
                                            <asp:Panel ID="itemPlaceholder" runat="server">
                                            </asp:Panel>
                                        </div>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </div>
                            <ul class="tab_Index2_menu">
                                <li><a href=""></a></li>
                                <li><a href=""></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="MainSkill">
        <div class="MainContentNavTitle">
            <span>技能</span>
            <div class="IndexMore">
                <a href="level2.aspx?alias=gxchannel7">更多&gt;&gt;</a>
            </div>
        </div>
        <div class="MainSkillContent">
            <%--<asp:ListView ID="SkillList" runat="server" OnItemDataBound="SkillList_ItemDataBound">
                    <ItemTemplate>
                        <div class="MainSkillContentItem">
                            <asp:Label ID="SkillCategoryAlias" CssClass="HideInfoLable" runat="server" Text='<%# Eval("CategoryAlias") %>'></asp:Label>
                            <img class="MainSkillContentImg" src="images/Skill<%# HomeChannelListOutpic(ref HomeChannelListCPicCount) %>.png" />
                            <div class="MainSkillContentText">
                                <span class="MainSkillContentTextTitle">
                                    <a href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank"><%# Eval("CategoryName") %></a></span><<span class="MainSkillContentTextTxt">
                                        <a href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank"><%# colleges.DataProcessing.SubstringText(Eval("Note").ToString(),30) %></a></span>
                            </div>
                            <div class="MainSkillContentText">
                              <asp:ListView ID="SkillListCourse" runat="server">
                                    <ItemTemplate>
                                        <li class="MainTrainListItem"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),15)%></a></li>
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
                    </ItemTemplate>
                    <LayoutTemplate>
                        <asp:Panel ID="itemPlaceholder" runat="server">
                        </asp:Panel>
                    </LayoutTemplate>
                </asp:ListView>--%>
            <asp:ListView ID="SkillNote" runat="server">
                <LayoutTemplate>
                    <div id="tab_Index1_box2" class="tab_Index1_box2">
                        <asp:Panel ID="itemPlaceholder" runat="server">
                        </asp:Panel>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="tab_Index1_box2_item hide">
                        <a href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                            <img class="PicNoBorder" src="images/Skill<%# HomeChannelListOutpic(ref HomeChannelListCPicCount) %>.png" /></a>
                        <div class="text">
                            <a href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                                <%# colleges.DataProcessing.SubstringText(colleges.DataProcessing.StringCut(Eval("Note").ToString(), ';', 1), 51)%></a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <div id="TabsIndex1">
                <asp:ListView ID="SkillTabs" runat="server">
                    <LayoutTemplate>
                        <ul class="tab_Index1_menu">
                            <asp:Panel ID="itemPlaceholder" runat="server">
                            </asp:Panel>
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li><a href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                            <%# Eval("CategoryName") %></a></li>
                    </ItemTemplate>
                </asp:ListView>
                <div class="tab_Index1_box">
                    <asp:ListView ID="TabIndex1Box" runat="server" OnItemDataBound="TabIndex1Box_ItemDataBound">
                        <LayoutTemplate>
                            <asp:Panel ID="itemPlaceholder" runat="server">
                            </asp:Panel>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="tab_Index1_box_item hide">
                                <asp:Label ID="SkillCategoryAlias" CssClass="HideInfoLable" runat="server" Text='<%# Eval("CategoryAlias") %>'></asp:Label>
                                <asp:ListView ID="TabUlLeft" runat="server">
                                    <LayoutTemplate>
                                        <ul class="UlLeft">
                                            <asp:Panel ID="itemPlaceholder" runat="server">
                                            </asp:Panel>
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li class="MainTrainListItem"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                            title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),19)%></a></li>
                                    </ItemTemplate>
                                </asp:ListView>
                                <asp:ListView ID="TabUlRight" runat="server">
                                    <LayoutTemplate>
                                        <ul class="UlRight">
                                            <asp:Panel ID="itemPlaceholder" runat="server">
                                            </asp:Panel>
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li class="MainTrainListItem"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                            title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),19)%></a></li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
    <div class="MainTrain">
        <div class="MainContentNavTitle">
            <span>培训</span>
            <div class="IndexMore">
                <a href="level2.aspx?alias=gxchannel8">更多&gt;&gt;</a>
            </div>
        </div>
        <div class="MainSKillContent">
            <asp:ListView ID="TrainList" runat="server" OnItemDataBound="TrainList_ItemDataBound">
                <ItemTemplate>
                    <div class="MainSkillContentItem">
                        <asp:Label ID="TrainCategoryAlias" CssClass="HideInfoLable" runat="server" Text='<%# Eval("CategoryAlias") %>'></asp:Label>
                        <a href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                            <img class="MainSkillContentImg" src="images/Train<%# HomeChannelListOutpic(ref HomeChannelListC2PicCount) %>.png" /></a>
                        <div class="MainSkillContentText">
                        </div>
                        <div class="MainSkillContentText">
                            <asp:ListView ID="TrainListCourse" runat="server">
                                <ItemTemplate>
                                    <li class="MainTrainListItem"><a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>"
                                        title="<%#Eval("Title")%>" target="_blank">
                                        <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),15)%></a></li>
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
                </ItemTemplate>
                <LayoutTemplate>
                    <asp:Panel ID="itemPlaceholder" runat="server">
                    </asp:Panel>
                </LayoutTemplate>
            </asp:ListView>
        </div>
    </div>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <asp:Button ID="UpdateBtn" runat="server" Text="Button" 
        onclick="UpdateBtn_Click" Visible="false"/>
    <asp:Label ID="CacheTest" runat="server" Text="Label" Visible="false"></asp:Label>
</asp:Content>
