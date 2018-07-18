using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace colleges
{
    public partial class PicFocusPicLite : System.Web.UI.Page
    {
        public string url;
        public string SpeakerInfoUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            string CourseKeyWord;
            string CourseSperker;
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
            string CourseId = Request.QueryString["ID"].ToString();
            if (CourseId == null) Response.Redirect("HomeLite.aspx");
            string CoursrQueryStr = "SELECT Top 1 ArticleGUID,State,Title,Area,Author,PlainText,Filename,CreateTime,KeyWord FROM ArticleCurrent Where ArticleGUID='" + CourseId + "'";
            string SpeakerQueryStr = "Select Top 1 PropertyValue From ArticleCurrentPropertyExt Where PropertyAlias = 'speakerresume' And ArticleGUID='" + CourseId + "'";
            string connectingString = ConfigurationManager.ConnectionStrings["zjspccmConnectionString"].ToString();
            String SpeakerFile;
            using (SqlConnection connection =
                       new SqlConnection(connectingString))
            {
                SqlCommand command = new SqlCommand(CoursrQueryStr, connection);
                connection.Open();
                SqlDataReader CourseReder = command.ExecuteReader();
                CourseReder.Read();
                TopPicLargeLink.NavigateUrl = "ShowVideo.aspx?ID=" + CourseReder[0].ToString();
                TopPlayLink.NavigateUrl = "ShowVideo.aspx?ID=" + CourseReder[0].ToString();
                TopPicLarge.ImageUrl = DataQuery.GetCoursePicPath(CourseReder[0].ToString(), url, "360x240.png");
                TopPicSmall.ImageUrl = DataQuery.GetCoursePicPath(CourseReder[0].ToString(), url, "001.jpg");
                SpeakerInfoUrl = DataQuery.GetCoursePicPath(CourseReder[0].ToString(), url, "data/jsData.js");
                TopTitle.Text = DataProcessing.SubstringText(CourseReder[2].ToString(), 17);
                TopSpeaker.Text = DataProcessing.SubstringText(CourseReder[4].ToString(), 17);
                TopSpeakerInfo.Text = DataProcessing.SubstringText(CourseReder[3].ToString(), 40);
                TopSpeakerInfo.ToolTip = CourseReder[3].ToString();
                TopSummary.Text = DataProcessing.SubstringText(DataProcessing.NoHTML(CourseReder[5].ToString()), 210);
                TopSummary.ToolTip = DataProcessing.NoHTML(CourseReder[5].ToString());
                CourseSperker = CourseReder[4].ToString();
                CourseKeyWord = CourseReder[8].ToString();
            }
            //主讲人简介
            //using (SqlConnection connection =  new SqlConnection(connectingString))
            //{
            //    connection.Open();
            //    SqlCommand SpeakerCommand = new SqlCommand(SpeakerQueryStr, connection);
            //    SqlDataReader SpeakerReder = SpeakerCommand.ExecuteReader();
            //    if (SpeakerReder.Read())
            //    {
            //        SpeakerFile = SpeakerReder[0].ToString();
            //        string SpeakerFilePath = DataQuery.GetCoursePicPath(CourseId, url, SpeakerFile);
            //        XmlDocument xmlDoc = new XmlDocument();
            //        try
            //        {
            //            xmlDoc.Load(SpeakerFilePath);
            //            XmlNode root = xmlDoc.SelectSingleNode("Speaker");
            //            //string SperkerSummary = DataProcessing.NoHTML((root.SelectSingleNode("Information1")).InnerXml);
            //            string SperkerSummary = (root.SelectSingleNode("Information1")).InnerXml;
            //            SperkerSummary = SperkerSummary.Replace("&lt;", "<");
            //            SperkerSummary = SperkerSummary.Replace("&gt;", ">");
            //            SperkerSummary = DataProcessing.NoHTML(SperkerSummary);
            //            SperkerSummary = SperkerSummary.Replace("![CDATA[", "");
            //            SperkerSummary = SperkerSummary.Replace("]]", "");
            //            SperkerSummary = SperkerSummary.Replace("<", "");
            //            SperkerSummary = SperkerSummary.Replace(">", "");
            //            TopSpeakerSummary.Text = DataProcessing.SubstringText(SperkerSummary,45);
            //        }
            //        catch (Exception eXML)
            //        {

            //        }
            //    }

            //}
            // 相关列表
            string SearchContent = "Author like '%" + CourseSperker + "%'";
            string RelatedQueryStr = "SELECT Top 10 ArticleGUID,Title,Author,Area as SpeakerInfo,Filename,CreateTime FROM ArticleCurrent Where " + SearchContent;
            using (SqlConnection connection =
                       new SqlConnection(connectingString))
            {
                SqlCommand command = new SqlCommand(RelatedQueryStr, connection);
                connection.Open();
                SqlDataReader CourseReder = command.ExecuteReader();
                DataTable CourseTable = new DataTable();
                CourseTable.Load(CourseReder);
                CoursesWithSperker.DataSource = CourseTable;
                CoursesWithSperker.DataBind();
            }
            if (CourseKeyWord != null)
            {
                CourseKeyWord = CourseKeyWord.Replace(" ", ",");
                SearchContent = new DAL.CategoryDAL().GetArticleSearchContent(CourseKeyWord);
                RelatedQueryStr = "SELECT Top 10 ArticleGUID,Title,Author,Area as SpeakerInfo,Filename,CreateTime FROM ArticleCurrent Where " + SearchContent;
                using (SqlConnection connection =
                       new SqlConnection(connectingString))
                {
                    SqlCommand command = new SqlCommand(RelatedQueryStr, connection);
                    connection.Open();
                    SqlDataReader CourseReder = command.ExecuteReader();
                    DataTable CourseTable = new DataTable();
                    CourseTable.Load(CourseReder);
                    CoursesWithTitle.DataSource = CourseTable;
                    CoursesWithTitle.DataBind();
                }
            }
        }
    }
}