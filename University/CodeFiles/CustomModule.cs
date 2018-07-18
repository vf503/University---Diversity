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

namespace MyHttpModule
{
    /// <summary>
    ///CustomModule 的摘要说明
    /// </summary>

    public class CustomModule : IHttpModule
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
            string url = context.Request.RawUrl.ToString();
            //获取客户端访问IP地址
            string IPAddress = context.Request.UserHostAddress;
            //获取客户端浏览器等信息
            string BrownAgentcontext = context.Request.Browser.Type.ToString();
            //获取请求时间
            DateTime RequestTime = context.Timestamp;
            //获取请求的页面信息
            string RequestPage = context.Request.Path.Substring(context.Request.Path.LastIndexOf("/") + 1);

            //是否需要记录

            if (RequestPage.ToLower() == "showvideo.aspx")//*CC*
            {
                string RequestName = string.Empty;

                ArrayList ChannelGuidArrayList = new ArrayList();//*CC*
                GetRequestInfo(ref RequestName, ref ChannelGuidArrayList, context); //获取请求内容名称及父级结构//*CC*

                string[] ChannelGuidList = (string[])ChannelGuidArrayList.ToArray(typeof(string));//*CC*

                if (RequestName != string.Empty || ChannelGuidList[0] != string.Empty)
                {
                    int j = 0;
                    foreach (string i in ChannelGuidList)
                    {
                        if (ChannelGuidList[j] != null)
                        {
                            RequestTime = RequestTime.AddMinutes(-j);
                            SaveRequestInfo(IPAddress, url, BrownAgentcontext, RequestTime, RequestName, ChannelGuidList[j], host); //保存请求信息
                        }
                        j++;
                    }
                }
            }
        }

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

        public static string GetEreaByIP(string ip)
        {
            string Erea = string.Empty;
            long ipNum = IpConvert(ip);

            Database db = GetDatabase("weblogConnectionString");
            DbCommand cmd = db.GetSqlStringCommand("SELECT GUID from IPAddress where AdjEndIP = (select min(AdjEndIP) from IPAddress where AdjEndIP >= @IPNum) and AdjBeginIP=(select max(AdjBeginIP) from IPAddress where AdjBeginIP <= @IPNum)");
            db.AddInParameter(cmd, "@IPNum", DbType.Int64, ipNum);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    Erea = System.Convert.ToString(dr.GetValue(0));
                }
            }
            //ip地址没有区域存储
            //if (Erea == string.Empty) { Erea = "156"; }
            return Erea;
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
        //
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
        //
        //
        public void Dispose()
        {

        }
    }
}

