using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace colleges
{
    public partial class TestPic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string sTitle = "技能";
            string sAlias = "gxn_trailer";
            if (string.IsNullOrEmpty(sTitle)) return;

            string str = HttpUtility.UrlEncode(sTitle, Encoding.GetEncoding("utf-8"));
            string str2 = Server.UrlDecode(str);
            byte[] btImg = GetAttachment(str2, sAlias);
            if (btImg.Length > 0)
            {
                Response.BinaryWrite(btImg);
            }
        }
        private byte[] GetAttachment(string sTitle, string alias)
        {
            DataTable dt = new DataTable();
            dt = new DAL.CategoryDAL().GetImage(alias, sTitle);
            if (dt.Rows.Count == 0) return new byte[] { };

            byte[] btImg = string.IsNullOrEmpty(dt.Rows[0]["ContentSize"].ToString()) ? new byte[] { } : ((byte[])dt.Rows[0]["Content"]);
            return btImg;
        }
    }
}
