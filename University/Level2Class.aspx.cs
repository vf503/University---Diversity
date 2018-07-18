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
    public partial class Level2Class : System.Web.UI.Page
    {
        public string url;
        protected void Page_Load(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
            string ChannelAlias = ConfigurationManager.AppSettings["ChannelClass"];
            string ChannelTitle = DataQuery.GetNameByCategoryAlias(ChannelAlias);
            //LeftList1
            string Lv2ClassLeftListAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "国内985大学");
            Lv2ClassLeftLink1.NavigateUrl = "Level3Class.aspx?alias=" + Lv2ClassLeftListAlias + "&IsChild=0";
            DataTable Lv2ClassLeftList1Items = DataQuery.GetSubCategories(Lv2ClassLeftListAlias, "6");
            Lv2ClassLeftList1.DataSource = Lv2ClassLeftList1Items;
            Lv2ClassLeftList1.DataBind();
            //LeftList2
            Lv2ClassLeftListAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "国外大学");
            Lv2ClassLeftLink2.NavigateUrl = "Level3Class.aspx?alias=" + Lv2ClassLeftListAlias + "&IsChild=0";
            DataTable Lv2ClassLeftList2Items = DataQuery.GetSubCategories(Lv2ClassLeftListAlias, "6");
            Lv2ClassLeftList2.DataSource = Lv2ClassLeftList2Items;
            Lv2ClassLeftList2.DataBind();
            //排行
            //DataTable HotListDataSrc = DataQuery.GetHotList();
            //HotList.DataSource = HotListDataSrc;
            //HotList.DataBind();
            //MidList
            string Lv2ClassMidListAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "精品课程");
            Lv2ClassMidLink.NavigateUrl = "Level3Class.aspx?alias=" + Lv2ClassMidListAlias + "&IsChild=1";
            DataTable Lv2FameMidListCourses = DataQuery.GetArticleListFromAlias(Lv2ClassMidListAlias, true, "desc", 0, 5);
            Lv2ClassMidList.DataSource = Lv2FameMidListCourses;
            Lv2ClassMidList.DataBind();
            //
            String CommendAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "推荐");
            DataTable CommendCourses = DataQuery.GetArticleListFromAlias(CommendAlias, true, "desc", 0, 12);
            CommendList.DataSource = CommendCourses;
            CommendList.DataBind();
        }
    }
}
