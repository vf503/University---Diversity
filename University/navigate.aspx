<%@ Page Language="C#" MasterPageFile="~/MasterPager/Normal.Master" AutoEventWireup="true"
    CodeBehind="navigate.aspx.cs" Inherits="colleges.navigate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
     $(function() {

         $("#FocusTabs").tabs({
             event: "mouseover"
         });
     });
    </script>

</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div id="TraceBox" class="DefaultPage">
        <span>当前位置：</span> <span><a href="index.aspx">首页</a></span> <span>〉</span>
        导航
    </div>
    <div id="NavigateBox">
        <div class="NavigateTitle">
            重点学科
        </div>
        <asp:ListView ID="NaviList1" runat="server">
            <ItemTemplate>
                <li><a href="Level3Navi.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0"">
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
        <div class="NavigateLine">
        </div>
        <div class="NavigateTitle">
            专业设置
        </div>
        <asp:ListView ID="NaviList2" runat="server">
            <ItemTemplate>
                <li><a href="Level3Navi.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0"">
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
        <div class="NavigateLine">
        </div>
        <div class="NavigateTitle">
            机构分类
        </div>
        <asp:ListView ID="NaviList3" runat="server">
            <ItemTemplate>
                <li><a href="Level3Navi.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0"">
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
</asp:Content>
