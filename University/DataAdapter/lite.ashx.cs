using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Data;
using colleges.EF;
using colleges.CodeFiles;
using System.Web.Caching;

namespace colleges.DataAdapter
{
    /// <summary>
    /// lite 的摘要说明
    /// </summary>
    public class lite : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetLastModified(DateTime.Now);
            context.Response.Cache.SetExpires(DateTime.Now.AddHours(2));
            //context.Response.Write("Hello World");

            //Request
            StreamReader sr = new StreamReader(HttpContext.Current.Request.InputStream);
            string strReq = sr.ReadToEnd();

            //Test
            //JObject Req = new JObject(
            //          new JProperty("ChannelAlias", "gxchannel1")
            //         );
            //string strReq = Req.ToString();

            #region Index
            if (HttpContext.Current.Request["method"] == "index")
            {
                if (context.Cache["index"] == null)  //判断是否存在缓存
                {
                    ZjspccmEntities DB = new ZjspccmEntities();
                    JObject rss = new JObject();
                    //
                    JObject JCategoryGroup = JObject.FromObject(new
                    {
                        front = new
                        {
    //                        sort =
    //(from c in DB.Categories.ToArray()
    // join cn in DB.CategoryNodePositions.ToArray()
    //  on c.CategoryGUID equals cn.CategoryGUID
    // orderby cn.XIndex ascending
    // where cn.CategoryPath.Contains("ca07a19943b846a58c8884222d3ed8e7")
    // && cn.XIndex == 5
    // select new
    // {
    //     title = c.CategoryName,
    //     id = c.CategoryGUID

    // }).Take(4),
                            list =
    (from c in DB.Categories.ToArray()
     join cn in DB.CategoryNodePositions.ToArray()
     on c.CategoryGUID equals cn.CategoryGUID
     where cn.CategoryPath.Contains("14073c0fbafe492aa2726a2034b1d454")
    && CheckChild(c.CategoryGUID) == false
     orderby c.CreateTime descending
     select new
     {
         title = c.CategoryName,
         id = c.CategoryGUID,
         PicUrl = GetGroupPicUrl(c.CategoryGUID)
     }).Take(5)
                        },
                        economic = new
                        {
    //                        sort =
    //(from c in DB.Categories.ToArray()
    // join cn in DB.CategoryNodePositions.ToArray()
    //  on c.CategoryGUID equals cn.CategoryGUID
    // orderby cn.XIndex ascending
    // where cn.CategoryPath.Contains("f884244d69204739b7fe09b9129141b2")
    // && cn.XIndex == 6
    // select new
    // {
    //     title = c.CategoryName,
    //     id = c.CategoryGUID

    // }).Take(4),
                            list =
    (from a in DB.ArticleCurrents.ToArray()
     join aoc in DB.ArticleCurrentOfCategoryTops.ToArray()
     on a.ArticleGUID equals aoc.ArticleGUID
     join cn in DB.CategoryNodePositions.ToArray()
     on aoc.CategoryGUID equals cn.CategoryGUID
     orderby a.CreateTime descending
     where aoc.CategoryGUID.Contains("f884244d69204739b7fe09b9129141b2")
     select new
     {
         title = a.Title,
         id = a.ArticleGUID,
         author = a.Author
     }).Take(12)
                        },
                        manageClass = new
                        {
    //                        sort =
    //(from c in DB.Categories.ToArray()
    // join cn in DB.CategoryNodePositions.ToArray()
    //  on c.CategoryGUID equals cn.CategoryGUID
    // orderby cn.XIndex ascending
    // where cn.CategoryPath.Contains("758f3c84f2f440ad89ecc50635629488")
    // && cn.XIndex == 6
    // select new
    // {
    //     title = c.CategoryName,
    //     id = c.CategoryGUID

    // }).Take(4),
                            list =
    (from a in DB.ArticleCurrents.ToArray()
     join aoc in DB.ArticleCurrentOfCategoryTops.ToArray()
     on a.ArticleGUID equals aoc.ArticleGUID
     join cn in DB.CategoryNodePositions.ToArray()
     on aoc.CategoryGUID equals cn.CategoryGUID
     orderby a.CreateTime descending
     where aoc.CategoryGUID.Contains("758f3c84f2f440ad89ecc50635629488")
     select new
     {
         title = a.Title,
         id = a.ArticleGUID,
         author = a.Author
     }).Take(12)
                        },
                        attainment = new
                        {
    //                        sort =
    //(from c in DB.Categories.ToArray()
    // join cn in DB.CategoryNodePositions.ToArray()
    //  on c.CategoryGUID equals cn.CategoryGUID
    // orderby cn.XIndex ascending
    // where cn.CategoryPath.Contains("31f8803471b4430bbd533752a9f41b86")
    // && cn.XIndex == 6
    // select new
    // {
    //     title = c.CategoryName,
    //     id = c.CategoryGUID

    // }).Take(4),
                            list =
    (from a in DB.ArticleCurrents.ToArray()
     join aoc in DB.ArticleCurrentOfCategoryTops.ToArray()
     on a.ArticleGUID equals aoc.ArticleGUID
     join cn in DB.CategoryNodePositions.ToArray()
     on aoc.CategoryGUID equals cn.CategoryGUID
     orderby a.CreateTime descending
     where aoc.CategoryGUID.Contains("31f8803471b4430bbd533752a9f41b86")
     select new
     {
         title = a.Title,
         id = a.ArticleGUID,
         author = a.Author,
         PicUrl = DataQuery.GetCoursePicPath(a.ArticleGUID, "/", "001.jpg")
     }).Take(9)
                        },
                        teacherOnline =
                       new JArray(
                       (from a in DB.ArticleCurrents.ToArray()
                        join aoc in DB.ArticleCurrentOfCategoryAlls.ToArray()
                    on a.ArticleGUID equals aoc.ArticleGUID
                        orderby a.CreateTime descending
                        where aoc.CategoryGUID.Equals("4e927be1671642ea8c00d4ec789fa72c")
                        select new JObject(
                         new JProperty("id", a.ArticleGUID),
                         new JProperty("title", a.Title),
                         new JProperty("author", a.Author),
                         new JProperty("position", a.Area),
                         new JProperty("date", a.CreateTime.ToString("yyyy-MM-dd")),
                         new JProperty("img", DataQuery.GetCoursePicPath(a.ArticleGUID, "/", "001.jpg"))
                         )
                            ).Take(2)
                       ),
                        organition =
                       new JArray(
                       (from a in DB.ArticleCurrents.ToArray()
                        join aoc in DB.ArticleCurrentOfCategoryTops.ToArray()
                    on a.ArticleGUID equals aoc.ArticleGUID
                        orderby a.CreateTime descending
                        where aoc.CategoryGUID.Equals("0cb86341b1514ac6ba3552f9716cd0c8")
                        select new JObject(
                         new JProperty("id", a.ArticleGUID),
                         new JProperty("title", a.Title),
                         new JProperty("author", a.Author),
                         new JProperty("position", a.Area),
                         new JProperty("date", a.CreateTime.ToString("yyyy-MM-dd")),
                         new JProperty("img", DataQuery.GetCoursePicPath(a.ArticleGUID, "/", "001.jpg"))
                         )
                            ).Take(2)
                            )
                            
                    });
                    //
                    rss.Merge(JCategoryGroup, new JsonMergeSettings
                    {
                        MergeArrayHandling = MergeArrayHandling.Concat
                    }
                       );
                    context.Cache.Add("index", rss.ToString(), null, DateTime.Now.AddHours(3),
     TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
                else
                {

                }
                context.Response.Write(context.Cache["index"]);
            }
            #endregion Index
            #region MainTree
            else if (HttpContext.Current.Request["method"] == "MainTree")
            {
                HttpRequest Request = context.Request;
                string ParentId = Request.QueryString["id"].ToString();
                JArray categories = new JArray();
                //
                string sql = "select YIndex from CategoryNodePosition where CategoryGuid= '" + ParentId + "'";
                DataSet ds = new DataSet();
                ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
                int Yindex = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString()) + 1;
                //
                sql = @"select c.CategoryName,c.CategoryGUID,c.Note,cn.CategoryPath from Category c join CategoryNodePosition cn 
on c.CategoryGUID=cn.CategoryGUID where cn.CategoryPath like '%" + ParentId + "%' and YIndex=" + Yindex + "order by XIndex";
                ds = new DataSet();
                ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
                //
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CategoryNode node = new CategoryNode(dr["CategoryGUID"].ToString(), dr["CategoryAlias"].ToString(), dr["CategoryName"].ToString(), dr["Note"].ToString());
                    categories.Add(AddChild(node));
                }
                //
                context.Response.Write(categories.ToString());
            }
            #endregion MainTree
            #region GruopTree
            else if (HttpContext.Current.Request["method"] == "GroupTree")
            {
                HttpRequest Request = context.Request;
                string ParentId = Request.QueryString["id"].ToString();
                JArray categories = new JArray();
                //
                string sql = "select YIndex from CategoryNodePosition where CategoryGuid= '" + ParentId + "'";
                DataSet ds = new DataSet();
                ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
                int Yindex = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString()) + 1;
                //
                sql = @"select c.CategoryName,c.CategoryGUID,c.Note,cn.CategoryPath from Category c join CategoryNodePosition cn 
on c.CategoryGUID=cn.CategoryGUID where cn.CategoryPath like '%" + ParentId + "%' and YIndex=" + Yindex + "order by XIndex";
                ds = new DataSet();
                ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
                //
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CategoryNode node = new CategoryNode(dr["CategoryGUID"].ToString(), dr["CategoryAlias"].ToString(),dr["CategoryName"].ToString(), dr["Note"].ToString());
                    categories.Add(AddGroupChild(node));
                }
                //
                context.Response.Write(categories.ToString());
            }
            #endregion GruopTree
            #region Course
            else if (HttpContext.Current.Request["method"] == "course")
            {
                HttpRequest Request = context.Request;
                string id = Request.QueryString["id"].ToString();
                string order= context.Request.Params["orderNum"].ToString();
                ZjspccmEntities DB = new ZjspccmEntities();
                JArray JCourse;
                if (order == "descending")
                {
                    JCourse = new JArray(
                        from a in DB.ArticleCurrents.ToArray()
                        join aoc in DB.ArticleCurrentOfCategoryAlls.ToArray() on a.ArticleGUID equals aoc.ArticleGUID
                        join cn in DB.CategoryNodePositions.ToArray() on aoc.CategoryGUID equals cn.CategoryGUID
                        where cn.CategoryPath.Contains(id)
                        //where aoc.CategoryGUID.Equals(id)
                        orderby a.CreateTime descending
                        select new JObject(
                             new JProperty("id", a.ArticleGUID),
                             new JProperty("title", a.Title),
                             new JProperty("pic", DataQuery.GetCoursePicPath(a.ArticleGUID, "/", "001.jpg")),
                             new JProperty("teacher", a.Author),
                             new JProperty("length", a.PageCount),
                             new JProperty("postion", a.Area),
                             new JProperty("date", a.CreateTime.ToShortDateString())
                             )
                             );
                }
                else {
                    JCourse = new JArray(
                           from a in DB.ArticleCurrents.ToArray()
                           join aoc in DB.ArticleCurrentOfCategories.ToArray() on a.ArticleGUID equals aoc.ArticleGUID
                           join cn in DB.CategoryNodePositions.ToArray() on aoc.CategoryGUID equals cn.CategoryGUID
                           where cn.CategoryPath.Contains(id)
                           //where aoc.CategoryGUID.Equals(id)
                           orderby a.CreateTime ascending
                           select new JObject(
                                new JProperty("id", a.ArticleGUID),
                                new JProperty("title", a.Title),
                                new JProperty("pic", DataQuery.GetCoursePicPath(a.ArticleGUID, "/", "001.jpg")),
                                new JProperty("teacher", a.Author),
                                new JProperty("length", a.PageCount),
                                new JProperty("postion", a.Area),
                                new JProperty("date", a.CreateTime.ToShortDateString())
                                )
                                );
                }

                context.Response.Write(JCourse.ToString());

            }
            #endregion Course
            #region GruopCourse
            else if (HttpContext.Current.Request["method"] == "GroupCourse")
            {
                HttpRequest Request = context.Request;
                string id = Request.QueryString["id"].ToString();
                string sql = "select YIndex from CategoryNodePosition where CategoryGuid= '" + id + "'";
                DataSet ds = new DataSet();
                ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
                int Yindex = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString()) + 1;
                sql = "select top 1 CategoryGUID from CategoryNodePosition where CategoryPath like '%" + id + "%' and YIndex =" + Yindex;
                DataSet ds2 = new DataSet();
                ds2 = DataQuery.SelectRows(ds2, sql, "zjspccmConnectionString");
                string FirstChildGuid = ds2.Tables[0].Rows[0][0].ToString();
                ZjspccmEntities DB = new ZjspccmEntities();
                JArray JCourse = new JArray(
                    from c in DB.Categories.ToArray()
                    join cn in DB.CategoryNodePositions.ToArray() on c.CategoryGUID equals cn.CategoryGUID
                    where cn.CategoryPath.Contains(id)
                   && cn.YIndex == Yindex
                   && !CheckChild(c.CategoryGUID)
                    orderby c.CreateTime descending
                    select new JObject(
                         new JProperty("id", c.CategoryGUID),
                         new JProperty("title", c.CategoryName),
                         new JProperty("pic", GetGroupPicUrl(c.CategoryGUID)),
                         new JProperty("list", new JArray(
                             from a in DB.ArticleCurrents.ToArray()
                             join aoc in DB.ArticleCurrentOfCategories.ToArray() on a.ArticleGUID equals aoc.ArticleGUID
                             where aoc.CategoryGUID == c.CategoryGUID
                             orderby a.CreateTime descending
                             select new JObject(
                                 new JProperty("id", a.ArticleGUID),
                                 new JProperty("title", a.Title),
                                 new JProperty("teacher", a.Author),
                                 new JProperty("length", a.PageCount),
                                 new JProperty("postion", a.Area),
                                 new JProperty("date", a.CreateTime)
                                 )
                         )
                         )
                         )
                         );
                context.Response.Write(JCourse.ToString());

            }
            #endregion GruopCourse
            else { }


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public bool CheckChild(string Guid)
        {
            string sql = "select YIndex from CategoryNodePosition where CategoryGuid= '" + Guid + "'";
            DataSet ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            int Yindex = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
            //
            sql = @"select Count(c.CategoryGUID) from Category c join CategoryNodePosition cn 
on c.CategoryGUID=cn.CategoryGUID where cn.CategoryPath like '%" + Guid + "%' and YIndex =" + (Yindex + 1) + "";
            ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            int count = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
            bool exist = (count > 0) ? true : false;
            return exist;
        }
        public JObject AddChild(CategoryNode node)
        {
            string alias;
            if (node.note == "jingji")
            {
                alias = "gxchannel1";
            }
            else if (node.note == "guanli")
            {
                alias = "gxchannel2";
            }
            else if (node.note == "suyang")
            {
                alias = "gxchannel3";
            }
            else if (node.note == "shizheng")
            {
                alias = "gxchannel9";
            }
            else {
                alias = "gxchannel1";
            }
            JObject JNode = new JObject();
            if (node.children.Count > 0)
            {
                JArray JNodeChildren = new JArray();
                foreach (CategoryNode child in node.children)
                {
                    JObject JChild = AddChild(child);
                    JNodeChildren.Add(JChild);
                }
                JNode = new JObject(
                    new JProperty("id", node.id),
                    new JProperty("text", node.title),
                    new JProperty("href", "Level2Lite.aspx?alias="+alias+"&id="+node.id),
                    new JProperty("nodes", JNodeChildren)
                    );
            }
            else
            {
                JNode = new JObject(
               new JProperty("id", node.id),
               new JProperty("text", node.title),
               new JProperty("href", "Level2Lite.aspx?alias="+alias+"&id="+node.id)
               );
            }
            return JNode;
        }
        public JObject AddGroupChild(CategoryNode node)
        {
            JObject JNode = new JObject();
            if (node.children.Count > 0)
            {
                JArray JNodeChildren = new JArray();
                foreach (CategoryNode child in node.children)
                {
                    JObject JChild = AddGroupChild(child);
                    JNodeChildren.Add(JChild);
                }
                JNode = new JObject(
                    new JProperty("id", node.id),
                    new JProperty("text", node.title),
                    new JProperty("href", "Level2GroupLite.aspx?id=" + node.id),
                    new JProperty("nodes", JNodeChildren)
                    );
            }
            else
            {
                JNode = new JObject(
               new JProperty("id", node.id),
               new JProperty("text", node.title),
               new JProperty("href", "Level3GroupLite.aspx?id=" + node.id)
               );
            }
            return JNode;
        }
        public static string GetGroupPicUrl(string Guid)
        {
            string PicUrl = "";
            string url = "/";
            string sql = @"select top 1 a.ArticleGUID from ArticleCurrent a 
join ArticleCurrentofCategoryAll aoc on a.ArticleGUID=aoc.ArticleGUID 
join CategoryNodePosition cn on aoc.CategoryGUID=cn.CategoryGUID
where cn.CategoryPath like '%" + Guid + "%' order by a.CreateTime desc";
            DataSet ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string ArticleGuid = ds.Tables[0].Rows[0][0].ToString();
                PicUrl = DataQuery.GetCoursePicPath(ArticleGuid, url, "001.jpg");
            }
            return PicUrl;
        }
    }
}