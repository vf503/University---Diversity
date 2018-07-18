using System;
using System.IO;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace colleges
{
    public partial class ShowVideoBeta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ID"])) return;
            string sArticleGUID = Request.QueryString["ID"];
            string indexPath = GetArticlePathBeta(sArticleGUID);
            string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];

            string[] pathList = strFilePath.Split('|');
            for (int i = 0; i < pathList.Length; i++)
            {
                if (string.IsNullOrEmpty(pathList[i])) continue;
                string sPath = pathList[i];
                if (!sPath.EndsWith("\\")) sPath = sPath + "\\";
                if (File.Exists(sPath.Split(',')[1] + indexPath))
                {
                    indexPath = sPath.Split(',')[0] + "/" + indexPath;
                    break;
                }
            }
            if (string.IsNullOrEmpty(indexPath))
                vframe.InnerHtml = "很抱歉，视频文件不存在！";
            else
            {
                this.VideoShowFrame.Attributes["src"] = indexPath;
                //Response.Redirect(indexPath);
            }
        }
        public string GetArticlePathBeta(string sArticleGUID)
        {
            string indexPath = string.Empty;
            string sql = "select FileName,Note from ArticleCurrent where ArticleGUID=@ArticleGUID";
            SqlParameter[] parameter = { new SqlParameter("@ArticleGUID", sArticleGUID) };
            DataTable dt = DAL.DbHelperSQL.Query(sql, parameter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                indexPath = dt.Rows[0][0].ToString() + "/beta.html";
            }
            return indexPath;
        }
    }
}
