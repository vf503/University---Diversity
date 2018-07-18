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
    public partial class Level3NaviLite : System.Web.UI.Page
    {
        public string url;
        public string alias = null; //Request:alias
        public string IsChild = null; //Request:IsChild
        public string ChannelAlias = null;
        public string ListType;
        public string ListOrder;
        public int Lv2CategoriesIndex;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";

            string ChannelTitle = null;
            // 痕迹&栏目标题
            if (Request.QueryString["alias"] == null)
            {
                Response.Redirect("HomeLite.aspx");
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
            string Lv2Alias = DataQuery.CategoryIDToAlias(CategoryPaths[5]);//2级分类
            ChannelTitle = DataQuery.GetNameByCategoryAlias(ChannelAlias);
            //CurrentTrace.Text = ChannelTitle;
            CurrentCategoryName.Text = ChannelTitle;
            //竖导航
            String Lv2sAlias = ChannelAlias;
            DataTable Lv2CategoriesInfo = DataQuery.GetSubCategories(Lv2sAlias);

            DataRow[] Lv2Current = Lv2CategoriesInfo.Select("CategoryAlias = '" + Lv2Alias + "'");
            if (Lv2Current.Length > 0)
            {
                Lv2CategoriesIndex = Array.IndexOf(Lv2CategoriesInfo.Select("", "XIndex  asc"), Lv2Current[0]);
            }

            Lv3Navi.DataSource = Lv2CategoriesInfo;
            Lv3Navi.DataBind();
            //主列表
            SetAliasAndIsChild();
            //
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
            ListView Lv3NaviLv2Items = (ListView)e.Item.FindControl("Lv3NaviLv2Items");
            Lv3NaviLv2Items.DataSource = Lv3CategoriesInfo;
            Lv3NaviLv2Items.DataBind();
        }
        protected void Lv3NaviLv2Items_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label CategoryAliasLabel = (Label)e.Item.FindControl("CurrentLv3Alias");
            string CurrentLv3Alias = CategoryAliasLabel.Text.ToString();
            DataTable Lv3CategoriesInfo = DataQuery.GetSubCategories(CurrentLv3Alias);
            ListView Lv3NaviLv3Items = (ListView)e.Item.FindControl("Lv3NaviLv3Items");
            Lv3NaviLv3Items.DataSource = Lv3CategoriesInfo;
            Lv3NaviLv3Items.DataBind();
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
                    RedirectUrl = "Level3NaviLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=pic&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    RedirectUrl = "Level3NaviLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=pic&order=" + ListOrder;
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
                    RedirectUrl = "Level3NaviLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=text&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    RedirectUrl = "Level3NaviLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=text&order=" + ListOrder;
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
                    RedirectUrl = "Level3NaviLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = "Level3NaviLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = "Level3NaviLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
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
                    RedirectUrl = "Level3NaviLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = "Level3NaviLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = "Level3NaviLite.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
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
    }
}