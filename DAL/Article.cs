using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

namespace DAL
{
    public class Article
    {
        #region 获得一个栏目的所有课件，已排除不包含并按人工干预排序
        /// <summary>
        /// 获取栏目的所有课件，已排除不包含并按人工干预排序2014-04-22修改：数据来源于服务写出的栏目课件挂接总表
        /// </summary>
        /// <param name="sCategoryGUID">栏目GUID</param>
        /// <param name="NeedSummary">是否展示简介</param>
        /// <param name="sSort">排序，升序为asc降序为desc</param>
        /// <param name="bHasSubCategory">是否带有子栏目</param>
        /// <returns></returns>
        public DataTable GetArticleList(string sCategoryGUID, bool NeedSummary, string sSort, bool bHasSubCategory)
        {
            // *CC* 改变排序机制后 不再使用MAX(ACA.XIndexTime)
            //            string sql = @" SELECT AC.ArticleGUID,Title,[Filename],PageCount as Duration,Area as SpeakerInfo,Industry as CoursePicture,Author,Note as IndexFile,CreateTime,T.XIndexTime";
            //            if (NeedSummary) sql += @",PlainText as Summary ";
            //            sql += @" FROM ArticleCurrent AC inner join (SELECT ACA.ArticleGUID,MAX(ACA.XIndexTime) AS XIndexTime 
            //	                FROM ArticleCurrentOfCategoryAll ACA INNER JOIN Category C ON ACA.CategoryGUID =C.CategoryGUID";
            //            if (bHasSubCategory) sql += @" INNER JOIN CategoryNodePosition CNP ON C.CategoryGUID=CNP.CategoryGUID AND CNP.CategoryPath like '%'+@CategoryGUID+'%'";
            //            else sql += @" AND C.CategoryGUID=@CategoryGUID";
            //            sql += @" GROUP BY ACA.ArticleGUID) T on AC.ArticleGUID=T.ArticleGUID ORDER BY T.XIndexTime " + sSort;
            string sql = @" SELECT distinct AC.ArticleGUID,Title,[Filename],PageCount as Duration,Area as SpeakerInfo,Industry as CoursePicture,Author,AC.Note as IndexFile,AC.CreateTime,ACA.XIndexTime";
            if (NeedSummary) sql += @",PlainText as Summary ";
            if (bHasSubCategory)
            {
                //sql += @" FROM ArticleCurrent AC inner join ArticleCurrentOfCategoryAll ACA on AC.ArticleGUID=ACA.ArticleGUID INNER JOIN Category C ON ACA.CategoryGUID =C.CategoryGUID";
                //sql += @" INNER JOIN CategoryNodePosition CNP ON C.CategoryGUID=CNP.CategoryGUID AND CNP.CategoryPath like '%'+@CategoryGUID+'%'";
                sql += @" FROM ArticleCurrent AC inner join ArticleCurrentOfCategoryAll ACA on AC.ArticleGUID=ACA.ArticleGUID INNER JOIN Category C ON ACA.CategoryGUID =C.CategoryGUID join ArticleCurrentOfCategory acn on AC.ArticleGUID=acn.ArticleGUID join CategoryNodePosition cn on cn.CategoryGUID=acn.CategoryGUID";
                sql += @" INNER JOIN CategoryNodePosition CNP ON C.CategoryGUID=CNP.CategoryGUID
where CNP.CategoryPath like '%'+@CategoryGUID+'%' and cn.CategoryPath not like '%3acefefae7be40d98f7b1da38ab04d75%' and cn.CategoryPath not like '%7c98a4a59f9a4a4a861d234fa38a46c9%'";
            }
            else
            {
                //sql += @" FROM ArticleCurrent AC inner join ArticleCurrentOfCategoryAll ACA on AC.ArticleGUID=ACA.ArticleGUID where ACA.CategoryGUID=@CategoryGUID";
                sql += @" FROM ArticleCurrent AC inner join ArticleCurrentOfCategoryAll ACA on AC.ArticleGUID=ACA.ArticleGUID
join ArticleCurrentOfCategory acn on AC.ArticleGUID=acn.ArticleGUID join CategoryNodePosition cn on cn.CategoryGUID=acn.CategoryGUID
where ACA.CategoryGUID=@CategoryGUID and cn.CategoryPath not like '%3acefefae7be40d98f7b1da38ab04d75%' and cn.CategoryPath not like '%7c98a4a59f9a4a4a861d234fa38a46c9%'";
            }
            sql += @" ORDER BY ACA.XIndexTime " + sSort;
            SqlParameter[] parameter = { new SqlParameter("@CategoryGUID", sCategoryGUID) };
            DataTable dtResult = DbHelperSQL.Query(sql, 6000, parameter).Tables[0];
            return dtResult;
        }

