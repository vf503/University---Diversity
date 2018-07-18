<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="colleges.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>中经视频--高校版</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Styles/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
    <link href="Styles/DefaultLayout.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.timer.js" type="text/javascript"></script>
    <script src="Scripts/slide.js" type="text/javascript"></script>
    <script src="Scripts/content.js" type="text/javascript"></script>
    <script type="text/javascript">
        function OpenWin(url) {
            window.open("" + url);
        }
        $(document).ready(function () {
            $('#timer').timer({ format: "yy年mm月dd日 W" });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="FullContent">
    <div class="TopContent">
            <div id="TopLogin" class="TopLogin">
                <div class="TopLoginLeft">
                <div class="InlineDiv" id="timer"></div>
                <div class="InlineDiv">用户名：<input name="UserId" type="text" value="" class="LoginInput" /></div>
                <div class="InlineDiv">密码：<input name="UserId" type="text" value="" class="LoginInput" /></div>
                <div class="InlineDiv" id="Div1"><a href="#">登陆</a></div>
                <div class="InlineDiv" id="Div2"><a href="#">试用申请</a></div>
                </div>
                <div class="TopLoginRight">
                <span><a href="#">通用版</a></span>
                <span>|</span>
                <span><a href="#">党政版</a></span>
                <span>|</span>
                <span><a href="#">企管课程库</a></span>
                <span>|</span>
                <span><a href="#">网络学院</a></span>
                <span>|</span>
                <span><a href="#">最新会议</a></span>
                </div>
            </div>
            <div class="TopImage">
                <img src="images/top-zjspgxb.png" />
            </div>
            <div class="TopSearch">
                <div class="TopSearchInputLine">
                    <div class="TopSearchInputBox">
                        <input type="text" name="keywords" id="TopSearchInput" value="请输入搜索关键词" />                       
                    </div>
                    <div class="TopSearchInputBtnBox">
                        <input type="button" name="keywords" style=" cursor:pointer" id="TopSearchInputBtn"/>
                    </div>
                    <div class="TopSearchInputBtnAdvBox">
                        <input type="button" name="buttonAdvSearch" style=" cursor:pointer" id="TopSearchInputBtnAdv"/>
                    </div>
                </div>
                <div class="TopSearchKeyWord" id="divHotKey" runat="server"></div>
            </div>
        </div>
        <div class="IndexNav">
            <div class="IndexNavHome">
                <a href="index.html">首页</a>
            </div>
            <img src="images/NavBgSpace.png"/>
            <div id="" class="NavContent">
               <div class="NavItem">
                  <span class="NavLevel1"><a href="level2.html">经济</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">形势</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">国际</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">政策</a></span>
               </div>
               <div class="NavItem">
                  <span class="NavLevel1"><a href="level2.html">经济</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">形势</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">国际</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">政策</a></span>
               </div>
               <div class="NavItem">
                  <span class="NavLevel1"><a href="level2.html">经济</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">形势</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">国际</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">政策</a></span>
               </div>
               <div class="NavItem">
                  <span class="NavLevel1"><a href="level2.html">经济</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">形势</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">国际</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">政策</a></span>
               </div>
               <div class="NavItem">
                  <span class="NavLevel1"><a href="level2.html">经济</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">形势</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">国际</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">政策</a></span>
               </div>
               <div class="NavItem">
                  <span class="NavLevel1"><a href="level2.html">经济</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">形势</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">国际</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">政策</a></span>
               </div>
               <div class="NavItem">
                  <span class="NavLevel1"><a href="level2.html">经济</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">形势</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">国际</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">政策</a></span>
               </div>
               <div class="NavItem">
                  <span class="NavLevel1"><a href="level2.html">经济</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">形势</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">国际</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">政策</a></span>
               </div>
               <div class="NavItem">
                  <span class="NavLevel1"><a href="level2.html">经济</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">形势</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">国际</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">政策</a></span>
               </div>
               <div class="NavItem">
                  <span class="NavLevel1"><a href="level2.html">经济</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">形势</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">国际</a></span>
                  <span class="NavLevel2"><a href="Level2Special.html">政策</a></span>
               </div>
            </div>
        </div>
        <!--<div id="line" class="line">
        </div>
        -->
        <div id="" class="UpsideContent">
            <div id="FocusTabs">
                <ul id="TabsList">
                    <li><a href="#tabs-1" onclick="OpenWin('PicFocusTxt.html')">经济</a></li>
                    <li><a href="#tabs-2" onclick="OpenWin('PicFocusTxt.html')">管理</a></li>
                    <li><a href="#tabs-3" onclick="OpenWin('PicFocusTxt.html')">社会</a></li>
                    <li><a href="#tabs-4" onclick="OpenWin('PicFocusTxt.html')">时事</a></li>
                    <li><a href="#tabs-5" onclick="OpenWin('PicFocusTxt.html')">政策</a></li>
                    <li><a href="#tabs-6" onclick="OpenWin('PicFocusTxt.html')">名家</a></li>
                </ul>
                <div id="tabs-1">
                   <a href="PicFocusPic.html" target="_blank"><img src="images/TempFocus5.png" /></a>
                </div>
                <div id="tabs-2">
                   <a href="PicFocusPic.html" target="_blank"><img src="images/TempFocus1.png" /></a>
                </div>
                <div id="tabs-3">
                   <a href="PicFocusPic.html" target="_blank"><img src="images/TempFocus2.png" /></a>
                </div>
                <div id="tabs-4">
                    <a href="PicFocusPic.html" target="_blank"><img src="images/TempFocus3.png" /></a>
                </div>
                <div id="tabs-5">
                    <a href="PicFocusPic.html" target="_blank"><img src="images/TempFocus5.png" /></a>
                </div>
                <div id="tabs-6">
                    <a href="PicFocusPic.html" target="_blank"><img src="images/TempFocus4.png" /></a>
                </div>
            </div>
            <div id="" class="SpecialFocus">
                <div id="SpecialAttentionTitle">
                   <span>特别关注</span>
                   <div class="IndexMore"><a href="SpecialIndex.aspx" target="_blank">更多>></a>
                   </div>
                </div>
                <div id="" class="SpecialFocusContent">
                     <ul>
                        <li class="SpecialFocusTop"><a href="SpecialAttentionLevel2.html">加快推进绿色低碳无处可逃建设着…</a></li>
                        <li class="SpecialFocusLi"><a href="#">建设人水和谐的城市02</a></li>
                        <li class="SpecialFocusLi"><a href="#">建设水文化特色的魅力城市</a></li>
                    </ul>
                    <ul>
                        <li class="SpecialFocusTop"><a href="SpecialAttentionLevel2.html">加快推进绿色低碳无处可逃建设着…</a></li>
                        <li class="SpecialFocusLi"><a href="#">建设人水和谐的城市02</a></li>
                        <li class="SpecialFocusLi"><a href="#">建设水文化特色的魅力城市</a></li>
                    </ul>
                    <ul>
                        <li class="SpecialFocusTop"><a href="SpecialAttentionLevel2.html">加快推进绿色低碳无处可逃建设着…</a></li>
                        <li class="SpecialFocusLi"><a href="#">建设人水和谐的城市02</a></li>
                        <li class="SpecialFocusLi"><a href="#">建设水文化特色的魅力城市</a></li>
                    </ul>
                </div>
            </div>
            <div class="DailyVideo">
                <div class="DailyVideoTitle">
                  <span>凤凰时政</span>
                  <div class="IndexMore"><a href="SpecialAttentionIndex.html" target="_blank">更多>></a>
                  </div>
                </div>
                <div class="DailyVideoPlayer">
                        <img src="images/TempItem31.png" />
                </div>
                <div class="DailyVideoInfo">
                    <a href="#">凤凰早班车</a>
                </div>
            </div>
            <div class="DailyVideoPadding"> </div>
            <div class="DailyVideo">
                <div class="DailyVideoTitle">
                  <span>凤凰时政</span>
                  <div class="IndexMore"><a href="SpecialAttentionIndex.html" target="_blank">更多>></a>
                  </div>
                </div>
                <div class="DailyVideoPlayer">
                        <img src="images/TempItem32.png" />
                </div>
                <div class="DailyVideoInfo">
                    <a href="#">凤凰早班车</a>
                </div>
            </div>
        </div>
        <div id="Footer">
          <div class="FooterNav">
                <a href="#">关于我们</a>
                <span>|</span> 
                <a href="#">产品介绍</a>
                <span>|</span> 
                <a href="#">内容资源</a>
                <span>|</span> 
                <a href="#">核心优势</a>
                <span>|</span> 
                <a href="#">业务类型</a>
                <span>|</span> 
                <a href="#">服务方式</a>
                <span>|</span> 
                <a href="#">成功案例</a>
                <span>|</span> 
                <a href="#">联系方式</a>
                <span>|</span> 
                <a href="#">版权声明</a>
                <span>|</span> 
                <a href="#">使用方式</a>
          </div>
          <div class="FooterCopyright">版权所有 中国经济信息网 中经视频-高校版</div>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
function doSubmit(){
    var sTxt=$("#TopSearchInput").val();
        if(sTxt=="" || sTxt.length<=0 ||sTxt=="请输入搜索关键词"){mini.alert("请先输入关键词!"); return;}
        location.href="Search.aspx?act=n&keywords="+sTxt;
}
function doSearch(){
    var sTxt=$("#ztKeyWords").val();
    if(sTxt=="" || sTxt.length<=0 ||sTxt=="请输入搜索关键词"){mini.alert("请先输入关键词!"); return;}
    location.href="Search.aspx?act=zt&keywords="+sTxt;
}
$(function(){
    $("#TopSearchInput").focus(function(){
        var sTxt=$(this).val();
        if(sTxt=="请输入搜索关键词")$(this).val("");
    });
    $("#TopSearchInput").blur(function(){
        var sTxt=$(this).val();
        if(sTxt=="" || sTxt.length<=0){$(this).val("请输入搜索关键词");}
    });
    $("#TopSearchInputBtn").click(function(){
        doSubmit();
    });
    $("#TopSearchInputBtnAdv").click(function(){
        mini.alert("后续开发，谢谢关注!");
    });
    document.onkeydown = function(e){
        e = e ? e : window.event; 
        var iKeyCode = e.which ? e.which : e.keyCode;
        if(iKeyCode==13) {
            var id=$("input:focus").attr("id");
            if(id=="TopSearchInput"){doSubmit();}
            if(id=="ztKeyWords"){doSearch();}
        }
	}
});

</script>