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
    public partial class level3pager : System.Web.UI.Page
    {
        public string url;
        public string alias = null; //Request:alias
        public string IsChild = null; //Request:IsChild
        public string ChannelAlias = null;
        public string ListType;
        public string ListOrder;
        public int Lv2CategoriesIndex;
        public string guid;
        public int RecordCount;
        protected AspNetPager Lv3Pager = new Wuqi.Webdiyer.AspNetPager();

        protected void Page_Load(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";

            string ChannelTitle = null;
            // 痕迹&栏目标题
            if (Request.QueryString["alias"] == null)
            {
                //Response.Redirect("index.aspx");
                alias = "gxchannel1_topics_1";
                IsChild = "0";
            }
            else
            {
                SetAliasAndIsChild();
            }

            string CategoryGUID = DataQuery.CategoryAliasToID(alias);
            string CategoryPath = DataQuery.CategoryPath(CategoryGUID);
            char[] PathSeparator = { '/' };
            string[] CategoryPaths = CategoryPath.Split(PathSeparator);
            ChannelAlias = DataQuery.CategoryIDToAlias(CategoryPaths[4]);
            string Lv2Alias = DataQuery.CategoryIDToAlias(CategoryPaths[6]);//2级分类
            ChannelTitle = DataQuery.GetNameByCategoryAlias(ChannelAlias);
            CurrentTrace.Text = ChannelTitle;
            CurrentCategoryName.Text = ChannelTitle;
            TraceLv2Link.NavigateUrl = "level2.aspx?alias=" + ChannelAlias;
            //竖导航
            String Lv2sAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "栏目");
            DataTable Lv2CategoriesInfo = DataQuery.GetSubCategories(Lv2sAlias);

            DataRow[] Lv2Current = Lv2CategoriesInfo.Select("CategoryAlias = '" + Lv2Alias + "'");
            if (Lv2Current.Length > 0)
            {
                Lv2CategoriesIndex = Array.IndexOf(Lv2CategoriesInfo.Select("", "XIndex  asc"), Lv2Current[0]);
            }

            Lv3Navi.DataSource = Lv2CategoriesInfo;
            Lv3Navi.DataBind();
            //主列表
            if (Request.QueryString["order"] == null)
            {
                ListOrder = "desc";
                RadioDesc.Checked = true;
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
            //
            ListType = GetListType();
            //
            guid = DataQuery.CategoryAliasToID(alias);
            Lv3Pager.PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            if (!IsPostBack)
            {
                DataTable dt = new DAL.Article().GetArticleList(guid, false, "desc", true);
                int iTotalRowsCount = dt.Rows.Count;
                Lv3Pager.RecordCount = iTotalRowsCount;
                MainDataBind();
            }
        }

        protected void Lv3Navi_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label CategoryAliasLabel = (Label)e.Item.FindControl("CurrentLv2Alias");
            string CurrentLv2Alias = CategoryAliasLabel.Text.ToString();
            DataTable Lv3CategoriesInfo = DataQuery.GetSubCategories(CurrentLv2Alias);
            ListView Lv3Items = (ListView)e.Item.FindControl("Lv3Items");
            Lv3Items.DataSource = Lv3CategoriesInfo;
            Lv3Items.DataBind();
        }

        protected void Level3MainListPic_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

        }

        protected void Lv3Pager_PageChanged(object sender, EventArgs e)
        {
            MainDataBind();
        }

        protected void PicBtn_Click(object sender, ImageClickEventArgs e)
        {
            SetAliasAndIsChild();
            string RedirectUrl;
            switch (RadioDesc.Checked)
            {
                case true:
                    ListOrder = "desc";
                    break;
                case false:
                    ListOrder = "asc";
                    break;
                default:
                    ListOrder = "desc";
                    break;
            }
            switch (ListType)
            {
                case "pic":
                    break;
                case "text":
                    RedirectUrl = Request.CurrentExecutionFilePath + "?alias=" + alias + "&IsChild=" + IsChild + "&type=pic&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    RedirectUrl = Request.CurrentExecutionFilePath + "?alias=" + alias + "&IsChild=" + IsChild + "&type=pic&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
            }
        }

        protected void TextBtn_Click(object sender, ImageClickEventArgs e)
        {
            SetAliasAndIsChild();
            string RedirectUrl;
            switch (RadioDesc.Checked)
            {
                case true:
                    ListOrder = "desc";
                    break;
                case false:
                    ListOrder = "asc";
                    break;
                default:
                    ListOrder = "desc";
                    break;
            }
            switch (ListType)
            {
                case "text":
                    break;
                case "pic":
                    RedirectUrl = Request.CurrentExecutionFilePath + "?alias=" + alias + "&IsChild=" + IsChild + "&type=text&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    RedirectUrl = Request.CurrentExecutionFilePath + "?alias=" + alias + "&IsChild=" + IsChild + "&type=text&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
            }
        }

        protected void RadioDesc_CheckedChanged(object sender, EventArgs e)
        {
            SetAliasAndIsChild();
            string RedirectUrl;
            ListType = GetListType();
            switch (RadioDesc.Checked)
            {
                case true:
                    ListOrder = "desc";
                    RedirectUrl = Request.CurrentExecutionFilePath + "?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = Request.CurrentExecutionFilePath + "?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = Request.CurrentExecutionFilePath + "?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
            }
        }

        protected void RadioAsc_CheckedChanged(object sender, EventArgs e)
        {
            SetAliasAndIsChild();
            string RedirectUrl;
            ListType = GetListType();
            switch (RadioDesc.Checked)
            {
                case true:
                    ListOrder = "desc";
                    RedirectUrl = Request.CurrentExecutionFilePath + "?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = Request.CurrentExecutionFilePath + "?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = Request.CurrentExecutionFilePath + "?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
            }
        }

        public string GetListType()
        {
            string ListType;
            if (Request.QueryString["type"] == null)
            {
                ListType = "pic";
                return ListType;
            }
            else
            {
                ListType = Request.QueryString["type"].ToString();
                return ListType;
            }
        }

        public void SetAliasAndIsChild()
        {
            IsChild = Request.QueryString["IsChild"].ToString();
            alias = Request.QueryString["alias"].ToString();
        }

        public void MainPicListDataBind(string guid, bool NeedSummary, string ListOrder, bool HasChildCategory)
        {
            int CurrentPage;
            CurrentPage = Lv3Pager.CurrentPageIndex;
            Level3MainListPic.DataSource = new DAL.Article().GetArticleList(guid, NeedSummary, ListOrder, CurrentPage, HasChildCategory, out RecordCount);
            Level3MainListPic.DataBind();
        }
        public void MainTextListDataBind(string guid, bool NeedSummary, string ListOrder, bool HasChildCategory)
        {
            int CurrentPage;
            CurrentPage = Lv3Pager.CurrentPageIndex;
            Level3MainListText.DataSource = new DAL.Article().GetArticleList(guid, NeedSummary, ListOrder, CurrentPage, HasChildCategory, out RecordCount);
            Level3MainListText.DataBind();
        }
        public void MainDataBind()
        {
           // System.Threading.Thread.Sleep(3000);
            if (IsChild == "0")
            {
                switch (ListType)
                {
                    case "pic":
                        MainPicListDataBind(guid, false, ListOrder, true);
                        break;
                    case "text":
                        MainTextListDataBind(guid, false, ListOrder, true);
                        break;
                    default:
                        MainPicListDataBind(guid, false, ListOrder, true);
                        break;
                }

            }
            else
            {
                switch (ListType)
                {
                    case "pic":
                        MainPicListDataBind(guid, false, ListOrder, false);
                        break;
                    case "text":
                        MainTextListDataBind(guid, false, ListOrder, false);
                        break;
                    default:
                        MainPicListDataBind(guid, false, ListOrder, false);
                        break;
                }
            }
        }
    }
}
