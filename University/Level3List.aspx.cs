using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Wuqi.Webdiyer;

namespace colleges
{
    //public partial class Level3List : System.Web.UI.Page
    //{

    //    protected AspNetPager Lv3Pager = new Wuqi.Webdiyer.AspNetPager();
    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        //主列表
           
    //        guid = DataQuery.CategoryAliasToID(alias);
    //        Lv3Pager.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
    //        if (!IsPostBack)
    //        {
    //            DataTable dt = new DAL.Article().GetArticleList(guid, false, "desc", true);
    //            int iTotalRowsCount = dt.Rows.Count;
    //            Lv3Pager.RecordCount = iTotalRowsCount;
    //            MainDataBind();
    //        }
    //    }
    //    protected void Lv3Pager_PageChanged(object sender, EventArgs e)
    //    {
    //        MainDataBind();
    //    }
    //    public void MainPicListDataBind(string guid, bool NeedSummary, string ListOrder, bool HasChildCategory)
    //    {
    //        int CurrentPage;
    //        CurrentPage = Lv3Pager.CurrentPageIndex;
    //        Level3MainListPic.DataSource = new DAL.Article().GetArticleList(guid, NeedSummary, ListOrder, CurrentPage, HasChildCategory, out RecordCount);
    //        Level3MainListPic.DataBind();
    //    }
    //    public void MainTextListDataBind(string guid, bool NeedSummary, string ListOrder, bool HasChildCategory)
    //    {
    //        int CurrentPage;
    //        CurrentPage = Lv3Pager.CurrentPageIndex;
    //        Level3MainListText.DataSource = new DAL.Article().GetArticleList(guid, NeedSummary, ListOrder, CurrentPage, HasChildCategory, out RecordCount);
    //        Level3MainListText.DataBind();
    //    }
    //    public void MainDataBind()
    //    {
    //        // System.Threading.Thread.Sleep(3000);
    //        if (IsChild == "0")
    //        {
    //            switch (ListType)
    //            {
    //                case "pic":
    //                    MainPicListDataBind(guid, false, ListOrder, true);
    //                    break;
    //                case "text":
    //                    MainTextListDataBind(guid, false, ListOrder, true);
    //                    break;
    //                default:
    //                    MainPicListDataBind(guid, false, ListOrder, true);
    //                    break;
    //            }

    //        }
    //        else
    //        {
    //            switch (ListType)
    //            {
    //                case "pic":
    //                    MainPicListDataBind(guid, false, ListOrder, false);
    //                    break;
    //                case "text":
    //                    MainTextListDataBind(guid, false, ListOrder, false);
    //                    break;
    //                default:
    //                    MainPicListDataBind(guid, false, ListOrder, false);
    //                    break;
    //            }
    //        }
    //    }
    //}
}
