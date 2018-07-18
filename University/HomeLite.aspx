<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPager/HomeLite.Master" AutoEventWireup="true" CodeBehind="HomeLite.aspx.cs" Inherits="colleges.HomeLite" %>
<%@ OutputCache Duration="3600" VaryByParam="showlogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="renderer" content="ie-stand" />
    
    <script type="text/javascript" src="Scripts/jquery-ui-tabs-rotate.js"></script>
    <script type="text/javascript" src="Scripts/jquery.tabs.js"></script>
    <script type="text/javascript">
       
        window.onload = function () {
            $("#FocusTabs").tabs({ event: "mouseover" }).tabs("rotate", 3000);
            $("#navTab a").mouseover(function (e) {
                $(this).tab("show");
            });
            $("#orgTab a").mouseover(function (e) {
                $(this).tab("show");
            });

            $(".tab_Index1_menu li:first").addClass("current");
            $("#tab_Index1_box2 div.tab_Index1_box2_item:first").removeClass("hide");
            $("div.tab_Index1_box_item:first").removeClass("hide");
            $("#TabsIndex1").TabsIndex1();
            //
            $(".tab_Index2_menu li:first").addClass("current");
            $("div.tab_Index2_box_item:first").removeClass("hide");
            $("#TabsIndex2").TabsIndex2();

            //加载首页数据—热点推送、前沿论坛、经济风向、管理课堂、素养提升、名师在线、机构
            $.ajax({
                type: 'get',
                dataType: 'json',
                data: '{}',
                async:true,
                url: 'DataAdapter/lite.ashx?method=index',
                success: function (data) {
                   // tree = JSON.parse(data);
                    //alert(data.front);
                    //加载前沿论坛模块数据
                    var frontTitleList = "";
                    var frontImgList = "";
                    var frontImgObj = data.front.list;
                    //var frontTitleObj = data.front.sort;
                    //for (var i = 0; i < frontTitleObj.length; i++) {
                    //    frontTitleList += "<a href='Level2GroupLite.aspx?id=" + frontTitleObj[i].id + "' target='_blank'>" + frontTitleObj[i].title + "</a>";
                    //}
                        for (var i = 0; i < frontImgObj.length; i++) {
                        frontImgList += "<div class='ColumnVideo'>"
                                      + "<div class='ColumnVideoPicBox'>"
                                      + "<a href='Level3GroupLite.aspx?id=" + frontImgObj[i].id + "' title='" + frontImgObj[i].title + "' target='_blank'>"
                                      + "<img src='" + frontImgObj[i].PicUrl + "'></a>"
                                      + "</div>"
                                      + "<div class='ColumnVideoLink'>"
                                      + "<a href='Level3GroupLite.aspx?id=" + frontImgObj[i].id + "' title='" + frontImgObj[i].title + "' target='_blank'>" + frontImgObj[i].title + "</a>"
                                      + "</div>"
                                      + "</div>";
                        }
                        //$("#front .ColumnShowSort").html(frontTitleList);
                        $("#front .ColumnVideoContent").html(frontImgList);

                    //加载经济风向模块数据
                        var economicTitleList = "";
                        var economicLeftList = "", economicRightList = "";
                        var economicListObj = data.economic.list;
                        var economicTitleObj = data.economic.sort;
                        //for (var i = 0; i < economicTitleObj.length; i++) {
                        //    economicTitleList += "<a href='level2lite.aspx?id=" + economicTitleObj[i].id + "' target='_blank'>" + economicTitleObj[i].title + "</a>";
                        //}
                        for (var i = 0; i < economicListObj.length; i++) {
                            if (i < 6) {
                                economicLeftList += "<div class='ColumnOtherLink'>"
                                         + "<span class='ColumnOtherLeft'><a href='ShowVideo.aspx?ID=" + economicListObj[i].id + "' title='" + economicListObj[i].title + "' target='_blank'>" + economicListObj[i].title + "</a></span>"
                                         + "<span class='ColumnOtherRight'>" + economicListObj[i].author + "</span>"
                                         + "</div>";
                            } else {
                                economicRightList += "<div class='ColumnOtherLink'>"
                                        + "<span class='ColumnOtherLeft'><a href='ShowVideo.aspx?ID=" + economicListObj[i].id + "' title='" + economicListObj[i].title + "' target='_blank'>" + economicListObj[i].title + "</a></span>"
                                        + "<span class='ColumnOtherRight'>" + economicListObj[i].author + "</span>"
                                        + "</div>";
                            }

                        }
                        //$("#economic .ColumnShowSort").html(economicTitleList);
                        $("#economic .ColumnOther:eq(0)").html(economicLeftList);
                        $("#economic .ColumnOther:eq(1)").html(economicRightList);

                    //加载管理课堂模块数据
                        var manageClassTitleList = "";
                        var manageClassLeftList = "", manageClassRightList = "";
                        var manageClassListObj = data.manageClass.list;
                        var manageClassTitleObj = data.manageClass.sort;
                        //for (var i = 0; i < manageClassTitleObj.length; i++) {
                        //    manageClassTitleList += "<a href='level2lite.aspx?id="+ manageClassTitleObj[i].id +  "' target='_blank'>" + manageClassTitleObj[i].title + "</a>";
                        //}
                        for (var i = 0; i < manageClassListObj.length; i++) {
                            if (i < 6) {
                                manageClassLeftList += "<div class='ColumnOtherLink'>"
                                         + "<span class='ColumnOtherLeft'><a href='ShowVideo.aspx?ID=" + manageClassListObj[i].id + "' title='" + manageClassListObj[i].title + "' target='_blank'>" + manageClassListObj[i].title + "</a></span>"
                                         + "<span class='ColumnOtherRight'>" + manageClassListObj[i].author + "</span>"
                                         + "</div>";
                            } else {
                                manageClassRightList += "<div class='ColumnOtherLink'>"
                                        + "<span class='ColumnOtherLeft'><a href='ShowVideo.aspx?ID=" + manageClassListObj[i].id + "' title='" + manageClassListObj[i].title + "' target='_blank'>" + manageClassListObj[i].title + "</a></span>"
                                        + "<span class='ColumnOtherRight'>" + manageClassListObj[i].author + "</span>"
                                        + "</div>";
                            }

                        }
                        //$("#manageClass .ColumnShowSort").html(manageClassTitleList);
                        $("#manageClass .ColumnOther:eq(0)").html(manageClassLeftList);
                        $("#manageClass .ColumnOther:eq(1)").html(manageClassRightList);

                    //加载素养提升模块数据
                        var attainmentTitleList = "";
                        var attainmentImgList = "";
                        var attainmentClassLeftList="";
                        var attainmentClassRightList = "";
                        var attainmentImgObj = data.attainment.list;
                        var attainmentTitleObj = data.attainment.sort;
                        //for (var i = 0; i < attainmentTitleObj.length; i++) {
                        //    attainmentTitleList += "<a href='level2lite.aspx?id=" + attainmentTitleObj[i].id + "' target='_blank'>" + attainmentTitleObj[i].title + "</a>";
                        //}
                       
                        //
                        for (var i = 0; i < attainmentImgObj.length; i++) {
                            if (i < 5) {
                                attainmentImgList += "<div class='ColumnVideo'>"
                                    + "<div class='ColumnVideoPicBox'>"
                                    + "<a href='ShowVideo.aspx?ID=" + attainmentImgObj[i].id + "' title='" + attainmentImgObj[i].title + "' target='_blank'>"
                                    + "<img src='" + attainmentImgObj[i].PicUrl + "'></a>"
                                    + "</div>"
                                    + "<div class='ColumnVideoLink'>"
                                    + "<a href='ShowVideo.aspx?ID=" + attainmentImgObj[i].id + "' title='" + attainmentImgObj[i].title + "' target='_blank'>" + attainmentImgObj[i].title + "</a>"
                                    + "</div>"
                                    + "</div>";
                            }
                            else if (i >= 5 && i < 7)
                            {
                                attainmentClassLeftList += "<div class='ColumnOtherLink'>"
                                    + "<span class='ColumnOtherLeft'><a href='ShowVideo.aspx?ID=" + attainmentImgObj[i].id + "' title='" + attainmentImgObj[i].title + "' target='_blank'>" + attainmentImgObj[i].title + "</a></span>"
                                    + "<span class='ColumnOtherRight'>" + attainmentImgObj[i].author + "</span>"
                                    + "</div>";
                            }
                            else
                            {
                                attainmentClassRightList += "<div class='ColumnOtherLink'>"
                            + "<span class='ColumnOtherLeft'><a href='ShowVideo.aspx?ID=" + attainmentImgObj[i].id + "' title='" + attainmentImgObj[i].title + "' target='_blank'>" + attainmentImgObj[i].title + "</a></span>"
                            + "<span class='ColumnOtherRight'>" + attainmentImgObj[i].author + "</span>"
                            + "</div>";
                            }
                        }

                        $("#attainment .ColumnVideoContent").html(attainmentImgList);
                        $("#attainment .ColumnOther:eq(0)").html(attainmentClassLeftList);
                        $("#attainment .ColumnOther:eq(1)").html(attainmentClassRightList);
                    //加载名师在线模块内容
                        var teacherOnline = "";
                        for(var i=0; i<data.teacherOnline.length; i++){
                            teacherOnline += "<div class='MainVerticalShowVideoPic'>"
                                          + "<a href='ShowVideo.aspx?ID="+data.teacherOnline[i].id+"' title='"+data.teacherOnline[i].title+"' target='_blank'>"
                                          + "<img src='"+data.teacherOnline[i].img+"'></a></div>"
                                          + "<div class='MainVerticalShowInfo'>"
                                          + "<div class='MainVerticalShowInfoTitle'>"
                                          + "<a href='ShowVideo.aspx?ID="+data.teacherOnline[i].id+"' title='"+data.teacherOnline[i].title+"'> "+data.teacherOnline[i].title+"</a></div>"  
                                          + "<div class='MainVerticalShowInfoOther'>" + data.teacherOnline[i].author + "</div>"
                                          + "<div class='MainVerticalShowInfoOther'>" + data.teacherOnline[i].position + "</div>"
                                          + "<div class='MainVerticalShowInfoOther'>" + data.teacherOnline[i].date + "</div>"
                                          + "</div>";
                        }
                        $("#teacher .MainVerticalShowVideo").html(teacherOnline);

                    //加载机构模块内容
                        var organition = "";
                        for (var i = 0; i < data.organition.length; i++) {
                            organition += "<div class='MainVerticalShowVideoPic'>"
                                          + "<a href='ShowVideo.aspx?ID=" + data.organition[i].id + "' title='" + data.organition[i].title + "' target='_blank'>"
                                          + "<img src='" + data.organition[i].img + "'></a></div>"
                                          + "<div class='MainVerticalShowInfo'>"
                                          + "<div class='MainVerticalShowInfoTitle'>"
                                          + "<a href='ShowVideo.aspx?ID=" + data.organition[i].id + "' title='" + data.organition[i].title + "'> " + data.organition[i].title + "</a></div>"
                                          + "<div class='MainVerticalShowInfoOther'>" + data.organition[i].author + "</div>"
                                          + "<div class='MainVerticalShowInfoOther'>" + data.organition[i].position + "</div>"
                                          + "<div class='MainVerticalShowInfoOther'>" + data.organition[i].date + "</div>"
                                          + "</div>";
                        }
                        $("#organization .MainVerticalShowVideo").html(organition);
                    
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
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
                        selectedBackColor: '#FFFFFF',
                        //onNodeSelected: function (event, node)
                        //{ alert(node.id);}
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
        }
    </script>
    <style type="text/css">
        .spinner { width: 60px; height: 60px; margin: 60px; animation: rotate 1.4s infinite ease-in-out, background 1.4s infinite ease-in-out alternate; } @keyframes rotate { 0% { transform: perspective(120px) rotateX(0deg) rotateY(0deg); } 50% { transform: perspective(120px) rotateX(-180deg) rotateY(0deg); } 100% { transform: perspective(120px) rotateX(-180deg) rotateY(-180deg); } } @keyframes background { 0% { background-color: #27ae60; } 50% { background-color: #9b59b6; } 100% { background-color: #c0392b; } }
    </style>   
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div id="Div1" class="MainContent">
        <div id="Div2" class="MainContentLeft">
    <div id="" class="UpsideContent">
        <div id="FocusTabs">
            <asp:ListView ID="FocusTabsTxt" runat="server">
                <ItemTemplate>
                    <li><a href="#tabs-<%#Eval("XIndex")%>" onclick="OpenWin('PicFocusTxtLite.aspx?SubAlias=<%#Eval("CategoryAlias")%>')">
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
                        <a href="PicFocusPicLite.aspx?ID=<%#Eval("ArticleGUID")%>" target="_blank">
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
                    <a href="SpecialIndexLite.aspx?ID=d46335e1f6dd45368eff799832968c48" target="_blank">
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
                        <li class="SpecialFocusTop"><a href="<%#colleges.DataProcessing.GetSubjectLiteLink(Eval("SubjectID").ToString(),Eval("SubjectTitle").ToString())%>"
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
      
    </div>
           <div class="ColumnShow" id="front">
                        <div class="ColumnShowTitle">
                            <span id="ctl00_IndexMainPlaceHolder_ListATitle1">论坛会议</span>
                            <div class="IndexMore">
                                <a id="ctl00_IndexMainPlaceHolder_ListALink1" href="Level2GroupLite.aspx?id=0f9364f1992a4f40ad76435f279a79f6">更多&gt;&gt;</a>
                            </div>
                            <div class="ColumnShowSort">
                            </div>                                
                        </div>
                        <div class="ColumnVideoContent">
                            <div class="spinner"></div>
                        </div>                
                        <div>
                        </div>
                    </div>
            <div class="ColumnShow" id="economic">
                        <div class="ColumnShowTitle">
                            <span id="ctl00_IndexMainPlaceHolder_ListATitle2">经济专题</span>
                            <div class="IndexMore">
                                <a id="ctl00_IndexMainPlaceHolder_ListALink2" href="Level2Lite.aspx?id=260fc8e83cf24a028f00058b8810e8ab">更多&gt;&gt;</a>
                            </div>
                            
                                    <div class="ColumnShowSort">
                                        

                                
                                    </div>
                                
                        </div>
                        
                        <div>
                            
                                    <div class="ColumnOther">
                                        
                                        <div class="spinner"></div>
                                
                                    </div>
                                
                            
                                    <div class="ColumnOther">
                                        
                                    </div>
                                
                        </div>
                    </div>
            <div class="ColumnShow" id="manageClass">
                        <div class="ColumnShowTitle">
                            <span id="ctl00_IndexMainPlaceHolder_ListATitle3">管理专题</span>
                            <div class="IndexMore">
                                <a id="ctl00_IndexMainPlaceHolder_ListALink3" href="level2lite.aspx?id=db99648e53974b6abb58ab60157975aa">更多&gt;&gt;</a>
                            </div>
                            
                                    <div class="ColumnShowSort">

                                
                                    </div>
                                
                        </div>
                        
                        <div>
                            
                                    <div class="ColumnOther">
                                        
<div class="spinner"></div>
                                
                                    </div>
                                
                            
                                    <div class="ColumnOther">
                                        

                                    </div>
                                
                        </div>
                    </div>
            <div class="ColumnShow" id="attainment">
                <div class="ColumnShowTitle">
                    <span id="ctl00_IndexMainPlaceHolder_ListATitle4">综合素养</span>
                    <div class="IndexMore">
                        <a id="ctl00_IndexMainPlaceHolder_ListALink4" href="level2lite.aspx?id=ecf94f50c2ce4a68a810a29655f02134">更多&gt;&gt;</a>
                    </div>

                    <div class="ColumnShowSort">
                    </div>

                </div>

                <div class="ColumnVideoContent">

                    <div class="spinner"></div>

                </div>

                <div>
                    <div class="ColumnOther">
                    </div>
                    <div class="ColumnOther">
                    </div>

                </div>
            </div>
             </div>
        <div id="Div3" class="MainContentRight">
            <div class="hotspot">
                <div class="DailyVideoTitle ">
                    <span>热点推送</span>
                </div>
                <div class="hotspotContent">
                    <asp:ListView ID="ListViewHotpoint" runat="server">
                        <ItemTemplate>
                            <li class="hotspotListCon">
                                <a href="SearchLite.aspx?act=h&ID=<%#Eval("id")%>" title="<%#Eval("title")%>" target="_blank"><%#colleges.DataProcessing.SubstringText(Eval("title").ToString(),15)%></a>
                            </li>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <div id="propelling" class="hotspotContent">
                                <ul>
                                    <asp:Panel ID="itemPlaceholder" runat="server">
                                    </asp:Panel>
                                </ul>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </div>
            <div class="NotableShowTab">
                <div class="NotableShowTitle">
                    <ul id="navTab" class="nav nav-tabs">
                        <li class="active" style="width:143px">
                            <a href="#seminar" style="width:143px;text-align:center" data-toggle="tab">专题导航</a>
                        </li>
                        <li class="" style="width:142px">
                            <a href="#suject" style="width:142px;text-align:center" data-toggle="tab">学科导航</a>
                        </li>
                    </ul>
                </div>
                <div class="MainVerticalShowIndexNav">
                    <div id="navTabContent" class="tab-content">
                        <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                                <div class="tab-pane fade  active in" id="seminar">
                                    <%--专题树形导航--%>
                                    <div id="seminarTree" class="treeview"></div> 
                                    <%--<asp:TreeView ID="SeminarTree" runat="server" OnTreeNodePopulate="TreeView_TreeNodePopulate" EnableClientScript="true" ExpandDepth="0" CssClass=""></asp:TreeView>
                                    <asp:TreeView ID="SeminarTree" runat="server"  ExpandDepth="0" CssClass=""></asp:TreeView>--%>
                                </div>
                                <div class="tab-pane fade" id="suject">
                                    <%--学科树形导航--%>
                                    <div id="sujectTree" class="treeview"></div> 
                                    <%--<asp:TreeView ID="SujectTree" runat="server"  ExpandDepth="0" CssClass=""></asp:TreeView>--%>
                                </div>
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>

                    </div>
                    <div class="NotableShow2">
                        <div class="NotableShowTitle">
                            <ul id="orgTab" class="nav nav-tabs">
                                <li class="active" style="width:143px;">
                                    <a href="#teacher" style="width:143px;text-align:center" data-toggle="tab">名师讲堂
                                    </a>
                                </li>
                                <li style="width:142px;" onclick="javascript:window.open('Level3NaviLite.aspx?alias=gxb2_institue1&IsChild=0','_blank')"><a href="#organization" style="width:142px;text-align:center" data-toggle="tab">机构</a></li>
                            </ul>
                        </div>
                        <div class="MainVerticalShowVideo">
                            <div id="orgTabContent" class="tab-content">
                                <div class="tab-pane fade in active" id="teacher">
                                    <div class="MainVerticalShowVideo">
                                        <div class="spinner"></div>
                                </div>
                                </div>
                                <div class="tab-pane fade" id="organization">
                                    <div class="MainVerticalShowVideo">
                                        <div class="spinner"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    
                    
                    
                    <div class="MainVerticalShow">
                        <div class="MainVerticalShowTitle">
                            <span>公开课</span>
<%--                            <div class="IndexMore">
                                <a href="Level2Class.aspx">更多&gt;&gt;</a>
                            </div>--%>
                        </div>
                        <div id="TabsIndex2">
                            <div class="tab_Index2_box">
                                
                                        <div class="tab_Index2_box_item">
                                            
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_1&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_1_1.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_1&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                清华大学</a>
                                        </div>
                                    
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_2&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_1_2.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_2&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                北京大学</a>
                                        </div>
                                    
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_3&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_1_3.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_3&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                中国人民大学</a>
                                        </div>
                                    
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_4&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_1_4.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_4&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                北京师范大学</a>
                                        </div>
                                    
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_5&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_1_5.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_5&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                南开大学</a>
                                        </div>
                                    
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_6&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_1_6.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_1_6&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                复旦大学</a>
                                        </div>
                                    
                                        </div>
                                    
                                
                                        <div class="tab_Index2_box_item hide">
                                            
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_1&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_2_1.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_1&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                耶鲁大学</a>
                                        </div>
                                    
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_2&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_2_2.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_2&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                哈佛大学</a>
                                        </div>
                                    
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_3&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_2_3.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_3&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                斯坦福大学</a>
                                        </div>
                                    
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_4&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_2_4.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_4&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                牛津大学</a>
                                        </div>
                                    
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_5&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_2_5.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_5&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                麻省理工</a>
                                        </div>
                                    
                                        <div class="IndexClassDadgeItem">
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_6&amp;IsChild=1" target="_blank">
                                                <img class="IndexClassDadge" src="images/gxb2_openclass_2_6.png"></a>
                                            <a href="Level3classLite.aspx?alias=gxb2_openclass_2_6&amp;IsChild=1" target="_blank" class="IndexClassDadgeLink">
                                                普林斯顿大学</a>
                                        </div>
                                    
                                        </div>
                                    
                            </div>
                            <ul class="tab_Index2_menu">
                                <li class="current"><a href=""></a></li>
                                <li><a href=""></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
             </div>
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="UpdateBtn" EventName="Click" />
        </Triggers>
        <ContentTemplate>--%>
           
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <div class="MainSkill">
        <div class="MainContentNavTitle">
            <span>技能</span>
            <div class="IndexMore">
                <a href="Level3Lite.aspx?alias=gxchannel7_topics_1&IsChild=0">更多&gt;&gt;</a>
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
                                    <a href="Level3Lite.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank"><%# Eval("CategoryName") %></a></span><<span class="MainSkillContentTextTxt">
                                        <a href="Level3Lite.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank"><%# colleges.DataProcessing.SubstringText(Eval("Note").ToString(),30) %></a></span>
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
                        <a href="Level3Lite.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
                            <img class="PicNoBorder" src="images/Skill<%# HomeChannelListOutpic(ref HomeChannelListCPicCount) %>.png" /></a>
                        <div class="text">
                            <a href="Level3Lite.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
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
                        <li><a href="Level3Lite.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
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
                <a href="Level3Lite.aspx?alias=gxchannel8_topics_7&IsChild=0">更多&gt;&gt;</a>
            </div>
        </div>
        <div class="MainSKillContent">
            <asp:ListView ID="TrainList" runat="server" OnItemDataBound="TrainList_ItemDataBound">
                <ItemTemplate>
                    <div class="MainSkillContentItem">
                        <asp:Label ID="TrainCategoryAlias" CssClass="HideInfoLable" runat="server" Text='<%# Eval("CategoryAlias") %>'></asp:Label>
                        <a href="Level3Lite.aspx?alias=<%# Eval("CategoryAlias") %>&IsChild=0" target="_blank">
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
    <%--<asp:Button ID="UpdateBtn" runat="server" Text="Button" 
        onclick="UpdateBtn_Click"/>--%>
    <asp:Label ID="CacheTest" runat="server" Text="Label" Visible="false"></asp:Label>
</asp:Content>