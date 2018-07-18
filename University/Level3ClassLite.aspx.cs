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
    public partial class Level2ClassLite : System.Web.UI.Page
    {
        public string url;
        public string alias = null; //Request:alias
        public string IsChild = null; //Request:IsChild
        public string ChannelAlias = null;
        public string ListType;
        public string ListOrder;
        public int Lv2CategoriesIndex;
        public string Lv3NaviAlias1;
        public string Lv3NaviAlias1a;
        public string Lv3NaviAlias1b;
        public string Lv3NaviAlias2;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
            ChannelAlias = ConfigurationManager.AppSettings["ChannelClass"];
            string ChannelTitle = "名校公开课";
            // 痕迹&栏目标题
            //CurrentTrace.Text = ChannelTitle;
            //CurrentCategoryName.Text = ChannelTitle;
            //TraceLv2Link.NavigateUrl = "Level2Class.aspx";
            //1

            //2
            Lv3NaviAlias1 = DataQuery.GetChannelAliasByName(ChannelAlias, "国内985大学");
            DataTable Lv3Navi2Items = DataQuery.GetSubCategories(Lv3NaviAlias1);
            Lv3Navi2.DataSource = Lv3Navi2Items;
            Lv3Navi2.DataBind();
            //2a
            Lv3NaviAlias1a = DataQuery.GetChannelAliasByName(ChannelAlias, "国内211大学");
            DataTable Lv3Navi2aItems = DataQuery.GetSubCategories(Lv3NaviAlias1a);
            Lv3Navi2a.DataSource = Lv3Navi2aItems;
            Lv3Navi2a.DataBind();
            //2b
            Lv3NaviAlias1b = DataQuery.GetChannelAliasByName(ChannelAlias, "国内其他大学");
            DataTable Lv3Navi2bItems = DataQuery.GetSubCategories(Lv3NaviAlias1b);
            Lv3Navi2b.DataSource = Lv3Navi2bItems;
            Lv3Navi2b.DataBind();
            //3
            Lv3NaviAlias2 = DataQuery.GetChannelAliasByName(ChannelAlias, "国外大学");
            DataTable Lv3Navi3Items = DataQuery.GetSubCategories(Lv3NaviAlias2);
            Lv3Navi3.DataSource = Lv3Navi3Items;
            Lv3Navi3.DataBind();
            //
            SetAliasAndIsChild();
            string CategoryGUID = DataQuery.CategoryAliasToID(alias);
            string CategoryPath = DataQuery.CategoryPath(CategoryGUID);
            char[] PathSeparator = { '/' };
            string[] CategoryPaths = CategoryPath.Split(PathSeparator);
            string Lv2Alias = DataQuery.CategoryIDToAlias(CategoryPaths[5]);//2级分类
            if (Lv2Alias == "gxb2_openclass_3") Lv2CategoriesIndex = 0;
            else if (Lv2Alias == Lv3NaviAlias1) Lv2CategoriesIndex = 1;
            else if (Lv2Alias == Lv3NaviAlias1a) Lv2CategoriesIndex = 2;
            else if (Lv2Alias == Lv3NaviAlias1b) Lv2CategoriesIndex = 3;
            else if (Lv2Alias == Lv3NaviAlias2) Lv2CategoriesIndex = 4;
            else Lv2CategoriesIndex = 0;
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
            string guid = DataQuery.CategoryAliasToID(alias);
            if (IsChild == "0")
            {
                switch (ListType)
                {
                    case "pic":
                        Level3MainListPic.DataSource = new DAL.Article().GetArticleList(guid, false, ListOrder, true);
                        Level3MainListPic.DataBind();
                        break;
                    case "text":
                        Level3MainListText.DataSource = new DAL.Article().GetArticleList(guid, false, ListOrder, true);
                        Level3MainListText.DataBind();
                        break;
                    default:
                        Level3MainListPic.DataSource = new DAL.Article().GetArticleList(guid, false, ListOrder, true);
                        Level3MainListPic.DataBind();
                        break;
                }
            }
            else
            {
                switch (ListType)
                {
                    case "pic":
                        Level3MainListPic.DataSource = new DAL.Article().GetArticleList(guid, false, ListOrder, false);
                        Level3MainListPic.DataBind();
                        break;
                    case "text":
                        Level3MainListText.DataSource = new DAL.Article().GetArticleList(guid, false, ListOrder, false);
                        Level3MainListText.DataBind();
                        break;
                    default:
                        Level3MainListPic.DataSource = new DAL.Article().GetArticleList(guid, false, ListOrder, false);
                        Level3MainListPic.DataBind();
                        break;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

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
                    RedirectUrl = "Level3classLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=pic&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    RedirectUrl = "Level3classLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=pic&order=" + ListOrder;
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
                    RedirectUrl = "Level3classLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=text&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    RedirectUrl = "Level3classLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=text&order=" + ListOrder;
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
                    RedirectUrl = "Level3classLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = "Level3classLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = "Level3classLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
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
                    RedirectUrl = "Level3classLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = "Level3classLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = "Level3classLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
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
            if (Request.QueryString["alias"] != null)
            {
                IsChild = Request.QueryString["IsChild"].ToString();
                alias = Request.QueryString["alias"].ToString();
            }
            else
            {
                IsChild = "1";
                alias = DataQuery.GetChannelAliasByName(ChannelAlias, "精品课程");
            }
        }
    }
}