<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPager/Level2Lite.Master" AutoEventWireup="true" CodeBehind="Level2Lite.aspx.cs" Inherits="colleges.Level2Lite" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        window.onload = function () {

            $("#navTab a").mouseover(function (e) {
                $(this).tab("show");
            });

            //动态加载树形导航
            var seminartreeData;
            $.ajax({
                type: 'get',
                dataType: 'text',
                data: '{}',
                async: true,
                url: 'JsonData/zt.json',
                success: function (data) {
                    //$.parseJSON(data);
                    seminartreeData = JSON.parse(data);
                    //tree = eval(data);
                    //专题导航
                    $('#seminarTree').treeview({
                        data: seminartreeData,
                        enableLinks: true,
                        levels: 1,
                        selectedColor: '#000000',
                        selectedBackColor: '#FFFFFF'
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
            });
            var sujecttreeData;
            $.ajax({
                type: 'get',
                dataType: 'text',
                data: '{}',
                async: true,
                url: 'JsonData/xk.json',
                success: function (data) {
                    //$.parseJSON(data);
                    sujecttreeData = JSON.parse(data);
                    //tree = eval(data);
                    //学科导航
                    $('#sujectTree').treeview({
                        data: sujecttreeData,
                        enableLinks: true,
                        levels: 1,
                        selectedColor: '#000000',
                        selectedBackColor: '#FFFFFF'
                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
            });
            //
            //$('#seminarTree').treeview('expandNode', ['818f536a0a4242c98f8191b703a3b25f', { levels: 2, silent: true }]);
            //
            //动态加载课程信息
            inittable();
            $("input[name=orderNum]").change(function () {
                var order = $("input[name=orderNum]:checked").val();
                var id = $.getUrlParam('id');
                //inittable();
                $('#table').bootstrapTable('refresh', {
                    url: 'DataAdapter/lite.ashx?method=course&id=' + id,
                    queryParams: function queryParams(params) {   //向后台传递参数 
                        var param = {
                            orderNum: $("input[name=orderNum]:checked").val()
                        };
                        return param;
                    }
                });
                //alert($("input[name=orderNum]:checked").val());
                // alert(id);
            });
            

        }

        function inittable() {
            var id = $.getUrlParam('id');
            if (id == "") { id = "818f536a0a4242c98f8191b703a3b25f"; }
            //alert(id);
            $('#table').bootstrapTable({
                url: 'DataAdapter/lite.ashx?method=course&id=' + id,
                method: "get",  //使用get请求到服务器获取数据 
                striped: true,  //表格显示条纹  
                pagination: true, //启动分页  
                pageSize: 10,  //每页显示的记录数  
                pageNumber: 1, //当前第几页  
                //pageList: [5, 10, 15, 20, 25],  //记录数可选列表  
                search: false,  //是否启用查询 
                showHeader:false,
                //showColumns: true,  //显示下拉框勾选要显示的列  
                showRefresh: false,  //显示刷新按钮  
                sidePagination: "client", //表示服务端请求 
                queryParams: function queryParams(params) {   //向后台传递参数 
                    var param = {
                        orderNum: $("input[name=orderNum]:checked").val()
                    };
                    return param;
                },
                columns: [
                                {
                                    field: "pic",//键名
                                    sortable: false,//是否可排序
                                    formatter: function (value, row, index) {
                                        var url = value.replace(/\s+/g, "");;
                                        var str = '<a href="ShowVideo.aspx?ID=' + row.id + '" title="' + row.title+ '" target="_blank">'
                                                   + '<img class="Level3MainListContentDetailPreview" src="' + url + '"></a>'
                                        return str;
                                    }
                                },
                                {
                                    field: "title",
                                    sortable: false,
                                    titleTooltip: "this is name",
                                    formatter: function (value, row, index) {
                                        var htmlStr = '<div class="Level3MainListContentDetailInfo">'
                                                    + '<div class="Level3MainListDetailLeft">'
                                                    + '<div class="Level3MainListDetailTitle">'
                                                    + '<a href="ShowVideo.aspx?ID=' + row.id + '" title="' + row.title + '" target="_blank">'
                                                    + value+'</a></div>'
                                                    + '<div class="Level3MainListDetailInfo">'
                                                    + '<ul>'
                                                    + '<li style="width: 95%;">'
                                                    + '<span>主讲人:</span> <span class="Level3MainListInfoTxt">' + row.teacher + '</span>'
                                                    + '</li style="width: 50%;">'
                                                    + '<li>'
                                                    + '<span>职务:</span> <span class="Level3MainListInfoTxt">' + row.postion + '</span>'
                                                    + '</li>'
                                                    + '</ul>'
                                                    + '<ul>'
                                                    + '<li style="width: 50%;">'
                                                    + '<span>时长:</span> <span class="Level3MainListInfoTxt">' + row.length + '</span>'
                                                    + '</li>'
                                                    + '<li style="width: 50%;">'
                                                    + '<span>日期:</span> <span class="Level3MainListInfoTxt">' + row.date + '</span>'
                                                    + '</li>'
                                                    + '</ul>'
                                                    + '</div>'
                                                    + '</div>'
                                                    + '</div>';
                                        return htmlStr;
                                    }
                                }
                                //{
                                //    field: "teacher",
                                //    sortable: false, formatter: function (value, row, index) {
                                //        return '<p>主讲人：' + value + '</p><p>时长：' + row.price + '</p>';
                                //    }
                                //},
                                //{
                                //    title: "length",
                                //    field: "length",
                                //    sortable: false,
                                //    sortable: true,//是否可排序
                                //    order: "asc"//默认排序方式
                                //}, {
                                //    field: "postion",
                                //    sortable: false
                                //}, {
                                //    title: "date",
                                //    field: "date",
                                //    sortable: true,//是否可排序
                                //    order: "asc",//默认排序方式
                                //    visible: true
                                //}
                ],
                onLoadSuccess: function (data) {  //加载成功时执行  
                   // alert(data);
                },
                onLoadError: function () {  //加载失败时执行  
                    alert("加载数据失败");
                }
            });
        }

        (function ($) {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);
    </script>

</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div id="Level2Main">      
        <div class="Level2Nav">
            <div class="NotableShowTitle">
                <ul id="navTab" class="nav nav-tabs">
                    <li class="active" style="width:114px;">
                        <a href="#seminar" style="width:114px; text-align:center" data-toggle="tab">专题导航</a>
                    </li>
                    <li class="" style="width:114px;">
                        <a href="#suject" style="width:114px; text-align:center" data-toggle="tab">学科导航</a>
                    </li>
                </ul>
            </div>
            <div class="MainVerticalShowIndexNav" style="height:1000px">
                <div id="navTabContent" class="tab-content">
                    <div class="tab-pane fade  active in" id="seminar">
                        <%--专题树形导航--%>
                        <div id="seminarTree" class="treeview"></div>
                    </div>
                    <div class="tab-pane fade" id="suject">
                        <%--学科树形导航--%>
                        <div id="sujectTree" class="treeview"></div>
                    </div>
                </div>
            </div>
        </div>
         
         <div  class="Level2Center">
             <div id="Level3MainList">
                 <div id="Level3MainListTitle" style="font-size: 12px">
                     <asp:Label ID="Trace" runat="server"></asp:Label>
                 </div>
                 <div style="padding:5px 0 5px 0; height:30px">
                     <label id="SearchResultOrderTxt" style="text-indent: 20px">排序方式:</label>
                     <span class="SearchResultOrderRadio">
                         <input id="ctl00_IndexMainPlaceHolder_RadioAsc" type="radio" name="orderNum" value="ascending"></span>
                     <label for="" class="SearchResultOrderLabel" style="font-size: 12px">
                         <img src="images/OrderUp.png">正序</label>
                     <span class="SearchResultOrderRadio">
                         <input id="ctl00_IndexMainPlaceHolder_RadioDesc" type="radio" name="orderNum" value="descending" checked="checked"></span>
                     <label for="" class="SearchResultOrderLabel" style="font-size: 12px">
                         <img src="images/OrderDown.png">倒序</label>
                 </div>
                <table id="table"></table>
            </div>
         </div>
      
          <div id="Level2MainLeft">
              <asp:ListView ID="LeftPicList" runat="server">
                <ItemTemplate>
                    <div class ="Level2LeftPicBox">
                        <a href="SearchLite.aspx?act=lv2h&ID=<%# Eval("CategoryGuid") %>" target="_blank">
                            <img class="Level2LeftPic" src="ShowBytePic.aspx?Title=<%# colleges.DataProcessing.UrlEnCode(Eval("CategoryName").ToString()) %>&Alias=<%=LeftPicAliasText%>" /></a>
                        <div class="Level2LeftPicText">
                            <a href="SearchLite.aspx?act=lv2h&ID=<%# Eval("CategoryGuid") %>" target="_blank" title="<%#GetLeftPicTextByTitle(Eval("CategoryName").ToString(),false)%>">
                                <%#colleges.DataProcessing.SubstringText(GetLeftPicTextByTitle(Eval("CategoryName").ToString(),false),54)%></a>
                            <span class="Level2LeftPicTextEnd">
                                <%#GetLeftPicTextByTitle(Eval("CategoryName").ToString(),true)%>
                            </span>
                        </div>

                    </div>
                </ItemTemplate>
                <LayoutTemplate>
                    <asp:Panel ID="itemPlaceholder" runat="server">
                    </asp:Panel>
                </LayoutTemplate>
            </asp:ListView>
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
        
            <%--<div class ="Level2LeftPicBox"><asp:Image ID="Level2LeftPicBot" CssClass="Level2LeftPic" runat="server" /></div>--%>
        </div>
    </div>
</asp:Content>