        //*CC* 结果为空进入ALL
        public DataTable GetArticleList(string sCategoryGUID, bool NeedSummary, int iTop)
        {
            string sql = " select distinct TOP " + iTop;
            sql += " ac.ArticleGUID,Title,[Filename],PageCount as Duration,Area as SpeakerInfo,Industry as CoursePicture,Author,AC.Note as IndexFile,AC.CreateTime,aca.XIndexTime";
            if (NeedSummary) sql += @",PlainText as Summary ";
            //sql += " from ArticleCurrent ac inner join ArticleCurrentOfCategoryTop aca on ac.ArticleGUID=aca.ArticleGUID where [State]=1 and ";
            //sql += " aca.CategoryGUID=@CategoryGUID order by aca.XIndexTime desc";
            sql += " from ArticleCurrent ac inner join ArticleCurrentOfCategoryTop aca on ac.ArticleGUID=aca.ArticleGUID join ArticleCurrentOfCategory acn on ac.ArticleGUID=acn.ArticleGUID join CategoryNodePosition cn on cn.CategoryGUID=acn.CategoryGUID  where [State]=1 and ";
            sql += " cn.CategoryPath not like '%3acefefae7be40d98f7b1da38ab04d75%' and aca.CategoryGUID=@CategoryGUID order by aca.XIndexTime desc";
            SqlParameter[] parameter = { new SqlParameter("@CategoryGUID", sCategoryGUID) };
            DataTable dtResult = DbHelperSQL.Query(sql, parameter).Tables[0];
            if (dtResult.Rows.Count == 0)
            {
                sql = "";
                sql += @" select distinct TOP " + iTop;
                sql += @" AC.ArticleGUID,Title,[Filename],PageCount as Duration,Area as SpeakerInfo,Industry as CoursePicture,Author,Note as IndexFile,ac.CreateTime,ACA.XIndexTime";
                if (NeedSummary) sql += @",PlainText as Summary ";
                // *CC* 改变排序机制后 不再使用MAX(ACA.XIndexTime)
                //                sql += @" FROM ArticleCurrent AC inner join (SELECT ACA.ArticleGUID,MAX(ACA.XIndexTime) AS XIndexTime 
                //	                FROM ArticleCurrentOfCategoryAll ACA INNER JOIN Category C ON ACA.CategoryGUID =C.CategoryGUID";
                //                sql += @" AND C.CategoryGUID=@CategoryGUID";
                //                sql += @" GROUP BY ACA.ArticleGUID) T on AC.ArticleGUID=T.ArticleGUID ORDER BY T.XIndexTime desc";
                //sql += @" FROM ArticleCurrent AC inner join ArticleCurrentOfCategoryAll ACA on AC.ArticleGUID=ACA.ArticleGUID where ACA.CategoryGUID=@CategoryGUID";
                //sql += @" ORDER BY ACA.XIndexTime desc";
                sql += @" FROM ArticleCurrent AC inner join ArticleCurrentOfCategoryAll ACA on AC.ArticleGUID=ACA.ArticleGUID join ArticleCurrentOfCategory acn on ac.ArticleGUID=acn.ArticleGUID  join CategoryNodePosition cn on cn.CategoryGUID=acn.CategoryGUID  where ACA.CategoryGUID=@CategoryGUID and cn.CategoryPath not like '%3acefefae7be40d98f7b1da38ab04d75%'";
                sql += @" ORDER BY ACA.XIndexTime desc";
                dtResult = DbHelperSQL.Query(sql, 6000, parameter).Tables[0];
            }
            return dtResult;
        }

