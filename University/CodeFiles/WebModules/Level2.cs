using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace colleges.WebModules
{
    public class CategoryLv2List
    {
        public DataTable PicInfo;
        public DataTable TextInfo;

        public CategoryLv2List(string Alias, int PicNumber, int TextNumber)
        {
            DataTable ListTable = new DataTable();
            string Guid = DataQuery.CategoryAliasToID(Alias);
            int count = PicNumber + TextNumber;
            ListTable = new DAL.Article().GetArticleList(Guid, false, count);
            if (ListTable.Rows.Count > 0)
            {
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
}
