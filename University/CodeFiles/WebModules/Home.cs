using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

namespace colleges.WebModules
{
    // A
    public class HomeChannelListA
    {
        public DataTable SubSortTable = new DataTable();
        public DataTable PicInfo;
        public DataTable TextInfoL;
        public DataTable TextInfoR;

        public HomeChannelListA(string Alias)
        {
            Alias = DataQuery.GetChannelAliasByName(Alias, "栏目");
            SubSortTable = DataQuery.GetSubCategories(Alias,"4");
            DataTable ListTable = new DataTable();
            string Guid = DataQuery.CategoryAliasToID(Alias);
            //ListTable = DataQuery.GetArticleIndexList(Guid, "11", "desc");
            ListTable = new DAL.Article().GetArticleList(Guid, false, 11);
            if (ListTable.Rows.Count > 0)
            {
                //PicInfo = from row in ListTable.AsEnumerable() where Convert.ToInt32(row["_RowNumber"]) > 0 && Convert.ToInt32(row["_RowNumber]"]) <= 5 select row;
                var ListInfo = from row in ListTable.AsEnumerable()
                               select row;
                var PicInfoSrc = ListInfo.Take(5);
                var TextInfoLSrc = ListInfo.Skip(5).Take(3);
                var TextInfoRSrc = ListInfo.Skip(8).Take(3);
                PicInfo = PicInfoSrc.CopyToDataTable<DataRow>();
                TextInfoL = TextInfoLSrc.CopyToDataTable<DataRow>();
                TextInfoR = TextInfoRSrc.CopyToDataTable<DataRow>();
            }
            
        }
    }
    // B
    public class HomeChannelListB
    {
        public DataTable PicInfo;
        public DataTable TextInfo;

        public HomeChannelListB(string Alias, int PicNumber, int TextNumber)
        {
            Alias = DataQuery.GetChannelAliasByName(Alias, "栏目");
            DataTable ListTable = new DataTable();
            string Guid = DataQuery.CategoryAliasToID(Alias);
            ListTable = new DAL.Article().GetArticleList(Guid, false, 11);
            if (ListTable.Rows.Count > 0)
            {
                //PicInfo = from row in ListTable.AsEnumerable() where Convert.ToInt32(row["_RowNumber"]) > 0 && Convert.ToInt32(row["_RowNumber]"]) <= 5 select row;
                var ListInfo = from row in ListTable.AsEnumerable()
                               select row;
                var PicInfoSrc = ListInfo.Take(PicNumber);
                PicInfo = PicInfoSrc.CopyToDataTable<DataRow>();
                if (TextNumber != 0)
                {
                    var TextInfoLSrc = ListInfo.Skip(PicNumber).Take(TextNumber);
                    TextInfo = TextInfoLSrc.CopyToDataTable<DataRow>();
                }
            }
            
        }

    }
    public class HomeChannelListBS
    {
        public DataTable PicInfo;

        public HomeChannelListBS(string Alias, int PicNumber)
        {
            string guid = DataQuery.CategoryAliasToID(Alias);
            DataTable ListTable = new DAL.Article().GetArticleList(guid, false, PicNumber);
            
            if (ListTable.Rows.Count > 0)
            {
                var ListInfo = from row in ListTable.AsEnumerable()
                               select row;
                var PicInfoSrc = ListInfo.Take(PicNumber);
                PicInfo = PicInfoSrc.CopyToDataTable<DataRow>();
            }

        }

    }
    // C
    public class HomeChannelListC
    {
        public DataTable CategoryInfo;
        public int count;

        public HomeChannelListC(string Alias)
        {
            Alias = DataQuery.GetChannelAliasByName(Alias, "栏目");
            string Version = ConfigurationManager.AppSettings["Version_Mark"];
            CategoryInfo = DataQuery.GetSubCategoriesApart(Alias, Version, "8");
            count = CategoryInfo.Rows.Count;
        }

    }
    // D
    public class HomeChannelListD
    {
        public DataTable CategoryInfo;

        public HomeChannelListD(string Alias)
        {
            Alias = DataQuery.GetChannelAliasByName(Alias, "栏目");
            CategoryInfo = DataQuery.GetSubCategories(Alias, "6");

        }

    }
}
