<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLite.Master" AutoEventWireup="true" CodeBehind="SpecialAttentionLite.aspx.cs" Inherits="colleges.SpecialAttentionLite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
 $(function() {
    $("div:empty").remove();
    })
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="SpecialAttentionLevel2Content">
          <%--<div class="SpecialAttentionLevel2ContentBanner">
            <img width="990" height="215" src="ShowZTImage.aspx?Title=<%=sFLTitle %>" alt="" border="0" />
          </div>--%>
            <div class="SpecialAttentionLevel2ContentLeft">
              <img width="359" height="239" src="ShowZTImage.aspx?Title=<%=sZTTitle %>" alt="" border="0" />
            </div>
            <div class="SpecialAttentionLevel2ContentRight" id="divLevel2Top">
            </div>
          <div class="SpecialAttentionLv2MainShow">
            <div class="SpecialAttentionLv2MainShowTitle">
            名家
            </div>
            <div class="SpecialAttentionFameBox" id="divLevel2Author"><img src="images/load.gif" alt="" /></div>
          </div>
          <div class="SpecialAttentionLv2MainShow">
            <div class="SpecialAttentionLv2MainShowTitle">
            最新动态
            </div>
            <div id="divLevel2New"><img src="images/load.gif" alt="" /></div>
         </div>
         <div class="SpecialAttentionLv2MainShow">
            <div class="SpecialAttentionLv2MainShowTitle">
            核心观点
            </div>
            <div id="divLevel2Key"><img src="images/load.gif" alt="" /></div>
        </div>
            <div class="SpecialAttentionLv2MainShow">
                <div class="SpecialAttentionLv2MainShowTitle">
                    相关内容
                </div>
                <div id="grid1" class="mini-datagrid" style="width:100%; height: 300px; margin:0px;"
                    allowcellselect="false" allowcelledit="false" url="Ajax.aspx" allowunselect="true"
                    allowmovecolumn="false" allowalternating="true" showemptytext="true" emptytext="没有记录"
                    virtualscroll="true" showfilterrow="false" idfield="ArticleGUID" resultasdata="true"
                    showpager="false" pagesize="50" onrowclick="gridRowClick(e)">
                    <div property="columns">
                        <div type="indexcolumn" width="34">
                        </div>
                        <div field="ArticleGUID" visible="false" headeralign="center">
                            编号</div>
                        <div field="Title" width="300" headeralign="center">
                            课程名称</div>
                        <div field="Author" visible="true" width="80" headeralign="center" align="center">
                            主讲人</div>
                        <div field="SpeakerInfo" width="150" visible="true" headeralign="center">
                            职务</div>
                        <div field="Duration" width="50" headeralign="center" align="center">
                            时长(分钟)</div>
                        <div field="CreateTime" visible="true" width="80" headeralign="center" align="center"
                            dateformat="yyyy-MM-dd">
                            日期</div>
                    </div>
                </div>
            </div>
</div>
<script type="text/javascript">
    GetData("Level2Top","<%=sZTTitle %>");
    GetData("Level2Author", "名家");
    GetData("Level2New", "最新动态");
    GetData("Level2Key", "核心观点");

    function GetData(act, Title) {
        var ajaxData = "act=" + act + "&ID=<%=sCategoryGUID %>&Title=" + Title;
           $.ajax({
               url: "Ajax.aspx",
               type: "post",
               data: ajaxData,
               success: function (text) {
                   var divID = "div" + act;
                   document.getElementById(divID).innerHTML = text;
               }
           });
       }

       mini.parse();
       var tip = new mini.ToolTip();
       tip.set(
           {
               target: document,
               selector: '[data-tooltip]',
               onbeforeopen: function (e) {
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
       var grid1 = mini.get("grid1");
       grid1.load({ act: "Level2Other", ID:"<%=sCategoryGUID %>", Title: "其他" });

       function gridRowClick(e) {
           window.open("ShowVideo.aspx?ID=" + e.row.ArticleGUID);
       }
</script>

</asp:Content>
