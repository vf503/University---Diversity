using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace colleges.CodeFiles
{
    public class CategoryNode
    {
        public string id;
        public string alias;
        public string title;
        public string note;
        public List<CategoryNode> children = new List<CategoryNode>();
        public int ChildrenCount;
        public CategoryNode(string Id,string Alias, string Title,string Note)
        {
            this.id = Id;
            this.alias = Alias;
            this.title = Title;
            this.note = Note;
            //this.children = GetChildren(this.id);
            this.ChildrenCount = GetChildrenCount(this.id);
        }
        //
        private bool IsHaveChild(string Id)
        {
            string sql = "select YIndex from CategoryNodePosition where CategoryGuid= '" + Id + "'";
            DataSet ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            int Yindex = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
            //
            sql = @"select Count(c.CategoryGUID) from Category c join CategoryNodePosition cn 
on c.CategoryGUID=cn.CategoryGUID where cn.CategoryPath like '%" + Id + "%' and YIndex =" + (Yindex + 1) + "";
            ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            int count = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
            bool exist = (count > 0) ? true : false;
            return exist;
        }
        public List<CategoryNode> GetChildren(string Id)
        {
            string sql = "select YIndex from CategoryNodePosition where CategoryGuid= '" + Id + "'";
            DataSet ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            int Yindex = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString()) + 1;

            sql = @"select c.CategoryName,c.CategoryGUID,c.CategoryCategoryAlias,c.Note,cn.CategoryPath from Category c join CategoryNodePosition cn 
on c.CategoryGUID=cn.CategoryGUID where cn.CategoryPath like '%" + Id + "%' and YIndex=" + Yindex + "order by XIndex";
            ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            List<CategoryNode> children = new List<CategoryNode>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CategoryNode child = new CodeFiles.CategoryNode(dr["CategoryGUID"].ToString(), dr["CategoryAlias"].ToString(), dr["CategoryName"].ToString(), dr["Note"].ToString());
                children.Add(child);
            }
            return children;
        }
        public int GetChildrenCount(string Id)
        {
            string sql = "select YIndex from CategoryNodePosition where CategoryGuid= '" + Id + "'";
            DataSet ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            int Yindex = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString()) + 1;

            sql = @"select count(c.CategoryGUID) from Category c join CategoryNodePosition cn 
on c.CategoryGUID=cn.CategoryGUID where cn.CategoryPath like '%" + Id + "%' and YIndex=" + Yindex;
            ds = new DataSet();
            ds = DataQuery.SelectRows(ds, sql, "zjspccmConnectionString");
            return Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
        }
    }
}