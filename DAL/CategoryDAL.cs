using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class CategoryDAL
    {
        #region 获取专题总页最新更新和精彩推荐的前几个课件
        public DataTable GetArticleIndexList(string sCategoryGUID,int iTop)
        {
            DataTable dt = new DataTable();
            string sql = " select top " + iTop + " ArticleGUID,Title,CreateTime from ( ";
            string sSearchContent = GetArticleSearchContentTop(sCategoryGUID,true);
            if (!string.IsNullOrEmpty(sSearchContent))
            {
                sql += @" select ArticleGUID,Title,CreateTime from ArticleCurrent where [State]=1 and (" + sSearchContent + ") and ContentType=''"; // *CC* and ContentType=''
                sql += @" union ";
            }
            sql += @" select ac.ArticleGUID,ac.Title,ac.CreateTime from ArticleCurrent ac inner join dbo.ArticleCurrentOfCategory acc on ac.ArticleGUID=acc.ArticleGUID and [State]=1 and acc.CategoryGUID in (select c.CategoryGUID from Category c,CategoryNodePosition p where c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '%@CategoryGUID/%') ";
            sql += @" ) t  ";
            sql += @" where t.ArticleGUID not in(select ArticleGUID from ArticleCurrentNotInCategory where CategoryGUID in (select c.CategoryGUID from Category c,CategoryNodePosition p where c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '%@CategoryGUID/%'))";
            sql += @" order by CreateTime desc";
            SqlParameter[] parameter = { new SqlParameter("@CategoryGUID", sCategoryGUID) };
            try
            {
                dt = DbHelperSQL.Query(sql,6000,parameter).Tables[0];
            }
            catch { }
            return dt;
        }


        /// <summary>
        /// 获取指定路径下一个指定名称的栏目的前几个或全部课件，简介可选
        /// </summary>
        /// <param name="sCategoryPath">指定路径</param>
        /// <param name="sCategoryName">指定名称的栏目</param>
        /// <param name="iTop">大于０时表示前几个，否则为全部</param>
        /// <param name="NeedSummary">是否带简介</param>
        /// <returns></returns>
        /// 
        public DataTable GetArticleIndexList(string sCategoryPath, string sCategoryName, int iTop, bool NeedSummary)
        {
            string sCategoryGUID = GetCategoryGUID(sCategoryPath, sCategoryName);
            if (string.IsNullOrEmpty(sCategoryGUID)) return new DataTable();
            if (iTop > 0)
                return new Article().GetArticleList(sCategoryGUID, NeedSummary, iTop);
            else
                return new Article().GetArticleList(sCategoryGUID, NeedSummary, "desc", false);
        }


        //*CC* Public 
        public string GetArticleSearchContentTop(string sCategoryGUID,bool HasChild)
        {
            string sContent = string.Empty;
            string sql = string.Empty;
            if (HasChild) sql = "select Note from Category c,CategoryNodePosition p where c.CategoryGUID=p.CategoryGUID and IsAttachmentInDB=1 and CategoryPath like '%" + sCategoryGUID + "/%'";
            else sql = "select Note from Category where CategoryGUID='" + sCategoryGUID + "' and IsAttachmentInDB=1";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                {
                    string sStr = GetArticleSearchContent(dt.Rows[i][0].ToString());
                    sContent += string.IsNullOrEmpty(sStr) ? string.Format("({0})", sStr) : " or " + string.Format("({0})", sStr);
                }
            }
            if (sContent.StartsWith(" or ")) sContent = sContent.Substring(4);
            return sContent;
        }

        private string GetCategoryGUID(string sCategoryPath, string sCategoryName)
        {
            string sCategoryGUID = string.Empty;
            string sql = "select c.CategoryGUID from Category c,CategoryNodePosition p where c.CategoryGUID=p.CategoryGUID and CategoryPath like '" + sCategoryPath + "/%' and CategoryName='" + sCategoryName + "'";
            object obj = DbHelperSQL.GetSingle(sql);
            if (obj != null) sCategoryGUID = obj.ToString();
            return sCategoryGUID;
        }
        public string GetSearchContentByKeyWords(string KeyWords,string sFieldName)
        {
            string sResult = string.Empty;
            KeyWords = KeyWords.Replace("；", ",").Replace("，", ",").Replace("：", ",");
            string[] sWords = KeyWords.Split(new char[] { ',' });
            foreach (string s in sWords) {
                sResult += string.IsNullOrEmpty(sResult) ? string.Format(" {0} like '%{1}%' ", sFieldName, s) : string.Format(" or {0} like '%{1}%'", sFieldName, s);
            }
            return string.Format("({0})", sResult);
        }

        public string GetArticleSearchContent(string sExpression)
        {
            if (string.IsNullOrEmpty(sExpression)) return string.Empty;
            sExpression = sExpression.Replace("；", ";").Replace("，", ",").Replace("：", ":");
            sExpression = sExpression.Replace("（", "(").Replace("）", ")").Replace("＆", "&").Replace("｜", "|");

            //分析表达式获取WHERE 条件 
            int iStartPosition = 0;
            int iEndPosition = 0;
            string[] sStrings = sExpression.Split(new char[] { '&', '|', '(', ')' });
            for (int i = 0; i < sStrings.Length; i++)
            {
                string sTemp = sStrings[i];
                if (!string.IsNullOrEmpty(sTemp))
                {
                    iEndPosition = iStartPosition + sTemp.Length;
                    bool bLike = true;
                    string sFiledName = string.Empty;
                    if (sTemp.StartsWith("姓名:") || sTemp.StartsWith("姓氏:"))
                    {
                        if (sTemp.StartsWith("姓氏:")) bLike = false;
                        sFiledName = "Author";
                    }
                    else if (sTemp.StartsWith("职务:")) sFiledName = "Area";
                    else if (sTemp.StartsWith("摘要:")) sFiledName = "PlainText";
                    if (sTemp.IndexOf(":") > 0) sTemp = sTemp.Substring(3);

                    string[] SearchWords = sTemp.Split(new char[] { ',' });
                    sTemp = string.Empty;
                    foreach (string sWord in SearchWords)
                    {
                        if (!string.IsNullOrEmpty(sWord))
                        {
                            if (string.IsNullOrEmpty(sFiledName))
                                sTemp += "Title like '%" + sWord + "%' OR KeyWord like '%" + sWord + "%' OR ";
                            else
                            {
                                sTemp += sFiledName + " like '";
                                if (bLike) sTemp += "%";
                                sTemp += sWord + "%' OR ";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(sTemp)) sTemp = "(" + sTemp.Substring(0, sTemp.Length - 4) + ")";

                    sTemp = sExpression.Substring(0, iStartPosition) + sTemp;
                    iStartPosition = sTemp.Length;
                    sExpression = sTemp + sExpression.Substring(iEndPosition);
                }
                iStartPosition++;
            }

            sExpression = sExpression.Replace("&", " AND ");
            sExpression = sExpression.Replace("|", " OR ");
            return sExpression;
        }

        #endregion

        /// <summary>
        /// 获取专题的路径和YIndex
        /// </summary>
        /// <param name="sCategoryGUID"></param>
        /// <returns></returns>
        public DataTable GetCategorySimpleInfo(string sCategoryGUID)
        {
            DataTable dt = new DataTable();
            string sql = " select CategoryPath,YIndex from CategoryNodePosition where CategoryGUID=@CategoryGUID";
            SqlParameter[] parameter = { new SqlParameter("@CategoryGUID", sCategoryGUID) };
            dt = DbHelperSQL.Query(sql, parameter).Tables[0];
            return dt;
        }

        /// <summary>
        ///  获取专题的路径和YIndex
        /// </summary>
        /// <returns></returns>
        public DataTable GetCategoryPathAndYIndex(string sAlias)
        {
            DataTable dt = new DataTable();
            string sql = " select CategoryPath,YIndex from CategoryNodePosition p,Category c where p.CategoryGUID=c.CategoryGUID and c.CategoryAlias=@Alias";
            SqlParameter[] parameter = { new SqlParameter("@Alias", sAlias) };
            dt = DbHelperSQL.Query(sql, parameter).Tables[0];
            return dt;
        }

        /// <summary>
        /// 专题总页导航
        /// </summary>
        /// <param name="ZTGUID"></param>
        /// <param name="sCategoryGUID"></param>
        /// <returns></returns>
        public DataTable GetZTMenu(string sAlias)
        {
            DataTable dt = new DataTable();
            dt = GetCategoryPathAndYIndex(sAlias);
            if (dt.Rows.Count == 0) return dt;
            string sCategoryPath=dt.Rows[0]["CategoryPath"].ToString();
            int iYIndex=int.Parse(dt.Rows[0]["YIndex"].ToString()) +1;

            string sql = " select c.CategoryName,c.CategoryGUID,c.CategoryAlias from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " and c.state=1 order by XIndex"; //*CC* 加栏目别名、栏目状态
            try
            {
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }

        /// <summary>
        /// 最新更新
        /// </summary>
        /// <param name="sCategoryPath"></param>
        /// <param name="iYIndex"></param>
        /// <returns></returns>
        public DataTable GetLastNewZT(string sCategoryPath,int iYIndex)
        {
            DataTable dt = new DataTable();
            string sql = " select top 1 c.CategoryName,c.CategoryGUID,c.CategoryAlias from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " order by XIndex";
            try
            {
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }

        /// <summary>
        /// 精彩推荐
        /// </summary>
        /// <param name="sCategoryPath"></param>
        /// <param name="iYIndex"></param>
        /// <returns></returns>
        public DataTable GetRecommendZT(string sCategoryPath, int iYIndex)
        {
            DataTable Result = new DataTable();
            DataTable dt = new DataTable();
            string sql = " select top 7 c.CategoryName,c.CategoryGUID,c.CategoryAlias from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " and CanComment=1 order by XIndex";
            try
            {
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch { }
            //*CC* 排除和最新重复
            DataTable Newdt = new DataTable();
            string Newsql = " select top 1 c.CategoryName,c.CategoryGUID,c.CategoryAlias from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " order by XIndex";
            try
            {
                Newdt = DbHelperSQL.Query(Newsql).Tables[0];
            }
            catch { }
            if (dt.Rows[0][1].ToString() == Newdt.Rows[0][1].ToString())
            {
                var dtSrc = from row in dt.AsEnumerable()
                               select row;
                var ResultSrc = dtSrc.Skip(1).Take(6);
                Result = ResultSrc.CopyToDataTable<DataRow>();
            }
            else
            {
                var dtSrc = from row in dt.AsEnumerable()
                           select row;
                var ResultSrc = dtSrc.Take(6);
                Result = ResultSrc.CopyToDataTable<DataRow>();
            }
            return Result;
        }
        /// <summary>
        /// *CC* 一个栏目下所有专题
        /// </summary>
        /// <param name="sCategoryPath"></param>
        /// <param name="iYIndex"></param>
        /// <param name="count"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public DataTable GetZTFromCategory(string sCategoryPath, int iYIndex, int count, string order)
        {
            DataTable dt = new DataTable();
            string sql;
            if (count == 0)
            {
                sql = "select";
            }
            else
            {
                sql = "select top " + count + " ROW_NUMBER() OVER (ORDER BY XIndex  ASC)  as Seq,";
            }
            sql += " c.CategoryName,c.CategoryGUID,c.CategoryAlias from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " order by XIndex "+ order ;
            try
            {
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }
        /// *CC* 一个栏目下所有专题有简介
        /// </summary>
        /// <param name="sCategoryPath"></param>
        /// <param name="iYIndex"></param>
        /// <param name="count"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public DataTable GetZTFromCategoryNote(string sCategoryPath, int iYIndex,string order)
        {
            DataTable dt = new DataTable();
            string sql;
            sql = " select distinct c.CategoryName,c.CategoryGUID,c.CategoryAlias,PlainText,XIndex FROM CategoryNodePosition p INNER JOIN Category c ON p.CategoryGUID = c.CategoryGUID , ArticleCurrent Where c.CategoryName = ArticleCurrent.Title and p.CategoryPath like  '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " order by XIndex " + order;
            try
            {
                dt = DbHelperSQL.Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }

        /// <summary>
        /// 热点专题
        /// </summary>
        /// <returns></returns>

        public DataTable GetHotZT()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] parameters = { 
                                            new SqlParameter("@CategoryAlias",SqlDbType.NVarChar,256)
                                            };
                string sSql = @"select top 5 c.CategoryGUID,c.CategoryName from Category c inner join CategoryNodePosition cnp on c.CategoryGUID=cnp.CategoryGUID and c.SecretLevel>=2 and cnp.CategoryPath like (select '%'+c.CategoryGUID+'%' from Category c where c.CategoryAlias=@CategoryAlias) order by c.SecretLevel";
                parameters[0].Value = System.Configuration.ConfigurationManager.AppSettings["TBGZ_Alias"];

                dt = DbHelperSQL.Query(sSql, parameters).Tables[0];
            }
            catch (Exception ex) { throw ex; }
            return dt;
        }


        /// <summary>
        /// 专题图片
        /// </summary>
        /// <param name="sCategoryGUID"></param>
        /// <param name="sTitle"></param>
        /// <param name="sTag"></param>
        /// <returns></returns>
        //public DataTable GetImage(string sZTSummaryAlias, string sTitle)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        //查询字符串 
        //        string sSql = @" select aa.Content,aa.MIMEType,aa.ContentSize from ArticleCurrentAttachment aa,ArticleCurrent a,ArticleCurrentOfCategory ac WHERE a.ArticleGUID=aa.ArticleGUID";
        //        sSql += @" AND a.ArticleGUID=ac.ArticleGUID AND a.Title=@Title AND ac.CategoryGUID=(select CategoryGUID from Category where CategoryAlias=@ZTSummaryAlias)";
        //        SqlParameter[] parameter = {
        //                                       new SqlParameter("@ZTSummaryAlias", sZTSummaryAlias),
        //                                       new SqlParameter("@Title", sTitle)
        //                                   };
        //        dt = DbHelperSQL.Query(sSql, parameter).Tables[0];
        //    }
        //    catch { }
        //    return dt;
        //}
        // *CC* ArticleCurrentAttachment CreateTime Desc
        public DataTable GetImage(string sZTSummaryAlias, string sTitle)
        {
            DataTable dt = new DataTable();
            try
            {
                //查询字符串 
                string sSql = @" select top 1 aa.Content,aa.MIMEType,aa.ContentSize,a.CreateTime from ArticleCurrentAttachment aa,ArticleCurrent a,ArticleCurrentOfCategory ac,CategoryNodePosition WHERE a.ArticleGUID=aa.ArticleGUID";
                sSql += @" AND a.ArticleGUID=ac.ArticleGUID AND a.Title=@Title AND ac.CategoryGUID = CategoryNodePosition.CategoryGUID AND CategoryNodePosition.CategoryPath like '%'+(select CategoryGUID from Category where CategoryAlias=@ZTSummaryAlias)+'%' order by aa.CreateTime Desc";
                SqlParameter[] parameter = {
                                               new SqlParameter("@ZTSummaryAlias", sZTSummaryAlias),
                                               new SqlParameter("@Title", sTitle)
                                           };
                dt = DbHelperSQL.Query(sSql, parameter).Tables[0];
            }
            catch { }
            return dt;
        }


        /// <summary>
        /// 获取专题简介
        /// </summary>
        /// <param name="sCategoryGUID"></param>
        /// <param name="sTitle"></param>
        /// <returns></returns>
        //public string GetZTSummaryFromTitle(string sZTSummaryAlias, string sTitle)
        //{
        //    string sSummary=string.Empty;
        //    try
        //    {
        //        //查询字符串 
        //        string sSql = @" select Content from ArticleCurrent a,ArticleCurrentOfCategory ac WHERE a.ArticleGUID=ac.ArticleGUID AND ac.CategoryGUID=(select CategoryGUID from Category where CategoryAlias=@ZTSummaryAlias) AND a.Title=@Title";
        //        SqlParameter[] parameter = {
        //                                       new SqlParameter("@ZTSummaryAlias", sZTSummaryAlias),
        //                                       new SqlParameter("@Title", sTitle)
        //                                   };
        //        object obj = DbHelperSQL.GetSingle(sSql, parameter);
        //        if (obj != null) sSummary = obj.ToString();
        //    }
        //    catch { }
        //    return sSummary;
        //}
        // *CC*
        public string GetZTSummaryFromTitle(string sZTSummaryAlias, string sTitle)
        {
            string sSummary = string.Empty;
            try
            {
                //查询字符串 
                string sSql = @" select top 1 Content from ArticleCurrent a,ArticleCurrentOfCategory ac,CategoryNodePosition WHERE a.ArticleGUID=ac.ArticleGUID AND ac.CategoryGUID = CategoryNodePosition.CategoryGUID AND CategoryNodePosition.CategoryPath like '%'+(select CategoryGUID from Category where CategoryAlias=@ZTSummaryAlias)+'%' AND a.Title=@Title";
                SqlParameter[] parameter = {
                                               new SqlParameter("@ZTSummaryAlias", sZTSummaryAlias),
                                               new SqlParameter("@Title", sTitle)
                                           };
                object obj = DbHelperSQL.GetSingle(sSql, parameter);
                if (obj != null) sSummary = obj.ToString();
            }
            catch { }
            sSummary = sSummary.Replace("<段落><![CDATA[", "");//*CC*
            sSummary = sSummary.Replace("]]></段落>", "");//*CC*
            return sSummary;
        }

        public string GetHotKey(string sHotKeyAlias)
        {
            string sHotKey = "<span>热门关键词：";
            try
            {
                string sSql = @" select CategoryPath,YIndex from Category c,CategoryNodePosition p WHERE c.CategoryGUID=p.CategoryGUID AND CategoryAlias=@HotKeyAlias";
                SqlParameter[] parameter = {new SqlParameter("@HotKeyAlias", sHotKeyAlias)};
                DataTable dt = DbHelperSQL.Query(sSql, parameter).Tables[0];
                int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

                sSql = @" select c.CategoryGUID,c.CategoryName from Category c,CategoryNodePosition p WHERE c.CategoryGUID=p.CategoryGUID AND p.CategoryPath like '" + dt.Rows[0][0].ToString() + "/%' AND p.YIndex=" + iYIndex + " order by XIndex";
                DataTable dt2 = DbHelperSQL.Query(sSql, parameter).Tables[0];
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sHotKey += string.Format("<a href=\"Search.aspx?act=h&ID={0}\" target=\"_blank\">{1}</a>", dt2.Rows[i][0].ToString(), dt2.Rows[i][1].ToString());
                }
            }
            catch { }
            sHotKey += "</span>";
            return sHotKey;
        }

        public string GetHotKeyLite(string sHotKeyAlias)
        {
            string sHotKey = "<span>热门关键词：";
            try
            {
                string sSql = @" select CategoryPath,YIndex from Category c,CategoryNodePosition p WHERE c.CategoryGUID=p.CategoryGUID AND CategoryAlias=@HotKeyAlias";
                SqlParameter[] parameter = { new SqlParameter("@HotKeyAlias", sHotKeyAlias) };
                DataTable dt = DbHelperSQL.Query(sSql, parameter).Tables[0];
                int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

                sSql = @" select c.CategoryGUID,c.CategoryName from Category c,CategoryNodePosition p WHERE c.CategoryGUID=p.CategoryGUID AND p.CategoryPath like '" + dt.Rows[0][0].ToString() + "/%' AND p.YIndex=" + iYIndex + " order by XIndex";
                DataTable dt2 = DbHelperSQL.Query(sSql, parameter).Tables[0];
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sHotKey += string.Format("<a href=\"SearchLite.aspx?act=h&ID={0}\" target=\"_blank\">{1}</a>", dt2.Rows[i][0].ToString(), dt2.Rows[i][1].ToString());
                }
            }
            catch { }
            sHotKey += "</span>";
            return sHotKey;
        }

        #region 通过关键词查询文档功能
        public DataSet GetAdvSearchArticleList(string sWhere, int iCurrPage, int iPageNums, string sSort,string sAlias) {
            if (string.IsNullOrEmpty(sAlias))
            {
                return GetSearchArticleList(sWhere, iCurrPage, iPageNums, sSort);
            }
            else
            {
                string sSql = " declare @Path nvarchar(max); ";
                sSql += string.Format(" select @Path=cnp.CategoryPath from Category c inner join CategoryNodePosition cnp on c.CategoryGUID=cnp.CategoryGUID where CategoryAlias='{0}'; ", sAlias);
                sSql += " with Art as( ";
                sSql += string.Format(" SELECT TOP {0} ac.ArticleGUID,Title,[Filename],PageCount as Duration,Area as SpeakerInfo,Industry as CoursePicture,Author,ac.CreateTime,PlainText as Summary ,ROW_NUMBER() Over (Order By ac.CreateTime " + sSort + ") as _RowNumber ", iCurrPage * iPageNums);
                string sForm = " from Category c inner join CategoryNodePosition cnp on c.CategoryGUID=cnp.CategoryGUID ";
                sForm += " inner join ArticleCurrentOfCategoryAll acoc on c.CategoryGUID=acoc.CategoryGUID ";//*CC* ArticleCurrentOfCategory--〉ArticleCurrentOfCategoryAll
                sForm += " inner join ArticleCurrent ac on acoc.ArticleGUID=ac.ArticleGUID ";
                sSql += sForm;
                string sTempWhere = string.Format(" where c.State<>3 and cnp.CategoryPath like @Path+'%' {0} ", string.IsNullOrEmpty(sWhere) ? string.Empty : string.Format(" and {0} ", sWhere));
                sSql += sTempWhere;
                sSql += " ) ";
                sSql += string.Format(" SELECT * FROM Art WHERE _RowNumber > {0} ;", (iCurrPage - 1) * iPageNums);
                sSql += string.Format("  select count(1) {0} {1}", sForm, sTempWhere);
                return DbHelperSQL.Query(sSql, 6000);
            }
            return new DataSet();
        }
        public DataSet GetSearchArticleList(string sWhere, int iCurrPage, int iPageNums,string sSort) {

            string sSql = " WITH Art AS( ";
            sSql += string.Format(" SELECT TOP {0} ArticleGUID,Title,[Filename],PageCount as Duration,Area as SpeakerInfo,Industry as CoursePicture,Author,CreateTime,PlainText as Summary ,ROW_NUMBER() Over (Order By CreateTime " + sSort + ") as _RowNumber ", iCurrPage * iPageNums);
            sSql += " FROM ArticleCurrent ac";
            if (!string.IsNullOrEmpty(sWhere)) {
                sWhere = " where ContentType='' and " + sWhere;
                sSql += sWhere;
            }
            sSql += ")";
            sSql += string.Format(" SELECT * FROM Art WHERE _RowNumber > {0} ;", (iCurrPage - 1)*iPageNums);
            sSql += string.Format(" select count(1)  FROM ArticleCurrent ac {0} ",sWhere);
            return DbHelperSQL.Query(sSql);
        }
        //分页显示通过关键词搜索专题列表 
        public DataSet GetSearchZTList(string sWhere, int iCurrPage, int iPageSize, string sSort) {
            string sSql = " declare @iYindex int; ";
            sSql += " declare @Path varchar(max); ";
            sSql += " select @iYindex=cnp.YIndex+2 ,@Path=cnp.CategoryPath ";
            sSql += string.Format(" from Category c inner join CategoryNodePosition cnp on c.CategoryGUID=cnp.CategoryGUID where c.CategoryAlias='{0}'; ", System.Configuration.ConfigurationManager.AppSettings["TBGZ_Alias"]);
            sSql += " with Art as( ";
            sSql += string.Format(" select top {0} c.*,ROW_NUMBER() Over (Order By CategoryName {1}) as _RowNumber ", iCurrPage * iPageSize, sSort);
            sSql += " from Category c inner join CategoryNodePosition cnp on c.CategoryGUID=cnp.CategoryGUID ";
            sSql += " where  cnp.CategoryPath like @Path+'%' and cnp.YIndex=@iYindex ";
            if (!string.IsNullOrEmpty(sWhere)) {
                sSql += string.Format(" and {0}", sWhere);
            }
            sSql += " order by c.CategoryName asc ";
            sSql += " ) ";
            sSql += string.Format(" select * from Art where _RowNumber>{0} ;", (iCurrPage - 1) * iPageSize);
            sSql += string.Format(" select count(1)  from Category c inner join CategoryNodePosition cnp on c.CategoryGUID=cnp.CategoryGUID where cnp.CategoryPath like @Path+'%' and cnp.YIndex=@iYindex {0} ;",string.IsNullOrEmpty(sWhere)?string.Empty:string.Format("and {0}",sWhere));
            return DbHelperSQL.Query(sSql,6000);
        }
        #endregion
        public DataTable GetCategoryData(string sAlias)
        {
            string sSql = " declare @YIndex int;";
            sSql += " declare @Path nvarchar(max);";
            sSql += string.Format(" select @Path=CategoryPath,@YIndex=YIndex from Category c inner join CategoryNodePosition cnp on c.CategoryGUID=cnp.CategoryGUID where c.CategoryAlias='{0}'", sAlias);
            sSql += " select c.CategoryGUID,c.CategoryName,c.CategoryAlias from Category c inner join CategoryNodePosition cnp on c.CategoryGUID=cnp.CategoryGUID ";
            sSql += " where c.State<>3 and cnp.YIndex=@YIndex+1 and cnp.CategoryPath like @Path+'%' ";
            sSql += " order by cnp.XIndex ;";
            return DbHelperSQL.Query(sSql, 6000).Tables[0];
        }
    }
}
