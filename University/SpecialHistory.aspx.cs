using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace colleges
{
    public partial class SpecialHistory : System.Web.UI.Page
    {
        protected string sCategoryGUID = string.Empty;
        private string sTitle = string.Empty;
        public string ListOrder;
        protected void Page_Load(object sender, EventArgs e)
        {
            sCategoryGUID = Request.QueryString["ID"].ToString(); ;
            DataTable CategoryInfo = new DAL.CategoryDAL().GetCategorySimpleInfo(sCategoryGUID);
            if (CategoryInfo.Rows.Count == 0) return;
            string sCategoryPath = CategoryInfo.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(CategoryInfo.Rows[0]["YIndex"].ToString()) + 1;
            //Left
            string sTBGZ_Alias = ConfigurationManager.AppSettings["TBGZ_Alias"];
            DataTable ZTNaviData = new DAL.CategoryDAL().GetZTMenu(sTBGZ_Alias);
            SpecialTree.DataSource = ZTNaviData;
            SpecialTree.DataBind();
            //Right
            if (Request.QueryString["order"] == null)
            {
                ListOrder = "Asc";
                RadioAsc.Checked = true;
            }
            else
            {
                ListOrder = Request.QueryString["order"].ToString();
                switch (ListOrder)
                {
                    case "asc":
                        RadioAsc.Checked = true;
                        break;
                    case "desc":
                        RadioDesc.Checked = true;
                        break;
                    default:
                        RadioDesc.Checked = true;
                        break;
                }
            }
            DataTable ZTMainData = new DAL.CategoryDAL().GetZTFromCategoryNote(sCategoryPath, iYIndex, ListOrder);
            ZTMainList.DataSource = ZTMainData;
            ZTMainList.DataBind();
        }
        protected void RadioDesc_CheckedChanged(object sender, EventArgs e)
        {
            string RedirectUrl;
            ID = Request.QueryString["ID"].ToString();
            switch (RadioDesc.Checked)
            {
                case true:
                    ListOrder = "desc";
                    RedirectUrl = "SpecialHistory.aspx?ID=" + ID+ "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = "SpecialHistory.aspx?ID=" + ID + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = "SpecialHistory.aspx?ID=" + ID + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
            }
        }

        protected void RadioAsc_CheckedChanged(object sender, EventArgs e)
        {
            string RedirectUrl;
            ID = Request.QueryString["ID"].ToString();
            switch (RadioDesc.Checked)
            {
                case true:
                    ListOrder = "asc";
                    RedirectUrl = "SpecialHistory.aspx?ID=" + ID + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "desc";
                    RedirectUrl = "SpecialHistory.aspx?ID=" + ID + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "asc";
                    RedirectUrl = "SpecialHistory.aspx?ID=" + ID + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
            }
        }
    }
}
