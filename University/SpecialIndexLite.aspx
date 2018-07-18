<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLite.Master" AutoEventWireup="true" CodeBehind="SpecialIndexLite.aspx.cs" Inherits="colleges.SpecialIndexLite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="SpecialAttentionBody">
        <div class="SpecialAttentionLeft">
            <div class="SpecialAttentionLeftItem">
                <div class="SpecialAttentionLeftTitle">最新更新</div>
                <div id="divLastNew"><img src="images/load.gif" alt="" /></div>
            </div>
            <div class="SpecialAttentionLeftItem">
                <div class="SpecialAttentionLeftTitle">精彩推荐</div>
                <div id="divRecommend"><img src="images/load.gif" alt="" /></div>
            </div>
        </div>
        <div class="SpecialAttentionRight">
          <div class="SpecialAttentionSearch">
            <span class="SpecialAttentionSearchTitle">专题搜索</span>
            <span><input type="text" id="ztKeyWords" class="SpecialAttentionSearchInput" /></span>
            <span><input type="button" class="SpecialAttentionSearchBtn" style=" cursor:pointer" /></span>
          </div>
          <div class="SpecialAttentionRightItem">
            <div class="SpecialAttentionRightHotTitle">热点专题</div>
            <div id="divHot"><img src="images/load.gif" alt="" /></div>
          </div>
            <div class="SpecialAttentionIndexHistory">
                <div class="SpecialAttentionHistoryTitle">
                    <div class="Level2MainTitleTxt">
                        以往回顾</div>
                        <div class="SpecialMore">
                        <%--<asp:HyperLink ID="HistoryMore" runat="server" >更多>></asp:HyperLink>--%>
                        </div>
                </div>
                <asp:ListView ID="HistoryList" runat="server">
                    <ItemTemplate>
                        <li><span class="Level2TopNumBg1">
                            <%#Eval("Seq")%></span></span> <span><a href="SpecialAttentionLIte.aspx?ID=<%#Eval("CategoryGuid")%>&Title=<%#HttpUtility.UrlEncode(Eval("CategoryName").ToString())%>"
                                target="_blank">
                                <%# colleges.DataProcessing.SubstringText(Eval("CategoryName").ToString(), 20)%></a></span>
                        </li>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <ul class="Level2FameTop">
                            <asp:Panel ID="itemPlaceholder" runat="server">
                            </asp:Panel>
                        </ul>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
<script type="text/javascript">
       GetData("LastNew");
       GetData("Recommend");
       GetData("Hot");
       function GetData(act)
       {
           var ajaxData="act=" + act + "&ID=<%=sCategoryGUID %>";
           $.ajax({
               url: "AjaxLite.aspx",
               type: "post",
               data: ajaxData,
               success: function(text) {
                var divID="div" + act;
                document.getElementById(divID).innerHTML=text;
               }
           });
        }
        mini.parse();
        var tip = new mini.ToolTip();
        tip.set(
        {
            target: document,
            selector: '[data-tooltip]',
            //width:400
            onbeforeopen:function(e){
                var el = e.element;
                if($(el).attr("data-tooltip").length<=0){
                    e.cancel = true;
                }
                var iW=$(el).width();
                iW=iW<=400?400:iW;
                tip.setWidth(iW);
            }
        }
        );
$(function(){
    $("input[type=button].SpecialAttentionSearchBtn")
        .css("cursor","porinter")
        .click(function(){
            var sKey=$("input[type=text].SpecialAttentionSearchInput").val();
            if(sKey.length<=0){mini.alert("必须输入关键词!");return;}
            location.href="SearchLite.aspx?act=zt&keywords="+sKey;
    });
});  
</script>
</asp:Content>
