using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

namespace colleges
{
    public partial class Level2News : System.Web.UI.Page
    {
        public string url;
        protected void Page_Load(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
            DataTable NewsList1Video = DataQuery.GetArticleListFromAlias("gxchannel9_topics_1_1", false, "desc", 0, 4);
            DataTable NewsList1Other = DataQuery.GetArticleListFromAlias("gxchannel9_topics_1_1", false, "desc", 4, 8);
            if (NewsList1Video.Rows.Count > 0)
            {
                Level2NewsList1Video.DataSource = NewsList1Video; 
                Level2NewsList1Video.DataBind();
                Level2NewsList1Other.DataSource = NewsList1Other;
                Level2NewsList1Other.DataBind();
            }
            //
            DataTable NewsList2Video = DataQuery.GetArticleListFromAlias("gxchannel9_topics_6_1", false, "desc", 0, 4);
            DataTable NewsList2Other = DataQuery.GetArticleListFromAlias("gxchannel9_topics_6_1", false, "desc", 4, 8);
            if (NewsList2Video.Rows.Count > 0)
            {
                Level2NewsList2Video.DataSource = NewsList1Video;
                Level2NewsList2Video.DataBind();
                Level2NewsList2Other.DataSource = NewsList1Other;
                Level2NewsList2Other.DataBind();
            }
            //
            string FameListAlias = DataQuery.GetChannelAliasByName("gxchannel10", "名家谈时事");
            DataTable Lv2NewsListLeftCourses = DataQuery.GetArticleListFromAlias(FameListAlias, false, "desc", 0, 5);
            Lv2NewsListLeft.DataSource = Lv2NewsListLeftCourses;
            Lv2NewsListLeft.DataBind();
            //改别名
            DataTable Lv2NewsList3BottomInfo = DataQuery.GetArticleListFromAlias("gxchannel9_topics_1_1", false, "desc", 0, 3);
            Level2NewsList3Bottom.DataSource = Lv2NewsList3BottomInfo;
            Level2NewsList3Bottom.DataBind();
            DataTable Lv2NewsList4BottomInfo = DataQuery.GetArticleListFromAlias("gxchannel9_topics_1_1", false, "desc", 0, 3);
            Level2NewsList4Bottom.DataSource = Lv2NewsList4BottomInfo;
            Level2NewsList4Bottom.DataBind();
            //
            DataTable Lv2NewsList5Info = DataQuery.GetArticleListFromAlias("gxchannel9_topics_3_1", false, "desc", 0, 5);
            Level2NewsList5.DataSource = Lv2NewsList5Info;
            Level2NewsList5.DataBind();
            Level2NewsList5Image.ImageUrl = DataQuery.GetCoursePicPath(Lv2NewsList5Info.Rows[0][0].ToString(), url, "001.jpg");
            DataTable Lv2NewsList6Info = DataQuery.GetArticleListFromAlias("gxchannel9_topics_5_1", false, "desc", 0, 5);
            Level2NewsList6.DataSource = Lv2NewsList6Info;
            Level2NewsList6.DataBind();
            Level2NewsList6Image.ImageUrl = DataQuery.GetCoursePicPath(Lv2NewsList6Info.Rows[0][0].ToString(), url, "001.jpg");
            DataTable Lv2NewsList7Info = DataQuery.GetArticleListFromAlias("gxchannel9_topics_4_1", false, "desc", 0, 5);
            Level2NewsList7.DataSource = Lv2NewsList7Info;
            Level2NewsList7.DataBind();
            Level2NewsList7Image.ImageUrl = DataQuery.GetCoursePicPath(Lv2NewsList7Info.Rows[0][0].ToString(), url, "001.jpg");
            //改别名
            DataTable Level2NewsList8Info = DataQuery.GetArticleListFromAlias("gxchannel9_topics_1_1", false, "desc", 0, 3);
            Level2NewsList8.DataSource = Level2NewsList8Info;
            Level2NewsList8.DataBind();
            //改别名
            DataTable Level2NewsList9Info = DataQuery.GetArticleListFromAlias("gxchannel9_topics_1_1", false, "desc", 0, 5);
            Level2NewsList9.DataSource = Level2NewsList9Info;
            Level2NewsList9.DataBind();
        }
    }
}
