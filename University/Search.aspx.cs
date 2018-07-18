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
    public partial class Search : System.Web.UI.Page
    {
        string sCategoryGUID = string.Empty;
        string sSearchWhere = string.Empty;
        protected string sSplitContent;
        //检索结果
        DataTable dtArt = new DataTable();
        private int iPage = 1;
        private int iPageSize = 0;
        string sUrl = "?act=n";
        protected string sOutTrStr = string.Empty;
        protected string sOutLiStr = string.Empty;
        protected string sSearchKeyWords = string.Empty;
        protected string sShow = string.Empty;
        protected string sSort = "desc";
        private int iTotalRowsCount = 0;
        private string sZTSummaryAlias = System.Configuration.ConfigurationManager.AppSettings["ZTSummaryAlias"];
        protected string sActionStr = string.Empty;
        private string sKeyWords = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
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
            sUrl = string.Format("?act={0}", Request.QueryString["act"]);
            string sAct = Request.QueryString["act"].Trim().ToLower();
            if (string.IsNullOrEmpty(sAct) || sAct == "h")
            {
                //热点
                if (string.IsNullOrEmpty(Request.QueryString["ID"])) return;
                sCategoryGUID = Request.QueryString["ID"];
                sUrl += string.Format("&ID={0}", sCategoryGUID);
                dtArt = new DAL.Article().GetArticleList(sCategoryGUID, true, sSort, iPage, out iTotalRowsCount);
                sActionStr = "热门关键词";
            }
            else if (sAct == "lv2h")//*CC*
            {
                //二级左下
                if (string.IsNullOrEmpty(Request.QueryString["ID"])) return;
                sCategoryGUID = Request.QueryString["ID"];
                sUrl += string.Format("&ID={0}", sCategoryGUID);
                dtArt = new DAL.Article().GetArticleList(sCategoryGUID, true, sSort, iPage, out iTotalRowsCount);
                string Lv2CategoryPath = DataQuery.CategoryPath(sCategoryGUID);
                string[] Lv2CategoryPathArray = Lv2CategoryPath.Split('/');
                string Lv2CategoryAlias = DataQuery.CategoryIDToAlias(Lv2CategoryPathArray[4].ToString());
                string Lv2CategoryName = DataQuery.GetNameByCategoryAlias(Lv2CategoryAlias);
                string Lv3CategoryName = DataQuery.GetNameByCategoryID(sCategoryGUID);
                TraceLv2Link.NavigateUrl = "level2.aspx?alias=" + Lv2CategoryAlias;
                TraceLv2Name.Text = Lv2CategoryName;
                sActionStr = ">&nbsp;" + Lv3CategoryName;
            }
            else if (sAct == "n")
            {
                //简单搜索
                sActionStr = "简单搜索";
                sKeyWords = Request.QueryString["keywords"];
                (this.Page.Master as MasterPager.NormalMasterPage).sSearchKeyWords = sKeyWords;
                string EnCodeKeyWords = Server.UrlEncode(sKeyWords);
                sUrl += string.Format("&keywords={0}", EnCodeKeyWords);
                sKeyWords = sKeyWords.Trim().Replace("＋", ",").Replace("　", ",").Replace("  ", ",").Replace(" ", ",").Replace("，", ",").Replace("｜", "|").Replace("、", "|").Replace("（", "%").Replace("）", "%").Replace(")", "%").Replace("(", "%").Replace("[", "%").Replace("]", "%").Replace("［", "%").Replace("］", "%").Replace(":", "%").Replace("：", "%").Replace(",,", ",").Replace("%%", "%");
                //CC 增加符号
                sSearchWhere = dal.GetArticleSearchContent(sKeyWords);
                ds = dal.GetSearchArticleList(sSearchWhere, iPage, iPageSize, sSort);
                dtArt = ds.Tables[0];
                if (!ds.Tables[1].Rows[0][0].Equals(null)) int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iTotalRowsCount); else iTotalRowsCount = 0;
            }
            else if (sAct == "a")
            {
                //高级检索
                sActionStr = "高级搜索";
            }
            else if (sAct == "zt")
            {
                sActionStr = "专题搜索";
                sKeyWords = Request.QueryString["keywords"];
                sUrl += string.Format("&keywords={0}", sKeyWords);
                sShow = "Thumb";
                sKeyWords = sKeyWords.Trim().Replace("＋", ",").Replace("　", ",").Replace("  ", ",").Replace("，", ",").Replace("｜", "|").Replace("、", "|").Replace(",,", ",");
                sSearchWhere = dal.GetSearchContentByKeyWords(sKeyWords, "CategoryName");

                ds = dal.GetSearchZTList(sSearchWhere, iPage, iPageSize, sSort);
                dtArt = ds.Tables[0];
                if (!ds.Tables[1].Rows[0][0].Equals(null)) int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iTotalRowsCount); else iTotalRowsCount = 0;
            }
            else if (sAct == "author") { 
                //作者搜索
                sActionStr = "简单搜索";
                sKeyWords = Request.QueryString["keywords"];
                sUrl += string.Format("&keywords={0}", sKeyWords);
                sKeyWords = sKeyWords.Trim().Replace("＋", ",").Replace("　", ",").Replace("  ", ",").Replace("，", ",").Replace("｜", "|").Replace("、", "|").Replace(",,", ",");

                sSearchWhere = dal.GetSearchContentByKeyWords(sKeyWords, "Author");
                ds = dal.GetSearchArticleList(sSearchWhere, iPage, iPageSize, sSort);
                dtArt = ds.Tables[0];
                if (!ds.Tables[1].Rows[0][0].Equals(null)) int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iTotalRowsCount); else iTotalRowsCount = 0;
            }
            sActionStr = string.Format("{0} {1}", sActionStr, string.IsNullOrEmpty(sKeyWords)?string.Empty:string.Format("关键词：<b>{0}</b>", sKeyWords));
            int iDescLength = (sAct=="zt")?270:63;
            string sDesc = string.Empty;
            string sContent = string.Empty;
            foreach (DataRow dr in dtArt.Rows)
            {
                string sDate = DateTime.Parse(dr["CreateTime"].ToString()).ToString("yyyy-MM-dd");
                if (sAct != "zt")
                {
                    sOutTrStr += string.Format("<tr><td class=\"MainListTitle\"><a href=\"{0}\">{1}</a></td><td>{2}</td><td>{3}分钟</td><td>{4}</td></tr>",
                        string.Format("ShowVideo.aspx?ID={0}", dr["ArticleGUID"]),
                        dr["Title"],
                        dr["Author"],
                        dr["Duration"],
                        sDate);
                }
                sOutLiStr += "<li>";
                string sImg = (sAct == "zt") ? string.Format("ShowZTImage.aspx?Title={0}", dr["CategoryName"]) : DAL.Article.GetArticleImgPath(dr["Filename"].ToString(), dr["CoursePicture"].ToString());
                if (sAct == "zt")//*CC*
                {
                    sOutLiStr += string.Format("<a href=\"{1}\" target=\"_blank\"><img class=\"SearchResultContentDetailPreview\" src=\"{0}\" /></a>", sImg, string.Format("SpecialAttention.aspx?ID={0}&Title={1}", dr["CategoryGUID"], HttpUtility.UrlEncode(dr["CategoryName"].ToString())));
                }
                else { sOutLiStr += string.Format("<a href=\"{1}\" target=\"_blank\"><img class=\"SearchResultContentDetailPreview\" src=\"{0}\" /></a>", sImg, string.Format("ShowVideo.aspx?ID={0}", dr["ArticleGUID"])); }
                string sTemp = " <div class=\"SearchResultDetailLeft\">";
                if (sAct == "zt")
                {
                    sTemp += string.Format("<div class=\"SearchResultDetailTitle\"><a href=\"{0}\" target=\"_blank\">{1}</a></div>", string.Format("SpecialAttention.aspx?ID={0}&Title={1}", dr["CategoryGUID"], HttpUtility.UrlEncode(dr["CategoryName"].ToString())), dr["CategoryName"]);
                }
                else
                {
                    sTemp += string.Format("<div class=\"SearchResultDetailTitle\"><a href=\"{0}\" target=\"_blank\">{1}</a></div>", string.Format("ShowVideo.aspx?ID={0}", dr["ArticleGUID"]), dr["Title"]);
                }
                if (sAct != "zt")
                {
                    sTemp += "<table class=\"SearchResultDetailInfo\">";
                    string sAuthor = string.Format("<a href=\"Search.aspx?act=author&keywords={0}\" target=\"_blank\">{0}</a>", dr["Author"]);
                    sTemp += string.Format("<tr><td><span>主讲人:</span> <span class=\"SearchResultInfoTxt\">{0}</span></td><td><span>职务:</span> <span class=\"SearchResultInfoTxt\">{1}</span></td></tr>", sAuthor, dr["SpeakerInfo"]);
                    sTemp += string.Format("<tr><td><span>时长:</span> <span class=\"SearchResultInfoTxt\">{0}分钟</span></td><td><span>日期:</span> <span class=\"SearchResultInfoTxt\">{1}</span></td></tr>", dr["Duration"], sDate);
                    sTemp += "</table> ";
                }
                sTemp += "</div>";
                if (sAct == "zt") {
                    string sTitle = dr["CategoryName"].ToString();
                    sContent = new DAL.CategoryDAL().GetZTSummaryFromTitle(sZTSummaryAlias, sTitle);
                }else{
                    sContent = string.IsNullOrEmpty(dr["Summary"].ToString()) ? string.Empty : DAL.Article.RemoveHtml(dr["Summary"].ToString());
                 }
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
    }
}
