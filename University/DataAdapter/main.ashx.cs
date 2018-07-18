using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Data;

namespace colleges.DataAdapter
{
    /// <summary>
    /// main 的摘要说明
    /// </summary>
    public class main : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            //Request
            StreamReader sr = new StreamReader(HttpContext.Current.Request.InputStream);
            string strReq = sr.ReadToEnd();

            //Test
            //JObject Req = new JObject(
            //          new JProperty("ChannelAlias", "gxchannel1")
            //         );
            //string strReq = Req.ToString();


            #region Level2FocusPic
            if (HttpContext.Current.Request["method"] == "level2focuspic")
            {
                string PicPath = "";
                JObject o = JObject.Parse(strReq);
                string ChannelAlias = (string)o["ChannelAlias"];
                String GuideAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "导视");
                String GuideGuid = DataQuery.CategoryAliasToID(GuideAlias);
                DataTable GuideCourses = new DAL.Article().GetArticleList(GuideGuid, true, 5);
                JObject rss = new JObject();
                rss = new JObject(
                    //new JProperty("method", "mytask")
                    );
                JObject JCourse =
                                new JObject(
                                    new JProperty("Course",
                                        new JArray(
                                            from cc in GuideCourses.AsEnumerable()
                                            orderby cc["XIndexTime"] descending
                                            select new JObject(
                                            //new JProperty("CustomProjectId", oc.CustomProjectId),
                                            //new JProperty("CustomProjectNo", oc.No),
                                            //new JProperty("Title", oc.Title),
                                            //new JProperty("SendingDate", oc.SendingDate.ToString("D")),
                                            //new JProperty("Lecturer", oc.Lecturer),
                                            //new JProperty("ProgressText", oc.ProgressText)
                                            from col in cc.Table.Columns.Cast<DataColumn>()
                                            select new JProperty(
                                                col.ColumnName, cc[col.Ordinal].ToString()
                                                ),
                                            new JProperty("Url", GetUrl(cc["ArticleGUID"].ToString(),out PicPath)),
                                            new JProperty("PicPath", PicPath)
                                                              )
                                                  )
                                                 )
                                            );
                rss.Merge(JCourse, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Concat
                }
                );
                context.Response.Write(rss.ToString());
            }
            #endregion Level2FocusPic
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string GetUrl(string sArticleGUID,out string PicPath)
        {
            string indexPath = new DAL.Article().GetArticlePath(sArticleGUID);
            string CourseID = indexPath.Split('/')[0];
            string[] pathList = { "http://vodedu.cei.com.cn//ccmfile3/", "http://vodedu.cei.com.cn//ccmfile2/", "http://vodedu.cei.com.cn//ccmfile/" };
            PicPath = "";
            for (int i = 0; i < pathList.Length; i++)
            {
                if (CheckUri(pathList[i] + indexPath) == true)
                {
                    indexPath = pathList[i] + indexPath;
                    PicPath = pathList[i] + CourseID+"/355x235.png";
                    break;
                }
            }
            return indexPath;
        }

        public static bool CheckUri(string strUri)
        {
            try
            {
                System.Net.HttpWebRequest.Create(strUri).GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}