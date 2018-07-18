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
    public partial class Level2Fame : System.Web.UI.Page
    {
        public string url;
        protected void Page_Load(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
            string ChannelAlias = ConfigurationManager.AppSettings["ChannelFame"];
            string ChannelTitle = DataQuery.GetNameByCategoryAlias(ChannelAlias);
            //LeftList1
            string Lv2FameLeftListAlias = DataQuery.GetChannelAliasByName(ChannelAlias,"名家谈时事");
            Lv2FameLeftLink1.NavigateUrl = "Level3Fame.aspx?alias=" +Lv2FameLeftListAlias+ "&IsChild=1";
            string Lv2FameLeftListGuid = DataQuery.CategoryAliasToID(Lv2FameLeftListAlias);
            DataTable Lv2FameLeftList1Courses = new DAL.Article().GetArticleList(Lv2FameLeftListGuid, false, 3);
            Lv2FameLeftList1.DataSource = Lv2FameLeftList1Courses;
            Lv2FameLeftList1.DataBind();
            //LeftList2
            Lv2FameLeftListAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "名家谈管理");
            Lv2FameLeftLink2.NavigateUrl = "Level3Fame.aspx?alias=" + Lv2FameLeftListAlias + "&IsChild=1";
            string Lv2FameLeftListGuid2 = DataQuery.CategoryAliasToID(Lv2FameLeftListAlias);
            DataTable Lv2FameLeftList2Courses = new DAL.Article().GetArticleList(Lv2FameLeftListGuid2, false, 3);
            Lv2FameLeftList2.DataSource = Lv2FameLeftList2Courses;
            Lv2FameLeftList2.DataBind();
            //排行
            //DataTable HotListDataSrc = DataQuery.GetHotList();
            //HotList.DataSource = HotListDataSrc;
            //HotList.DataBind();
            //MidList
            string Lv2FameMidListAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "名师推荐");
            Lv2FameMidLink.NavigateUrl = "Level3Fame.aspx?alias=" + Lv2FameLeftListAlias + "&IsChild=1";
            string Lv2FameMidListGuid = DataQuery.CategoryAliasToID(Lv2FameMidListAlias);
            DataTable Lv2FameMidListCourses = new DAL.Article().GetArticleList(Lv2FameMidListGuid, true, 5);
            Lv2FameMidList.DataSource = Lv2FameMidListCourses;
            Lv2FameMidList.DataBind();
            //RightList1
            string Lv2RightListAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "专家名录");
            Lv2FameRightLink1.NavigateUrl = "Level3Fame.aspx?alias=" + Lv2RightListAlias + "&IsChild=0";
            DataTable Lv2RightListItems = DataQuery.GetSubCategories(Lv2RightListAlias, "48");
            Lv2FameRightList1.DataSource = Lv2RightListItems;
            Lv2FameRightList1.DataBind();

        }
    }
}
