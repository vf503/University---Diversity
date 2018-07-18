<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPager/HomeLite.Master" AutoEventWireup="true" CodeBehind="SearchLite.aspx.cs" Inherits="colleges.SearchLite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
      <div id="TraceBox" class="DefaultPage">
        <span>当前位置：</span> <span><a href="index.aspx">首页</a></span> <span>〉</span>
        <asp:HyperLink ID="TraceLv2Link" Target="_blank" runat="server">
            <asp:Label ID="TraceLv2Name" runat="server" Text=""></asp:Label></asp:HyperLink>
        <%=sActionStr%>
    </div>
    <div id="SearchResultBox">
        <%if (Request.QueryString["act"] != "zt")
          { %>
        <div id="SearchResultTitle">
            <form action="" id="SearchResultFunction">
            <div class="SearchResultTitleLeft">
                <label id="SearchResultOrderTxt">
                    排序方式:</label>
                <input type="radio" name="SearchResultOrder" id="" class="SearchResultOrderRadio"
                    value="asc" /><label for="" class="SearchResultOrderLabel"><img src="images/OrderUp.png" />日期正序</label>
                <input type="radio" name="SearchResultOrder" id="" class="SearchResultOrderRadio"
                    value="desc" checked="checked" /><label for="" class="SearchResultOrderLabel"><img
                        src="images/OrderDown.png" />日期倒序</label>
            </div>
            <div class="SearchResultTitleRight">
                <input type="button" name="" id="SearchResultThumb" />
                <input type="button" name="" id="SearchResultList" /></div>
            </form>
        </div>
        <%} %>
        <div id="SearchResultContent">
            <ul>
                <%=sOutLiStr%></ul>
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
                    <%=sOutTrStr%>
                </tbody>
            </table>
            <div class="SearchPager">
                <%=sSplitContent %></div>
        </div>
    </div>

    <script type="text/javascript">
        var ShowType = "";
        $(function() {
            $("input[name=SearchResultOrder][value=<%=sSort %>]").attr("checked", true);
            ChangeShow("<%=sShow %>");

            $("#SearchResultThumb").click(function() { ChangeShow("Thumb"); $(this).hide(); $("#SearchResultList").show(); });

            $("#SearchResultList").click(function() { ChangeShow("List"); $(this).hide(); $("#SearchResultThumb").show(); });

            $("input[name=SearchResultOrder]").click(function() {
                var sValue = $(this).attr("value");
                $("input[name=SearchResultOrder][value=" + sValue + "]").attr("checked", true);
                var sHref = $("div.SearchPager span a").eq(0).attr("href"); //*CC*确认选择器class=SearchPager正确
                if (sHref.indexOf("&show=") > 0) {
                    sHref = sHref.substring(0, sHref.indexOf("&show="));
                }
                sHref += "&show=" + ShowType;
                if (typeof (sValue) != "undefined") {
                    sHref += "&s=" + sValue;
                }
                location.href = sHref;
            });
        });
        function PageUrlFilter() {
            var sSort = $("input[name=SearchResultOrder]:checked").val();
            $("div.SearchPager span a").each(function() {//*CC*
                var sHref = $(this).attr("href");
                if (sHref.indexOf("&show=") > 0) {
                    sHref = sHref.substring(0, sHref.indexOf("&show="));
                }
                sHref += "&show=" + ShowType;
                if (typeof (sSort) != "undefined") {
                    sHref += "&s=" + sSort;
                }
                $(this).attr("href", sHref);
            });
            $("div.SearchPager option").each(function() {//*CC*
                var sValue = $(this).attr("value");
                if (sValue.indexOf("&show=") > 0) {
                    sValue = sValue.substring(0, sValue.indexOf("&show="));
                }
                sValue += "&show=" + ShowType;
                if (typeof (sSort) != "undefined") {
                    sValue += "&s=" + sSort;
                }
                $(this).attr("value", sValue);
            });

        }
        function ChangeShow(act) {
            ShowType = act;
            if (act == "Thumb" || act == "thumb") {
                $("#SearchResultThumb").hide();
                $("#SearchResultList").show();
                $("#SearchResultContent ul").show();
                $("#SearchResultContent table.SearchResultContentTable").hide();
            }
            if (act == "List" || act == "list") {
                $("#SearchResultThumb").show();
                $("#SearchResultList").hide();
                $("#SearchResultContent table.SearchResultContentTable").show();
                $("#SearchResultContent ul").hide();
            }
            PageUrlFilter();

        }
        mini.parse();
        var tip = new mini.ToolTip();
        tip.set({
            target: document,
            selector: '[data-tooltip]',
            //width:400
            onbeforeopen: function(e) {
                var el = e.element;
                if ($(el).attr("data-tooltip").length <= 0) {
                    e.cancel = true;
                }
                var iW = $(el).width();
                iW = iW <= 400 ? 400 : iW;
                tip.setWidth(iW);
            }
        }
);
    </script>
</asp:Content>
