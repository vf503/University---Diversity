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
using System.IO;
using System.Xml;

namespace colleges
{
    public partial class Ajax : System.Web.UI.Page
    {
        protected string sZTSummaryAlias = ConfigurationManager.AppSettings["ZTSummaryAlias"];
        protected string ZTMenu = string.Empty;
        string sCategoryPath = string.Empty;
        int iYIndex = 5;
        string sCategoryGUID =string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["ID"]))
            {
                sCategoryGUID = Request.Form["ID"];

                DataTable dt = new DAL.CategoryDAL().GetCategorySimpleInfo(sCategoryGUID);
                if (dt.Rows.Count == 0) return;
                sCategoryPath = dt.Rows[0]["CategoryPath"].ToString();
                iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString());
            }
            string sDiv = string.Empty;
            string sValue = string.Empty;
            switch (Request.Params["act"].ToString())
            {
                //最新更新
                case "LastNew":
                    sDiv = GetLastNew();
                    break;
                //精彩推荐
                case "Recommend":
                    sDiv = GetRecommend();
                    break;
                //热点专题
                case "Hot":
                    sDiv = GetHot();
                    break;
                //专题第二页上部分
                case "Level2Top":
                    sDiv = GetLevel2Top();
                    break;
                //专题第二页名家
                case "Level2Author":
                    sDiv = GetLevel2Author();
                    break;
                //专题第二页最新动态
                case "Level2New":
                    sDiv = GetLevel2New();
                    break;
                //专题第二页核心观点
                case "Level2Key":
                    sDiv = GetLevel2Key();
                    break;
                //专题第二页其他
                case "Level2Other":
                    sDiv = GetLevel2Other();
                    sDiv = "[" + sDiv + "]";
                    break;
                case "GetArea":
                    sValue = Request.Params["Area"];
                    if (!string.IsNullOrEmpty(sValue))
                    {
                        sValue = sValue.Replace("'", "");
                        sDiv=GetAreaJson(sValue);
                    }
                    else { 
                        sDiv="[]";
                    }
                    break;
                case "GetAuthor":
                    sValue = Request.Params["Author"];
                    if (!string.IsNullOrEmpty(sValue))
                    {
                        sValue = sValue.Replace("'", "");
                        sDiv = GetAuthorJson(sValue);
                    }
                    else
                    {
                        sDiv = "[]";
                    }
                    break;
                case "Subject":
                    sDiv = GetSubjectClass(Request.Params["Alias"]);
                    break;
                case "Major":
                    sDiv = GetMajorClass(Request.Params["Alias"]);
                    break;
            }
            Response.Write(sDiv);
        }
        #region 学科分类
        private string GetSubjectClass(string sAlias) {
            if (string.IsNullOrEmpty(sAlias)) {
                sAlias = System.Configuration.ConfigurationManager.AppSettings["SUBJECT"];
            }
            string sStr = string.Empty;
            DataTable dt = new DAL.CategoryDAL().GetCategoryData(sAlias);
            if (dt.Rows.Count > 0)
            {
                sStr = "{" + string.Format("Alias:\"{0}\",Name:\"{1}\"", string.Empty, "全部") + "}";
                foreach (DataRow dr in dt.Rows)
                {
                    sStr += ",{" + string.Format("Alias:\"{0}\",Name:\"{1}\"", dr["CategoryAlias"], dr["CategoryName"]) + "}";
                }
            }
            return string.Format("[{0}]", sStr);
        }
        //专业
        private string GetMajorClass(string sAlias)
        {
            if (string.IsNullOrEmpty(sAlias))
            {
                sAlias = System.Configuration.ConfigurationManager.AppSettings["MAJOR"];
            }
            DataTable dt = new DAL.CategoryDAL().GetCategoryData(sAlias);
            string sStr = string.Empty;
            if (dt.Rows.Count > 0)
            {
                sStr = "{" + string.Format("Alias:\"{0}\",Name:\"{1}\"", string.Empty, "全部") + "}";
                foreach (DataRow dr in dt.Rows)
                {
                    sStr += ",{" + string.Format("Alias:\"{0}\",Name:\"{1}\"", dr["CategoryAlias"], dr["CategoryName"]) + "}";
                }
            }
            return string.Format("[{0}]", sStr);
        }
        
        #endregion
        private string GetAuthorJson(string sLikeWords)
        {
            string sStr = string.Empty;
            try
            {
                DataTable dt = new DAL.Article().GetList("Author", string.Format("Author<>'' {0}", string.IsNullOrEmpty(sLikeWords) ? string.Empty : string.Format(" and Author like '%{0}%' order by Author", sLikeWords)));
                
                foreach (DataRow dr in dt.Rows)
                {
                    sStr += ",{" + string.Format("Author:\"{0}\"", dr["Author"]) + "}";
                }
            }
            catch { sStr = string.Empty; }
            return string.Format("[{0}]", string.IsNullOrEmpty(sStr)?string.Empty:sStr.Substring(1));
        }
        private string GetAreaJson( string sLikeWords) {
            string sStr = string.Empty;
            try
            {
                DataTable dt = new DAL.Article().GetList("Area", string.Format("Area<>'' {0}", string.IsNullOrEmpty(sLikeWords) ? string.Empty : string.Format(" and Area like '%{0}%' order by Area", sLikeWords)));
                
                foreach (DataRow dr in dt.Rows)
                {
                    sStr += ",{" + string.Format("Area:\"{0}\"", dr["Area"]) + "}";
                }
            }
            catch { sStr = string.Empty; }
            return string.Format("[{0}]", string.IsNullOrEmpty(sStr)?string.Empty:sStr.Substring(1));
        }

        private string GetLastNew()
        {
            int iDescLength = 110;
            DataTable dt = new DAL.CategoryDAL().GetLastNewZT(sCategoryPath, iYIndex + 1);
            if (dt == null || dt.Rows.Count == 0) return string.Empty ;
            DataTable dtArticle = new DAL.Article().GetArticleList(dt.Rows[0]["CategoryGUID"].ToString(), false, 3);
            string sTitle = dt.Rows[0]["CategoryName"].ToString();
            string sContent = new DAL.CategoryDAL().GetZTSummaryFromTitle(sZTSummaryAlias, sTitle);
            
            
            sContent = DAL.Article.RemoveHtml(sContent);
            string sDesc = sContent;
            if (sContent.Length > iDescLength)
            {
                sContent = sContent.Replace(" ", "");//*CC*
                sContent = sContent.Substring(0, iDescLength);
                sDesc = sDesc.Remove(0, iDescLength);
                if (sDesc.StartsWith(">")) sDesc.Remove(0, 1);
            }

            string sDiv = "<div class=\"SpecialAttentionNewLeft\">";
            sDiv += string.Format("<a href=\"SpecialAttention.aspx?ID={0}&Title={1}\" title=\"{2}\" target=\"_blank\">", dt.Rows[0]["CategoryGUID"].ToString(), HttpUtility.UrlEncode(sTitle), sTitle);
            sDiv += "<img width=\"276\" height=\"195\" src=\"ShowZTImage.aspx?Title=" + HttpUtility.UrlEncode(sTitle) + "\" alt=\"\" border=\"0\" /></a>";
            sDiv += "</div><div class=\"SpecialAttentionNewRight\"><div class=\"SpecialAttentionNewTitle\">";
            sDiv += string.Format("<a href=\"SpecialAttention.aspx?ID={0}&Title={1}\" target=\"_blank\">{2}</a>", dt.Rows[0]["CategoryGUID"].ToString(), HttpUtility.UrlEncode(sTitle), DataProcessing.SubstringText(sTitle,19)); //*CC*
            sDiv += "</div>";
            sDiv += string.Format("<div class=\"ZTSummary\" data-tooltip=\"{0}\" data-placement=\"bottom\">", string.IsNullOrEmpty(sDesc)?string.Empty:string.Format("<div class='divTooltip'>{0}</div>",sDesc));
            sDiv += string.Format("<a href=\"SpecialAttention.aspx?ID={0}&Title={1}\" target=\"_blank\">", dt.Rows[0]["CategoryGUID"].ToString(),HttpUtility.UrlEncode(sTitle)) + sContent + "</a>";
            sDiv += "</div><table class=\"SpecialAttentionNewTable\" style=\"table-layout:fixed;\" width=\"355\">";
            if (dtArticle.Rows.Count > 0)
            {
                for (int i = 0; i < dtArticle.Rows.Count; i++)
                {
                    sDiv += "<tr><td class=\"SpecialAttentionNewTableTitle SpecialAttentionNewTableTitleZTTop\" style=\"overflow:hidden;text-overflow:ellipsis;white-space:nowrap;\">";
                    sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\">{2}</a></td>", dtArticle.Rows[i]["ArticleGUID"].ToString(), dtArticle.Rows[i]["Title"].ToString(), DataProcessing.SubstringText(dtArticle.Rows[i]["Title"].ToString(), 18));//*CC*
                    sDiv += "<td align=\"right\">" + DateTime.Parse(dtArticle.Rows[i]["CreateTime"].ToString()).ToString("yyyy-MM-dd") + "</td></tr>";
                }
            }
            sDiv += "</table></div>";
            return sDiv;
        }

        private string GetRecommend()
        {
            string sDiv = string.Empty;
            int iDescLength = 108;
            DataTable dt = new DAL.CategoryDAL().GetRecommendZT(sCategoryPath, iYIndex + 1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dtArticle = new DAL.Article().GetArticleList(dt.Rows[i]["CategoryGUID"].ToString(), false, 4);
                string sTitle = dt.Rows[i]["CategoryName"].ToString();
                string sContent = new DAL.CategoryDAL().GetZTSummaryFromTitle(sZTSummaryAlias, sTitle);
                
                
                sContent = DAL.Article.RemoveHtml(sContent);
                string sDesc = sContent;
                if (sContent.Length > iDescLength)
                {
                    sContent = sContent.Substring(0, iDescLength);
                    sDesc = sDesc.Remove(0, iDescLength);
                    if (sDesc.StartsWith(">")) sDesc.Remove(0, 1);
                }
                else {
                    sDesc = string.Empty;
                }

                sDiv += "<div class=\"SpecialAttentionLeftCommendItem\">";
                sDiv += "<div class=\"SpecialAttentionNewLeft\">";
                sDiv += string.Format("<a href=\"SpecialAttention.aspx?ID={0}&Title={1}\" title=\"{2}\" target=\"_blank\">", dt.Rows[i]["CategoryGUID"].ToString(), HttpUtility.UrlEncode(sTitle), sTitle);
                sDiv += "<img width=\"158\" height=\"118\" src=\"ShowZTImage.aspx?Title=" + HttpUtility.UrlEncode(sTitle) + "\" alt=\"\" border=\"0\" /></a>";
                sDiv += "</div>";
                sDiv += "<div class=\"SpecialAttentionNewRightL\">";
                sDiv += "<div class=\"SpecialAttentionNewTitleL\">";
                sDiv += string.Format("<a href=\"SpecialAttention.aspx?ID={0}&Title={1}\" target=\"_blank\">{2}</a></div>", dt.Rows[i]["CategoryGUID"].ToString(),HttpUtility.UrlEncode(sTitle), sTitle);
                sDiv += string.Format("<div class=\"ZTSummary\" data-tooltip=\"{0}\" data-placement=\"bottom\">", string.IsNullOrEmpty(sDesc) ? string.Empty : string.Format("<div class='divTooltip'>{0}</div>", sDesc));
                sDiv += string.Format("<a href=\"SpecialAttention.aspx?ID={0}&Title={1}\" target=\"_blank\">", dt.Rows[i]["CategoryGUID"].ToString(), HttpUtility.UrlEncode(sTitle)) + sContent + "</a>";
                sDiv += "</div><table class=\"SpecialAttentionNewTableL\" style=\"table-layout:fixed;\"  width=\"465\">";
                if (dtArticle.Rows.Count > 0)
                {
                    for (int j = 0; j < dtArticle.Rows.Count; j++)
                    {
                        if (j % 2 == 0) sDiv += "<tr>";
                        sDiv += "<td class=\"SpecialAttentionNewTableTitleL\" style=\"overflow:hidden;text-overflow:ellipsis;white-space:nowrap;\"  width=\"48%\">";
                        sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\">{1}</a></td>", dtArticle.Rows[j]["ArticleGUID"].ToString(), dtArticle.Rows[j]["Title"].ToString());
                        if (j % 2 == 1) sDiv += "</tr>";
                    }
                }
                sDiv += "</table></div></div>";
            }
            return sDiv;
        }

        private string GetHot()
        {
            string sDiv = string.Empty;
            try
            {
                int iDescLength = 66;
                DataTable dt = new DAL.CategoryDAL().GetHotZT();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sTitle = dt.Rows[i]["CategoryName"].ToString();
                    string sFixLenTitle = sTitle;
                    if (sFixLenTitle.Length > 17) sFixLenTitle = sFixLenTitle.Substring(0, 16) + "...";
                    string sContent = new DAL.CategoryDAL().GetZTSummaryFromTitle(sZTSummaryAlias, sTitle);


                    sContent = DAL.Article.RemoveHtml(sContent);
                    string sDesc = sContent;
                    if (sContent.Length > iDescLength)
                    {
                        sContent = sContent.Substring(0, iDescLength);
                        sDesc = sDesc.Remove(0, iDescLength);
                        if (sDesc.StartsWith(">")) sDesc.Remove(0, 1);
                    }
                    sDiv += "<div class=\"SpecialAttentionRightHotItem\">";
                    sDiv += "<div class=\"SpecialAttentionRightHotItemTitle\">";
                    sDiv += string.Format("<a href=\"SpecialAttention.aspx?ID={0}&Title={1}\" title=\"{2}\" target=\"_blank\">{3}</a>", dt.Rows[i]["CategoryGUID"].ToString(), HttpUtility.UrlEncode(sTitle), sTitle, sFixLenTitle);
                    sDiv += "</div>";
                    sDiv += "<div class=\"SpecialAttentionRightHotItemLeft\">";
                    sDiv += string.Format("<a href=\"SpecialAttention.aspx?ID={0}&Title={1}\" title=\"{2}\" target=\"_blank\">", dt.Rows[i]["CategoryGUID"].ToString(), HttpUtility.UrlEncode(sTitle), sTitle);
                    sDiv += "<img width=\"118\" height=\"95\" src=\"ShowZTImage.aspx?Title=" + HttpUtility.UrlEncode(sTitle) + "\" alt=\"\" border=\"0\" /></a>";
                    sDiv += "</div>";
                    sDiv += string.Format("<div class=\"ZTSummary\" style=\"width:168px;margin:0 auto;\" data-tooltip=\"{0}\" data-placement=\"bottom\">", string.IsNullOrEmpty(sDesc) ? string.Empty : string.Format("<div class='divTooltip'>{0}</div>", sDesc));
                    sDiv += "<a href=\"ShowSummary.aspx?Title=" + HttpUtility.UrlEncode(sTitle) + "\" target=\"_blank\">" + sContent + "</a>";
                    sDiv += "</div></div>";
                }
            }
            catch { return string.Empty; }
            return sDiv;
        }

        private string GetLevel2Top()
        {
            //专题标题
            string sTitle = Request.Form["Title"];
            //专题简介
            int iDescLength = 144;
            string sContent = new DAL.CategoryDAL().GetZTSummaryFromTitle(sZTSummaryAlias, sTitle);
            
            sContent = DAL.Article.RemoveHtml(sContent);
            string sDesc = sContent;
            if (sContent.Length > iDescLength) { 
                sContent = sContent.Substring(0, iDescLength);
                sDesc = sDesc.Remove(0, iDescLength);
                if (sDesc.StartsWith(">")) sDesc=sDesc.Remove(0, 1);
            }
            //核心观点
            DataTable dt1 = new DAL.CategoryDAL().GetArticleIndexList(sCategoryPath, "核心观点", 2,false);
            //最新动态
            DataTable dt2 = new DAL.CategoryDAL().GetArticleIndexList(sCategoryPath, "最新动态", 2,false);

            string sDiv = "";
            sDiv += string.Format("<div class=\"ZTLevel2ContentRightCenterTitle\">{0}</div>", sTitle);
            sDiv += string.Format("<div class=\"ZTSummary\"  data-tooltip=\"{0}\" data-placement=\"bottom\">{1}</div>", string.IsNullOrEmpty(sDesc) ? string.Empty : string.Format("<div class='divTooltip'>{0}</div>", sDesc), sContent);
            sDiv += "    <table class=\"SpecialAttentionNewTableXL\">";
            sDiv += "      <tr><td class=\"SpecialAttentionNewTableLineTitle\">核心观点</td>";
            sDiv += "          <td class=\"ZTNewTableTitleXL\">";
            if (dt1.Rows.Count > 0)
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\">{2}</a>", dt1.Rows[0]["ArticleGUID"].ToString(), dt1.Rows[0]["Title"].ToString(), DataProcessing.SubstringText(dt1.Rows[0]["Title"].ToString(),35));
            sDiv += "</td>";
            sDiv += "</tr><tr><td></td><td class=\"ZTNewTableTitleXL\">";
            if (dt1.Rows.Count > 1)
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\">{2}</a>", dt1.Rows[1]["ArticleGUID"].ToString(), dt1.Rows[1]["Title"].ToString(), DataProcessing.SubstringText(dt1.Rows[1]["Title"].ToString(), 35));
            sDiv += "</td>";
            sDiv += "</tr><tr><td class=\"SpecialAttentionNewTableXLSpace\"></td></tr><tr><td class=\"SpecialAttentionNewTableLineTitle\">最新动态</td>";
            sDiv += "          <td class=\"ZTNewTableTitleXL\">";
            if (dt2.Rows.Count > 0)
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\">{2}</a>", dt2.Rows[0]["ArticleGUID"].ToString(), dt2.Rows[0]["Title"].ToString(), DataProcessing.SubstringText(dt2.Rows[0]["Title"].ToString(), 35));
            sDiv += "</td>";
            sDiv += "</tr><tr><td></td><td class=\"ZTNewTableTitleXL\">";
            if (dt2.Rows.Count > 1)
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\">{2}</a>", dt2.Rows[1]["ArticleGUID"].ToString(), dt2.Rows[1]["Title"].ToString(), DataProcessing.SubstringText(dt2.Rows[1]["Title"].ToString(), 35));
            sDiv += "</td></tr></table>";
            return sDiv;
       }

        //private string GetLevel2Author()
        //{ 
        //    //名家
        //    string sDiv =string.Empty;
        //    DataTable dt = new DAL.CategoryDAL().GetArticleIndexList(sCategoryPath, "名家", 10,false);

        //    string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];
        //    string[] pathList = strFilePath.Split('|');

        //    for (int i=0;i<dt.Rows.Count;i++)
        //    {
        //        string picpath = string.Empty;
        //        sDiv += "<div class=\"SpecialAttentionFameItem\">";
        //        if (!string.IsNullOrEmpty(dt.Rows[i]["coursepicture"].ToString()))
        //        {
        //            foreach (string sPath in pathList)
        //            {
        //                string tempPath = sPath.Split(',')[1] + "\\" + dt.Rows[i]["Filename"].ToString();
        //                if (Directory.Exists(tempPath))
        //                {
        //                    picpath = sPath.Split(',')[0] + "/" + dt.Rows[i]["Filename"].ToString() + "/" + dt.Rows[i]["coursepicture"].ToString().Split(',')[0];
        //                    break;
        //                }
        //            }
        //        }
        //        sDiv += string.Format("<img src=\"{0}\" width=\"173px\" height=\"210px\">", picpath);
        //        sDiv += "<div class=\"ZTFameInfo\">";
        //        sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{2}\" target=\"_blank\">{1}&nbsp;{2}</a>", dt.Rows[i]["ArticleGUID"].ToString(), dt.Rows[i]["Author"].ToString(), dt.Rows[i]["Title"].ToString());
        //        sDiv += "</div></div>";
        //    }
        //    return sDiv;
        //}
        //**CC**
        private string GetLevel2Author()
        {
            //名家
            string sDiv = string.Empty;
            string sContent = string.Empty;
            string sDesc = string.Empty;
            int iDescLength = 98;
            DataTable dt = new DAL.CategoryDAL().GetArticleIndexList(sCategoryPath, "名家", 6, true);

            string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];
            string[] pathList = strFilePath.Split('|');

            sDiv += "<table border=\"0\" style=\"width:100%;table-layout:fixed;\">";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 2 == 0) sDiv += "<tr>";
                sDiv += "<td>";
                string picpath = string.Empty;
                sDiv += string.Format("<div class=\"SpecialAttentionLv2ViewpointItem {0}\">", (i >= dt.Rows.Count - 2) ? "noborder" : string.Empty);
                if (!string.IsNullOrEmpty(dt.Rows[i]["coursepicture"].ToString()))
                {
                    foreach (string sPath in pathList)
                    {
                        string tempPath = sPath.Split(',')[1] + "\\" + dt.Rows[i]["Filename"].ToString();
                        if (Directory.Exists(tempPath))
                        {
                            picpath = sPath.Split(',')[0] + "/" + dt.Rows[i]["Filename"].ToString() + "/" + dt.Rows[i]["coursepicture"].ToString().Split(',')[1];
                            break;
                        }
                    }
                }
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\"><img src=\"{2}\" width=\"160px\" height=\"120px\" class=\"PicNoBorder\"></a>", dt.Rows[i]["ArticleGUID"].ToString(), dt.Rows[i]["Title"].ToString(), picpath);
                sDiv += "<div class=\"SpecialAttentionLv2ViewpointItemInfo\"><div class=\"ZTLv2ViewpointItemTitle\">";
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{2}\" target=\"_blank\">{1}&nbsp;{2}</a>", dt.Rows[i]["ArticleGUID"].ToString(), dt.Rows[i]["Author"].ToString(), dt.Rows[i]["Title"].ToString());
                sDiv += "</div>";
                sContent = DAL.Article.RemoveHtml(dt.Rows[i]["Summary"].ToString());
                if (sContent.Length > iDescLength)
                {
                    sDesc = sContent.Remove(0, iDescLength);
                    sContent = sContent.Substring(0, iDescLength);
                }
                else
                {
                    sDesc = string.Empty;
                }
                sDiv += string.Format("<div class=\"SpecialAttentionLv2ViewpointItemText\" data-tooltip=\"{0}\" data-placement=\"bottom\">{1}</div>", string.IsNullOrEmpty(sDesc) ? string.Empty : string.Format("<div class='divTooltip'>{0}</div>", sDesc), sContent);
                sDiv += "</div></div>";
                sDiv += "</td>";
                if (i % 2 == 1) sDiv += "</tr>";
            }
            if (!sDiv.EndsWith("</tr>")) sDiv += "</tr>";
            sDiv += "</table>";
            return sDiv;
        }

        //private string GetLevel2New()
        //{
        //    //最新动态
        //    int iDescLength = 360;
        //    string sDiv = string.Empty;
        //    string sDesc = string.Empty;
        //    DataTable dt = new DAL.CategoryDAL().GetArticleIndexList(sCategoryPath, "最新动态", 3,true);
        //    sDiv = "<div class=\"SpecialAttentionLv2NewLeft\">";
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sDiv += "<div class=\"SpecialAttentionLv2NewItem\">";
        //        sDiv += "<div class=\"SpecialAttentionLv2NewTitle\">";
        //        sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\">{1}</a>", dt.Rows[i]["ArticleGUID"].ToString(), dt.Rows[i]["Title"].ToString());
        //        sDiv += "</div>";
        //        string sContent = DAL.Article.RemoveHtml(dt.Rows[i]["Summary"].ToString());
        //        if (sContent.Length > iDescLength) {
        //            sDesc = sContent.Remove(0, iDescLength);
        //            sContent = sContent.Substring(0, iDescLength);
        //        }
        //        sDiv += string.Format("<div class=\"ZTSummary\"  data-tooltip=\"{0}\" data-placement=\"bottom\">{1}</div>", string.IsNullOrEmpty(sDesc) ? string.Empty : string.Format("<div class='divTooltip'>{0}</div>", sDesc), sContent);
        //        sDiv += "</div>";
        //    }
        //    sDiv += "</div>";

        //    sDiv += "<div class=\"SpecialAttentionLv2NewRight\">";

        //    string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];
        //    string[] pathList = strFilePath.Split('|');

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (i >= 2) break;
        //        string picpath = string.Empty;
        //        sDiv += "<div class=\"SpecialAttentionLv2NewRightItem\">";
        //        if (!string.IsNullOrEmpty(dt.Rows[i]["coursepicture"].ToString()))
        //        {
        //            foreach (string sPath in pathList)
        //            {
        //                string tempPath = sPath.Split(',')[1] + "\\" + dt.Rows[i]["Filename"].ToString();
        //                if (Directory.Exists(tempPath))
        //                {
        //                    string[] sTemp = dt.Rows[i]["coursepicture"].ToString().Split(',');
        //                    picpath = sPath.Split(',')[0] + "/" + dt.Rows[i]["Filename"].ToString() + "/" + sTemp[sTemp.Length-1];
        //                    break;
        //                }
        //            }
        //        }
        //        sDiv += string.Format("<img src=\"{0}\" width=\"288px\" height=\"187px\">", picpath);
        //        sDiv += "<div class=\"SpecialAttentionLv2NewRightItemTitle\">";
        //        sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{2}\" target=\"_blank\">{1}&nbsp;{2}</a>", dt.Rows[i]["ArticleGUID"].ToString(), dt.Rows[i]["Author"].ToString(), dt.Rows[i]["Title"].ToString());
        //        sDiv += "</div>";
        //    }
        //    sDiv += "</div>";

        //    return sDiv;
        //}
        //CC
        private string GetLevel2New()
        {
            //最新动态
            int iDescLength = 98;
            string sDiv = string.Empty;
            string sDesc = string.Empty;
            DataTable dt = new DAL.CategoryDAL().GetArticleIndexList(sCategoryPath, "最新动态", 9, true);
            sDiv = "<div class=\"SpecialAttentionLv2NewLeft\">";
            for (int i = 3; i < dt.Rows.Count; i++)
            {
                sDiv += "<div class=\"SpecialAttentionLv2NewItem\">";
                sDiv += "<div class=\"SpecialAttentionLv2NewTitle\">";
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\">{1}</a>", dt.Rows[i]["ArticleGUID"].ToString(), dt.Rows[i]["Title"].ToString());
                sDiv += "</div>";
                string sContent = DAL.Article.RemoveHtml(dt.Rows[i]["Summary"].ToString());
                if (sContent.Length > iDescLength)
                {
                    sDesc = sContent.Remove(0, iDescLength);
                    sContent = sContent.Substring(0, iDescLength);
                }
                sDiv += string.Format("<div class=\"ZTSummary\"  data-tooltip=\"{0}\" data-placement=\"bottom\">{1}</div>", string.IsNullOrEmpty(sDesc) ? string.Empty : string.Format("<div class='divTooltip'>{0}</div>", sDesc), sContent);
                sDiv += "</div>";
            }
            sDiv += "</div>";

            sDiv += "<div class=\"SpecialAttentionLv2NewRight\">";

            string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];
            string[] pathList = strFilePath.Split('|');

            for (int i = 0; i < 3; i++)
            {
                string picpath = string.Empty;
                sDiv += "<div class=\"SpecialAttentionLv2NewRightItem\">";
                if (!string.IsNullOrEmpty(dt.Rows[i]["coursepicture"].ToString()))
                {
                    foreach (string sPath in pathList)
                    {
                        string tempPath = sPath.Split(',')[1] + "\\" + dt.Rows[i]["Filename"].ToString();
                        if (Directory.Exists(tempPath))
                        {
                            string[] sTemp = dt.Rows[i]["coursepicture"].ToString().Split(',');
                            picpath = sPath.Split(',')[0] + "/" + dt.Rows[i]["Filename"].ToString() + "/" + sTemp[sTemp.Length - 1];
                            break;
                        }
                    }
                }
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\"><img src=\"{2}\" width=\"160px\" height=\"120px\" class=\"PicNoBorder\"></a>", dt.Rows[i]["ArticleGUID"].ToString(), dt.Rows[i]["Title"].ToString(), picpath);
                sDiv += "<div class=\"SpecialAttentionLv2NewRightItemTitle\">";
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\">{2}</a>", dt.Rows[i]["ArticleGUID"].ToString(), dt.Rows[i]["Title"].ToString(), DataProcessing.SubstringText(dt.Rows[i]["Title"].ToString(),17));
                sDiv += "</div>";
            }
            sDiv += "</div>";

            return sDiv;
        }

        private string GetLevel2Key()
        {
            //核心观点
            string sDiv = string.Empty;
            string sContent=string.Empty;
            string sDesc=string.Empty;
            int iDescLength=98;
            DataTable dt = new DAL.CategoryDAL().GetArticleIndexList(sCategoryPath, "核心观点",0,true);

            string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];
            string[] pathList = strFilePath.Split('|');

            sDiv += "<table border=\"0\" style=\"width:100%;table-layout:fixed;\">";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 2 == 0) sDiv += "<tr>";
                sDiv += "<td>";
                string picpath = string.Empty;
                sDiv += string.Format("<div class=\"SpecialAttentionLv2ViewpointItem {0}\">",(i>=dt.Rows.Count-2)?"noborder":string.Empty);
                if (!string.IsNullOrEmpty(dt.Rows[i]["coursepicture"].ToString()))
                {
                    foreach (string sPath in pathList)
                    {
                        string tempPath = sPath.Split(',')[1] + "\\" + dt.Rows[i]["Filename"].ToString();
                        if (Directory.Exists(tempPath))
                        {
                            picpath = sPath.Split(',')[0] + "/" + dt.Rows[i]["Filename"].ToString() + "/" + dt.Rows[i]["coursepicture"].ToString().Split(',')[1];
                            break;
                        }
                    }
                }
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{1}\" target=\"_blank\"><img src=\"{2}\" width=\"160px\" height=\"120px\" class=\"PicNoBorder\"></a>", dt.Rows[i]["ArticleGUID"].ToString(),dt.Rows[i]["Title"].ToString(), picpath);
                sDiv += "<div class=\"SpecialAttentionLv2ViewpointItemInfo\"><div class=\"ZTLv2ViewpointItemTitle\">";
                sDiv += string.Format("<a href=\"ShowVideo.aspx?ID={0}\" title=\"{2}\" target=\"_blank\">{1}&nbsp;{2}</a>", dt.Rows[i]["ArticleGUID"].ToString(), dt.Rows[i]["Author"].ToString(), dt.Rows[i]["Title"].ToString());
                sDiv += "</div>";
                sContent=DAL.Article.RemoveHtml(dt.Rows[i]["Summary"].ToString());
                if (sContent.Length > iDescLength)
                {
                    sDesc = sContent.Remove(0, iDescLength);
                    sContent = sContent.Substring(0, iDescLength);
                }
                else {
                    sDesc = string.Empty;
                }
                sDiv += string.Format("<div class=\"SpecialAttentionLv2ViewpointItemText\" data-tooltip=\"{0}\" data-placement=\"bottom\">{1}</div>",string.IsNullOrEmpty(sDesc) ? string.Empty : string.Format("<div class='divTooltip'>{0}</div>", sDesc), sContent);
                sDiv+="</div></div>";
                sDiv += "</td>";
                if (i % 2 == 1) sDiv += "</tr>";
            }
            if (!sDiv.EndsWith("</tr>")) sDiv += "</tr>";
            sDiv += "</table>";
           return sDiv;
        }

        private string GetLevel2Other()
        {
            //其他
            string sDiv = string.Empty;
            DataTable dt = new DAL.CategoryDAL().GetArticleIndexList(sCategoryPath, "相关内容", 0, false);
            foreach (DataRow dr in dt.Rows)
            {
                sDiv += ",{" + string.Format("ArticleGUID:\"{0}\",Title:\"{1}\",Author:\"{2}\",SpeakerInfo:\"{3}\",Duration:\"{4}\",CreateTime:\"{5}\"",
                    dr["ArticleGUID"],
                    dr["Title"],
                    dr["Author"],
                    dr["SpeakerInfo"],
                    dr["Duration"],
                    DateTime.Parse(dr["CreateTime"].ToString()).ToString("yyyy-MM-dd")
                    ) + "}";
            }
            if (sDiv.Length > 0) sDiv = sDiv.Substring(1);
            return sDiv;
        }
    }
}