        //ALL前N
        public DataTable GetArticleListAll(string sCategoryGUID, bool NeedSummary, string sSort, bool bHasSubCategory, int iTop)
        {
            string sql = @" select distinct TOP " + iTop;
            sql += @" AC.ArticleGUID,Title,[Filename],PageCount as Duration,Area as SpeakerInfo,Industry as CoursePicture,Author,AC.Note as IndexFile,AC.CreateTime,ACA.XIndexTime";
            if (NeedSummary) sql += @",PlainText as Summary ";
            // *CC* 改变排序机制后 不再使用MAX(ACA.XIndexTime)
//            sql += @" FROM ArticleCurrent AC inner join (SELECT ACA.ArticleGUID,MAX(ACA.XIndexTime) AS XIndexTime 
//	                FROM ArticleCurrentOfCategoryAll ACA INNER JOIN Category C ON ACA.CategoryGUID =C.CategoryGUID";
//            if (bHasSubCategory) sql += @" INNER JOIN CategoryNodePosition CNP ON C.CategoryGUID=CNP.CategoryGUID AND CNP.CategoryPath like '%'+@CategoryGUID+'%'";
//            else sql += @" AND C.CategoryGUID=@CategoryGUID";
//            sql += @" GROUP BY ACA.ArticleGUID) T on AC.ArticleGUID=T.ArticleGUID ORDER BY T.XIndexTime " + sSort;
            if (bHasSubCategory)
            {
                sql += @" FROM ArticleCurrent AC inner join ArticleCurrentOfCategoryAll ACA on AC.ArticleGUID=ACA.ArticleGUID INNER JOIN Category C ON ACA.CategoryGUID =C.CategoryGUID";
                sql += @" INNER JOIN CategoryNodePosition CNP ON C.CategoryGUID=CNP.CategoryGUID AND CNP.CategoryPath like '%'+@CategoryGUID+'%'";
            }
            else
            {
                sql += @" FROM ArticleCurrent AC inner join ArticleCurrentOfCategoryAll ACA on AC.ArticleGUID=ACA.ArticleGUID where ACA.CategoryGUID=@CategoryGUID";
            }
            sql += @" ORDER BY ACA.XIndexTime " + sSort;
            SqlParameter[] parameter = { new SqlParameter("@CategoryGUID", sCategoryGUID) };
            DataTable dtResult = DbHelperSQL.Query(sql, 6000, parameter).Tables[0];
            return dtResult;
        }

        //分页读取栏目课件
        public DataTable GetArticleList(string sCategoryGUID, bool NeedSummary, string sSort, int iCurrPage, out int iTotalRowsCount)
        {
            int iPageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            DataTable dt = GetArticleList(sCategoryGUID, NeedSummary, sSort, true);
            iTotalRowsCount = dt.Rows.Count;
            int iStartRow = (iCurrPage - 1) * iPageSize;
            int iEndRow = (iStartRow + iPageSize) > iTotalRowsCount ? iTotalRowsCount : iStartRow + iPageSize;
            DataTable dtRS = dt.Clone();
            for (int i = iStartRow; i < iEndRow; i++)
            {
                dtRS.ImportRow(dt.Rows[i]);
            }
            return dtRS;
        }

