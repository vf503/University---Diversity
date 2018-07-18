using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace colleges
{
    public partial class embed : System.Web.UI.Page
    {
        public string url;
        protected void Page_Load(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
            string EmbedGuid = DataQuery.CategoryAliasToID(ConfigurationManager.AppSettings["EmbedCategory"]);
            EmbedList.DataSource = new DAL.Article().GetArticleList(EmbedGuid, false, 15);
            EmbedList.DataBind();
        }
    }
}
