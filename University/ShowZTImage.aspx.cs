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
    public partial class ShowZTImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sTitle = Request.QueryString["Title"];
            if (string.IsNullOrEmpty(sTitle)) return;

            byte[] btImg = GetAttachment(sTitle);
            if (btImg.Length > 0)
            {
                Response.BinaryWrite(btImg);
            }
        }

        private byte[] GetAttachment( string sTitle)
        {
            string sZTSummaryAlias = ConfigurationManager.AppSettings["ZTSummaryAlias"];
            DataTable dt = new DataTable();
            dt = new DAL.CategoryDAL().GetImage(sZTSummaryAlias, sTitle);
            if (dt.Rows.Count == 0) return new byte[] { };

            byte[] btImg = string.IsNullOrEmpty(dt.Rows[0]["ContentSize"].ToString()) ? new byte[] { } : ((byte[])dt.Rows[0]["Content"]);
            return btImg;
        }

    }
}
