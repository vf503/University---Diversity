﻿using System;
using System.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CEInet.Pisces.Web.USSLIB;
using System.Text;

namespace colleges.MasterPager
{
    public partial class Level2MasterPage : System.Web.UI.MasterPage
    {
        public string url;
        public string sSearchKeyWords = string.Empty;
        // 绑定导航热点
        protected void TopNaviList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label CategoryAliasLabel = (Label)e.Item.FindControl("CategoryAlias");
            string Lv2CategoryAlias = CategoryAliasLabel.Text.ToString();
            String HotAlias = DataQuery.GetChannelAliasByName(Lv2CategoryAlias, "热点");
            DataTable SubNaviDate;
            SubNaviDate = DataQuery.GetSubCategories(HotAlias, "3");
            ListView TopNaviSubListView = (ListView)e.Item.FindControl("TopNaviSubList");
            TopNaviSubListView.DataSource = SubNaviDate;
            TopNaviSubListView.DataBind();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
            //热门关键词
            string sHotKey_Alias = ConfigurationManager.AppSettings["HotKeyAlias"];
            divHotKey.InnerHtml = new DAL.CategoryDAL().GetHotKey(sHotKey_Alias);

            //导航
            string VoidGuid = ConfigurationManager.AppSettings["Void_Guid"];
            string VersionMark = ConfigurationManager.AppSettings["Version_Mark"];
            DataTable NaviListData = DataQuery.GetNaviCategories(VoidGuid, "10", VersionMark);
            TopNaviList.DataSource = NaviListData;
            TopNaviList.DataBind();

