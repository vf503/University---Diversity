using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

namespace colleges
{
    public partial class AdvanceSearch : System.Web.UI.Page
    {
        string sCategoryGUID = string.Empty;
        string sSearchWhere = string.Empty;
        protected string sSplitContent;
        //检索结果
        DataTable dtArt = new DataTable();
        private int iPage = 1;
        private int iPageSize = 0;
        string sUrl = "?";
        protected string sOutTrStr = string.Empty;
        protected string sOutLiStr = string.Empty;
        protected string sSearchKeyWords = string.Empty;
        protected string sShow = string.Empty;
        protected string sSort = "desc";
        private int iTotalRowsCount = 0;
        private string sZTSummaryAlias = System.Configuration.ConfigurationManager.AppSettings["ZTSummaryAlias"];
        protected string sActionStr = string.Empty;
        protected string sAlias = string.Empty;
        protected string sAlias1 = string.Empty;
        protected string sAlias2 = string.Empty;
        protected string sTempAlias=string.Empty;
        protected string sArea = string.Empty;
        protected string sAuthor = string.Empty;
        protected string sStartDate = string.Empty;
        protected string sEndDate = string.Empty;
        protected string sKeyWords = string.Empty;
        protected int sSelectType = 0;
        protected string a1, a2, a = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            InitForm();
            iPageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            //显示方式
            sShow = string.IsNullOrEmpty(Request.QueryString["show"]) ? System.Configuration.ConfigurationManager.AppSettings["DefaultSearchDisplay"] : Request.QueryString["show"];
            DataSet ds = new DataSet();
            DAL.CategoryDAL dal = new DAL.CategoryDAL();
            //排序方式
            sSort = string.IsNullOrEmpty(Request.QueryString["s"]) ? System.Configuration.ConfigurationManager.AppSettings["DefaultSearchSort"] : Request.QueryString["s"];
            if (string.IsNullOrEmpty(sSort)) sSort = " asc ";

            //当前页面
            int.TryParse(string.IsNullOrEmpty(Request.QueryString["PageNo"]) ? "0" : Request.QueryString["PageNo"], out iPage);
            if (iPage < 1) iPage = 1;

            //分页l链接路径
           
            //高级搜索
            sActionStr = "高级搜索";

            if (string.IsNullOrEmpty(Request.QueryString["SelectType"])) return;
            sSelectType = int.Parse(Request.QueryString["SelectType"]);
            sUrl = string.Format("?SelectType={0}",sSelectType); 

            //获取参数
            sAlias = Request.QueryString["Alias"];
            sAlias1 = Request.QueryString["Alias1"];
            sAlias2 = Request.QueryString["Alias2"];
            if (!string.IsNullOrEmpty(sAlias2))
            {
                sTempAlias = sAlias2;
            }
            else if (!string.IsNullOrEmpty(sAlias1))
            {
                sTempAlias = sAlias1;
            }
            else if (!string.IsNullOrEmpty(sAlias))
            {
                sTempAlias = sAlias;
            }
            else {
                sTempAlias = string.Empty;
            }

            sArea = Request.QueryString["Area"];
            sAuthor = Request.QueryString["Author"];
            sKeyWords = Request.QueryString["keywords"];
            sStartDate = Request.QueryString["StartDate"];
            sEndDate = Request.QueryString["EndDate"];
            //组合参数
            sUrl += string.Format("&Alias={0}&Alias1={1}&Alias2={2}&Area={3}&Author={4}&keywords={5}&StartDate={6}&EndDate={7}",
                sAlias,
                sAlias1,
                sAlias2,
                sArea,
                Server.UrlEncode(sAuthor),
                Server.UrlEncode(sKeyWords),
                sStartDate,
                sEndDate);

            if (!string.IsNullOrEmpty(sKeyWords))
            {
                sKeyWords = sKeyWords.Trim().Replace("＋", ",").Replace("　", ",").Replace("  ", ",").Replace("，", ",").Replace("｜", "|").Replace("、", "|").Replace(",,", ",");
            }
            sSearchWhere = dal.GetArticleSearchContent(sKeyWords);

