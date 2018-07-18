<%@ Page Language="C#" MasterPageFile="~/MasterPager/PicNews.Master" AutoEventWireup="true" CodeBehind="PicFocusTxt.aspx.cs" Inherits="colleges.PicFocusTxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script src="Scripts/slide.js" type="text/javascript"></script>
  <script src="Scripts/content.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
<div class="SpecialAttentionLevel2Content">
    <div class="SpecialAttentionLevel2ContentTop">
        <div class="SpecialAttentionLevel2ContentLeft">
            <asp:HyperLink ID="TopPicLargeLink" runat="server" Target="_blank">
                <asp:Image ID="TopPicLarge" CssClass=""PicFocusTopPicLarge" runat="server" /></asp:HyperLink></div>
        <div class="SpecialAttentionLevel2ContentRight">
            <div>
                <div class="SpecialAttentionLevel2ContentRightInfo">
                    <div class="SpecialAttentionLevel2ContentRightInfoLine">
                        <span class="SpecialAttentionLevel2ContentRightInfoTitle">课程标题</span>
                        <asp:Label ID="TopTitle" runat="server" Text="" CssClass="SpecialAttentionLevel2ContentRightInfoCourseTitle"></asp:Label>
                    </div>
                    <div class="SpecialAttentionLevel2ContentRightInfoLine">
                        <span class="SpecialAttentionLevel2ContentRightInfoTitle">主讲人</span>
                        <asp:Label ID="TopSpeaker" runat="server" Text="" CssClass="SpecialAttentionLevel2ContentRightInfoCourseInfo"></asp:Label>
                    </div>
                    <div class="SpecialAttentionLevel2ContentRightInfoLine">
                        <span class="SpecialAttentionLevel2ContentRightInfoTitle">主要职务</span>
                        <asp:Label ID="TopSpeakerInfo" runat="server" Text="" CssClass="SpecialAttentionLevel2ContentRightInfoCourseInfo"></asp:Label>
                    </div>
                    <div class="SpecialAttentionLevel2ContentRightInfoLine">
                        <span class="SpecialAttentionLevel2ContentRightInfoTitle">课程简介</span> <span class="SpecialAttentionLevel2ContentRightInfoCourseInfo">
                        </span>
                    </div>
                </div>
                <div class="PicFocusTopRightPic">
                    <asp:Image ID="TopPicSmall" CssClass="PicFocusTopPicCourse" runat="server" />
                    <asp:HyperLink ID="PlayLink" runat="server" Target="_blank"><img src="images/FocusPlay.png" class="PicFocusTopPicPlay"/></asp:HyperLink>
                </div>
            </div>
            <div Class="SpecialAttentionLevel2ContentRightText">
                  <asp:Label ID="TopSummary" runat="server" Text=""></asp:Label> 
            </div>
        </div>
    </div>
    <div class="SpecialAttentionLevel2Time">
    </div>
    <asp:ListView ID="SpecialAttentionLevel2CourseList" runat="server">
        <ItemTemplate>
            <div class="SpecialAttentionLevel2CourseItem">
                <div class="SpecialAttentionLevel2CourseItemLeft">
                    <div class="SpecialAttentionLevel2CourseItemPic">
                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank"><img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>"></a>
                    </div>
                    <div class="SpecialAttentionLevel2CourseItemDate">
                        <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %>
                    </div>
                </div>
                <div class="SpecialAttentionLevel2CourseItemRight">
                    <div class="SpecialAttentionLevel2CourseItemTitle">
                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank"><%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),10) %></a>
                    </div>
                    <div class="SpecialAttentionLevel2CourseItemInfo">
                        <span class="SpecialAttentionLevel2CourseItemInfoLeft">主讲人：</span><span><%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),15) %></span>
                    </div>
                    <div class="SpecialAttentionLevel2CourseItemInfo">
                        <span class="SpecialAttentionLevel2CourseItemInfoLeft">职务：</span><span><%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),10)%></span>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <LayoutTemplate>
            <div class="SpecialAttentionLevel2CourseList">
                <asp:Panel ID="itemPlaceholder" runat="server">
                </asp:Panel>
                <div class="PicNewsPagerBox">
                    <asp:DataPager ID="PicNewsPager" PageSize="18" runat="server" class="Level3MainListPager">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                ShowPreviousPageButton="True" FirstPageText="首页" LastPageText="尾页" />
                            <asp:NumericPagerField ButtonCount="5" CurrentPageLabelCssClass="Level3MainListPagerCurrent" />
                            <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="True"
                                ShowPreviousPageButton="False" FirstPageText="首页" LastPageText="尾页" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </div>
        </LayoutTemplate>
    </asp:ListView>
</div>
</asp:Content>
