<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPager/HomeLite.Master" AutoEventWireup="true" CodeBehind="Level2GroupLite.aspx.cs" Inherits="colleges.Level2GroupLite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
       
        window.onload = function () {
          
            $(".Level2MainBannerNoDisplay").eq(0).attr("class", "Level2MainBanner");

            $("#navTab a").mouseover(function (e) {
                $(this).tab("show");
            });


            //动态加载树形导航
            var MainData;
            $.ajax({
                type: 'get',
                dataType: 'text',
                data: '{}',
                async: true,
                url: 'JsonData/qy.json',
                success: function (data) {
                    //$.parseJSON(data);
                    MainData = JSON.parse(data);
                    //tree = eval(data);
                    //专题导航
                    $('#MainTree').treeview({
                        data: MainData,
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
           
            //动态加载课程信息
            inittable();
            $("input[name=orderNum]").change(function () {
                inittable();
                //alert($("input[name=orderNum]:checked").val());
            });
        }




        function inittable() {
            var id = $.getUrlParam('id');
            if (id == null) { id = "0f9364f1992a4f40ad76435f279a79f6" }
            $('#table').bootstrapTable({
                url: 'DataAdapter/lite.ashx?method=GroupCourse&id=' + id,
                //url: 'JsonData/front.json',
                method: "get",  //使用get请求到服务器获取数据 
                async:true,
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
                                    field: "id",//键名
                                    sortable: false,//是否可排序
                                    formatter: function (value, row, index) {
                                        var str = '<a href="Level3GroupLite.aspx?ID=' + row.id + '" title="" target="_blank">'
                                                   + '<img class="Level3MainListContentDetailPreview" src="' + row.pic + '"></a><div class="Level3MainListContentDetailPreviewTitle">' + row.title + '</div>'
                                        return str;
                                    }
                                },
                                {
                                    field: "id",
                                    sortable: false,
                                    titleTooltip: "this is name",
                                    formatter: function (value, row, index) {
                                        var len = row.list.length;
                                        var htmlStr = "<div class='FrontInfoHeight'>";
                                        for (var i = 0; i < len; i++) {
                                            htmlStr += '<div class="FrontMainListDetailInfo">'
                                                       + '<ul>'
                                                        + '<li style="width: 50%;"><span class="FrontMainListDetailTitle"></span>'
                                                        + '<span><a href="ShowVideo.aspx?ID=' + row.list[i].id + '" title="" target="_blank">' + row.list[i].title + '</a></span>'
                                                        + '</li>'
                                                        + '<li style="width: 15%;">'
                                                        + '<span>' + row.list[i].teacher + '</span>'
                                                        + '</li>'
                                                        + '<li style="width: 35%;">'
                                                        + '<span>' + row.list[i].postion + '</span>'
                                                        + '</li>'
                                                        + '</ul></div>';
                                        }
                                        return htmlStr+"</div>";
                                        //var htmlStr = '<table class="FrontMainListDetailInfo">'
                                        //            + '<tbody><tr>'
                                        //            + '<td><span class="con-color glyphicon glyphicon-play"></span>'
                                        //            + '<span>' + row.title + '</span>'
                                        //            + '</td>'
                                        //            + '<td>'
                                        //            + '<span>' + row.teacher + '</span>'
                                        //            + '</td>'
                                        //            + '<td>'
                                        //            + '<span>' + row.postion + '</span>'
                                        //            + '</td>'
                                        //            + '</tr>'
                                        //            + '</tbody></table>';
                                        //return htmlStr;
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
<asp:Content ID="Content2" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div id="Level2Main">
        
        <%--<asp:ListView ID="Level2MainRight" GroupItemCount="4" OnItemDataBound="Level2MainRight_ItemDataBound"
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
        </asp:ListView>--%>
      
          <div class="Level2Nav">
            <div class="NotableShowTitle">
                <ul id="navTab" class="nav nav-tabs">
                    <li class="active">
                        <a href="#seminar" data-toggle="tab">论坛会议</a>
                    </li>
                </ul>
            </div>
            <div class="MainVerticalShowIndexNav" style="height:1000px;overflow-y:scroll; overflow-x:scroll;">
                <div id="navTabContent" class="tab-content" style="min-width:380px">
                    <div class="tab-pane fade  active in" id="seminar">
                        <div id="MainTree" class="treeview"></div>
                    </div>
                </div>
            </div>
        </div>
         
         <div  class="Level2Center">
             <div id="FrontRightList"> 
                 <div id="Level3MainListTitle">
                     <asp:Label ID="Trace" runat="server" Font-Size="12px" Font-Bold="false" ForeColor="#666666"></asp:Label>
                        <%--<label id="SearchResultOrderTxt">
                            排序方式:</label>
                        <span class="SearchResultOrderRadio"><input id="ctl00_IndexMainPlaceHolder_RadioAsc" type="radio" name="orderNum" value="RadioAsc"></span><label for="" class="SearchResultOrderLabel"><img src="images/OrderUp.png">日期正序</label>
                        <span class="SearchResultOrderRadio"><input id="ctl00_IndexMainPlaceHolder_RadioDesc" type="radio" name="orderNum" value="RadioDesc" checked="checked"></span><label for="" class="SearchResultOrderLabel"><img src="images/OrderDown.png">日期倒序</label>--%>
                        
                    </div>
                <table id="table"></table>
            </div>
         </div>
      
    </div>
</asp:Content>