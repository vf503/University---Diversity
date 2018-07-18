using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using CEInet.Pisces.SystemFramework;


namespace MyHttpModule
{
    public class CountModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        //请求开始时，获取请求信息
        public void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            string host = context.Request.Url.Host;

            //获取客户端请求原始URL
            //string url = context.Request.QueryString.ToString();//改为参数,中文参数被转码
            string url = context.Request.RawUrl.ToString();
            if (url.IndexOf("?") > -1)
            {
                url = url.Split(new char[] { '?' })[1];
            }
            else
            {
                url = "";
            }
            //获取客户端访问IP地址
            string IPAddress = context.Request.UserHostAddress;
            IPAddress = IPTo3Digit(IPAddress);
            //获取客户端浏览器等信息
            string BrownAgentcontext = context.Request.Browser.Type.ToString();
            //获取请求时间
            DateTime RequestTime = context.Timestamp;
            //获取请求的页面信息
            string RequestPage = context.Request.Path.Substring(context.Request.Path.LastIndexOf("/") + 1);
            //UserID
            string UserID = "Anonymous";
            if (context.Request.Cookies["USSUserID"] == null)
            {
                UserID = GetUserIDByIP(IPAddress);
            }
            else
            {
                UserID = Decrypt(context.Request.Cookies["USSUserID"].Value.ToString());
            }
            //

