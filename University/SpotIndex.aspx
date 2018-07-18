<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpotIndex.aspx.cs" Inherits="colleges.SpotIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/DefaultLayout.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jquery-webox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="Scripts/boot.js"></script>
    <style>
        .foot
        {
            float: left;
            margin-left:7px;
            width: 1002px;
            height: 90px;
            background: url(/images/EmbedFoot.jpg) repeat-x;
        }
        .foot p
        {
            text-align: center;
            padding-top: 3px;
            height: 20px;
        }
        .foot .p2
        {
            padding-top: 20px;
            line-height: 40px;
            margin-left: 224px;
        }
        .foot .p2 .red
        {
            color: red;
        }
        .foot .p2 .tubiao
        {
            display: inline-block;
            float: left;
            background: url(/images/EmbedIcon.jpg) center center no-repeat;
            height: 40px;
            width: 22px;
        }
        .foot .p2 span
        {
            float: left;
            display: inline-block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <img src="images/EmbedLogo.jpg" style="float: left; margin-left: 7px;"></div>
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
                    <%--<input type="button" name="" id="SearchResultThumb" />
                    <input type="button" name="" id="SearchResultList" />--%></div>
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
                                名称
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
        <div class="foot">
            <p>
            </p>
            <p class="p2">
                <span>国家信息中心 中经网数据有限公司 版权所有 建议使用1024*768分辨率浏览<font class="red"> 京ICP证000069号&nbsp;</font></span><a
                    class="tubiao" href="#"></a></p>
        </div>
    </div>
    </form>

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

</body>
</html>