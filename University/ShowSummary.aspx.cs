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
    public partial class ShowSummary : System.Web.UI.Page
    {
        protected string sZTSummaryAlias = ConfigurationManager.AppSettings["ZTSummaryGUID"];
        protected string sTitle =string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            sTitle = Request.QueryString["Title"];
            if (string.IsNullOrEmpty(sTitle)) return;
            divSummary.InnerHtml = new DAL.CategoryDAL().GetZTSummaryFromTitle(sZTSummaryAlias, sTitle);
        }
    }
}
