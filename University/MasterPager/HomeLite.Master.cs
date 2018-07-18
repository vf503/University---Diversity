using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CEInet.Pisces.Web.USSLIB;

namespace colleges.MasterPager
{
    public partial class HomeLite : System.Web.UI.MasterPage
    {
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
            //热门关键词
            string sHotKey_Alias = ConfigurationManager.AppSettings["HotKeyAlias"];
            divHotKey.InnerHtml = new DAL.CategoryDAL().GetHotKeyLite(sHotKey_Alias);

            //导航
            string VoidGuid = ConfigurationManager.AppSettings["Void_Guid"];
            string VersionMark = ConfigurationManager.AppSettings["Version_Mark"];
            DataTable NaviListData = DataQuery.GetNaviCategories(VoidGuid, "15", VersionMark);
            //TopNaviList.DataSource = NaviListData;
            // TopNaviList.DataBind();
        }
        //
        protected void Page_Load(object sender, EventArgs e)
        {

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
                //Response.Redirect(url);
                Response.Write("<script language=javascript>$('#LoginFrame').attr('src', $('#LoginFrame').attr('src'));</script>");
            }
            else
                Response.Redirect(sURL);
        }
    }
}