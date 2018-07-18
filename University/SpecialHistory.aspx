<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SpecialHistory.aspx.cs" Inherits="colleges.SpecialHistory" Title="中经视频高校新版"%>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function() {
        $("#" + $.getUrlParam("ID")).css("color", "#2f7bdb");
        });
        (function($) {
            $.getUrlParam = function(name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="Level2Main">
            <div id="Level2MainLeft">
                <div class="SpecialTreeTitle">
                    历史回顾
                </div>
                <asp:ListView ID="SpecialTree" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="SpecialTreeItem">
                            <a id="<%#Eval("CategoryGuid")%>" href="SpecialHistory.aspx?ID=<%#Eval("CategoryGuid")%>"><%#Eval("CategoryName")%></a></div>
                        <a id="A1" href="SpecialHistory.aspx?ID=<%#Eval("CategoryGuid")%>"><img class="SpecialTreeItemImg" src="images/<%#Eval("CategoryAlias")%>.png" /></a>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div id="Level2MainRight">
                <div id="Level3MainList">
                    <div id="Level3MainListTitle">
                        <label id="SearchResultOrderTxt">
                            排序方式:</label>
                        <asp:RadioButton ID="RadioAsc" CssClass="SearchResultOrderRadio" runat="server" OnCheckedChanged="RadioAsc_CheckedChanged"
                            GroupName="DateOrder" AutoPostBack="True" /><label for="" class="SearchResultOrderLabel"><img src="images/OrderUp.png" />正序</label>
                        <asp:RadioButton ID="RadioDesc" CssClass="SearchResultOrderRadio" runat="server"
                            OnCheckedChanged="RadioDesc_CheckedChanged" GroupName="DateOrder" 
                            AutoPostBack="True" /><label for=""
                                class="SearchResultOrderLabel"><img src="images/OrderDown.png" />倒序</label>
                    </div>
                    <div id="Level3MainListContent">
                        <asp:ListView ID="ZTMainList" runat="server">
                            <LayoutTemplate>
                                <ul>
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </ul>
                                <div class="Level3MainListPagerBox">
                                    <asp:DataPager ID="ZTMainListPicPager" PageSize="20" runat="server" class="Level3MainListPager">
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
                                <li><a href="SpecialAttention.aspx?ID=<%#Eval("CategoryGuid")%>&Title=<%#HttpUtility.UrlEncode(Eval("CategoryName").ToString())%>" target="_blank">
                                    <img class="SearchResultContentDetailPreview" src="ShowZTImage.aspx?Title=<%#HttpUtility.UrlEncode(Eval("CategoryName").ToString())%>" /></a>
                                    <div class="ZTHistoryContentDetailInfo">
                                        <div class="SearchResultDetailLeft">
                                            <div class="SearchResultDetailTitle">
                                                <a href="SpecialAttention.aspx?ID=<%#Eval("CategoryGuid")%>&Title=<%#HttpUtility.UrlEncode(Eval("CategoryName").ToString())%>" target="_blank">
                                                    <%#Eval("CategoryName")%></a></div>
                                        </div>
                                        <div class="ZTHistorySummary">
                                            简介：<span><%#Eval("PlainText")%></span></div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