            string ChannelAlias = null;
            string ChannelTitle = null;
            // 跳转&痕迹
            string CurrentPage = Path.GetFileName(Request.PhysicalPath);
            if (CurrentPage == "Level2Fame.aspx")
            {
                ChannelAlias = ConfigurationManager.AppSettings["ChannelFame"];
                ChannelTitle = DataQuery.GetNameByCategoryAlias(ChannelAlias);
                CurrentTrace.Text = ChannelTitle;
                TraceLv2Link.NavigateUrl = "Level2Fame.aspx";
            }
            else if (CurrentPage == "Level2Class.aspx")
            {
                ChannelAlias = ConfigurationManager.AppSettings["ChannelClass"];
                ChannelTitle = DataQuery.GetNameByCategoryAlias(ChannelAlias);
                CurrentTrace.Text = ChannelTitle;
                TraceLv2Link.NavigateUrl = "Level2Class.aspx";
            }
            else if (CurrentPage == "Level2News.aspx")
            {
                ChannelAlias = ConfigurationManager.AppSettings["ChannelNews"];
                ChannelTitle = DataQuery.GetNameByCategoryAlias(ChannelAlias);
                CurrentTrace.Text = ChannelTitle;
                TraceLv2Link.NavigateUrl = "Level2News.aspx";
            }
            else
            {
                try
                {
                    ChannelAlias = Request.QueryString["alias"].ToString();
                }
                catch (Exception AliasNull)
                {
                    Response.Redirect("index.aspx");
                }
                if (ChannelAlias == ConfigurationManager.AppSettings["ChannelFame"])
                {
                    Response.Redirect("Level2Fame.aspx");
                }
                else if (ChannelAlias == ConfigurationManager.AppSettings["ChannelClass"])
                {
                    Response.Redirect("Level2Class.aspx");
                }
                //else if (ChannelAlias == ConfigurationManager.AppSettings["ChannelNews"])
                //{
                //    Response.Redirect("Level2News.aspx");
                //}
                else
                {
                    ChannelTitle = DataQuery.GetNameByCategoryAlias(ChannelAlias);
                    CurrentTrace.Text = ChannelTitle;
                    TraceLv2Link.NavigateUrl = "level2.aspx?alias=" + ChannelAlias;
                }
            }
            // 导视
            String GuideAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "导视");
            String GuideGuid = DataQuery.CategoryAliasToID(GuideAlias);
            DataTable GuideCourses = new DAL.Article().GetArticleList(GuideGuid, true, 5);
            //Remove Html
            SlideShowPic.DataSource = GuideCourses;
            SlideShowPic.DataBind();
            SlideShowText.DataSource = GuideCourses;
            SlideShowText.DataBind();
            // 图片
            Level2Title.ImageUrl = "../images/" + ChannelAlias + ".png";
            //预告
            String TrailerAlias = ConfigurationManager.AppSettings["ChannelTrailer"];
            string EnCodeChannelTitle = HttpUtility.UrlEncode(ChannelTitle, Encoding.GetEncoding("utf-8"));
            PreviewPic.ImageUrl = "../ShowBytePic.aspx?Title=" + EnCodeChannelTitle + "&Alias=" + TrailerAlias;
            String TrailerText = DataQuery.GetTrailerText(TrailerAlias, ChannelTitle);
            if (TrailerText != null)
            {
                TrailerText = TrailerText.Replace("；", ";");
                string[] TrailerInfo = TrailerText.Split(';');
                PreviewTitle.Text = DataProcessing.SubstringText(TrailerInfo[0], 10);
                PreviewTitle.ToolTip = TrailerInfo[0].ToString();
                PreviewSpeaker.Text = TrailerInfo[1];
                PreviewSpeakerInfo.Text = DataProcessing.SubstringText(TrailerInfo[2], 10);
                PreviewSpeakerInfo.ToolTip = TrailerInfo[2].ToString();
            }
        }
        //
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase myPage;
            try
            {
                myPage = new PageBase(this.Page);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('WebService地址或访问权限配置不正确！');", true);
                trLogin.Visible = false;
                trExit.Visible = false;
                return;
            }

            lblWelcome.Text = "已登录";

            if (!IsPostBack)
            {
                if (IsLogin(Page))
                {
                    ShowExit(myPage.get_UserID(true));
                }
                else
                {
                    try
                    {
                        DLLUserService.User usr = new DLLUserService.User();
                        string sUserID = "";
                        string sToken;

                        sToken = usr.Login(ref sUserID, "", Request.UserHostAddress);
                        //sToken = usr.Login(ref sUserID, "", "192.168.194.105");
                        myPage.set_UserID(true, sUserID);
                        myPage.set_Token(true, sToken);
                        if (sToken != "")
                            ShowExit(sUserID);
                        else
                            ShowLogin();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().IndexOf(" 404 失败") > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('WebService地址或访问权限配置不正确！');", true);
                            trLogin.Visible = false;
                            trExit.Visible = false;
                            return;
                        }
                        else
                            ShowLogin();
                    }
                }
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            string sUid;
            string sPwd;
            string sToken = "";
            bool isSession = true;
            PageBase myPage = new PageBase(Page);
            DLLUserService.User myUser = new DLLUserService.User();

            sUid = txtUid.Text.Trim();
            sPwd = txtPwd.Text.Trim();

            if (Request.Form["chLogAuto"] == "1") isSession = false;

            try
            {
                //sToken = myUser.Login(ref sUid, sPwd, "192.168.194.105");
                sToken = myUser.Login(ref sUid, sPwd, Request.UserHostAddress);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('" + GetSoapError(ex) + "');", true);
                return;
            }

            try
            {
                myUser.Logout(myPage.TokenEx);
            }
            catch (Exception ex)
            {
            }

            //写cookie
            myPage.set_UserID(true, sUid);
            myPage.set_Token(isSession, sToken);

            GotoPage();
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            PageBase myPage = new PageBase(Page);
            DLLUserService.User myUser = new DLLUserService.User();

            try
            {
                myUser.Logout(myPage.TokenEx);
            }
            catch (Exception ex)
            {
            }
            //清空cookie
            myPage.set_UserID(true, "");
            ShowLogin();
        }

        private bool IsLogin(Page pag)
        {
            DLLUserService.User usr = new DLLUserService.User();
            PageBase pb;
            try
            {
                pb = new PageBase(pag);
            }
            catch (Exception ex)
            {
                HttpCookie cookie;
                cookie = pag.Request.Cookies["USSUserID"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    pag.Response.SetCookie(cookie);
                }
                cookie = pag.Request.Cookies["USSToken"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    pag.Response.SetCookie(cookie);
                }
                cookie = pag.Request.Cookies["USSUserInfo"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    pag.Response.SetCookie(cookie);
                }
                cookie = pag.Request.Cookies["USSDocInfo"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    pag.Response.SetCookie(cookie);
                }

                pb = new PageBase(pag);
            }

            try
            {
                usr.Authenticate(pb.TokenEx, 0);
            }
            catch (Exception ex)
            {
                return false;
            }
            pb.UserActiveTime = System.DateTime.Now;
            return true;
        }

        private void ShowExit(string sUser)
        {
            lblUser.Text = sUser;
            trExit.Visible = true;
            trLogin.Visible = false;
        }

        private void ShowLogin()
        {
            trExit.Visible = false;
            trLogin.Visible = true;
            txtUid.Text = "";
            txtPwd.Text = "";
        }

        private string GetSoapError(System.Exception ex)
        {
            string sErrorMsg = ex.Message;
            int iIndex;

            iIndex = sErrorMsg.IndexOf("--> ");
            if (iIndex > 0) sErrorMsg = sErrorMsg.Substring(iIndex + 4);
            iIndex = sErrorMsg.IndexOf(" at");
            if (iIndex > 0) sErrorMsg = sErrorMsg.Substring(0, iIndex);
            return sErrorMsg.Trim();
        }

        private void GotoPage()
        {
            //根据是否有URLReffer参数来决定是展示欢迎词还是转向页面
            //根据URLReffer参数转向页面
            string sURL = HttpUtility.UrlDecode(Request.QueryString["URLReffer"]);
            if (sURL == "" || sURL == null)
            {
                string url = Request.Url.ToString();
                Response.Redirect(url);
            }
            else
                Response.Redirect(sURL);
        }
    }
}

