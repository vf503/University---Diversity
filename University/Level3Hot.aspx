<%@ Page Language="C#" MasterPageFile="~/MasterPager/Normal.Master" AutoEventWireup="true" CodeBehind="Level3Hot.aspx.cs" Inherits="colleges.Level3Hot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div class="FullContent">
        <div id="TraceBox">
            <span>当前位置：</span> <span><a href="index.aspx">首页</a></span> <span>〉</span>
            <asp:Label ID="CurrentTrace" runat="server" Text=""></asp:Label>
        </div>
        <div id="Level2Main">
            <div id="Level2MainLeft">
                <div class="Level3HotTreeTitle">
                    <asp:Label ID="CurrentCategoryName" runat="server" Text=""></asp:Label>
                </div>
                <asp:ListView ID="Lv3HotNavi" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <a href="Level3Hot.aspx?alias=<%# Eval("CategoryAlias") %>" target="_self">
                                    <%# Eval("CategoryName") %></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <div class="Level3Tree" id="Level3Tree">
                            <table class="Lv3HotList">
                                <asp:Panel ID="itemPlaceholder" runat="server">
                                </asp:Panel>
                            </table>
                        </div>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
            <div id="Level2MainRight">
                <div id="Level3MainList">
                    <div id="Level3MainListTitle">
                        <%--<form action="" id="Level3MainListFunction">--%>
                        <label id="SearchResultOrderTxt">
                            排序方式:</label>
                        <asp:RadioButton ID="RadioAsc" CssClass="SearchResultOrderRadio" runat="server" OnCheckedChanged="RadioAsc_CheckedChanged"
                            GroupName="DateOrder" AutoPostBack="True" /><label for="" class="SearchResultOrderLabel"><img src="images/OrderUp.png" />日期正序</label>
                        <asp:RadioButton ID="RadioDesc" CssClass="SearchResultOrderRadio" runat="server"
                            OnCheckedChanged="RadioDesc_CheckedChanged" GroupName="DateOrder" 
                            AutoPostBack="True" /><label for=""
                                class="SearchResultOrderLabel"><img src="images/OrderDown.png" />日期倒序</label>
                        <asp:ImageButton ID="PicBtn" ImageUrl="images/SearchResultThumb.png" CssClass="Level3MainListThumb"
                            runat="server" OnClick="PicBtn_Click" />
                        <asp:ImageButton ID="TextBtn" ImageUrl="images/SearchResultList.png" CssClass="Level3MainListList"
                            runat="server" OnClick="TextBtn_Click" />
                        <%--</form>--%>
                    </div>
                    <div id="Level3MainListContent">
                        <asp:ListView ID="Level3MainListPic" runat="server">
                            <LayoutTemplate>
                                <ul>
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </ul>
                                <div class="Level3MainListPagerBox">
                                    <asp:DataPager ID="Level3MainListPicPager" PageSize="20" runat="server" class="Level3MainListPager">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                ShowPreviousPageButton="True" FirstPageText="首页" LastPageText="尾页" />
                                            <asp:NumericPagerField ButtonCount="5" CurrentPageLabelCssClass="Level3MainListPagerCurrent" />
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="True"
                                                ShowPreviousPageButton="False" FirstPageText="首页" LastPageText="尾页" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank"><img class="Level3MainListContentDetailPreview" src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" /></a>
                                    <div class="Level3MainListContentDetailInfo">
                                        <div class="Level3MainListDetailLeft">
                                            <div class="Level3MainListDetailTitle">
                                                <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                                    <%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),34) %></a></div>
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
                                <div class="Level3MainListPagerBox">
                                    <asp:DataPager ID="Level3MainListTextPager" PageSize="30" runat="server"  class="Level3MainListPager">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                ShowPreviousPageButton="True" FirstPageText="首页" LastPageText="尾页" />
                                            <asp:NumericPagerField ButtonCount="5" CurrentPageLabelCssClass="Level3MainListPagerCurrent" />
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="True"
                                                ShowPreviousPageButton="False" FirstPageText="首页" LastPageText="尾页" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="MainListTitle">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),40) %></a>
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>