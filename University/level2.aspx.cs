using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace colleges
{
    public partial class level2 : System.Web.UI.Page
    {
        public string url;
        public int BannerCount = 0;
        public string LeftPicAliasText;
        public String LeftPicLv2AliasText;
        // 主列表栏目名

        // 主列表栏连接名
        protected void Level2MainRight_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label CategoryAliasLabel = (Label)e.Item.FindControl("CurrentLv2Alias");
            string CurrentLv2Alias = CategoryAliasLabel.Text.ToString();
            WebModules.CategoryLv2List CategoryLv2List = new WebModules.CategoryLv2List(CurrentLv2Alias, 1, 5);
            ListView Lv2ListPic = (ListView)e.Item.FindControl("Lv2ListPic");
            ListView Lv2ListText = (ListView)e.Item.FindControl("Lv2ListText");
            Lv2ListPic.DataSource = CategoryLv2List.PicInfo;
            Lv2ListPic.DataBind();
            Lv2ListText.DataSource = CategoryLv2List.TextInfo;
            Lv2ListText.DataBind();
            Label Lv2ListTitle = (Label)e.Item.FindControl("Lv2ListTitle");
            HyperLink Lv2ListLink = (HyperLink)e.Item.FindControl("Lv2ListLink");
            Lv2ListTitle.Text = DataQuery.GetNameByCategoryAlias(CurrentLv2Alias);
            Lv2ListLink.NavigateUrl = "level3.aspx?alias=" + CurrentLv2Alias + "&IsChild=0";
            BannerCount++;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";
            string ChannelAlias = Request.QueryString["alias"].ToString();
            string ChannelTitle = DataQuery.GetNameByCategoryAlias(ChannelAlias);
            // 推荐
            String CommendAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "推荐");
            string CommendGuid = DataQuery.CategoryAliasToID(CommendAlias);
            DataTable CommendCourses = new DAL.Article().GetArticleList(CommendGuid, false, 12);
            CommendList.DataSource = CommendCourses;
            CommendList.DataBind();
            //排行
            //DataTable HotListDataSrc = DataQuery.GetHotList();
            //HotList.DataSource = HotListDataSrc;
            //HotList.DataBind();

            // 主列表
            String CategoryLv2Alias = DataQuery.GetChannelAliasByName(ChannelAlias, "栏目");
            string Version= ConfigurationManager.AppSettings["Version_Mark"];
            DataTable GetSubCategories = DataQuery.GetSubCategoriesApart(CategoryLv2Alias,Version);
            Level2MainRight.DataSource = GetSubCategories;
            Level2MainRight.DataBind();
            Image Level2MainBanner = (Image)FindControl<Image>("Level2MainBanner");
            Level2MainBanner.ImageUrl = "images/AD" + ChannelAlias + ".gif";

            //LeftPic
            LeftPicAliasText = ConfigurationManager.AppSettings["ChannelPicCategory"];
            LeftPicLv2AliasText = DataQuery.GetChannelAliasByName(LeftPicAliasText,ChannelTitle);
            Level2LeftPicBot.ImageUrl = "ShowBytePic.aspx?Title=底部图片&Alias=" + LeftPicLv2AliasText;
            int LeftPicCount = (GetSubCategories.Rows.Count-6)/2;
            String  LeftPicAlias = DataQuery.GetChannelAliasByName(ChannelAlias, "图片专题");
            DataTable LeftPicGetSubCategories = DataQuery.GetSubCategories(LeftPicAlias,LeftPicCount.ToString());
            LeftPicList.DataSource = LeftPicGetSubCategories;
            LeftPicList.DataBind();
            //Level2LeftPic1.ImageUrl = "images/" + ChannelAlias + "_1.jpg";
            //int SubCategoriesCount = GetSubCategories.Rows.Count;
            //if (SubCategoriesCount >= 10)
            //{
            //    Level2LeftPic2.Visible = true;
            //    Level2LeftPic2.ImageUrl = "images/" + ChannelAlias + "_2.jpg";
            //}
            //if (SubCategoriesCount >= 12)
            //{
            //    Level2LeftPic3.Visible = true;
            //    Level2LeftPic3.ImageUrl = "images/" + ChannelAlias + "_3.jpg";
            //}
            //if (SubCategoriesCount >= 14)
            //{
            //    Level2LeftPic4.Visible = true;
            //    Level2LeftPic4.ImageUrl = "images/" + ChannelAlias + "_4.jpg";
            //}
        }
        //
        public string GetLeftPicTextByTitle(string Title,bool IsEnd)
        {
            string AllText = DataQuery.GetText(LeftPicLv2AliasText, Title);
            if (AllText != null)
            {
                AllText = AllText.Replace("<段落><![CDATA[", "");
                AllText = AllText.Replace("]]></段落>", "");
                //return AllText;
                string[] AllTextArray = AllText.Split(';');
                if (IsEnd == false)
                {
                    return AllTextArray[0].ToString();
                }
                else
                {
                    if (AllTextArray.Length > 1)
                    {
                        return AllTextArray[1].ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }
        public T FindControl<T>(string id) where T : Control
        {
            return FindControl<T>(Page, id);
        }
        public static T FindControl<T>(Control startingControl, string id) where T : Control
        {
            // 取得 T 的預設值，通常是 null
            T found = default(T);

            int controlCount = startingControl.Controls.Count;

            if (controlCount > 0)
            {
                for (int i = 0; i < controlCount; i++)
                {
                    Control activeControl = startingControl.Controls[i];
                    if (activeControl is T)
                    {
                        found = startingControl.Controls[i] as T;
                        if (string.Compare(id, found.ID, true) == 0) break;
                        else found = null;
                    }
                    else
                    {
                        found = FindControl<T>(activeControl, id);
                        if (found != null) break;
                    }
                }
            }
            return found;

        }
    }
}
