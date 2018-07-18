using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace colleges
{
    public partial class navigate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //导航
            string NaviList1Alias = ConfigurationManager.AppSettings["subject"];
            DataTable NaviList1Items = DataQuery.GetSubCategories(NaviList1Alias);
            NaviList1.DataSource = NaviList1Items;
            NaviList1.DataBind();
            string NaviList2Alias = ConfigurationManager.AppSettings["MAJOR"];
            DataTable NaviList2Items = DataQuery.GetSubCategories(NaviList2Alias);
            NaviList2.DataSource = NaviList2Items;
            NaviList2.DataBind();
            string NaviList3Alias = ConfigurationManager.AppSettings["INSTITUTION"];
            DataTable NaviList3Items = DataQuery.GetSubCategories(NaviList3Alias);
            NaviList3.DataSource = NaviList3Items;
            NaviList3.DataBind();
        }
    }
}