            string sAreaWhere = string.IsNullOrEmpty(sArea) ? string.Empty : GetWhere(sArea, "Area");
            string sAuthorWhere = GetWhere(sAuthor, "Author");
            string sDateWhere = GetWhere(sStartDate, sEndDate, "ac.CreateTime");
            string sWhere = string.IsNullOrEmpty(sAreaWhere) ? string.Empty : sAreaWhere;
            if (!string.IsNullOrEmpty(sAuthorWhere)) {
                sWhere = string.Format(" {0} {1} ", string.IsNullOrEmpty(sWhere) ? string.Empty : string.Format("{0} and ", sWhere), sAuthorWhere);
            }
            if (!string.IsNullOrEmpty(sDateWhere))
            {
                sWhere = string.Format(" {0} {1} ", string.IsNullOrEmpty(sWhere) ? string.Empty : string.Format("{0} and ", sWhere), sDateWhere);
            }
            if (!string.IsNullOrEmpty(sSearchWhere))
            {
                sWhere = string.Format(" {0} {1} ", string.IsNullOrEmpty(sWhere) ? string.Empty : string.Format("{0} and ", sWhere), sSearchWhere);
            }

            ds = dal.GetAdvSearchArticleList(sWhere, iPage, iPageSize, sSort, sTempAlias);
            dtArt = ds.Tables[0];
            if (!ds.Tables[1].Rows[0][0].Equals(null)) int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iTotalRowsCount); else iTotalRowsCount = 0;
           