            //记录
            if (RequestPage.ToLower() == "showvideo.aspx")//*CC* Course
            {
                string CourseGUID = "";
                string CourseName = string.Empty;
                GetCourseInfo(ref CourseName, ref CourseGUID, context); //*CC*
                if (CourseName != string.Empty)
                {
                    SaveCourseDate(IPAddress, UserID, url, BrownAgentcontext, RequestTime, CourseGUID, RequestPage.ToLower(), host);
                }
            }
            else if (RequestPage.ToLower() == "" || RequestPage.ToLower() == "index.aspx" || RequestPage.ToLower() == "advancesearch.aspx" || RequestPage.ToLower() == "navigate.aspx")//*CC* Page_Home
            {
                SavePageDate(IPAddress, UserID, url, BrownAgentcontext, RequestTime, "", RequestPage.ToLower(), host);
            }
            else if (RequestPage.ToLower() == "search.aspx")
            {
                switch (context.Request.QueryString["act"])
                {
                    case "n":
                        {
                            SavePageDate(IPAddress, UserID, url, BrownAgentcontext, RequestTime, "", RequestPage.ToLower(), host);
                            break;
                        }
                    case "h":
                        {
                            string ChannelGUID = context.Request.QueryString["ID"];
                            SavePageDate(IPAddress, UserID, url, BrownAgentcontext, RequestTime, ChannelGUID, RequestPage.ToLower(), host);
                            break;
                        }
                    case "lv2h":
                        {
                            string ChannelGUID = context.Request.QueryString["ID"];
                            SavePageDate(IPAddress, UserID, url, BrownAgentcontext, RequestTime, ChannelGUID, RequestPage.ToLower(), host);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else if (RequestPage.ToLower() == "picfocuspic.aspx")
            {
                string CourseGUID = "";
                string CourseName = string.Empty;
                GetCourseInfo(ref CourseName, ref CourseGUID, context); //*CC*
                SaveCourseDate(IPAddress, UserID, url, BrownAgentcontext, RequestTime, CourseGUID, RequestPage.ToLower(), host);
            }
            else if (RequestPage.ToLower() == "picfocustxt.aspx")
            {
                string ChannelGUID = colleges.DataQuery.CategoryAliasToID(context.Request.QueryString["SubAlias"]);
                SavePageDate(IPAddress, UserID, url, BrownAgentcontext, RequestTime, ChannelGUID, RequestPage.ToLower(), host);
            }
            else if (RequestPage.ToLower() == "level2.aspx" || RequestPage.ToLower() == "level2fame.aspx" || RequestPage.ToLower() == "level2class.aspx")
            {
                string ChannelGUID = string.Empty;
                switch (RequestPage.ToLower())
                {
                    case "level2.aspx":
                        {
                            string ChannelAlias = context.Request.QueryString["alias"].ToString();
                            if (ChannelAlias != ConfigurationManager.AppSettings["ChannelFame"].ToString() && ChannelAlias != ConfigurationManager.AppSettings["ChannelClass"].ToString())
                            {
                                ChannelGUID = colleges.DataQuery.CategoryAliasToID(ChannelAlias);
                                SavePageDate(IPAddress, UserID, url, BrownAgentcontext, RequestTime, ChannelGUID, RequestPage.ToLower(), host);
                            }
                            else
                            {

                            }
                            break;
                        }
                    case "level2fame.aspx":
                        {
                            string SkipUrl = "/level2.aspx?alias=" + ConfigurationManager.AppSettings["ChannelFame"].ToString();
                            ChannelGUID = colleges.DataQuery.CategoryAliasToID(ConfigurationManager.AppSettings["ChannelFame"].ToString());
                            SavePageDate(IPAddress, UserID, SkipUrl, BrownAgentcontext, RequestTime, ChannelGUID, "level2.aspx", host);
                            break;
                        }
                    case "level2class.aspx":
                        {
                            string SkipUrl = "/level2.aspx?alias=" + ConfigurationManager.AppSettings["ChannelClass"].ToString();
                            ChannelGUID = colleges.DataQuery.CategoryAliasToID(ConfigurationManager.AppSettings["ChannelClass"].ToString());
                            SavePageDate(IPAddress, UserID, SkipUrl, BrownAgentcontext, RequestTime, ChannelGUID, "level2.aspx", host);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else if (RequestPage.ToLower() == "level3.aspx" || RequestPage.ToLower() == "level3class.aspx" || RequestPage.ToLower() == "level3fame.aspx" || RequestPage.ToLower() == "level3hot.aspx" || RequestPage.ToLower() == "level3navi.aspx")
            {
                string ChannelAlias = context.Request.QueryString["alias"].ToString();
                string ChannelGUID = colleges.DataQuery.CategoryAliasToID(ChannelAlias);
                SavePageDate(IPAddress, UserID, url, BrownAgentcontext, RequestTime, ChannelGUID, RequestPage.ToLower(), host);
            }
            else if (RequestPage.ToLower() == "specialattention.aspx" || RequestPage.ToLower() == "specialindex.aspx" || RequestPage.ToLower() == "specialhistory.aspx")
            {
                string ChannelGUID = context.Request.QueryString["ID"].ToString();
                SavePageDate(IPAddress, UserID, url, BrownAgentcontext, RequestTime, ChannelGUID, RequestPage.ToLower(), host);
            }
            else
            {

            }
        }

        //Course OP
        //GetCourseInfo
        private void GetCourseInfo(ref string CourseName, ref string CourseGUID, HttpContext context)//*CC*
        {
            string cPath = string.Empty;
            Database db = GetDatabase("zjspccmConnectionString");
            DbCommand cmd = db.GetSqlStringCommand("select a.Title,c.CategoryGUID,p.CategoryPath from ArticleCurrent a,Category c,ArticleCurrentOfCategory d,CategoryNodePosition p where a.ArticleGUID=@ArticleGUID and a.ArticleGUID=d.ArticleGUID and c.CategoryGUID=d.CategoryGUID and c.CategoryGUID=p.CategoryGUID and p.CategoryPath not like '/1b875bff617b447d8e4b14a800d62084/7c98a4a59f9a4a4a861d234fa38a46c9%'");  //排除课件资源库分类
            CourseGUID = context.Request.QueryString["ID"];//*CC*
            db.AddInParameter(cmd, "@ArticleGUID", DbType.String, CourseGUID);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                {
                    CourseName = dr["Title"].ToString();
                }
            }
        }

        //Save Course Date
        private void SaveCourseDate(string IPAddress, string UserID, string URL, string BrownAgentcontext, DateTime RequestTime, string ArticleGUID, string RequestPage, string host)
        {
            Database CountDB = GetDatabase("weblogConnectionString");
            //用using语句,不需要显示释放数据库资源
            using (DbConnection CountDBConn = GetConnection(CountDB))
            {
                DbTransaction tran = CountDBConn.BeginTransaction();
                try
                {
                    //GUID
                    if (ArticleGUID != "")
                    {
                        DbCommand cmd = CountDB.GetSqlStringCommand("select count(ArticleGUID) from Article where ArticleGUID=@ArticleGUID");
                        CountDB.AddInParameter(cmd, "@ArticleGUID", DbType.String, ArticleGUID);
                        //Channel结构表没有记录，则增加记录
                        if ((int)CountDB.ExecuteScalar(cmd) == 0)
                        {
                            cmd = CountDB.GetSqlStringCommand("INSERT INTO Article(ArticleGUID,Title,Author,CreateTime) VALUES (@ArticleGUID,@Title,@Author,@CreateTime)");
                            CountDB.AddInParameter(cmd, "@ArticleGUID", DbType.String, ArticleGUID);

                            //获取结构信息
                            Database CcmDB = GetDatabase("zjspccmConnectionString");
                            DbCommand cmd1 = CcmDB.GetSqlStringCommand("Select ArticleGUID,Title,Author,convert(char(10),CreateTime,120) as CreateTime  from ArticleCurrent where ArticleGUID=@ArticleGUID");
                            CcmDB.AddInParameter(cmd1, "@ArticleGUID", DbType.String, ArticleGUID);
                            using (IDataReader dr = CcmDB.ExecuteReader(cmd1))
                            {
                                if (dr.Read())
                                {
                                    CountDB.AddInParameter(cmd, "@Title", DbType.String, dr["Title"].ToString());
                                    CountDB.AddInParameter(cmd, "@Author", DbType.String, dr["Author"].ToString());
                                    CountDB.AddInParameter(cmd, "@CreateTime", DbType.Date, Convert.ToDateTime(dr["CreateTime"]));
                                }
                                CountDB.ExecuteNonQuery(cmd, tran);
                            }

                        }
                    }
                    else
                    {

                    }
                    //Count
                    DbCommand InsertCourseDate;
                    InsertCourseDate = CountDB.GetSqlStringCommand("INSERT INTO Record(UserIP,UserID,URLPath,RequestTime,UserAgent,ChannelGUID,ArticleGUID,PageName,Host,IsAdd,GUID) VALUES (@IPAddress,@UserID,@URL,@RequestTime,@BrownAgentcontext,@ChannelGUID,@ArticleGUID,@PageName,@host,@IsAdd,newid())");
                    CountDB.AddInParameter(InsertCourseDate, "@IPAddress", DbType.String, IPAddress);
                    CountDB.AddInParameter(InsertCourseDate, "@UserID", DbType.String, UserID);
                    CountDB.AddInParameter(InsertCourseDate, "@URL", DbType.String, URL);
                    CountDB.AddInParameter(InsertCourseDate, "@RequestTime", DbType.DateTime, RequestTime);
                    CountDB.AddInParameter(InsertCourseDate, "@BrownAgentcontext", DbType.String, BrownAgentcontext);
                    CountDB.AddInParameter(InsertCourseDate, "@ChannelGUID", DbType.String, "");
                    CountDB.AddInParameter(InsertCourseDate, "@ArticleGUID", DbType.String, ArticleGUID);
                    CountDB.AddInParameter(InsertCourseDate, "@PageName", DbType.String, RequestPage);
                    CountDB.AddInParameter(InsertCourseDate, "@Host", DbType.String, host);
                    CountDB.AddInParameter(InsertCourseDate, "@IsAdd", DbType.Boolean, false);
                    CountDB.ExecuteNonQuery(InsertCourseDate, tran);  //插入访问记录数据
                    //Fake
                    if (ConfigurationManager.AppSettings["IsModify"].ToString() == "1")
                    {
                        Random FakeCount = new Random();
                        int fakeMax = Convert.ToInt32(ConfigurationManager.AppSettings["MaxModify"]);
                        int fakeMin = Convert.ToInt32(ConfigurationManager.AppSettings["MinModify"]);
                        int fakeN = FakeCount.Next(fakeMin, fakeMax);
                        if (fakeN == 0) fakeN = 1;
                        string FakeUserID = string.Empty;
                        for (int i = 0; i < fakeN; i++)
                        {
                            string EndIP;
                            string StartIP = GetRandomIPData(out EndIP, out FakeUserID);
                            string FAkeIP = GetRandomIP(StartIP, EndIP);
                            int FakeTime = i+1;
                            InsertCourseDate.Parameters["@IPAddress"].Value = FAkeIP.ToString();
                            InsertCourseDate.Parameters["@UserID"].Value = FakeUserID;
                            InsertCourseDate.Parameters["@RequestTime"].Value = RequestTime.AddDays(-FakeTime).AddHours(-0).AddMinutes(-FakeTime); //Night No AddHours(-FakeTime)
                            InsertCourseDate.Parameters["@IsAdd"].Value = true;
                            CountDB.ExecuteNonQuery(InsertCourseDate, tran);
                        }
                    }
                    else
                    {

                    }
                    //Commit
                    tran.Commit();
                    CloseConnection(CountDBConn);
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        //Course ED

        //Page OP

        //Save Page Date
        private void SavePageDate(string IPAddress, string UserID, string URL, string BrownAgentcontext, DateTime RequestTime, string ChannelGUID, string RequestPage, string host)
        {
            Database CountDB = GetDatabase("weblogConnectionString");
            //用using语句,不需要显示释放数据库资源
            using (DbConnection CountDBConn = GetConnection(CountDB))
            {
                DbTransaction tran = CountDBConn.BeginTransaction();
                try
                {
                    //GUID
                    if (ChannelGUID != "")
                    {
                        DbCommand cmd = CountDB.GetSqlStringCommand("select count(ChannelGUID) from channel where ChannelGUID=@ChannelGUID");
                        CountDB.AddInParameter(cmd, "@ChannelGUID", DbType.String, ChannelGUID);
                        //Channel结构表没有记录，则增加记录
                        if ((int)CountDB.ExecuteScalar(cmd) == 0)
                        {
                            cmd = CountDB.GetSqlStringCommand("INSERT INTO Channel(ChannelGUID,ParentPath,CName,zorder,Note) VALUES (@ChannelGUID,@ParentPath,@CName,@zorder,@Note)");
                            CountDB.AddInParameter(cmd, "@ChannelGUID", DbType.String, ChannelGUID);

                            //获取结构信息
                            Database CcmDB = GetDatabase("zjspccmConnectionString");
                            DbCommand cmd1 = CcmDB.GetSqlStringCommand("Select a.CategoryName,CategoryPath,YIndex from Category a,CategoryNodePosition b where a.CategoryGUID=b.CategoryGUID and a.CategoryGUID=@CategoryGUID");
                            CcmDB.AddInParameter(cmd1, "@CategoryGUID", DbType.String, ChannelGUID);
                            using (IDataReader dr = CcmDB.ExecuteReader(cmd1))
                            {
                                if (dr.Read())
                                {
                                    CountDB.AddInParameter(cmd, "@CName", DbType.String, dr["CategoryName"].ToString());
                                    CountDB.AddInParameter(cmd, "@ParentPath", DbType.String, dr["CategoryPath"].ToString());
                                    CountDB.AddInParameter(cmd, "@zorder", DbType.String, dr["YIndex"].ToString());
                                    CountDB.AddInParameter(cmd, "@Note", DbType.String, string.Empty);
                                }
                                CountDB.ExecuteNonQuery(cmd, tran);
                            }

                        }
                    }
                    else
                    {

                    }
                    //Count
                    DbCommand InsertCourseDate;
                    InsertCourseDate = CountDB.GetSqlStringCommand("INSERT INTO Record(UserIP,UserID,URLPath,RequestTime,UserAgent,ChannelGUID,ArticleGUID,PageName,Host,IsAdd,GUID) VALUES (@IPAddress,@UserID,@URL,@RequestTime,@BrownAgentcontext,@ChannelGUID,@ArticleGUID,@PageName,@host,@IsAdd,newid())");
                    CountDB.AddInParameter(InsertCourseDate, "@IPAddress", DbType.String, IPAddress);
                    CountDB.AddInParameter(InsertCourseDate, "@UserID", DbType.String, UserID);
                    CountDB.AddInParameter(InsertCourseDate, "@URL", DbType.String, URL);
                    CountDB.AddInParameter(InsertCourseDate, "@RequestTime", DbType.DateTime, RequestTime);
                    CountDB.AddInParameter(InsertCourseDate, "@BrownAgentcontext", DbType.String, BrownAgentcontext);
                    CountDB.AddInParameter(InsertCourseDate, "@ChannelGUID", DbType.String, ChannelGUID);
                    CountDB.AddInParameter(InsertCourseDate, "@ArticleGUID", DbType.String, "");
                    CountDB.AddInParameter(InsertCourseDate, "@PageName", DbType.String, RequestPage);
                    CountDB.AddInParameter(InsertCourseDate, "@Host", DbType.String, host);
                    CountDB.AddInParameter(InsertCourseDate, "@IsAdd", DbType.Boolean, false);
                    CountDB.ExecuteNonQuery(InsertCourseDate, tran);  //插入访问记录数据
                    //tran.Commit();
                    //Fake
                    if (ConfigurationManager.AppSettings["IsModify"].ToString() == "1")
                    {
                        Random FakeCount = new Random();
                        int fakeMax = Convert.ToInt32(ConfigurationManager.AppSettings["MaxModify"]);
                        int fakeMin = Convert.ToInt32(ConfigurationManager.AppSettings["MinModify"]);
                        int fakeN = FakeCount.Next(fakeMin, fakeMax);
                        if (fakeN == 0) fakeN = 1;
                        //string FakeUserID = string.Empty;
                        DataTable FakeIPTable;
                        FakeIPTable = GetRandomIPData(fakeN);
                        for (int i = 0; i < fakeN; i++)
                        {
                            //string EndIP;
                            //string StartIP = GetRandomIPData(out EndIP, out FakeUserID);
                            //string FAkeIP = GetRandomIP(StartIP, EndIP);
                            int FakeTime = i + 1;
                            string FAkeIP = GetRandomIP(FakeIPTable.Rows[i]["StartIP"].ToString(), FakeIPTable.Rows[i]["EndIP"].ToString());
                            InsertCourseDate.Parameters["@IPAddress"].Value = FAkeIP.ToString();
                            InsertCourseDate.Parameters["@UserID"].Value = FakeIPTable.Rows[i]["UserID"].ToString();
                            InsertCourseDate.Parameters["@RequestTime"].Value = RequestTime.AddDays(-FakeTime).AddHours(-0).AddMinutes(-FakeTime);
                            //InsertCourseDate.Parameters["@RequestTime"].Value = RequestTime;
                            InsertCourseDate.Parameters["@IsAdd"].Value = true;
                            CountDB.ExecuteNonQuery(InsertCourseDate, tran);
                        }
                    }
                    else
                    {

                    }
                    //Commit
                    tran.Commit();
                    CloseConnection(CountDBConn);
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        //Page ED

        //获取请求内容名称及父级结构
        private void GetRequestInfo(ref string RequestName, ref ArrayList ChannelGuidArrayList, HttpContext context)//*CC*
        {
            string cPath = string.Empty;
            Database db = GetDatabase("zjspccmConnectionString");
            DbCommand cmd = db.GetSqlStringCommand("select a.Title,c.CategoryGUID,p.CategoryPath from ArticleCurrent a,Category c,ArticleCurrentOfCategory d,CategoryNodePosition p where a.ArticleGUID=@ArticleGUID and a.ArticleGUID=d.ArticleGUID and c.CategoryGUID=d.CategoryGUID and c.CategoryGUID=p.CategoryGUID and p.CategoryPath not like '/1b875bff617b447d8e4b14a800d62084/7c98a4a59f9a4a4a861d234fa38a46c9%'");  //排除课件资源库分类
            string sArticleGUID = context.Request.QueryString["ID"];//*CC*
            db.AddInParameter(cmd, "@ArticleGUID", DbType.String, sArticleGUID);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                // if (dr.Read())
                // {
                int j = 0;
                while (dr.Read())
                {
                    RequestName = dr["Title"].ToString();
                    cPath = dr["CategoryPath"].ToString();
                    string[] SplitArray = cPath.Trim().Split(new char[] { '/' });
                    ChannelGuidArrayList.Add(SplitArray[4].ToString());//*CC*
                    j++;
                }
                //  }
            }
        }

        //保存请求信息
        private void SaveRequestInfo(string IPAddress, string URL, string BrownAgentcontext, DateTime RequestTime, string RequestName, string ChannelGUID, string host)
        {
            Database db = GetDatabase("weblogConnectionString");

            //用using语句,不需要显示释放数据库资源
            using (DbConnection cn = GetConnection(db))
            {


                DbTransaction tran = cn.BeginTransaction();
                try
                {
                    Random rd = new Random();
                    int fakeMax = Convert.ToInt32(ConfigurationManager.AppSettings["fakeMax"]);
                    int fakeMin = Convert.ToInt32(ConfigurationManager.AppSettings["fakeMin"]);
                    int fakeN = rd.Next(fakeMin, fakeMax);
                    if (fakeN == 0) fakeN = 1;
                    DbCommand cmd;
                    for (int i = 0; i < fakeN; i++)
                    {
                        cmd = db.GetSqlStringCommand("INSERT INTO Record(UserIP,IPAreaID,URLPath,VisitName,RequestTime,UserAgent,ChannelGUID,Host) VALUES (@IPAddress,@IPAreaID,@URL, @RequestName, @RequestTime,@BrownAgentcontext,@ChannelGUID,@host)");
                        db.AddInParameter(cmd, "@IPAddress", DbType.String, IPAddress);
                        db.AddInParameter(cmd, "@IPAreaID", DbType.String, GetEreaByIP(IPAddress));
                        db.AddInParameter(cmd, "@URL", DbType.String, URL);
                        db.AddInParameter(cmd, "@RequestName", DbType.String, RequestName);
                        db.AddInParameter(cmd, "@RequestTime", DbType.DateTime, RequestTime.AddDays(-i));
                        db.AddInParameter(cmd, "@BrownAgentcontext", DbType.String, BrownAgentcontext);
                        db.AddInParameter(cmd, "@ChannelGUID", DbType.String, ChannelGUID);
                        db.AddInParameter(cmd, "@Host", DbType.String, host);
                        db.ExecuteNonQuery(cmd, tran);  //插入访问记录数据
                    }

                    cmd = db.GetSqlStringCommand("select Distinct * from historyRecord where VisitName=@VisitName and Host=@Host");
                    db.AddInParameter(cmd, "@VisitName", DbType.String, RequestName);
                    db.AddInParameter(cmd, "@Host", DbType.String, host);
                    IDataReader idr = db.ExecuteReader(cmd);


                    if (idr.Read()) //History表有记录，则增加次数
                    {
                        cmd = db.GetSqlStringCommand("update historyRecord set ClickTimes = ClickTimes + @fakeN where  VisitName=@VisitName and Host=@Host");
                        db.AddInParameter(cmd, "@fakeN", DbType.Int32, fakeN);
                        db.AddInParameter(cmd, "@VisitName", DbType.String, RequestName);
                        db.AddInParameter(cmd, "@Host", DbType.String, host);
                        db.ExecuteNonQuery(cmd, tran);  //插入history访问记录数据
                    }
                    else //History表没有记录，则增加记录
                    {
                        cmd = db.GetSqlStringCommand("INSERT INTO historyRecord( VisitName,  Host,  ClickTimes,  ChannelGUID ,  URLPath) VALUES (@VisitName,@Host,@ClickTimes, @ChannelGUID, @URLPath)");
                        db.AddInParameter(cmd, "@VisitName", DbType.String, RequestName);
                        db.AddInParameter(cmd, "@Host", DbType.String, host);
                        db.AddInParameter(cmd, "@ClickTimes", DbType.Int32, fakeN);
                        db.AddInParameter(cmd, "@ChannelGUID", DbType.String, ChannelGUID);
                        db.AddInParameter(cmd, "@URLPath", DbType.String, URL);
                        db.ExecuteNonQuery(cmd, tran);  //插入history访问记录数据
                    }

                    if (ChannelGUID != string.Empty)   //非首页记录信息
                    {

                        cmd = db.GetSqlStringCommand("select count(ChannelGUID) from channel where ChannelGUID=@ChannelGUID");
                        db.AddInParameter(cmd, "@ChannelGUID", DbType.String, ChannelGUID);
                        //Channel结构表没有记录，则增加记录
                        if ((int)db.ExecuteScalar(cmd) == 0)
                        {
                            cmd = db.GetSqlStringCommand("INSERT INTO Channel(ChannelGUID,ParentPath,CName,zorder,Note) VALUES (@ChannelGUID,@ParentPath,@CName,@zorder,@Note)");
                            db.AddInParameter(cmd, "@ChannelGUID", DbType.String, ChannelGUID);

                            //获取结构信息
                            Database db1 = GetDatabase("zjspccmConnectionString");
                            DbCommand cmd1 = db1.GetSqlStringCommand("Select a.CategoryName,CategoryPath,YIndex from Category a,CategoryNodePosition b where a.CategoryGUID=b.CategoryGUID and a.CategoryGUID=@CategoryGUID");
                            db1.AddInParameter(cmd1, "@CategoryGUID", DbType.String, ChannelGUID);
                            using (IDataReader dr = db1.ExecuteReader(cmd1))
                            {
                                while (dr.Read())
                                {
                                    db.AddInParameter(cmd, "@CName", DbType.String, dr["CategoryName"].ToString());
                                    db.AddInParameter(cmd, "@ParentPath", DbType.String, dr["CategoryPath"].ToString());
                                    db.AddInParameter(cmd, "@zorder", DbType.String, dr["YIndex"].ToString());
                                    db.AddInParameter(cmd, "@Note", DbType.String, string.Empty);
                                }
                                db.ExecuteNonQuery(cmd, tran);
                            }

                        }
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

        }
        //IP OP
        public static Int32 GetEreaByIP(string ip)
        {
            Int32 Erea =0;
            long ipNum = IpConvert(ip);

            Database db = GetDatabase("weblogConnectionString");
            //Max Begin & Min End
            DbCommand cmd = db.GetSqlStringCommand("SELECT GUID from IPAddress where AdjEndIP = (select min(AdjEndIP) from IPAddress where AdjEndIP >= @IPNum) and AdjBeginIP=(select max(AdjBeginIP) from IPAddress where AdjBeginIP <= @IPNum)");
            db.AddInParameter(cmd, "@IPNum", DbType.Int64, ipNum);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    Erea = System.Convert.ToInt32(dr.GetValue(0));
                }
            }
            //ip地址没有区域存储
            //if (Erea == null) { Erea = 0; }
            return Erea;
        }
        public static string GetUserIDByIP(string ip)
        {
            string UserID = "Anonymous";
            Database db = GetDatabase("weblogConnectionString");
            DbCommand cmd = db.GetSqlStringCommand("SELECT top 1 UserID from IPAddress where  EndIP >= @IP and StartIP  <= @IP order by StartIP Desc");
            db.AddInParameter(cmd, "@IP", DbType.String, ip);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    UserID = dr.GetValue(0).ToString();
                }
            }
            return UserID;
        }
        //转换ip地址为可比较的数据
        public static long IpConvert(string ip)
        {
            string[] ip_List;
            long ipNum = 0;

            ip_List = ip.Split(Convert.ToChar("."));
            for (int i = 0; i < ip_List.Length; i++)
            {
                ipNum = ipNum * 256 + Convert.ToInt16(ip_List[i]);
            }
            return ipNum;
        }

        //public System.Net.IPAddress GetRandomIPData(out System.Net.IPAddress EndIP, out string UserID)
        //{
        //    System.Net.IPAddress FirstIP;
        //    Database db = GetDatabase("weblogConnectionString");
        //    DbCommand cmd = db.GetSqlStringCommand("SELECT TOP 1 BeginIP,EndIP,IPName FROM IPAddress WHERE GUID <> 0 order by NEWID()");
        //    using (IDataReader dr = db.ExecuteReader(cmd))
        //    {
        //        dr.Read();
        //        FirstIP = System.Net.IPAddress.Parse(dr["BeginIP"].ToString());
        //        EndIP = System.Net.IPAddress.Parse(dr["EndIP"].ToString());
        //        UserID = dr["IPName"].ToString();
        //    }
        //    return FirstIP;
        //}
        public string GetRandomIPData(out string EndIP, out string UserID)
        {
            string FirstIP="";
            Database db = GetDatabase("weblogConnectionString");
            DbCommand cmd = db.GetSqlStringCommand("SELECT TOP 1 StartIP,EndIP,UserID FROM IPAddress order by NEWID()");
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                dr.Read();
                FirstIP =dr["StartIP"].ToString();
                EndIP = dr["EndIP"].ToString();
                UserID = dr["UserID"].ToString();
            }
            return FirstIP;
        }
        public DataTable GetRandomIPData(int n)
        {
            DataSet IP = new DataSet();
            Database db = GetDatabase("weblogConnectionString");
            string cmd = "SELECT TOP " + n + " StartIP,EndIP,UserID FROM IPAddress order by NEWID()";
            IP = colleges.DataQuery.SelectRows(IP, cmd, "weblogConnectionString");
            return IP.Tables[0];
        }
        //
        //protected System.Net.IPAddress GetRandomIP(System.Net.IPAddress firstIP, System.Net.IPAddress endIP)
        //{
        //    string[] firstIPList = firstIP.ToString().Split('.');
        //    string[] endIPList = endIP.ToString().Split('.');

        //    int a = Convert.ToInt32(firstIPList[0]) * 256 * 256 * 256 + Convert.ToInt32(firstIPList[1]) * 256 * 256 + Convert.ToInt32(firstIPList[2]) * 256 + Convert.ToInt32(firstIPList[3]);
        //    int b = Convert.ToInt32(endIPList[0]) * 256 * 256 * 256 + Convert.ToInt32(endIPList[1]) * 256 * 256 + Convert.ToInt32(endIPList[2]) * 256 + Convert.ToInt32(endIPList[3]);
        //    int c = new Random().Next(b - a) + a;
        //    string ip = (c / (256 * 256 * 256) % 256).ToString() + "." + (c / (256 * 256) % 256).ToString() + "." + ((c / 256) % 256).ToString() + "." + (c % 256).ToString();
        //    Console.Write(ip);
        //    return System.Net.IPAddress.Parse(ip);
        //}
        protected string GetRandomIP(string firstIP, string endIP)
        {
            firstIP = FormatIP(firstIP);
            endIP = FormatIP(endIP);
            string[] firstIPList = firstIP.Split('.');
            string[] endIPList = endIP.Split('.');
            //long ta = long.Parse(firstIPList[0]) * 256 * 256 * 256;
            //long tb = long.Parse(firstIPList[1]) * 256 * 256;
            //long tc = long.Parse(firstIPList[2]) * 256;
            //long td = long.Parse(firstIPList[3]);
            //long a = ta + tb + tc;
            long a = long.Parse(firstIPList[0]) * 256 * 256 * 256 + long.Parse(firstIPList[1]) * 256 * 256 + long.Parse(firstIPList[2]) * 256 + long.Parse(firstIPList[3]);
            long b = long.Parse(endIPList[0]) * 256 * 256 * 256 + long.Parse(endIPList[1]) * 256 * 256 + long.Parse(endIPList[2]) * 256 + long.Parse(endIPList[3]);
            long c = NextLong(0, b - a) + a;
            string ip = (c / (256 * 256 * 256) % 256).ToString() + "." + (c / (256 * 256) % 256).ToString() + "." + ((c / 256) % 256).ToString() + "." + (c % 256).ToString();
            Console.Write(ip);
            return IPTo3Digit(ip);
        }
        public static long NextLong(long minValue, long maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("minValue is great than maxValue", "minValue");
            }
            Random random = new Random();
            long num = maxValue - minValue;
            return minValue + (long)(random.NextDouble() * num);
        }
        protected string IPTo3Digit(string IP)
        {
            string ThreeDigitIP = "";
            string[] split = IP.Split(new Char[] { '.' });
            for (int i = 0; i < 4; i++)
            {
                while (split[i].Length < 3)
                {
                    split[i] = "0" + split[i];
                }
                ThreeDigitIP += '.' + split[i];
            }
            return ThreeDigitIP.Substring(1, ThreeDigitIP.Length - 1);
        }
        protected string FormatIP(string IP)
        {
            byte[] ips = Array.ConvertAll<string, byte>(IP.Split('.'), Convert.ToByte);
            string str = string.Join(".", Array.ConvertAll<byte, string>(ips, Convert.ToString));
            return str;
        }
        //IP ED
        // toolkit
        public static Database GetDatabase()
        {
            return DatabaseFactory.CreateDatabase();
        }

        public static Database GetDatabase(string sName)
        {
            return DatabaseFactory.CreateDatabase(sName);
        }

        public static DbConnection GetConnection(Database database)
        {
            DbConnection cn = database.CreateConnection();
            cn.Open();
            return cn;
        }

        public static void CloseConnection(DbConnection cn)
        {
            if (cn.State != ConnectionState.Closed)
            {
                cn.Close();
            }
        }

        public static string Decrypt(string sSrc)
        {
            byte[] rgbKey = new byte[] { 0x2a, 0x10, 0x5d, 0x9c, 0x4e, 4, 0xda, 0x20 };
            byte[] rgbIV = new byte[] { 0x37, 0x67, 0xf6, 0x4f, 0x24, 0x63, 0xa7, 3 };
            int index = 0;
            int length = 0;
            bool flag = true;
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = Convert.FromBase64String(sSrc);
            length = buffer.Length;
            int num3 = length - 0x11;
            byte[] buffer5 = new byte[num3 + 1];
            while (index <= num3)
            {
                buffer5[index] = buffer[index];
                index++;
            }
            index = num3 + 1;
            byte[] buffer2 = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(buffer5);
            for (num3 = 0; index < length; num3++)
            {
                if (buffer2[num3] != buffer[index])
                {
                    flag = false;
                    break;
                }
                index++;
            }
            if (flag)
            {
                string str;
                MemoryStream stream2 = new MemoryStream(buffer5);
                CryptoStream stream = new CryptoStream(stream2, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
                stream.Close();
                stream.Clear();
                reader.Close();
                stream2.Close();
                return str;
            }
            return new UserDefinedException("ApplicationToolkit: Decrypt", 120L).ToString();
        }
        //
        public void Dispose()
        {

        }
    }
}
