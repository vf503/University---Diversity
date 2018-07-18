<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPager/Level2Lite.Master" AutoEventWireup="true" CodeBehind="Level2LiteFame.aspx.cs" Inherits="colleges.Level2LiteFame" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
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
     <iframe src="Level3FameEmbed.aspx?alias=gxchannel10_3&IsChild=1" scrolling="no" style="border:none; width:985px;height:2530px"></iframe>
    </div>    
</asp:Content>