            sActionStr = string.Format("{0} {1}", sActionStr, string.IsNullOrEmpty(sKeyWords)?string.Empty:string.Format("关键词：<b>{0}</b>", sKeyWords));
            int iDescLength = 128;
            string sDesc = string.Empty;
            string sContent = string.Empty;
            foreach (DataRow dr in dtArt.Rows)
            {
                string sDate = DateTime.Parse(dr["CreateTime"].ToString()).ToString("yyyy-MM-dd");
                sOutTrStr += string.Format("<tr><td class=\"MainListTitle\"><a href=\"{0}\" target=\"_blank\">{1}</a></td><td>{2}</td><td>{3}分钟</td><td>{4}</td></tr>",
                        string.Format("ShowVideo.aspx?ID={0}", dr["ArticleGUID"]),
                        dr["Title"],
                        dr["Author"],
                        dr["Duration"],
                        sDate);
                sOutLiStr += "<li>";
                string sImg = DAL.Article.GetArticleImgPath(dr["Filename"].ToString(), dr["CoursePicture"].ToString());
                sOutLiStr += string.Format("<a href=\"{1}\" target=\"_blank\"><img class=\"SearchResultContentDetailPreview\" src=\"{0}\" /></a>", sImg,string.Format("ShowVideo.aspx?ID={0}", dr["ArticleGUID"]));
                string sTemp = " <div class=\"SearchResultDetailLeft\">";
                sTemp += string.Format("<div class=\"SearchResultDetailTitle\"><a href=\"{0}\">{1}</a></div>", string.Format("ShowVideo.aspx?ID={0}", dr["ArticleGUID"]), dr["Title"]);
                sTemp += "<table class=\"SearchResultDetailInfo\">";
                string sTempAuthor = string.Format("<a href=\"Search.aspx?act=author&keywords={0}\" target=\"_blank\">{0}</a>", dr["Author"]);
                sTemp += string.Format("<tr><td><span>主讲人:</span> <span class=\"SearchResultInfoTxt\">{0}</span></td><td><span>职务:</span> <span class=\"SearchResultInfoTxt\">{1}</span></td></tr>", sTempAuthor, dr["SpeakerInfo"]);
                sTemp += string.Format("<tr><td><span>时长:</span> <span class=\"SearchResultInfoTxt\">{0}分钟</span></td><td><span>日期:</span> <span class=\"SearchResultInfoTxt\">{1}</span></td></tr>", dr["Duration"], sDate);
                sTemp += "</table> ";
                sTemp += "</div>";
                sContent = string.IsNullOrEmpty(dr["Summary"].ToString()) ? string.Empty : DAL.Article.RemoveHtml(dr["Summary"].ToString());
                sContent = DAL.Article.RemoveHtml(sContent);
                if (sContent.Length > iDescLength)
                {
                    sDesc = sContent.Remove(0, iDescLength);
                    sContent = sContent.Substring(0, iDescLength);
                }
                else {
                    sDesc = string.Empty;
                }
                sTemp += string.Format("<div class=\"SearchResultDetailSummary\" data-tooltip=\"{0}\" data-placement=\"bottom\">简介：<span>{1}</span></div>", string.IsNullOrEmpty(sDesc) ? string.Empty : string.Format("<div class='divTooltip'>{0}</div>", sDesc), sContent);
                
                sOutLiStr += string.Format("<div class=\"SearchResultContentDetailInfo\">{0}</div>", sTemp);
                sOutLiStr += "</li>";
            }
            sSplitContent = GetSplitHtml(iPage, iTotalRowsCount, iPageSize, sUrl);
        }
        #region 分页
        //分页
        protected string GetSplitHtml(int iPageNo, int outTotalRows, int iPageSize, string sPath)
        {
            int iTotalPages;
            StringBuilder sb = new StringBuilder();
            StringBuilder optionSb = new StringBuilder();
            string optionsFormatStr = "<option value=\"{0}\" {2}>{1}</option>";
            string sHref = string.Format("{0}&PageNo={1}", sPath, "{0}");
            try
            {
                iTotalPages = (0 == outTotalRows % iPageSize) ? outTotalRows / iPageSize : outTotalRows / iPageSize + 1;
            }
            catch
            {
                iTotalPages = 0;
            }

            sb.AppendFormat("<span><a href=\"{0}\" class=\"next_link\" target=\"_self\">{1}</a>　　", string.Format(sHref, 1), "首页", (iTotalPages > 0) ? iTotalPages : 1);
            sb.AppendFormat("<a href=\"{0}\" class=\"next_link\"  target=\"_self\">{1}</a>　　", string.Format(sHref, ((iPageNo - 1) > 0) ? iPageNo - 1 : 1), "上一页");
            for (int i = 1; i <= iTotalPages; i++)
            {
                optionSb.AppendFormat(optionsFormatStr, string.Format(sHref, i), i, (i == iPageNo) ? " selected " : "");
            }
            sb.AppendFormat("<a href=\"{0}\"  class=\"next_link\" target=\"_self\">{1}</a>　　", string.Format(sHref, ((iPageNo + 1) < iTotalPages) ? iPageNo + 1 : (iTotalPages > 0) ? iTotalPages : 1), "下一页");
            sb.AppendFormat("<a href=\"{0}\"  class=\"next_link\" target=\"_self\">{1}</a>　　", string.Format(sHref, (iTotalPages > 0) ? iTotalPages : 1), "末页");
            sb.AppendFormat("</span>第 {0} 页 / 共 {1} 页　　共 {2} 条　", iPageNo, iTotalPages, outTotalRows);
            sb.AppendFormat(" <label>跳转到第<select   onchange=\"window.location.href=this.value\">{0}</select>页 </label>", optionSb);

            return sb.ToString();
        }
        #endregion
        private void InitForm() {
            //string sSql = " select distinct Author from ArticleCurrent  where Author <>''; ";
            //sSql += " select distinct Area from ArticleCurrent where Area <>''; ";
            //DataSet ds = DAL.DbHelperSQL.Query(sSql, 6000);
            //DataTable dtAuthor = ds.Tables[0];
            //DataTable dtArea = ds.Tables[1];

        }
        private string GetWhere(string sStartDate,string sEndDate, string sFieldName)
        {
            if ((string.IsNullOrEmpty(sStartDate) && string.IsNullOrEmpty(sEndDate)) || string.IsNullOrEmpty(sFieldName)) return string.Empty;
            string sRs = string.Empty;
            if (!string.IsNullOrEmpty(sStartDate) && string.IsNullOrEmpty(sEndDate)) {
                sRs = string.Format(" {0} >='{1}'", sFieldName, sStartDate);
            }
            else if (!string.IsNullOrEmpty(sStartDate) && !string.IsNullOrEmpty(sEndDate)) {
                sRs = string.Format(" {0} between '{1}' and '{2}' ", sFieldName, sStartDate,sEndDate);
            }
            else if (string.IsNullOrEmpty(sStartDate) && !string.IsNullOrEmpty(sEndDate)) {
                sRs = string.Format(" {0} <= '{1}'", sFieldName, sEndDate);
            }
            if (!string.IsNullOrEmpty(sRs)) sRs = string.Format("({0})", sRs);
            return sRs;
        }
        private string GetWhere(string sValue, string sFieldName) {
            if (string.IsNullOrEmpty(sValue) || string.IsNullOrEmpty(sFieldName)) return string.Empty;
            string[] sArr = sValue.Split(new char[] { ',', ' ','，','　' });
            string sRs = string.Empty;
            foreach (string s in sArr) {
                if (string.IsNullOrEmpty(s)) continue;
                sRs += string.IsNullOrEmpty(sRs)?string.Format(" {0} like '%{1}%' ", sFieldName, s):string.Format(" or {0} like '%{1}%'", sFieldName, s);
            }
            if (!string.IsNullOrEmpty(sRs)) sRs = string.Format("({0})", sRs);
            return sRs;
        }
    }
}
