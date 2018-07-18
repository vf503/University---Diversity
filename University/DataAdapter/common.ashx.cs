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
using System.Text;

namespace colleges.DataAdapter
{
    /// <summary>
    /// common 的摘要说明
    /// </summary>
    public class common : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetLastModified(DateTime.Now);
            context.Response.Cache.SetExpires(DateTime.Now.AddHours(2));
            context.Response.AddHeader("Access-Control-Allow-Origin","*");
            //context.Response.Write("Hello World");

            //Request
            StreamReader sr = new StreamReader(HttpContext.Current.Request.InputStream);
            string strReq = sr.ReadToEnd();

            //Test
            //JObject Req = new JObject(
            //          new JProperty("ChannelAlias", "gxchannel1")
            //         );
            //string strReq = Req.ToString();

            #region MainTree
            if (HttpContext.Current.Request["method"] == "MainTreeZtree")
            {
                HttpRequest Request = context.Request;
                string ParentId="";
                if (strReq == "")
                {
                    ParentId = "8274501d5e094996be8868a0f1fd48fb";
                }
                else
                {
                    ParentId = DataQuery.CategoryAliasToID(context.Request["id"]);
                    if (ParentId == "" || ParentId is null)
                    { ParentId = Request.QueryString["RootId"].ToString(); }
                    else { }
                }
                JArray categories = new JArray();
                //
                string sql = "select YIndex from CategoryNodePosition where CategoryGuid= '" + ParentId + "'";
                DataSet ds = new DataSet();
                ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
                int Yindex = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString()) + 1;
                //
                sql = @"select c.CategoryName,c.CategoryGUID,c.CategoryAlias,c.Note,cn.CategoryPath from Category c join CategoryNodePosition cn 
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
            else if (HttpContext.Current.Request["method"] == "MainTreeNodeZtree")
            {
                HttpRequest Request = context.Request;
                JArray categories = new JArray();
                //
                string id = "";
                if (Request.QueryString["id"].ToString() == "")
                {

                }
                else
                {
                    id = Request.QueryString["id"].ToString();
                }
                string sql = @"select cn.CategoryPath from Category c join CategoryNodePosition cn 
on c.CategoryGUID=cn.CategoryGUID where c.CategoryAlias= '" + id + "'";
                DataSet ds = new DataSet();
                ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
                //
                StringBuilder ret=new StringBuilder();
                string[] sArray = ds.Tables[0].Rows[0][0].ToString().Split('/');
                foreach (string i in sArray)
                {
                    if (i != "")
                    {
                        sql = @"select CategoryAlias from Category where CategoryGUID= '" + i + "'";
                        ds = new DataSet();
                        ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
                        if (ds.Tables[0].Rows[0][0].ToString() != "%zjsp%" && ds.Tables[0].Rows[0][0].ToString() != "%shicategory%")
                        { ret.Append(ds.Tables[0].Rows[0][0].ToString() + ";"); }                      
                    }
                }
                //
                context.Response.Write(ret.ToString());
            }
            #endregion MainTree
            #region Dic
            else if (HttpContext.Current.Request["method"] == "DicTreeZtree")
            {
                HttpRequest Request = context.Request;
                JArray categories = new JArray();
                //
                string ParentId = "1";
                if (strReq=="")
                {

                }
                else
                {
                    ParentId = context.Request["id"];
                    if (ParentId == "" || ParentId is null)
                    { ParentId = "1"; }
                    else { }
                }

                //string ParentId = Request.QueryString["id"].ToString();
                //
                string sql = @"select d.Name,d.GUID,d.Note from DicKeys d where d.GUID = '"+ ParentId + "'  order by XOrder";
                DataSet ds = new DataSet();
                ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
                //
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    categories = AddDicChild(ParentId);
                }
                //
                context.Response.Write(categories.ToString());
            }
            else if (HttpContext.Current.Request["method"] == "DicTreeNodeZtree")
            {
                HttpRequest Request = context.Request;
                JArray categories = new JArray();
                //
                string id = "";
                if (Request.QueryString["id"].ToString() == "")
                {

                }
                else
                {
                    id = Request.QueryString["id"].ToString();
                }
                string sql = @"select d.GUIDPath from DicKeys d where d.GUID = '" + id + "'";
                DataSet ds = new DataSet();
                ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
                //
                StringBuilder ret = new StringBuilder();
                string[] sArray = ds.Tables[0].Rows[0][0].ToString().Split('/');
                foreach (string i in sArray)
                {
                    if (i != ""&& i!="1")
                    {
                        ret.Append(i + ";");
                    }
                }
                //
                context.Response.Write(ret.ToString());
            }
            #endregion Dic
            else
            {   
                string id = context.Request["id"];
                context.Response.Write(strReq + ";" + id);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public JObject AddChild(CategoryNode node)
        {
            JObject JNode = new JObject();
            if (node.ChildrenCount > 0)
            {
                JNode = new JObject(
                    new JProperty("id", node.alias),
                    new JProperty("name", node.title),
                    new JProperty("isParent", "true"),
                    new JProperty("nocheck", "true")
                    );
            }
            else
            {
                JNode = new JObject(
                new JProperty("id", node.alias),
                new JProperty("name", node.title),
                new JProperty("isParent", "false")
                );
            }
            return JNode;
        }
        public JArray AddDicChild(string Guid)
        {
            string sql = @"select d.Name,d.GUID,d.Note from DicKeys d where d.ParentGUID = '" + Guid + "' order by XOrder";
            DataSet ChildDs = new DataSet();
            ChildDs = DataQuery.SelectRows(ChildDs, sql, "zjspccmConnectionString");
            //
            sql = @"select d.Name,d.GUID,d.Note from DicKeys d where d.GUID = '" + Guid +"'";
            DataSet ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            //
            JArray JNodeChildren = new JArray();
            if (ChildDs.Tables[0].Rows.Count > 0)
            {  
                foreach (DataRow dr in ChildDs.Tables[0].Rows)
                {
                    bool IsHaveChild = IsDicHaveChild(dr["GUID"].ToString());
                    Object JChild;
                    if (IsHaveChild)
                    {
                        JChild = new JObject(
                        new JProperty("id", dr["GUID"]),
                        new JProperty("name", dr["Name"]),
                        new JProperty("isParent", "true"),
                        new JProperty("nocheck", "true")
                         );
                    }
                    else
                    {
                        JChild = new JObject(
                        new JProperty("id", dr["GUID"]),
                        new JProperty("name", dr["Name"]),
                         new JProperty("isParent", "false")
                         );
                    }
                    JNodeChildren.Add(JChild);
                }
            }
            else
            { }
            return JNodeChildren;
        }
        private bool IsDicHaveChild(string Id)
        {
            string sql = "select count(d.GUID) from DicKeys d where d.ParentGUID = '"+ Id + "'";
            DataSet ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            int count = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
            bool exist = (count > 0) ? true : false;
            return exist;
        }
    }
}