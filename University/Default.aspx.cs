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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //热门关键词
            string sHotKey_Alias = ConfigurationManager.AppSettings["HotKeyAlias"];
            divHotKey.InnerHtml = new DAL.CategoryDAL().GetHotKey(sHotKey_Alias);
        }
    }
}
