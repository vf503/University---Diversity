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
    public partial class Level3Hot : System.Web.UI.Page
    {
        public string url;
        public string Lv2HotAlias = null;
        public string ChannelAlias = null;
        public string ListType;
        public string ListOrder;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";

            string ChannelTitle = null;

            // 痕迹&栏目标题
            try
            {
                Lv2HotAlias = Request.QueryString["alias"].ToString();
                string Lv2HotCategoryGUID = DataQuery.CategoryAliasToID(Lv2HotAlias);
                string CategoryPath = DataQuery.CategoryPath(Lv2HotCategoryGUID);
                char[] PathSeparator = { '/' };
                string[] CategoryPaths = CategoryPath.Split(PathSeparator);
                ChannelAlias = DataQuery.CategoryIDToAlias(CategoryPaths[4]);
                ChannelTitle = DataQuery.GetNameByCategoryAlias(ChannelAlias);
                CurrentTrace.Text = ChannelTitle;
                CurrentCategoryName.Text = ChannelTitle;
            }
            catch (Exception AliasNull)
            {
                Response.Redirect("index.aspx");
            }
            
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
            ListType = GetListType();
            string Lv2HotGuid = DataQuery.CategoryAliasToID(Lv2HotAlias);
            switch (ListType)
            {
                case "pic":
                    Level3MainListPic.DataSource = new DAL.Article().GetArticleList(Lv2HotGuid, false, ListOrder, false);
                    Level3MainListPic.DataBind();
                    break;
                case "text":
                    Level3MainListText.DataSource = new DAL.Article().GetArticleList(Lv2HotGuid, false, ListOrder, false);
                    Level3MainListText.DataBind();
                    break;
                default:
                    Level3MainListPic.DataSource = new DAL.Article().GetArticleList(Lv2HotGuid, false, ListOrder, false);
                    Level3MainListPic.DataBind();
                    break;
            }

            //竖导航
            DataTable SubNaviDate;
            String HotAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "热点");
            SubNaviDate = DataQuery.GetSubCategories(HotAlias);
            Lv3HotNavi.DataSource = SubNaviDate;
            Lv3HotNavi.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
 
        }

        protected void Level3MainListPic_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

        }

        protected void PicBtn_Click(object sender, ImageClickEventArgs e)
        {
            string RedirectUrl;
            Lv2HotAlias = Request.QueryString["alias"].ToString();
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
                    RedirectUrl = "Level3Hot.aspx?alias=" + Lv2HotAlias + "&type=pic&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    RedirectUrl = "Level3Hot.aspx?alias=" + Lv2HotAlias + "&type=pic&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
            }
        }

        protected void TextBtn_Click(object sender, ImageClickEventArgs e)
        {
            string RedirectUrl;
            Lv2HotAlias = Request.QueryString["alias"].ToString();
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
                    RedirectUrl = "Level3Hot.aspx?alias=" + Lv2HotAlias + "&type=text&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    RedirectUrl = "Level3Hot.aspx?alias=" + Lv2HotAlias + "&type=text&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
            }
        }

        protected void RadioDesc_CheckedChanged(object sender, EventArgs e)
        {
            string RedirectUrl;
            Lv2HotAlias = Request.QueryString["alias"].ToString();
            ListType = GetListType();
            switch (RadioDesc.Checked)
            {
                case true:
                    ListOrder = "desc";
                    RedirectUrl = "Level3Hot.aspx?alias=" + Lv2HotAlias + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = "Level3Hot.aspx?alias=" + Lv2HotAlias + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = "Level3Hot.aspx?alias=" + Lv2HotAlias + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
            }
        }

        protected void RadioAsc_CheckedChanged(object sender, EventArgs e)
        {
            string RedirectUrl;
            Lv2HotAlias = Request.QueryString["alias"].ToString();
            ListType = GetListType();
            switch (RadioDesc.Checked)
            {
                case true:
                    ListOrder = "desc";
                    RedirectUrl = "Level3Hot.aspx?alias=" + Lv2HotAlias + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                case false:
                    ListOrder = "asc";
                    RedirectUrl = "Level3Hot.aspx?alias=" + Lv2HotAlias + "&type=" + ListType + "&order=" + ListOrder;
                    Response.Redirect(RedirectUrl);
                    break;
                default:
                    ListOrder = "desc";
                    RedirectUrl = "Level3Hot.aspx?alias=" + Lv2HotAlias + "&type=" + ListType + "&order=" + ListOrder;
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
    }
}
