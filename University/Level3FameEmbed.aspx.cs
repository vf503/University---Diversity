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
    public partial class Level3FameEmbed : System.Web.UI.Page
    {
        public string url;
        //
        public string alias = null; //Request:alias
        public string IsChild = null; //Request:IsChild
        public string ChannelAlias = null;
        public string ListType;
        public string ListOrder;
        public int Lv2CategoriesIndex;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
            ChannelAlias = ConfigurationManager.AppSettings["ChannelFame"];
            string ChannelTitle = "名家";
            // 痕迹&栏目标题

            //音序索引
            string Lv3InitialAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "音序索引");
            DataTable Lv3InitialListItems = DataQuery.GetSubCategories(Lv3InitialAlias);
            Lv3InitialList.DataSource = Lv3InitialListItems;
            Lv3InitialList.DataBind();
            //机构索引
            //string Lv3OrgAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "机构索引");
            //DataTable Lv3OrgListItems = DataQuery.GetSubCategories(Lv3OrgAlias);
            //Lv3OrgList.DataSource = Lv3OrgListItems;
            //Lv3OrgList.DataBind();
            //嘉宾名录
            string Lv3NameAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "专家名录");
            DataTable Lv3NameListItems = DataQuery.GetSubCategories(Lv3NameAlias);
            Lv3NameList.DataSource = Lv3NameListItems;
            Lv3NameList.DataBind();
            //折叠菜单状态
            SetAliasAndIsChild();
            string CategoryGUID = DataQuery.CategoryAliasToID(alias);
            string CategoryPath = DataQuery.CategoryPath(CategoryGUID);
            char[] PathSeparator = { '/' };
            string[] CategoryPaths = CategoryPath.Split(PathSeparator);
            string Lv2Alias = DataQuery.CategoryIDToAlias(CategoryPaths[5]);//2级分类
            if (Lv2Alias == Lv3InitialAlias) Lv2CategoriesIndex = 0;
            //else if (Lv2Alias == Lv3OrgAlias) Lv2CategoriesIndex = 1;
            else if (Lv2Alias == Lv3NameAlias) Lv2CategoriesIndex = 2;
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
                    RedirectUrl = "Level3FameEmbed.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=pic&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    RedirectUrl = "Level3FameEmbed.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=pic&order=" + ListOrder;
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
                    RedirectUrl = "Level3FameEmbed.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=text&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    RedirectUrl = "Level3FameEmbed.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=text&order=" + ListOrder;
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
                    RedirectUrl = "Level3FameEmbed.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = "Level3FameEmbed.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = "Level3FameEmbed.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
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
                    RedirectUrl = "Level3FameEmbed.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = "Level3FameEmbed.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = "Level3FameEmbed.aspx?alias=" + alias + "&IsChild=" + IsChild + "&type=" + ListType + "&order=" + ListOrder;
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
                IsChild = "0";
                alias = DataQuery.GetChannelAliasByName(ChannelAlias, "音序索引");
            }
        }
    }
}