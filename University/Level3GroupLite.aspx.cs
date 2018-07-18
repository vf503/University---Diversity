using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace colleges
{
    public partial class Level3GroupLite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String CategoryGUID = "";
            string TraceText = "";
            if (Request.QueryString["id"] == null)
            {
                CategoryGUID = "0f9364f1992a4f40ad76435f279a79f6";
            }
            else
            {
                CategoryGUID = Request.QueryString["id"].ToString();
            }
            char[] PathSeparator = { '/' };
            string CategoryPath = DataQuery.CategoryPath(CategoryGUID);
            string[] CategoryPaths = CategoryPath.Split(PathSeparator);
            for (int i = 5; i < CategoryPaths.Length; i++)
            {
                string title = DataQuery.GetNameByCategoryID(CategoryPaths[i]);
                TraceText += title + " / ";
            }
            Trace.Text = TraceText;
        }
    }
}