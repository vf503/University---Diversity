using System;
using System.Data;
using System.Data.SqlClient;

namespace colleges
{
    public partial class SpecialIndexLite : System.Web.UI.Page
    {
        protected string sCategoryGUID = string.Empty;
        private string sTitle = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            sCategoryGUID = (string)Session["ZJSP_ZTFL_GUID"];
            //*CC*
            DataTable CategoryInfo = new DAL.CategoryDAL().GetCategorySimpleInfo(sCategoryGUID);
            if (CategoryInfo.Rows.Count == 0) return;
            string sCategoryPath = CategoryInfo.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(CategoryInfo.Rows[0]["YIndex"].ToString()) + 1;
            DataTable ZTList = new DAL.CategoryDAL().GetZTFromCategory(sCategoryPath, iYIndex, 10, "asc");
            HistoryList.DataSource = ZTList;
            HistoryList.DataBind();
            //HistoryMore.NavigateUrl = "SpecialHistory.aspx?ID=" + sCategoryGUID;
        }
    }
}