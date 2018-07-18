using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace colleges
{
    public class DataQuery
    {
        // *自制* 获取非子集栏目课件集合
        public static DataTable GetArticleIndexList(string sCategoryGUID, string order)
        {
            DataSet ds = new DataSet();
            string sql = "select ArticleGUID,Title,Filename,Author,Duration,SpeakerInfo,CreateTime from ( ";
            string sSearchContent = new DAL.CategoryDAL().GetArticleSearchContentTop(sCategoryGUID, true);
            if (!string.IsNullOrEmpty(sSearchContent))
            {
                sql += @"select ArticleGUID,Title,Filename,Author,PageCount as Duration,Area as SpeakerInfo,CreateTime from ArticleCurrent where [State]=1 and (" + sSearchContent + ") and ContentType='' ";
                sql += @" union ";
            }
            sql += @" select ac.ArticleGUID,ac.Title,ac.Filename,ac.Author,ac.PageCount as Duration,ac.Area as SpeakerInfo,ac.CreateTime from ArticleCurrent ac inner join dbo.ArticleCurrentOfCategory acc on ac.ArticleGUID=acc.ArticleGUID and [State]=1 and acc.CategoryGUID in (select c.CategoryGUID from Category c,CategoryNodePosition p where c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '%" + sCategoryGUID + "/%') ";
            sql += @" ) t  ";
            sql += @" where t.ArticleGUID not in(select ArticleGUID from ArticleCurrentNotInCategory where CategoryGUID in (select c.CategoryGUID from Category c,CategoryNodePosition p where c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '%"+ sCategoryGUID +"/%'))";
            sql += @" order by CreateTime " + order;
            SelectRows(ds, sql);
            return ds.Tables[0];
        }
        public static DataTable GetArticleIndexList(string sCategoryGUID,string count, string order)
        {
            DataSet ds = new DataSet();
            string sql = "select top "+count+" ArticleGUID,Title,Filename,Author,Duration,SpeakerInfo,CreateTime from ( ";
            string sSearchContent = new DAL.CategoryDAL().GetArticleSearchContentTop(sCategoryGUID, true);
            if (!string.IsNullOrEmpty(sSearchContent))
            {
                sql += @"select ArticleGUID,Title,Filename,Author,PageCount as Duration,Area as SpeakerInfo,CreateTime from ArticleCurrent where [State]=1 and (" + sSearchContent + ") and ContentType='' ";
                sql += @" union ";
            }
            sql += @" select ac.ArticleGUID,ac.Title,ac.Filename,ac.Author,ac.PageCount as Duration,ac.Area as SpeakerInfo,ac.CreateTime from ArticleCurrent ac inner join dbo.ArticleCurrentOfCategory acc on ac.ArticleGUID=acc.ArticleGUID and [State]=1 and acc.CategoryGUID in (select c.CategoryGUID from Category c,CategoryNodePosition p where c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '%" + sCategoryGUID + "/%') ";
            sql += @" ) t  ";
            sql += @" where t.ArticleGUID not in(select ArticleGUID from ArticleCurrentNotInCategory where CategoryGUID in (select c.CategoryGUID from Category c,CategoryNodePosition p where c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '%" + sCategoryGUID + "/%'))";
            sql += @" order by CreateTime " + order;
            SelectRows(ds, sql);
            return ds.Tables[0];
        }

        private static DataSet SelectRows(DataSet dataset,string queryString)
        {
            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["zjspccmConnectionString"].ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(
                    queryString, connection);
                adapter.Fill(dataset);
                return dataset;
            }
        }

        public static DataSet SelectRows(DataSet dataset, string queryString, string ConnectionString)
        {
            using (SqlConnection connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(
                    queryString, connection);
                adapter.Fill(dataset);
                return dataset;
            }
        }
        public static DataSet ArticleQuery(string ZjspccmConnectionString, string ItemsCount, string CategoryAlias)
        {
            string QueryStringCount = "";
            if (ItemsCount != "")
                QueryStringCount = "top " + ItemsCount;
            string ArticleQueryString = "select distinct " + QueryStringCount + " ac.Title,ac.ArticleGUID,ac.CreateTime,ac.Filename,ac.Author,ac.Area as SpeakerInfo "
                                        + "from (  select distinct acc.articleguid from articlecurrentofcategory acc "
                                        + "join (select categoryguid from dbo.CategoryNodePosition "
                                        + "where categorypath like '%'+(select categoryguid from category where categoryalias = '" + CategoryAlias + "')+'%') temp "
                                        + "on acc.categoryguid = temp.categoryguid) temp "
                                        + "join articlecurrent ac on temp.articleguid = ac.articleguid and ac.state =1 and (ac.ApproveFlag = 1 OR ac.ApproveFlag = 2) "
                                        + "order by ac.CreateTime desc";
            DataSet ArticleDataSet = new DataSet();
            ArticleDataSet = SelectRows(ArticleDataSet, ArticleQueryString);
            return ArticleDataSet;
        }

        public static DataSet ArticleQuerySummary(string ZjspccmConnectionString, string ItemsCount, string CategoryAlias)
        {
            string QueryStringCount = "";
            if (ItemsCount != "")
                QueryStringCount = "top " + ItemsCount;
            string ArticleQueryString = "select distinct " + QueryStringCount + " ac.Title,ac.ArticleGUID,ac.CreateTime,ac.Filename,ac.Author,ac.Area as SpeakerInfo,ac.PlainText as Summary "
                                        + "from (  select distinct acc.articleguid from articlecurrentofcategory acc "
                                        + "join (select categoryguid from dbo.CategoryNodePosition "
                                        + "where categorypath like '%'+(select categoryguid from category where categoryalias = '" + CategoryAlias + "')+'%') temp "
                                        + "on acc.categoryguid = temp.categoryguid) temp "
                                        + "join articlecurrent ac on temp.articleguid = ac.articleguid and ac.state =1 and (ac.ApproveFlag = 1 OR ac.ApproveFlag = 2) "
                                        + "order by ac.CreateTime desc";
            DataSet ArticleDataSet = new DataSet();
            ArticleDataSet = SelectRows(ArticleDataSet, ArticleQueryString);
            return ArticleDataSet;
        }
        public static DataSet ArticleExtQuery(string ZjspccmConnectionString, string ItemsCount, string CategoryAlias, string PropertyAlias)
        {
            string QueryStringCount = "";
            string TempOrder = "";
            if (ItemsCount != "")
            {
                QueryStringCount = "top " + ItemsCount;
                TempOrder = "order by ac.CreateTime desc";
            }
            string ArticleExtQueryString = "select ArticleCurrentPropertyExt.articleguid,ArticleCurrentPropertyExt.PropertyValue "
                                          + "from ArticleCurrentPropertyExt join (select distinct " + QueryStringCount + " ac.ArticleGUID,ac.CreateTime "
                                          + "from (select distinct acc.articleguid from articlecurrentofcategory acc "
                                          + "join (select categoryguid from dbo.CategoryNodePosition  where categorypath "
                                          + "like '%'+(select categoryguid from category where categoryalias = '" + CategoryAlias + "')+'%') temp "
                                          + "on acc.categoryguid = temp.categoryguid) temp join articlecurrent ac "
                                          + "on temp.articleguid = ac.articleguid and ac.state =1 and (ac.ApproveFlag = 1 OR ac.ApproveFlag = 2) "
                                          + "" + TempOrder + ") As ArticleGuids on ArticleGuids.articleguid = ArticleCurrentPropertyExt.articleguid and PropertyAlias='" + PropertyAlias + "' "
                                          + "join articlecurrent on articlecurrent.articleguid = ArticleCurrentPropertyExt.articleguid order by articlecurrent.CreateTime desc";
            DataSet ArticleExtDataSet = new DataSet();
            ArticleExtDataSet = SelectRows(ArticleExtDataSet,ArticleExtQueryString);
            return ArticleExtDataSet;
        }
        //   多ccmfile路径
        public static string GetCourseFilePath(string CourseID)
        {
            string CourseIndexName = string.Empty;
            string FolderName = string.Empty;
            string FilePath = string.Empty; /// 文件夹+index文件
            string FullFolderPath = string.Empty;
            string sql = "select FileName,Note from ArticleCurrent where ArticleGUID=@ArticleGUID";
            SqlParameter[] parameter = { new SqlParameter("@ArticleGUID", CourseID) };
            DataTable dt = DAL.DbHelperSQL.Query(sql, parameter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                FolderName = dt.Rows[0][0].ToString();
                CourseIndexName = dt.Rows[0][1].ToString();
                FilePath = dt.Rows[0][0].ToString() + "/" + dt.Rows[0][1].ToString();
            }
            string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];
            string[] pathList = strFilePath.Split('|');
            string path = string.Empty;
            foreach (string sPath in pathList)
            {
                string tempPath = string.Empty;
                if (sPath.Split(',')[1].EndsWith("\\"))
                    tempPath = sPath.Split(',')[1] + FolderName;
                else
                    tempPath = sPath.Split(',')[1] + "\\" + FolderName;
                if (Directory.Exists(tempPath))
                {
                    path = sPath.Split(',')[0];
                    FullFolderPath = sPath.Split(',')[0] + "/" + FolderName + "/" + CourseIndexName;                   
                    break;    
                }
            }
            return FullFolderPath;
        }
        public static string GetCoursePicPath(string CourseID, string url, string CoursePicName)
        {
            string FolderName = string.Empty;
            string FullFolderPath = string.Empty;
            string sql = "select FileName,Note from ArticleCurrent where ArticleGUID=@ArticleGUID";
            SqlParameter[] parameter = { new SqlParameter("@ArticleGUID", CourseID) };
            DataTable dt = DAL.DbHelperSQL.Query(sql, parameter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                FolderName = dt.Rows[0][0].ToString();
            }
            string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];
            string[] pathList = strFilePath.Split('|');
            string path = string.Empty;
            foreach (string sPath in pathList)
            {
                string tempPath = string.Empty;
                if (sPath.Split(',')[1].EndsWith("\\"))
                    tempPath = sPath.Split(',')[1] + FolderName;
                else
                    tempPath = sPath.Split(',')[1] + "\\" + FolderName;
                if (Directory.Exists(tempPath))
                {
                    path = sPath.Split(',')[0];
                    FullFolderPath =url + sPath.Split(',')[0] + "/" + FolderName + "/" + CoursePicName;
                    break;
                }
            }
            return FullFolderPath;
        }
        public static string GetFileStreamPath(string CourseID, string FilePath)
        {
            string FolderName = string.Empty;
            string FullFolderPath = string.Empty;
            string sql = "select FileName,Note from ArticleCurrent where ArticleGUID=@ArticleGUID";
            SqlParameter[] parameter = { new SqlParameter("@ArticleGUID", CourseID) };
            DataTable dt = DAL.DbHelperSQL.Query(sql, parameter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                FolderName = dt.Rows[0][0].ToString();
            }
            string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];
            string[] pathList = strFilePath.Split('|');
            string path = string.Empty;
            foreach (string sPath in pathList)
            {
                string tempPath = string.Empty;
                if (sPath.Split(',')[1].EndsWith("\\"))
                    tempPath = sPath.Split(',')[1] + FolderName;
                else
                    tempPath = sPath.Split(',')[1] + "\\" + FolderName;
                if (Directory.Exists(tempPath))
                {
                    path = sPath.Split(',')[0];
                    FullFolderPath = tempPath + "/" + FilePath;
                    break;
                }
            }
            return FullFolderPath;
        }
        //    栏目Alias获取备注
        public static string GetNoteByCategoryAlias(string CategoryAlias)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["zjspccmConnectionString"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnStr); 
            SqlCommand Comm = new SqlCommand("SELECT Note FROM Category WHERE CategoryAlias ='"+ CategoryAlias +"'", Conn);
            try
            {
                Conn.Open();
                SqlDataReader InfoReader = Comm.ExecuteReader();
                InfoReader.Read();
                string CategoryNote = InfoReader[0].ToString();
                return CategoryNote;
                InfoReader.Close(); 
            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                Conn.Close(); 
            }
        }
        //    栏目Alias获取名称
        public static string GetNameByCategoryAlias(string CategoryAlias)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["zjspccmConnectionString"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnStr);
            SqlCommand Comm = new SqlCommand("SELECT CategoryName FROM Category WHERE CategoryAlias ='" + CategoryAlias + "'", Conn);
            try
            {
                Conn.Open();
                SqlDataReader InfoReader = Comm.ExecuteReader();
                InfoReader.Read();
                string CategoryNote = InfoReader[0].ToString();
                return CategoryNote;
                InfoReader.Close();
            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                Conn.Close();
            }
        }
        public static string GetNameByCategoryID(string CategoryID)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["zjspccmConnectionString"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnStr);
            SqlCommand Comm = new SqlCommand("SELECT CategoryName FROM Category WHERE CategoryGuid ='" + CategoryID + "'", Conn);
            try
            {
                Conn.Open();
                SqlDataReader InfoReader = Comm.ExecuteReader();
                InfoReader.Read();
                string CategoryNote = InfoReader[0].ToString();
                return CategoryNote;
                InfoReader.Close();
            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                Conn.Close();
            }
        }
        //    栏目AliasToID
        public static string CategoryAliasToID(string CategoryAlias)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["zjspccmConnectionString"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnStr); 
            SqlCommand Comm = new SqlCommand("SELECT CategoryGUID FROM Category WHERE CategoryAlias ='"+ CategoryAlias +"'", Conn);
            try
            {
                Conn.Open();
                SqlDataReader reader = Comm.ExecuteReader();
                if (reader.Read())
                {
                    string CategoryID = reader[0].ToString();
                    return CategoryID;
                }
                else
                {
                    return null;
                }
                reader.Close(); 
            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                Conn.Close(); 
            }

        }
        //    栏目IDtoAlias
        public static string CategoryIDToAlias(string CategoryGuid)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["zjspccmConnectionString"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnStr);
            SqlCommand Comm = new SqlCommand("SELECT CategoryAlias FROM Category WHERE CategoryGUID ='" + CategoryGuid + "'", Conn);
            try
            {
                Conn.Open();
                SqlDataReader reader = Comm.ExecuteReader();
                if (reader.Read())
                {
                    string CategoryID = reader[0].ToString();
                    return CategoryID;
                }
                else
                {
                    return null;
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                Conn.Close();
            }

        }
        //    栏目YIndex
        public static string CategoryYindex(string CategoryAlias)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["zjspccmConnectionString"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnStr);
            SqlCommand Comm = new SqlCommand("SELECT CategoryNodePosition.YIndex FROM Category INNER JOIN CategoryNodePosition ON Category.CategoryGUID = CategoryNodePosition.CategoryGUID where Category.CategoryAlias= '" + CategoryAlias + "'", Conn);
            try
            {
                Conn.Open();
                SqlDataReader reader = Comm.ExecuteReader();
                if (reader.Read())
                {
                    string CategoryID = reader[0].ToString();
                    return CategoryID;
                    reader.Close();
                }
                else
                {
                    return null;
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                Conn.Close();
            }

        }
        //    栏目Path
        public static string CategoryPath(string CategoryGUID)
        {
            DataTable HotSubjectInfo = new DAL.CategoryDAL().GetCategorySimpleInfo(CategoryGUID);
            if (HotSubjectInfo.Rows.Count > 0)
            {
                string CategoryPath = HotSubjectInfo.Rows[0]["CategoryPath"].ToString();
                return CategoryPath;
            }
            else
            {
                return null;
            }
        }
        //   统计课件排行
        public static DataTable GetHotList()
        {
            DataSet ds = new DataSet();
            string sql = "Select top 10 VisitName,URLPath,count(VisitName) As Count,ROW_NUMBER() OVER(ORDER BY count(VisitName) desc) as Seq from Record where VisitName not like '%公告%' group by VisitName,URLPath order by Count desc";
            SelectRows(ds, sql, "weblogConnectionString");
            return ds.Tables[0];
        }
        //数据查询结果集相关
        //  


        // *修改*
        public static string connectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["zjspccmConnectionString"].ToString();
            }
        }
        // _修改:获取栏目下子栏目集合
        public static DataTable GetSubCategories(string sAlias)
        {
            DataTable dt = new DataTable();
            dt = GetCategoryPathAndYIndex(sAlias);
            if (dt.Rows.Count == 0) return dt;
            string sCategoryPath = dt.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

            string sql = "select c.CategoryName,c.CategoryGUID,c.CategoryAlias,p.XIndex from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " and c.State =1 order by XIndex";
            try
            {
                dt = Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }
        // _修改:获取栏目下子栏目集合 指定版本
        public static DataTable GetSubCategoriesApart(string sAlias, string note)
        {
            DataTable dt = new DataTable();
            dt = GetCategoryPathAndYIndex(sAlias);
            if (dt.Rows.Count == 0) return dt;
            string sCategoryPath = dt.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

            string sql = "select c.CategoryName,c.CategoryGUID,c.CategoryAlias,p.XIndex from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " and (c.note ='' or c.note like '%"+ note +"%') and c.State =1 order by XIndex";
            try
            {
                dt = Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }
        // _修改:获取栏目下子栏目集合 前N
        public static DataTable GetSubCategories(string sAlias, string amount)
        {
            DataTable dt = new DataTable();
            dt = GetCategoryPathAndYIndex(sAlias);
            if (dt.Rows.Count == 0) return dt;
            string sCategoryPath = dt.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

            string sql = "select top " + amount + " c.CategoryName,c.CategoryGUID,c.CategoryAlias,p.XIndex from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " and c.State =1 order by XIndex";
            try
            {
                dt = Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }
        // _修改:获取栏目下子栏目集合 指定版本 前N
        public static DataTable GetSubCategoriesApart(string sAlias, string note, string amount)
        {
            DataTable dt = new DataTable();
            dt = GetCategoryPathAndYIndex(sAlias);
            if (dt.Rows.Count == 0) return dt;
            string sCategoryPath = dt.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

            string sql = "select top " + amount + " c.CategoryName,c.CategoryGUID,c.CategoryAlias,p.XIndex from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " and (c.note ='' or c.note like '%" + note + "%') and c.State =1 order by XIndex";
            try
            {
                dt = Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }
        // _修改:获取栏目下子栏目集合 前N 指定备注
        public static DataTable GetSubCategoriesByNote(string sAlias, string amount,string note)
        {
            DataTable dt = new DataTable();
            dt = GetCategoryPathAndYIndex(sAlias);
            if (dt.Rows.Count == 0) return dt;
            string sCategoryPath = dt.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

            string sql = "select top " + amount + " c.CategoryName,c.CategoryGUID,c.CategoryAlias,p.XIndex from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + "and c.Note like '" + note + "' and c.State =1 order by XIndex";
            try
            {
                dt = Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }
        public static DataTable GetSubCategoriesByNoteGuid(string Guid, string amount, string note)
        {
            DataTable dt = new DataTable();
            dt = GetCategoryPathAndYIndexFromGuid(Guid);
            if (dt.Rows.Count == 0) return dt;
            string sCategoryPath = dt.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

            string sql = "select top " + amount + " c.CategoryName,c.CategoryGUID,c.CategoryAlias,p.XIndex from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + "and c.Note like '" + note + "' and c.State =1 order by XIndex";
            try
            {
                dt = Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }
        public static DataTable GetNaviCategories(string Guid, string amount, string note)
        {
            DataTable dt = new DataTable();
            string sCategoryPath = Guid;
            int iYIndex = 4;
            string sql = "select top " + amount + " c.CategoryName,c.CategoryGUID,c.CategoryAlias,p.XIndex from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '/%" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + "and c.Note like '%" + note + "%' and c.State =1 order by XIndex";
            try
            {
                dt = Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }
        // _修改:获取栏目下子栏目集合 前N 有备注
        public static DataTable GetSubCategoriesNote(string sAlias, string amount)
        {
            DataTable dt = new DataTable();
            dt = GetCategoryPathAndYIndex(sAlias);
            if (dt.Rows.Count == 0) return dt;
            string sCategoryPath = dt.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

            string sql = "select top " + amount + " c.CategoryName,c.CategoryGUID,c.CategoryAlias,c.Note,p.XIndex from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " and c.State =1 order by XIndex";
            try
            {
                dt = Query(sql).Tables[0];
            }
            catch { }
            return dt;
        }
        // _修改:获取频道下特定名称栏目的别名
        public static string GetChannelAliasByName(string sAlias, string name)
        {
            DataTable dt = new DataTable();
            dt = GetCategoryPathAndYIndex(sAlias);
            if (dt.Rows.Count == 0) return null;
            string sCategoryPath = dt.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

            string sql = "select c.CategoryAlias,p.XIndex from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " and c.CategoryName like'" + name + "'";
            try
            {
                dt = Query(sql).Tables[0];
            }
            catch { }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
        }
        // _修改:获取频道下特定名称栏目的ID
        public static string GetSubChannelGuidByName(string sAlias, string name)
        {
            DataTable dt = new DataTable();
            dt = GetCategoryPathAndYIndex(sAlias);
            if (dt.Rows.Count == 0) return null;
            string sCategoryPath = dt.Rows[0]["CategoryPath"].ToString();
            int iYIndex = int.Parse(dt.Rows[0]["YIndex"].ToString()) + 1;

            string sql = "select c.CategoryGuid,p.XIndex from Category c inner join CategoryNodePosition p on c.CategoryGUID=p.CategoryGUID and p.CategoryPath like '" + sCategoryPath + "/%' and p.YIndex=" + iYIndex + " and c.CategoryName like'" + name + "'";
            try
            {
                dt = Query(sql).Tables[0];
            }
            catch { }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
        }
        //
        public static DataTable GetCategoryPathAndYIndex(string sAlias)
        {
            DataTable dt = new DataTable();
            string sql = " select CategoryPath,YIndex from CategoryNodePosition p,Category c where p.CategoryGUID=c.CategoryGUID and c.CategoryAlias=@Alias";
            SqlParameter[] parameter = { new SqlParameter("@Alias", sAlias) };
            dt = Query(sql, parameter).Tables[0];
            return dt;
        }
        public static DataTable GetCategoryPathAndYIndexFromGuid(string guid)
        {
            DataTable dt = new DataTable();
            string sql = " select CategoryPath,YIndex from CategoryNodePosition where CategoryGUID=@guid";
            SqlParameter[] parameter = { new SqlParameter("@guid", guid) };
            dt = Query(sql, parameter).Tables[0];
            return dt;
        }
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        // _修改:课件 前N条 从栏目ID
        public static DataTable GetArticleList(string sCategoryGUID, bool NeedSummary, string sSort, int iStartRow,int iEndRow)
        {
            DataTable dt = new DAL.Article().GetArticleList(sCategoryGUID, NeedSummary, sSort,false);
            if (dt.Rows.Count > 0)
            {
                if (iEndRow != 0)
                {
                    DataTable dtRS = dt.Clone();
                    if (dt.Rows.Count <= iEndRow) iEndRow = dt.Rows.Count;
                    for (int i = iStartRow; i < iEndRow; )
                    {
                        dtRS.ImportRow(dt.Rows[i]);
                        i++;
                    }
                    return dtRS;
                }
                else return dt;
            }
            else
            {
                return null;
            }
        }
        // _修改:课件 从栏目别名
        public static DataTable GetArticleListFromAlias(string CategoryAlias, bool NeedSummary, string sSort)
        {
            string sCategoryGUID = CategoryAliasToID(CategoryAlias);
            DataTable dt = new DAL.Article().GetArticleList(sCategoryGUID, NeedSummary, sSort,false);
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
                
        }     
        // _修改:课件 前N条 从栏目别名
        public static DataTable GetArticleListFromAlias(string CategoryAlias, bool NeedSummary, string sSort, int iStartRow, int iEndRow)
        {
            string sCategoryGUID = CategoryAliasToID(CategoryAlias);
            DataTable dt = new DAL.Article().GetArticleList(sCategoryGUID, NeedSummary, sSort,false);
            if (dt.Rows.Count > 0)
            {
                if (iEndRow != 0)
                {
                    DataTable dtRS = dt.Clone();
                    if (dt.Rows.Count <= iEndRow) iEndRow = dt.Rows.Count;
                    for (int i = iStartRow; i < iEndRow; )
                    {
                        dtRS.ImportRow(dt.Rows[i]);
                        i++;
                    }
                    return dtRS;
                }
                else return dt;
            }
            else
            {
                return null;
            }
        }
        // _修改:前3条热点专题
        public static DataTable GetNHotZT()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] parameters = { 
                                            new SqlParameter("@CategoryAlias",SqlDbType.NVarChar,256)
                                            };
                string sSql = @"select top 3 c.CategoryGUID,c.CategoryName from Category c inner join CategoryNodePosition cnp on c.CategoryGUID=cnp.CategoryGUID and c.SecretLevel>=2 and cnp.CategoryPath like (select '%'+c.CategoryGUID+'%' from Category c where c.CategoryAlias=@CategoryAlias) order by c.SecretLevel";
                parameters[0].Value = System.Configuration.ConfigurationManager.AppSettings["TBGZ_Alias"];

                dt = DAL.DbHelperSQL.Query(sSql, parameters).Tables[0];
            }
            catch (Exception ex) { throw ex; }
            return dt;
        }
        //  _修改:获取预告文字
        public static string GetTrailerText(string Alias, string Title)
        {
            DataTable dt = new DataTable();
            try
            {
                string sSql = @" select top 1 a.PlainText,a.CreateTime from ArticleCurrent a,ArticleCurrentOfCategory ac WHERE ";
                sSql += @" a.ArticleGUID=ac.ArticleGUID AND a.Title=@Title AND ac.CategoryGUID=(select CategoryGUID from Category where CategoryAlias=@Alias) order by a.CreateTime Desc";
                SqlParameter[] parameter = {
                                               new SqlParameter("@Alias", Alias),
                                               new SqlParameter("@Title", Title)
                                           };
                dt = DAL.DbHelperSQL.Query(sSql, parameter).Tables[0];
            }
            catch { }
            if (dt.Rows.Count > 0) return dt.Rows[0][0].ToString();
            else return null;
        }
        //  _修改:获取图片专题文字
        public static string GetText(string Alias, string Title)
        {
            DataTable dt = new DataTable();
            try
            {
                string sSql = @" select top 1 a.Content,a.CreateTime from ArticleCurrent a,ArticleCurrentOfCategory ac WHERE ";
                sSql += @" a.ArticleGUID=ac.ArticleGUID AND a.Title=@Title AND ac.CategoryGUID=(select CategoryGUID from Category where CategoryAlias=@Alias) order by a.CreateTime Desc";
                SqlParameter[] parameter = {
                                               new SqlParameter("@Alias", Alias),
                                               new SqlParameter("@Title", Title)
                                           };
                dt = DAL.DbHelperSQL.Query(sSql, parameter).Tables[0];
            }
            catch { }
            if (dt.Rows.Count > 0) return dt.Rows[0][0].ToString();
            else return null;
        }
        //
        public static string GetCategoryAliasByid(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                string sSql = @" select top 1 c.CategoryAlias from Category c WHERE c.CategoryGUID=@id";
                SqlParameter[] parameter = {
                                               new SqlParameter("@id", id),
                                           };
                dt = DAL.DbHelperSQL.Query(sSql, parameter).Tables[0];
            }
            catch { }
            if (dt.Rows.Count > 0) return dt.Rows[0][0].ToString();
            else return null;
        }
    }
}
