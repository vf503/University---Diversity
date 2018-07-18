<%@ Page Language="C#" MasterPageFile="~/MasterPager/Normal.Master" AutoEventWireup="true" CodeBehind="Level3Ajax.aspx.cs" Inherits="colleges.Level3Ajax" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //var Lv2CurrentAlias = document.getElementById("").innerHTML;
        $(function() {
            $("#Level3Tree").accordion({
                header: "a.lv3header",
                event: "dblclick",
                active: parseInt($("#HideLv2Alias").html()),
                heightStyle: "content",
                icons: null
            })
            $("#"+$.getUrlParam("alias")).css("color", "#fe0100");
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
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div class="FullContent">
        <div id="TraceBox">
            <span>当前位置：</span> <span><a href="index.aspx">首页</a></span> <span>〉</span>
            <asp:HyperLink ID="TraceLv2Link" runat="server"><asp:Label ID="CurrentTrace" runat="server" Text=""></asp:Label></asp:HyperLink>
        </div>
        <div id="Level2Main">
            <div id="Level2MainLeft">
                <span id="HideLv2Alias" class="HideInfoLable"><%=Lv2CategoriesIndex%></span>
                <div class="Level3HotTreeTitle">
                    <asp:Label ID="CurrentCategoryName" runat="server" Text=""></asp:Label>
                </div>
                <asp:ListView ID="Lv3Navi" runat="server" OnItemDataBound="Lv3Navi_ItemDataBound">
                    <ItemTemplate>
                        <a id="<%# Eval("CategoryAlias") %>" class="lv3header" href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_self">
                            <%# Eval("CategoryName")%></a>
                        <div>
                        <asp:Label ID="CurrentLv2Alias" CssClass="HideInfoLable" runat="server" Text='<%# Eval("CategoryAlias") %>'></asp:Label>
                            <ul>
                                <asp:ListView ID="Lv3Items" runat="server">
                                    <ItemTemplate>
                                        <li><a id="<%# Eval("CategoryAlias") %>" href="level3.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=1" target="_self">
                                        <%# colleges.DataProcessing.SubstringText(Eval("CategoryName").ToString(),10) %></a></li>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <asp:Panel ID="itemPlaceholder" runat="server">
                                        </asp:Panel>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <div class="Level3Tree" id="Level3Tree">
                            <asp:Panel ID="itemPlaceholder" runat="server">
                            </asp:Panel>
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
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>