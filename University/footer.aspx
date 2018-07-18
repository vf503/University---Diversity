<%@ Page Language="C#" MasterPageFile="~/MasterPager/Normal.Master" AutoEventWireup="true"
    CodeBehind="footer.aspx.cs" Inherits="colleges.footer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="renderer" content="ie-stand" />
    <script type="text/javascript">
        $(function() {
        $(".FooterMain").attr("src", "FooterPage/" + $.getUrlParam("footerid") + ".htm");
        $(".FooterMain").load(function() {
            var mainheight = $(this).contents().find("body").height() + 20;
            $(this).height(mainheight);
        }); 
        });
        (function($) {
            $.getUrlParam = function(name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
<iframe class="FooterMain" scrolling="no" frameborder="0">
</iframe>
</asp:Content>
