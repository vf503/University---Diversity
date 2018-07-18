<%@ Page Language="C#" MasterPageFile="~/MasterPager/AdvSearchLite.Master" AutoEventWireup="true" CodeBehind="AdvanceSearchLite.aspx.cs" Inherits="colleges.AdvanceSearchLite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
        <div id="AdvancedSearch">
            <form method="get" name="AdvancedForm" id="AdvancedForm" action="" onsubmit="return checkForm();">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td style="width: 880px;">
                        <div class="AdvancedSearchRow" style="width: auto; padding-left: 80px;">
                            <input id="SelectType" name="SelectType" class="mini-radiobuttonlist" repeatitems="1"
                                repeatlayout="table" repeatdirection="vertical" textfield="Text" valuefield="ID" />
                        </div>
                        <div id="Item1" class="AdvancedSearchRow" style="display: none; height: 25px;">
                            <span id="SearchNormalTxt">学科检索</span>
                            <input id="Alias" name="Alias" class="mini-combobox" style="width: 240px;" textfield="Name"
                                valuefield="Alias" onvaluechanged="AliasChanged" valuefromselect="true" />
                            <input id="Alias1" name="Alias1" class="mini-combobox" style="width: 240px;" textfield="Name"
                                valuefield="Alias" onvaluechanged="Alias1Changed" valuefromselect="true" />
                            <input id="Alias2" name="Alias2" class="mini-combobox" style="width: 240px;" textfield="Name"
                                valuefield="Alias" valuefromselect="true" />
                        </div>
                        <div id="Item2" class="AdvancedSearchRow" style="display: none; height: 25px;">
                            <span id="SearchNormalTxt">机构检索</span>
                            <input id="InstitutionClass" name="Area" class="mini-textboxlist" searchfield="Area"
                                required="true" style="width: 736px; height: 24px;" valuefield="Area" textfield="Area"
                                url="Ajax.aspx?act=GetArea" />
                        </div>
                        <div class="AdvancedSearchRow">
                            <span id="SearchKeyTxt">关键词</span>
                            <input id="AdvancedTextInput" name="keywords" class="mini-textbox" style="width: 736px;
                                height: 24px;" emptytext="请输入关键词" />
                        </div>
                        <div class="AdvancedSearchRow">
                            <span id="SearchAuthorTxt">作者</span>
                            <input id="tblAuthor" name="Author" class="mini-textboxlist" searchfield="Author"
                                required="true" style="width: 418px; height: 24px;" valuefield="Author" textfield="Author"
                                url="Ajax.aspx?act=GetAuthor" />
                            <span style="padding-left: 36px">起止日期</span>
                            <input id="StartDate" name="StartDate" class="mini-datepicker" style="width: 100px;"
                                value="" />-<input id="EndDate" name="EndDate" class="mini-datepicker" style="width: 100px;"
                                    value="" /><%--<input type="text" class="AdvancedDateInput" name="" id="Text3" value="" />--%>
                        </div>
                    </td>
                    <td valign="bottom">
                        <input class="btnAdvSubmit" type="submit" value="检　索" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
        <div id="SearchResultBox">
        <%if (Request.QueryString["act"] != "zt")
          { %>
          <div id="SearchResultTitle">
            <form action="" id="SearchResultFunction">  
              <label id="SearchResultOrderTxt">排序方式:</label>
              <input type="radio" name="SearchResultOrder" id="" class="SearchResultOrderRadio" value="asc"/><label for="" class="SearchResultOrderLabel"><img src="images/OrderUp.png" />日期正序</label>
              <input type="radio" name="SearchResultOrder" id="" class="SearchResultOrderRadio" value="desc" checked=checked/><label for="" class="SearchResultOrderLabel"><img src="images/OrderDown.png" />日期倒序</label>
              <input type="button" name="" id="SearchResultThumb" />
              <input type="button" name="" id="SearchResultList" />
            </form>
          </div>
          <%} %>
          <div id="SearchResultContent">
             <ul><%=sOutLiStr%></ul>
             <table class="SearchResultContentTable">
             <thead>
               <tr>
                 <th>课程名称</th>
                 <th>主讲人</th>
                 <th>时长</th>
                 <th>日期</th>
               </tr>
             </thead>
             <tbody>
               <%=sOutTrStr%>
             </tbody>
            </table>
            <div class="SearchPager"><%=sSplitContent %></div>
          </div>
        </div>