        //分页读取栏目课件 HasSubCategory *CC*
        public DataTable GetArticleList(string sCategoryGUID, bool NeedSummary, string sSort, int iCurrPage, bool bHasSubCategory, out int iTotalRowsCount)
        {
            int iPageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            DataTable dt = GetArticleList(sCategoryGUID, NeedSummary, sSort, bHasSubCategory);
            iTotalRowsCount = dt.Rows.Count;
            int iStartRow = (iCurrPage - 1) * iPageSize;
            int iEndRow = (iStartRow + iPageSize) > iTotalRowsCount ? iTotalRowsCount : iStartRow + iPageSize;
            DataTable dtRS = dt.Clone();
            for (int i = iStartRow; i < iEndRow; i++)
            {
                dtRS.ImportRow(dt.Rows[i]);
            }
            return dtRS;
        }

        //EmbedPage
        public DataTable GetArticleListEmbed(string sCategoryGUID, bool NeedSummary, string sSort, int iCurrPage, out int iTotalRowsCount)
        {
            int iPageSize = 15;
            DataTable dt = GetArticleListAll(sCategoryGUID, NeedSummary, sSort, true,30);
            iTotalRowsCount = dt.Rows.Count;
            int iStartRow = (iCurrPage - 1) * iPageSize;
            int iEndRow = (iStartRow + iPageSize) > iTotalRowsCount ? iTotalRowsCount : iStartRow + iPageSize;
            DataTable dtRS = dt.Clone();
            for (int i = iStartRow; i < iEndRow; i++)
            {
                dtRS.ImportRow(dt.Rows[i]);
            }
            return dtRS;
        }

        #endregion

        /// <summary>
        /// 获取课件的入口文件
        /// </summary>
        /// <param name="sArticleGUID"></param>
        /// <returns></returns>
        public string GetArticlePath(string sArticleGUID)
        {
            string indexPath = string.Empty;
            string sql = "select FileName,Note from ArticleCurrent where ArticleGUID=@ArticleGUID";
            SqlParameter[] parameter = { new SqlParameter("@ArticleGUID", sArticleGUID) };
            DataTable dt = DbHelperSQL.Query(sql, parameter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                indexPath = dt.Rows[0][0].ToString() + "/" + dt.Rows[0][1].ToString();
            }
            return indexPath;
        }
        public static string RemoveHtml(string src)
        {
            Regex htmlReg = new Regex(@"<[^>]+>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex htmlSpaceReg = new Regex(";", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex spaceReg = new Regex(" \\;", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex styleReg = new Regex(@"<style(.*?)</style>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex scriptReg = new Regex(@"<script(.*?)</script>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            src = styleReg.Replace(src, string.Empty);
            src = scriptReg.Replace(src, string.Empty);
            src = htmlReg.Replace(src, string.Empty);
            src = htmlSpaceReg.Replace(src, " ");
            src = spaceReg.Replace(src, " ");
            src = src.Replace("&ldquo;", "“").Replace("&rdquo;", "”").Replace("&ldquo", "“").Replace("&rdquo", "”");
            src = src.Replace("&nbsp;", "").Replace("&nbsp", "");
            src = src.Replace("&mdash;", "—").Replace("&mdash", "—");
            return src.Trim();
        }
        public static string GetArticleImgPath(string sFileName, string sCoursePicture)
        {
            string picpath = string.Empty;
            string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];
            string[] pathList = strFilePath.Split('|');
            if (!string.IsNullOrEmpty(sCoursePicture))
            {
                foreach (string sPath in pathList)
                {
                    string tempPath = sPath.Split(',')[1] + "\\" + sFileName;
                    if (Directory.Exists(tempPath))
                    {
                        string[] sTemp = sCoursePicture.Split(',');
                        picpath = sPath.Split(',')[0] + "/" + sFileName + "/" + sTemp[0];
                        break;
                    }
                    else
                    {
                        picpath = "/images/kong.png";
                    }
                }
            }
            else { picpath = "/images/kong.png"; }
            return picpath;
        }
        public DataTable GetList(string sDistinctFields, string sWhere)
        {
            string sSql = string.Format(" select distinct {0} from ArticleCurrent where {1} ", sDistinctFields, sWhere);
            return DbHelperSQL.Query(sSql).Tables[0];
        }
    }
}

