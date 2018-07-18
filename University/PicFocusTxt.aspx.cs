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
    public partial class PicFocusTxt : System.Web.UI.Page
    {
        public string url;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
            string ChannelAlias = null;
            try
            {
                ChannelAlias = Request.QueryString["SubAlias"].ToString();
            }
            catch (Exception AliasNull)
            {
                Response.Redirect("index.aspx");
            }
            string ChannelGuid = DataQuery.CategoryAliasToID(ChannelAlias);
            string CategoryName = DataQuery.GetNameByCategoryAlias(ChannelAlias);
            string OtherAlias = DataQuery.GetChannelAliasByName(ConfigurationManager.AppSettings["NewsCategoriesAlias"],CategoryName);
            string OtherGuid = DataQuery.CategoryAliasToID(OtherAlias);
            DataTable TopInfo = DataQuery.ArticleQuerySummary("", "1", ChannelAlias).Tables[0];
            DataTable OtherInfo = DataQuery.ArticleQuery("", "", OtherAlias).Tables[0];

            TopPicLargeLink.NavigateUrl = "ShowVideo.aspx?ID=" + TopInfo.Rows[0]["ArticleGUID"].ToString();
            PlayLink.NavigateUrl = "ShowVideo.aspx?ID=" + TopInfo.Rows[0]["ArticleGUID"].ToString();
            TopPicLarge.ImageUrl = DataQuery.GetCoursePicPath(TopInfo.Rows[0]["ArticleGUID"].ToString(), url, "360x240.png");
            TopPicSmall.ImageUrl = DataQuery.GetCoursePicPath(TopInfo.Rows[0]["ArticleGUID"].ToString(), url, "001.jpg");
            TopTitle.Text = DataProcessing.SubstringText(TopInfo.Rows[0]["Title"].ToString(),17);
            TopSpeaker.Text = DataProcessing.SubstringText(TopInfo.Rows[0]["Author"].ToString(), 17);
            TopSpeakerInfo.Text = DataProcessing.SubstringText(TopInfo.Rows[0]["SpeakerInfo"].ToString(), 25);
            TopSpeakerInfo.ToolTip = TopInfo.Rows[0]["SpeakerInfo"].ToString();
            TopSummary.Text = DataProcessing.SubstringText(DataProcessing.NoHTML(TopInfo.Rows[0]["Summary"].ToString()),155);
            TopSummary.ToolTip = DataProcessing.NoHTML(TopInfo.Rows[0]["Summary"].ToString());
            //
            SpecialAttentionLevel2CourseList.DataSource = OtherInfo;
            SpecialAttentionLevel2CourseList.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
