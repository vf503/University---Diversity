<%@ Page Language="C#" MasterPageFile="~/MasterPager/Level2.Master" AutoEventWireup="true" CodeBehind="Level2News.aspx.cs" Inherits="colleges.Level2News" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="IndexMainPlaceHolder" runat="server">
<div id="Level2Main">
          <div id="Level2NewsPart1">
              <div class="Level2NewsPart1Left">
                  <div class="Level2NewsList1">
                      <div class="Level2MainTitle">
                          <div class="Level2MainTitleTxt">
                              凤凰早班车</div>
                          <div class="Level2MainTitleMore">
                              <a href="level3.aspx?alias=gxchannel9_topics_1_1&IsChild=1" target="_blank">更多>></a>
                          </div>
                      </div>
                      <asp:ListView ID="Level2NewsList1Video" runat="server">
                          <LayoutTemplate>
                              <div class="Lv2NewsList1VideoContent">
                                  <asp:Panel ID="itemPlaceholder" runat="server">
                                  </asp:Panel>
                              </div>
                          </LayoutTemplate>
                          <ItemTemplate>
                              <div class="Lv2NewsList1Video">
                                  <div class="Lv2NewsList1VideoPicBox">
                                      <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" />
                                  </div>
                                  <div class="Lv2NewsList1VideoLink">
                                      <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                          <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a>
                                  </div>
                              </div>
                          </ItemTemplate>
                      </asp:ListView>
                      <asp:ListView ID="Level2NewsList1Other" runat="server" GroupItemCount="2">
                          <LayoutTemplate>
                              <div>
                                  <asp:PlaceHolder ID="groupPlaceholder" runat="server" />
                              </div>
                          </LayoutTemplate>
                          <GroupTemplate>
                              <div class="Lv2NewsList1Other">
                                  <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                              </div>
                          </GroupTemplate>
                          <ItemTemplate>
                              <div class="Lv2NewsList1OtherLink">
                                  <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                      <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a>
                              </div>
                          </ItemTemplate>
                      </asp:ListView>
                  </div>
                  <div class="Level2NewsList1">
                      <div class="Level2MainTitle">
                          <div class="Level2MainTitleTxt">
                              经济半小时</div>
                          <div class="Level2MainTitleMore">
                              <a href="level3.aspx?alias=gxchannel9_topics_6_1&IsChild=1" target="_blank">更多>></a>
                          </div>
                      </div>
                      <asp:ListView ID="Level2NewsList2Video" runat="server">
                          <LayoutTemplate>
                              <div class="Lv2NewsList1VideoContent">
                                  <asp:Panel ID="itemPlaceholder" runat="server">
                                  </asp:Panel>
                              </div>
                          </LayoutTemplate>
                          <ItemTemplate>
                              <div class="Lv2NewsList1Video">
                                  <div class="Lv2NewsList1VideoPicBox">
                                      <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" />
                                  </div>
                                  <div class="Lv2NewsList1VideoLink">
                                      <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                          <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a>
                                  </div>
                              </div>
                          </ItemTemplate>
                      </asp:ListView>
                      <asp:ListView ID="Level2NewsList2Other" runat="server" GroupItemCount="2">
                          <LayoutTemplate>
                              <div>
                                  <asp:PlaceHolder ID="groupPlaceholder" runat="server" />
                              </div>
                          </LayoutTemplate>
                          <GroupTemplate>
                              <div class="Lv2NewsList1Other">
                                  <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                              </div>
                          </GroupTemplate>
                          <ItemTemplate>
                              <div class="Lv2NewsList1OtherLink">
                                  <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                      <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a>
                              </div>
                          </ItemTemplate>
                      </asp:ListView>
                  </div>
              </div>
              <div class="Level2NewsPart1Right">
                  <div class="Lv2NewsList2">
                      <div class="Lv2NewsList2Left">
                          <div class="Level2MainTitleTxt">
                              名家谈时事</div>
                          <div class="Level2MainTitleMore">
                              <a href="Level3Fame.aspx?alias=gxchannel10_3&IsChild=1" target="_blank">更多>></a>
                          </div>
                      </div>
                      <asp:ListView ID="Lv2NewsListLeft" runat="server">
                          <LayoutTemplate>
                              <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                          </LayoutTemplate>
                          <ItemTemplate>
                              <div class="Lv2NewsList2LeftItem">
                                  <img class="Lv2NewsList2LeftPic" src="images/templv2newslist2.png" />
                                  <div class="Lv2NewsList2LeftInfo">
                                      <div class="Lv2NewsList2LeftInfoTitle">
                                          <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                              <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a></div>
                                      <div class="Lv2NewsList2LeftInfoOther">
                                          <%# colleges.DataProcessing.SubstringText(Eval("Author").ToString(),4) %></div>
                                      <div class="Lv2NewsList2LeftInfoOther">
                                          <%# colleges.DataProcessing.SubstringText(Eval("SpeakerInfo").ToString(),11)%></div>
                                  </div>
                              </div>
                          </ItemTemplate>
                      </asp:ListView>
                  </div>
              </div>
          </div>
            <div id="Level2NewsPart2">
                <div class="Level2NewsPart2Left">
                    <div class="Level2MainTitle">
                        <div class="Level2MainTitleTxt">
                            风云对话</div>
                        <div class="Level2MainTitleMore">
                            <a href="">更多>></a>
                        </div>
                    </div>
                    <div class="Level2NewsList3Top">
                        <img class="Level2NewsList3TopFace" src="images/Lv2NewsList3Face1.png" />
                        <div class="Level2NewsList3TopInfo">
                            <div class="Level2NewsList3TopInfoType">
                                主持人：<span>阮次山</span></div>
                            <div class="Level2NewsList3TopInfoType">
                                简介：</div>
                            <div class="Level2NewsList3TopInfoText">
                                围绕国际焦点事件、叱咤风云的重量级人物，专访全球政治最关键、最敏感的人物，当权者，当事人。赫赫有名的世界级人物，亲自向您剖析他们的观点。阮次山以“中国心”和“中国眼”看世界，运用巧妙手法，让世界上最有权力的人，逐渐褪去伪装。</div>
                        </div>
                    </div>
                    <asp:ListView ID="Level2NewsList3Bottom" runat="server">
                        <LayoutTemplate>
                            <div class="Level2NewsList3Bottom">
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                        <div class="Lv2NewsList3Video">
                            <div class="Lv2NewsList3VideoPicBox">
                                <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" />
                            </div>
                            <div class="Lv2NewsList3VideoLink">
                                <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                      <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a>
                            </div>
                        </div>
                        </ItemTemplate>
                    </asp:ListView>          
                </div>
                <div class="Level2NewsPart2Left">
                    <div class="Level2MainTitle">
                        <div class="Level2MainTitleTxt">
                            问答神州</div>
                        <div class="Level2MainTitleMore">
                            <a href="">更多>></a>
                        </div>
                    </div>
                    <div class="Level2NewsList3Top">
                        <img class="Level2NewsList3TopFace" src="images/Lv2NewsList3Face2.png" />
                        <div class="Level2NewsList3TopInfo">
                            <div class="Level2NewsList3TopInfoType">
                                主持人：<span>吴小莉</span></div>
                            <div class="Level2NewsList3TopInfoType">
                                简介：</div>
                            <div class="Level2NewsList3TopInfoText">
                                《问答神州》将眼光投向快速增长的中国经济，改革转型中的大时代，由凤凰卫视金牌主持人吴小莉担纲，致力采访两岸四地关键人物，与政坛精英高端对话，透过她的视角，观众可第一时间掌握中国的最新变化和发展。</div>
                        </div>
                    </div>
                    <asp:ListView ID="Level2NewsList4Bottom" runat="server">
                        <LayoutTemplate>
                            <div class="Level2NewsList3Bottom">
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                        <div class="Lv2NewsList3Video">
                            <div class="Lv2NewsList3VideoPicBox">
                                <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" />
                            </div>
                            <div class="Lv2NewsList3VideoLink">
                                <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                      <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a>
                            </div>
                        </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
            <div id="Level2NewsPart3">
                <div class="Level2NewsPart3Left">
                    <div class="Level2NewsPart3List4">
                        <div class="Level2MainTitle">
                            <div class="Level2MainTitleTxt">
                                财经今日谈</div>
                            <div class="Level2MainTitleMore">
                                 <a href="level3.aspx?alias=gxchannel9_topics_3_1&IsChild=1" target="_blank">更多>></a>
                            </div>
                        </div>
                        <asp:ListView ID="Level2NewsList5" runat="server">
                            <LayoutTemplate>
                                <div class="Level2NewsPart3List4Items">
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="Lv2NewsList4Link">
                                    <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                        <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:Image ID="Level2NewsList5Image" CssClass="Level2NewsPart3List4Pic" runat="server" />
                    </div>
                    <div class="Level2NewsPart3List4">
                        <div class="Level2MainTitle">
                            <div class="Level2MainTitleTxt">
                                中文财经</div>
                            <div class="Level2MainTitleMore">
                                <a href="level3.aspx?alias=gxchannel9_topics_5_1&IsChild=1" target="_blank">更多>></a>
                            </div>
                        </div>
                        <asp:ListView ID="Level2NewsList6" runat="server">
                            <LayoutTemplate>
                                <div class="Level2NewsPart3List4Items">
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="Lv2NewsList4Link">
                                    <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                        <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:Image ID="Level2NewsList6Image" CssClass="Level2NewsPart3List4Pic" runat="server" />
                    </div>
                    <div class="Level2NewsPart3List4">
                        <div class="Level2MainTitle">
                            <div class="Level2MainTitleTxt">
                                英文财经</div>
                            <div class="Level2MainTitleMore">
                                 <a href="level3.aspx?alias=gxchannel9_topics_4_1&IsChild=1" target="_blank">更多>></a>
                            </div>
                        </div>
                        <asp:ListView ID="Level2NewsList7" runat="server">
                            <LayoutTemplate>
                                <div class="Level2NewsPart3List4Items">
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="Lv2NewsList4Link">
                                    <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                        <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),13)%></a>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:Image ID="Level2NewsList7Image" CssClass="Level2NewsPart3List4Pic" runat="server" />
                    </div>
                </div>
                <div class="Level2NewsPart3Right">
                    <div class="Lv2NewsList5">
                        <div class="Level2MainTitle">
                            <div class="Level2MainTitleTxt">
                                时事开讲</div>
                            <div class="Level2MainTitleMore">
                                <a href="">更多>></a>
                            </div>
                        </div>
                        <asp:ListView ID="Level2NewsList8" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="Lv2NewsList5Item">
                                    <img class="Lv2NewsList5Pic" src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>" />
                                    <div class="Lv2NewsList5Info">
                                        <div class="Lv2NewsList5InfoTitle">
                                            <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                                <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),18)%></a></div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <div class="Lv2NewsList6">
                        <div class="Level2MainTitle">
                            <div class="Level2MainTitleTxt">
                                鲁豫有约</div>
                            <div class="Level2MainTitleMore">
                                <a href="">更多>></a>
                            </div>
                        </div>
                        <img src="images/Lv2NewsList6.png" />
                        <asp:ListView ID="Level2NewsList9" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="Lv2NewsList6Item">
                                    <div class="Lv2NewsList6TitleTxt">
                                        <a href="ShowVideo.aspx?ID=<%#Eval("ArticleGUID")%>" title="<%#Eval("Title")%>" target="_blank">
                                            <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),14)%></a>
                                    </div>
                                <div class="Lv2NewsList6Date">
                                    <%# Convert.ToDateTime(Eval("CreateTime")).ToShortDateString() %>
                                </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    </div>
            </div>
        </div>
</asp:Content>