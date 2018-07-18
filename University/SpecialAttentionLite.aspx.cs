using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace colleges
{
    public partial class SpecialAttentionLite : System.Web.UI.Page
    {
        protected string sFLTitle = string.Empty;
        protected string sZTTitle = string.Empty;
        protected string sCategoryGUID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ID"])) return;

            sFLTitle = (string)Session["ZJSP_ZTFL_NAME"];
            sCategoryGUID = (string)Session["ZJSP_ZTFL_GUID"];

            string sID = Request.QueryString["ID"].ToString();
            sZTTitle = Request.QueryString["Title"].ToString();
        }
    }
}