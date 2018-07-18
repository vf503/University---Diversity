<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPager/PicNewsLite.Master" AutoEventWireup="true" CodeBehind="PicFocusPicLite.aspx.cs" Inherits="colleges.PicFocusPicLite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script src="Scripts/slide.js" type="text/javascript"></script>
  <script src="Scripts/content.js" type="text/javascript"></script>
  <script src="<%= SpeakerInfoUrl %>" type="text/javascript"></script>
  <script type="text/javascript">
      $(function() {
          $("#TopSpeakerSummary").html(speakerResume);
          $("#TopSpeakerSummary").attr("title", speakerResume);
          //
          $(document.body).limit();
      });
      jQuery.fn.limit = function() {
          //这里的div去掉的话，就可以运用li和其他元素上了，否则就只能div使用。
          var self = $("div[limit]");
          self.each(function() {
              var objString = $(this).text();
              var objLength = $(this).text().length;
              var num = $(this).attr("limit");
              if (objLength > num) {
                  //这里可以设置鼠标移动上去后显示标题全文的title属性，可去掉。
                  $(this).attr("title", objString);
                  objString = $(this).text(objString.substring(0, num) + "…");
              }
          })
      }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
    <div class="SpecialAttentionLevel2Content">
          <div class="SpecialAttentionLevel2ContentTop">
            <div class="SpecialAttentionLevel2ContentLeft">
             <asp:HyperLink ID="TopPicLargeLink" runat="server" Target="_blank">
                <asp:Image ID="TopPicLarge" CssClass=""PicFocusTopPicLarge" runat="server" /></asp:HyperLink></div>
            <div class="SpecialAttentionLevel2ContentRight">
                <div>
                    <div class="SpecialAttentionLevel2ContentRightInfo">
                        <div class="SpecialAttentionLevel2ContentRightInfoLine">
                            <span class="SpecialAttentionLevel2ContentRightInfoTitle">课程标题</span><asp:Label ID="TopTitle" runat="server" Text="" CssClass="SpecialAttentionLevel2ContentRightInfoPicCourseTitle"></asp:Label>
                        </div>                                
                        <div class="SpecialAttentionLevel2ContentRightInfoLine">
                            <span class="SpecialAttentionLevel2ContentRightInfoTitle">课程简介</span><span class="SpecialAttentionLevel2ContentRightInfoPicCourseInfo">
                            </span>
                        </div>
                    </div>
                </div>
              <div class="SpecialAttentionLevel2ContentRightText">
              <asp:Label ID="TopSummary" runat="server" Text=""></asp:Label> 
              </div>
              <asp:HyperLink ID="TopPlayLink" runat="server" Target="_blank"><img src="images/FocusPlay.png" class="PicFocusPicTopPicPlay"/></asp:HyperLink>
            </div>
          </div>
          <div class="SpecialAttentionLevel2ContentL">
                <div>
                    <div class="SpecialAttentionLevel2ContentRightInfo">
                        <div class="SpecialAttentionLevel2ContentRightInfoLine">
                            <span class="SpecialAttentionLevel2ContentRightInfoTitle">主讲人</span><asp:Label ID="TopSpeaker" runat="server" Text="" CssClass="SpecialAttentionLevel2ContentRightInfoCourseInfoL"></asp:Label>
                        </div>
                        <div class="SpecialAttentionLevel2ContentRightInfoLine">
                            <span class="SpecialAttentionLevel2ContentRightInfoTitle">主要职务</span><asp:Label ID="TopSpeakerInfo" runat="server" Text="" CssClass="SpecialAttentionLevel2ContentRightInfoCourseInfoL"></asp:Label>
                        </div>
                        <div class="SpecialAttentionLevel2ContentRightSpeakerInfoLine">
                            <span class="SpecialAttentionLevel2ContentRightInfoTitle">主要简历</span><div id="TopSpeakerSummary" class="SpecialAttentionLevel2ContentRightInfoSpeakerInfo" limit="110"></div>
                        </div>
                    </div>
                    <div class="SpecialAttentionLevel2ContentRightPic">
                        <asp:Image ID="TopPicSmall" runat="server" />
                    </div>
                </div>
            </div>
            <div class="SpecialAttentionLevel2ContentL">
                <div class="SpecialAttentionLevel2ContentRightInfoLineL">
                    <span class="SpecialAttentionLevel2ContentRightInfoTitleL">该报告人的其他讲座有</span>         
                </div>
                <asp:ListView ID="CoursesWithSperker" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="SpecialAttentionLevel2TableTitle">
                                <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                    <%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),20) %></a>
                            </td>
                            <td>
                                <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),15) %>
                            </td>
                            <td>
                                <%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),15)%></span>
                                <td>
                                    <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %>
                                </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table>
                            <asp:Panel ID="itemPlaceholder" runat="server">
                            </asp:Panel>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <div class="SpecialAttentionLevel2ContentRightInfoLineL">
                    <span class="SpecialAttentionLevel2ContentRightInfoTitleL">与该报告相关的讲座还有</span>         
                </div>
                <asp:ListView ID="CoursesWithTitle" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="SpecialAttentionLevel2TableTitle">
                                <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                    <%# colleges.DataProcessing.SubstringText(Eval("Title").ToString(),20) %></a>
                            </td>
                            <td>
                                <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),15) %>
                            </td>
                            <td>
                                <%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),15)%></span>
                                <td>
                                    <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %>
                                </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table>
                            <asp:Panel ID="itemPlaceholder" runat="server">
                            </asp:Panel>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>
    <asp:Label ID="HideUrl" runat="server" Text="Label" CssClass="HideInfoLable"></asp:Label> 
</asp:Content>