<script type="text/javascript">
var ShowType="";
$(function(){
    $("input[name=SearchResultOrder][value=<%=sSort %>]").attr("checked",true);
    ChangeShow("<%=sShow %>");    
    
    $("#SearchResultThumb").click(function(){ChangeShow("Thumb");$(this).hide();$("#SearchResultList").show();});
    
    $("#SearchResultList").click(function(){ChangeShow("List");$(this).hide();$("#SearchResultThumb").show();});
    
    $("input[name=SearchResultOrder]").click(function(){
        var sValue=$(this).attr("value");
        $("input[name=SearchResultOrder][value="+sValue+"]").attr("checked",true);
        var sHref=$("div.SearchPager span a").eq(0).attr("href");
        if(sHref.indexOf("&show=")>0){
            sHref=sHref.substring(0,sHref.indexOf("&show="));
        }
        sHref+="&show="+ShowType;
        if(typeof(sValue)!="undefined"){
            sHref+="&s="+sValue;  
        }
        location.href=sHref;
    });
});
function PageUrlFilter(){
    var sSort=$("input[name=SearchResultOrder]:checked").val();
    $("div.SearchPager span a").each(function(){
        var sHref=$(this).attr("href");
        if(sHref.indexOf("&show=")>0){
            sHref=sHref.substring(0,sHref.indexOf("&show="));
        }
        sHref+="&show="+ShowType;
        if(typeof(sSort)!="undefined"){
            sHref+="&s="+sSort;  
        }
        $(this).attr("href",sHref);
    });
    $("div.SearchPager option").each(function(){
        var sValue=$(this).attr("value");
        if(sValue.indexOf("&show=")>0){
            sValue=sValue.substring(0,sValue.indexOf("&show="));
        }
        sValue+="&show="+ShowType;
        if(typeof(sSort)!="undefined"){
            sValue+="&s="+sSort;  
        }
        $(this).attr("value",sValue);
    });
    
}
function ChangeShow(act){
    ShowType=act;
    if(act=="Thumb" ||act=="thumb"){
        $("#SearchResultThumb").hide();
        $("#SearchResultList").show();
        $("#SearchResultContent ul").show();
        $("#SearchResultContent table.SearchResultContentTable").hide();
    }
    if(act=="List" || act=="list"){
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

var Alias = mini.get("Alias");
var Alias1 = mini.get("Alias1");
var Alias2 = mini.get("Alias2");
var Area=mini.get("InstitutionClass");
var keyWords=mini.get("AdvancedTextInput");
var Author=mini.get("tblAuthor");
var StartDate=mini.get("StartDate");
var EndDate=mini.get("EndDate");


var SelectType=mini.get("SelectType");
var SelectTypeRadioData=[{ID:"1",Text:"学科分类"},{ID:"2",Text:"专业分类"},{ID:"3",Text:"机构分类"}];
var SelectIndex=<%=sSelectType %>;
if(SelectIndex<=0)SelectIndex=1;
var AliasValue="<%=sAlias %>";
var Alias1Value="<%=sAlias1 %>";
var Alias2Value="<%=sAlias2 %>";
var AreaValue="<%=sArea %>";
var keyWordsValue="<%=sKeyWords %>";
var AuthorValue="<%=sAuthor %>";
var StartDateValue="<%=sStartDate %>";
var EndDateValue="<%=sEndDate %>";

var IsFirst=true;
$(function(){
    SelectType.setData(SelectTypeRadioData); 
    SelectType.select(SelectIndex<=1?0:SelectIndex-1);
    RadioChange(SelectIndex);
    if(IsFirst){
        Alias.setUrl("Ajax.aspx?act=Subject");
        Alias.select(0);
    }
    SelectType.on("valuechanged", function (e) {        RadioChange(parseInt(e.value));    });    if(keyWordsValue!=""){keyWords.setValue(keyWordsValue);}    if(AuthorValue!=""){Author.setValue(AuthorValue);}    if(StartDateValue!=""){StartDate.setValue(StartDateValue);}    if(EndDateValue!=""){EndDate.setValue(EndDateValue);}});
function RadioChange(v){
        Alias1.disable();
        Alias2.disable();
        var i=v;        var sUrl="Ajax.aspx?act=";        if(i==1){            $("#Item1 span").eq(0).html("学科检索");            $("#Item2").hide();$("#Item1").show();            sUrl+="Subject";        }        if(i==2){            $("#Item1 span").eq(0).html("专业检索");            $("#Item2").hide();$("#Item1").show();            sUrl+="Major";         }        if(i==3){             $("#Item2").show();            $("#Item1").hide();            Area.setValue(AreaValue);        }                 Alias.setValue("");        Alias1.setValue("");        Alias1.disable();        Alias2.setValue("");        Alias2.disable();                if(i!=3){            Alias.load(sUrl);            Alias.select(0);        }        if(SelectIndex==SelectType.getValue()){            if(AliasValue!=""){Alias.setValue(AliasValue);}            if(Alias1Value!=""){                AliasChanged();                Alias1.setValue(Alias1Value);            }              if(Alias2Value!=""){                Alias1Changed();                Alias2.setValue(Alias2Value);            }         }else{            AliasValue=Alias1Value=Alias2Value="";            AreaValue="";        }          if(IsFirst){IsFirst=false;}
}
function AliasChanged(){
    var sValue=Alias.getValue();
    Alias1.setValue("");
    if(sValue==""){
        Alias2.setValue("");
        return;
    }
    var sUrl="Ajax.aspx?act=Subject&Alias="+sValue;
    Alias1.setUrl(sUrl);
    if(Alias1.getData().length>1){
        Alias1.enable();
        Alias1.select(0);
    }else{
        Alias1.disable();
    }
}
function Alias1Changed(){
    var sValue=Alias1.getValue();
    Alias2.setValue("");
    if(sValue==""){
        Alias2.disable();
        return;
    }
    var sUrl="Ajax.aspx?act=Subject&Alias="+sValue;
    Alias2.setUrl(sUrl);
    if(Alias2.getData().length>1){
        Alias2.enable();
        Alias2.select(0);
    }else{
        Alias2.disable();
    }
}
function checkForm(){
    var s=StartDate.getValue();
    var e=EndDate.getValue();
    if(s!="" && e!="" && s>=e){
        mini.alert("起始日期必须小于结束日期");
        return false;
    }
    return true;
}
</script>
</asp:Content>
