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
    public partial class index : BasePage
    {

        public string url;
        // 主列表栏目分割线
        public int HomeChannelListALinkSplitCount = 1;
        // 栏目图片
        public int HomeChannelListCPicCount = 0;
        public int HomeChannelListC2PicCount = 0;

        // 主列表栏目分割线
        public string HomeChannelListALinkSplit(int lenth)
        {
            if (HomeChannelListALinkSplitCount <= lenth)
            {
                HomeChannelListALinkSplitCount++;
                return "|";
            }
            else
            {
                HomeChannelListALinkSplitCount = 1;
                return "";       
            }
        }
        // 栏目图片
        public int HomeChannelListOutpic(ref int count)
        {
                count++;
                return count;    
        }
        // 绑定技能、培训课件ListView(嵌套)
        protected void TabIndex1Box_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label CategoryAliasLabel = (Label)e.Item.FindControl("SkillCategoryAlias");
            string CategoryAlias = CategoryAliasLabel.Text.ToString();
            DataTable ListDataTable = new DataTable();
            string CategoryGUID = DataQuery.CategoryAliasToID(CategoryAlias);
            //ListDataTable = new DAL.Article().GetArticleListAll(CategoryGUID, false, "desc" , true, 16);  //ALL
            ListDataTable = new DAL.Article().GetArticleList(CategoryGUID, false, 16);
            if (ListDataTable.Rows.Count > 0)
            {
                
                var ListInfo = from row in ListDataTable.AsEnumerable()
                               select row;
                var LeftInfoSrc = ListInfo.Take(8);
                var  RightInfoSrc = ListInfo.Skip(8).Take(8);
                DataTable LeftInfo = LeftInfoSrc.CopyToDataTable<DataRow>();
                ListView SkillListLeft = (ListView)e.Item.FindControl("TabUlLeft");
                SkillListLeft.DataSource = LeftInfo;
                SkillListLeft.DataBind();
                if (ListDataTable.Rows.Count > 8)
                {
                    DataTable RightInfo = RightInfoSrc.CopyToDataTable<DataRow>();
                    ListView SkillListRight = (ListView)e.Item.FindControl("TabUlRight");
                    SkillListRight.DataSource = RightInfo;
                    SkillListRight.DataBind();
                }
            }
        }
        protected void TrainList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label CategoryAliasLabel = (Label)e.Item.FindControl("TrainCategoryAlias");
            string CategoryAlias = CategoryAliasLabel.Text.ToString();
            DataTable ListDataTable = new DataTable();
            string CategoryGUID = DataQuery.CategoryAliasToID(CategoryAlias);
            ListDataTable = new DAL.Article().GetArticleList(CategoryGUID, false, 4);
            ListView SkillListCourse = (ListView)e.Item.FindControl("TrainListCourse");
            SkillListCourse.DataSource = ListDataTable;
            SkillListCourse.DataBind();
        }
        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(2000);
            string ChannelListAliasA1 = ConfigurationManager.AppSettings["HomeChannelListA_CategoryAlias_1"];
            WebModules.HomeChannelListA HomeChannelListA1 = new WebModules.HomeChannelListA(ChannelListAliasA1);
            ColumnShowSort1.DataSource = HomeChannelListA1.SubSortTable;
            ColumnShowSort1.DataBind();
            ColumnVideo1.DataSource = HomeChannelListA1.PicInfo;
            ColumnVideo1.DataBind();
            ColumnOtherL1.DataSource = HomeChannelListA1.TextInfoL;
            ColumnOtherL1.DataBind();
            ColumnOtherR1.DataSource = HomeChannelListA1.TextInfoR;
            ColumnOtherR1.DataBind();
            string ChannelListATitle1 = ConfigurationManager.AppSettings["HomeChannelListA_Title_1"];
            ListATitle1.Text = ChannelListATitle1;
            ListALink1.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasA1;

            string ChannelListAliasA2 = ConfigurationManager.AppSettings["HomeChannelListA_CategoryAlias_2"];
            WebModules.HomeChannelListA HomeChannelListA2 = new WebModules.HomeChannelListA(ChannelListAliasA2);
            ColumnShowSort2.DataSource = HomeChannelListA2.SubSortTable;
            ColumnShowSort2.DataBind();
            ColumnVideo2.DataSource = HomeChannelListA2.PicInfo;
            ColumnVideo2.DataBind();
            ColumnOtherL2.DataSource = HomeChannelListA2.TextInfoL;
            ColumnOtherL2.DataBind();
            ColumnOtherR2.DataSource = HomeChannelListA2.TextInfoR;
            ColumnOtherR2.DataBind();
            string ChannelListATitle2 = ConfigurationManager.AppSettings["HomeChannelListA_Title_2"];
            ListATitle2.Text = ChannelListATitle2;
            ListALink2.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasA2;

            string ChannelListAliasA3 = ConfigurationManager.AppSettings["HomeChannelListA_CategoryAlias_3"];
            WebModules.HomeChannelListA HomeChannelListA3 = new WebModules.HomeChannelListA(ChannelListAliasA3);
            ColumnShowSort3.DataSource = HomeChannelListA3.SubSortTable;
            ColumnShowSort3.DataBind();
            ColumnVideo3.DataSource = HomeChannelListA3.PicInfo;
            ColumnVideo3.DataBind();
            ColumnOtherL3.DataSource = HomeChannelListA3.TextInfoL;
            ColumnOtherL3.DataBind();
            ColumnOtherR3.DataSource = HomeChannelListA3.TextInfoR;
            ColumnOtherR3.DataBind();
            string ChannelListATitle3 = ConfigurationManager.AppSettings["HomeChannelListA_Title_3"];
            ListATitle3.Text = ChannelListATitle3;
            ListALink3.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasA3;

            string ChannelListAliasA4 = ConfigurationManager.AppSettings["HomeChannelListA_CategoryAlias_4"];
            WebModules.HomeChannelListA HomeChannelListA4 = new WebModules.HomeChannelListA(ChannelListAliasA4);
            ColumnShowSort4.DataSource = HomeChannelListA4.SubSortTable;
            ColumnShowSort4.DataBind();
            ColumnVideo4.DataSource = HomeChannelListA4.PicInfo;
            ColumnVideo4.DataBind();
            ColumnOtherL4.DataSource = HomeChannelListA4.TextInfoL;
            ColumnOtherL4.DataBind();
            ColumnOtherR4.DataSource = HomeChannelListA4.TextInfoR;
            ColumnOtherR4.DataBind();
            string ChannelListATitle4 = ConfigurationManager.AppSettings["HomeChannelListA_Title_4"];
            ListATitle4.Text = ChannelListATitle4;
            ListALink4.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasA4;

            //右侧列表
            string ChannelListAliasB2 = ConfigurationManager.AppSettings["HomeChannelListB_CategoryAlias_2"];
            WebModules.HomeChannelListB HomeChannelListB2 = new WebModules.HomeChannelListB(ChannelListAliasB2, 1, 8);
            VerticalShowVideo2.DataSource = HomeChannelListB2.PicInfo;
            VerticalShowVideo2.DataBind();
            VerticalShowList2.DataSource = HomeChannelListB2.TextInfo;
            VerticalShowList2.DataBind();
            string ChannelListBTitle2 = ConfigurationManager.AppSettings["HomeChannelListB_Title_2"];
            ListBTitle2.Text = ChannelListBTitle2;
            ListBLink2.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasB2;

            string ChannelListAliasB3 = ConfigurationManager.AppSettings["HomeChannelListB_CategoryAlias_3"];
            WebModules.HomeChannelListB HomeChannelListB3 = new WebModules.HomeChannelListB(ChannelListAliasB3, 1, 4);
            VerticalShowVideo3.DataSource = HomeChannelListB3.PicInfo;
            VerticalShowVideo3.DataBind();
            VerticalShowList3.DataSource = HomeChannelListB3.TextInfo;
            VerticalShowList3.DataBind();
            string ChannelListBTitle3 = ConfigurationManager.AppSettings["HomeChannelListB_Title_3"];
            ListBTitle3.Text = ChannelListBTitle3;
            ListBLink3.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasB3;
            //右侧名家
            string ChannelListAliasBS1 = ConfigurationManager.AppSettings["HomeChannelListBS_CategoryAlias_1"];
            WebModules.HomeChannelListBS HomeChannelListBS1 = new WebModules.HomeChannelListBS(ChannelListAliasBS1, 2);
            VerticalShowVideo1.DataSource = HomeChannelListBS1.PicInfo;
            VerticalShowVideo1.DataBind();
            string ChannelListBSTitle1 = ConfigurationManager.AppSettings["HomeChannelListBS_Title_1"];
            ListBTitle1.Text = ChannelListBSTitle1;
            string FameAlias = ConfigurationManager.AppSettings["ChannelFame"];
            ListBLink1.NavigateUrl = "level2.aspx?alias=" + FameAlias;
            //右侧公开课
            string ChannelListAliasBS2 = ConfigurationManager.AppSettings["ChannelClass"];
            //WebModules.HomeChannelListBS HomeChannelListBS2 = new WebModules.HomeChannelListBS(ChannelListAliasBS2, 2);
            //VerticalShowVideo4.DataSource = HomeChannelListBS2.PicInfo;
            //VerticalShowVideo4.DataBind();
            //LeftList
            string Lv2ClassLeftListAlias = DataQuery.GetChannelAliasByName(ChannelListAliasBS2, "国内985大学");
            DataTable Lv2ClassLeftList1Items = DataQuery.GetSubCategories(Lv2ClassLeftListAlias, "6");
            TabIndex2List1.DataSource = Lv2ClassLeftList1Items;
            TabIndex2List1.DataBind();
            //LeftList
            Lv2ClassLeftListAlias = DataQuery.GetChannelAliasByName(ChannelListAliasBS2, "国外大学");
            DataTable Lv2ClassLeftList2Items = DataQuery.GetSubCategories(Lv2ClassLeftListAlias, "6");
            TabIndex2List2.DataSource = Lv2ClassLeftList2Items;
            TabIndex2List2.DataBind();
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
            CacheTest.Text = DateTime.Now.ToString();

            url = "http://" + Request.Url.Authority + Request.ApplicationPath + "/";

            //图片新闻
            // Text
            string NewsCommendAlias = ConfigurationManager.AppSettings["NewsCommendAlias"];
            DataTable NewsCommendText = new DataTable();
            NewsCommendText = DataQuery.GetSubCategories(NewsCommendAlias);
            FocusTabsTxt.DataSource = NewsCommendText;
            FocusTabsTxt.DataBind();
            //  Pic
            if (NewsCommendText.Rows.Count > 0)
            {

                DataTable NewsCommendPic = new DataTable();
                //NewsCommendPic = new DAL.Article().GetArticleList(NewsCommendText.Rows[0][1].ToString(),false,1); //缓存
                NewsCommendPic = DataQuery.ArticleQuery("","1",NewsCommendText.Rows[0][2].ToString()).Tables[0];
                if (NewsCommendPic != null)
                {
                    NewsCommendPic.Columns.Add("FocusTabsIndex", typeof(String));
                    NewsCommendPic.Rows[0]["FocusTabsIndex"] = NewsCommendText.Rows[0]["XIndex"];
                    DataTable NewsCommendPicMerge = new DataTable();
                    for (int i = 1; i < NewsCommendText.Rows.Count; i++)
                    {
                        //NewsCommendPicMerge = new DAL.Article().GetArticleList(NewsCommendText.Rows[i][1].ToString(),false,1); //缓存
                        NewsCommendPicMerge = DataQuery.ArticleQuery("", "1", NewsCommendText.Rows[i][2].ToString()).Tables[0];
                        NewsCommendPicMerge.Columns.Add("FocusTabsIndex", typeof(String));
                        NewsCommendPicMerge.Rows[0]["FocusTabsIndex"] = NewsCommendText.Rows[i]["XIndex"];
                        NewsCommendPic.Merge(NewsCommendPicMerge);
                    }
                    FocusTabsPic.DataSource = NewsCommendPic;
                    FocusTabsPic.DataBind();
                }
            }

            //特别关注
            string SubjectGUID = DataQuery.CategoryAliasToID(ConfigurationManager.AppSettings["TBGZ_Alias"]);
            string SubjectPath = DataQuery.CategoryPath(SubjectGUID); //专题路径
            DataTable HotSubjects = new DataTable() ;
           HotSubjects = DataQuery.GetNHotZT();
            //  获取课件
            //string SubjectHotGUID = DataQuery.CategoryAliasToID(ConfigurationManager.AppSettings["SAHot_Alias"]);
           // string SubjectHotPath = DataQuery.CategoryPath(SubjectGUID);
            DataTable HotCoursesMerge = new DataTable();
            // 第一个专题
            if (HotSubjects.Rows.Count > 0)
            {
                //string SubjectHotItemPath = SubjectPath + "/" + HotSubjects.Rows[0][0].ToString();
                // DataTable HotCourses = new DAL.CategoryDAL().GetArticleIndexList(SubjectHotItemPath, "最新动态", 2, false);
                string HotSubjectAlias = DataQuery.CategoryIDToAlias(HotSubjects.Rows[0][0].ToString());
                string SubjectNewAlias = DataQuery.GetChannelAliasByName(HotSubjectAlias, "最新动态");
                string SubjectNewGuid = DataQuery.CategoryAliasToID(SubjectNewAlias);
                DataTable HotCourses = new DAL.Article().GetArticleList(SubjectNewGuid, false, 2);
                HotCourses.Columns.Add("SubjectID", Type.GetType("System.String"));
                HotCourses.Columns.Add("SubjectTitle", Type.GetType("System.String"));
                for (int i = 0; i < HotCourses.Rows.Count; i++)
                {
                    HotCourses.Rows[i]["SubjectID"] = HotSubjects.Rows[0][0].ToString();
                    HotCourses.Rows[i]["SubjectTitle"] = HotSubjects.Rows[0][1].ToString();
                }
                //  后N个专题
                for (int i = 1; i < HotSubjects.Rows.Count; i++)
                {
                    //SubjectHotItemPath = SubjectPath + "/" + HotSubjects.Rows[i][0].ToString();
                    //HotCoursesMerge = new DAL.CategoryDAL().GetArticleIndexList(SubjectHotItemPath, "最新动态", 2, false);
                    HotSubjectAlias = DataQuery.CategoryIDToAlias(HotSubjects.Rows[i][0].ToString());
                    SubjectNewGuid = DataQuery.GetSubChannelGuidByName(HotSubjectAlias, "最新动态");
                    HotCoursesMerge = new DAL.Article().GetArticleList(SubjectNewGuid, false, 2);
                    HotCoursesMerge.Columns.Add("SubjectID", Type.GetType("System.String"));
                    HotCoursesMerge.Columns.Add("SubjectTitle", Type.GetType("System.String"));
                    for (int j = 0; j < HotCoursesMerge.Rows.Count; j++)
                    {
                        HotCoursesMerge.Rows[j]["SubjectID"] = HotSubjects.Rows[i][0].ToString();
                        HotCoursesMerge.Rows[j]["SubjectTitle"] = HotSubjects.Rows[i][1].ToString();
                    }
                    HotCourses.Merge(HotCoursesMerge);
                }
                SpecialFocusContent.DataSource = HotCourses;
                SpecialFocusContent.DataBind();
            }

            //时事新闻
            DataTable HomeTopDataTable1 = new DataTable();
            string HomeNews1Guid = DataQuery.CategoryAliasToID(ConfigurationManager.AppSettings["HomeTop1Alias"]);
            HomeTopDataTable1 = new DAL.Article().GetArticleList(HomeNews1Guid, true, 1);
            if (HomeTopDataTable1 != null)
            {
                //
                //TopVideoPic1.ImageUrl = DataQuery.GetCoursePicPath(HomeTopDataTable1.Rows[0][0].ToString(), url, "000.jpg");
                TopVideoLink1a.NavigateUrl = "ShowVideo.aspx?ID=" + HomeTopDataTable1.Rows[0][0].ToString();
                TopVideoLink1b.NavigateUrl = "ShowVideo.aspx?ID=" + HomeTopDataTable1.Rows[0][0].ToString();
                string TopVideoLink1Title = HomeTopDataTable1.Rows[0][1].ToString();
                string[] TopVideoLink1Array = TopVideoLink1Title.Split(new char[] { '[' });
                TopVideoLink1Title = TopVideoLink1Array[1].Replace("]", "");
                TopVideoLink1b.Text = TopVideoLink1Title;
                TopVideoText1.Text = DataProcessing.SubstringText(HomeTopDataTable1.Rows[0][10].ToString(),26);
                //
                DataTable HomeTopDataTable2 = new DataTable();
                string HomeNews2Guid = DataQuery.CategoryAliasToID(ConfigurationManager.AppSettings["HomeTop2Alias"]);
                HomeTopDataTable2 = new DAL.Article().GetArticleList(HomeNews2Guid, false, 1);
                //TopVideoPic2.ImageUrl = DataQuery.GetCoursePicPath(HomeTopDataTable2.Rows[0][0].ToString(), url, "000.jpg");
                TopVideoLink2a.NavigateUrl = "ShowVideo.aspx?ID=" + HomeTopDataTable2.Rows[0][0].ToString();
                TopVideoLink2b.NavigateUrl = "ShowVideo.aspx?ID=" + HomeTopDataTable2.Rows[0][0].ToString();
                string TopVideoLink2Title = HomeTopDataTable2.Rows[0][1].ToString();
                string[] TopVideoLink2Array = TopVideoLink2Title.Split(new char[] { '[' });
                TopVideoLink2Title = TopVideoLink2Array[1].Replace("]", "");
                TopVideoLink2b.Text = TopVideoLink2Title;
                string CSVPath = DataQuery.GetFileStreamPath(HomeTopDataTable2.Rows[0][0].ToString(),"vod/data.csv");
                //string CSVPath = DataQuery.GetFileStreamPath(HomeTopDataTable2.Rows[0][0].ToString(), "vod/");
                if (CSVPath != null)
                {
                    DataTable CSVDataSrc = new DataTable();
                    try
                    {
                        CSVDataSrc = DataProcessing.ImportCSV(CSVPath);
                        //CSVDataSrc = DataProcessing.getCsvData(CSVPath, "data.csv");
                        var CSVData = from row in CSVDataSrc.AsEnumerable()
                                      select row;
                        CSVDataSrc = CSVData.Take(1).CopyToDataTable<DataRow>();
                        //DailyVideoInfoTextLines.DataSource = CSVDataSrc;
                        //DailyVideoInfoTextLines.DataBind();
                        string CSVDataText = CSVDataSrc.Rows[0][5].ToString();
                        CSVDataText = CSVDataText.Remove(0,1);
                        TopVideoText2.Text = DataProcessing.SubstringText(CSVDataText, 26);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            //导航
            string NaviList1Alias = ConfigurationManager.AppSettings["subject"];
            DataTable NaviList1Items = DataQuery.GetSubCategories(NaviList1Alias,"9");
            NaviList1.DataSource = NaviList1Items;
            NaviList1.DataBind();
            string NaviList2Alias = ConfigurationManager.AppSettings["MAJOR"];
            DataTable NaviList2Items = DataQuery.GetSubCategories(NaviList2Alias, "9");
            NaviList2.DataSource = NaviList2Items;
            NaviList2.DataBind();
            string NaviList3Alias = ConfigurationManager.AppSettings["INSTITUTION"];
            DataTable NaviList3Items = DataQuery.GetSubCategories(NaviList3Alias, "9");
            NaviList3.DataSource = NaviList3Items;
            NaviList3.DataBind();
            //主列表
            string ChannelListAliasA1 = ConfigurationManager.AppSettings["HomeChannelListA_CategoryAlias_1"];
            WebModules.HomeChannelListA HomeChannelListA1 = new WebModules.HomeChannelListA(ChannelListAliasA1);
            ColumnShowSort1.DataSource = HomeChannelListA1.SubSortTable;
            ColumnShowSort1.DataBind();
            ColumnVideo1.DataSource = HomeChannelListA1.PicInfo;
            ColumnVideo1.DataBind();
            ColumnOtherL1.DataSource = HomeChannelListA1.TextInfoL;
            ColumnOtherL1.DataBind();
            ColumnOtherR1.DataSource = HomeChannelListA1.TextInfoR;
            ColumnOtherR1.DataBind();
            string ChannelListATitle1 = ConfigurationManager.AppSettings["HomeChannelListA_Title_1"];
            ListATitle1.Text = ChannelListATitle1;
            ListALink1.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasA1;

            string ChannelListAliasA2 = ConfigurationManager.AppSettings["HomeChannelListA_CategoryAlias_2"];
            WebModules.HomeChannelListA HomeChannelListA2 = new WebModules.HomeChannelListA(ChannelListAliasA2);
            ColumnShowSort2.DataSource = HomeChannelListA2.SubSortTable;
            ColumnShowSort2.DataBind();
            ColumnVideo2.DataSource = HomeChannelListA2.PicInfo;
            ColumnVideo2.DataBind();
            ColumnOtherL2.DataSource = HomeChannelListA2.TextInfoL;
            ColumnOtherL2.DataBind();
            ColumnOtherR2.DataSource = HomeChannelListA2.TextInfoR;
            ColumnOtherR2.DataBind();
            string ChannelListATitle2 = ConfigurationManager.AppSettings["HomeChannelListA_Title_2"];
            ListATitle2.Text = ChannelListATitle2;
            ListALink2.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasA2;

            string ChannelListAliasA3 = ConfigurationManager.AppSettings["HomeChannelListA_CategoryAlias_3"];
            WebModules.HomeChannelListA HomeChannelListA3 = new WebModules.HomeChannelListA(ChannelListAliasA3);
            ColumnShowSort3.DataSource = HomeChannelListA3.SubSortTable;
            ColumnShowSort3.DataBind();
            ColumnVideo3.DataSource = HomeChannelListA3.PicInfo;
            ColumnVideo3.DataBind();
            ColumnOtherL3.DataSource = HomeChannelListA3.TextInfoL;
            ColumnOtherL3.DataBind();
            ColumnOtherR3.DataSource = HomeChannelListA3.TextInfoR;
            ColumnOtherR3.DataBind();
            string ChannelListATitle3 = ConfigurationManager.AppSettings["HomeChannelListA_Title_3"];
            ListATitle3.Text = ChannelListATitle3;
            ListALink3.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasA3;

            string ChannelListAliasA4 = ConfigurationManager.AppSettings["HomeChannelListA_CategoryAlias_4"];
            WebModules.HomeChannelListA HomeChannelListA4 = new WebModules.HomeChannelListA(ChannelListAliasA4);
            ColumnShowSort4.DataSource = HomeChannelListA4.SubSortTable;
            ColumnShowSort4.DataBind();
            ColumnVideo4.DataSource = HomeChannelListA4.PicInfo;
            ColumnVideo4.DataBind();
            ColumnOtherL4.DataSource = HomeChannelListA4.TextInfoL;
            ColumnOtherL4.DataBind();
            ColumnOtherR4.DataSource = HomeChannelListA4.TextInfoR;
            ColumnOtherR4.DataBind();
            string ChannelListATitle4 = ConfigurationManager.AppSettings["HomeChannelListA_Title_4"];
            ListATitle4.Text = ChannelListATitle4;
            ListALink4.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasA4;

            //右侧列表
            string ChannelListAliasB2 = ConfigurationManager.AppSettings["HomeChannelListB_CategoryAlias_2"];
            WebModules.HomeChannelListB HomeChannelListB2 = new WebModules.HomeChannelListB(ChannelListAliasB2, 1, 8);
            VerticalShowVideo2.DataSource = HomeChannelListB2.PicInfo;
            VerticalShowVideo2.DataBind();
            VerticalShowList2.DataSource = HomeChannelListB2.TextInfo;
            VerticalShowList2.DataBind();
            string ChannelListBTitle2 = ConfigurationManager.AppSettings["HomeChannelListB_Title_2"];
            ListBTitle2.Text = ChannelListBTitle2;
            ListBLink2.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasB2;

            string ChannelListAliasB3 = ConfigurationManager.AppSettings["HomeChannelListB_CategoryAlias_3"];
            WebModules.HomeChannelListB HomeChannelListB3 = new WebModules.HomeChannelListB(ChannelListAliasB3, 1, 4);
            VerticalShowVideo3.DataSource = HomeChannelListB3.PicInfo;
            VerticalShowVideo3.DataBind();
            VerticalShowList3.DataSource = HomeChannelListB3.TextInfo;
            VerticalShowList3.DataBind();
            string ChannelListBTitle3 = ConfigurationManager.AppSettings["HomeChannelListB_Title_3"];
            ListBTitle3.Text = ChannelListBTitle3;
            ListBLink3.NavigateUrl = "level2.aspx?alias=" + ChannelListAliasB3;
            //右侧名家
            string ChannelListAliasBS1 = ConfigurationManager.AppSettings["HomeChannelListBS_CategoryAlias_1"];
            WebModules.HomeChannelListBS HomeChannelListBS1 = new WebModules.HomeChannelListBS(ChannelListAliasBS1, 2);
            VerticalShowVideo1.DataSource = HomeChannelListBS1.PicInfo;
            VerticalShowVideo1.DataBind();
            string ChannelListBSTitle1 = ConfigurationManager.AppSettings["HomeChannelListBS_Title_1"];
            ListBTitle1.Text = ChannelListBSTitle1;
            string FameAlias = ConfigurationManager.AppSettings["ChannelFame"];
            ListBLink1.NavigateUrl = "level2.aspx?alias=" + FameAlias;
            //右侧公开课
            string ChannelListAliasBS2 = ConfigurationManager.AppSettings["ChannelClass"];
            //WebModules.HomeChannelListBS HomeChannelListBS2 = new WebModules.HomeChannelListBS(ChannelListAliasBS2, 2);
            //VerticalShowVideo4.DataSource = HomeChannelListBS2.PicInfo;
            //VerticalShowVideo4.DataBind();
            //LeftList
            string Lv2ClassLeftListAlias = DataQuery.GetChannelAliasByName(ChannelListAliasBS2, "国内985大学");
            DataTable Lv2ClassLeftList1Items = DataQuery.GetSubCategories(Lv2ClassLeftListAlias, "6");
            TabIndex2List1.DataSource = Lv2ClassLeftList1Items;
            TabIndex2List1.DataBind();
            //LeftList
            Lv2ClassLeftListAlias = DataQuery.GetChannelAliasByName(ChannelListAliasBS2, "国外大学");
            DataTable Lv2ClassLeftList2Items = DataQuery.GetSubCategories(Lv2ClassLeftListAlias, "6");
            TabIndex2List2.DataSource = Lv2ClassLeftList2Items;
            TabIndex2List2.DataBind();

            //下方列表
            string SkillListAlias = ConfigurationManager.AppSettings["HomeChannelListC_CategoryAlias"];
            DataTable SkillCategoryInfo = DataQuery.GetSubCategoriesNote(DataQuery.GetChannelAliasByName(SkillListAlias, "栏目"), "8");
            SkillTabs.DataSource = SkillCategoryInfo;
            SkillTabs.DataBind();
            TabIndex1Box.DataSource = SkillCategoryInfo;
            TabIndex1Box.DataBind();
            SkillNote.DataSource = SkillCategoryInfo;
            SkillNote.DataBind();

            string ChannelListAliasD = ConfigurationManager.AppSettings["HomeChannelListD_CategoryAlias"];
            WebModules.HomeChannelListC HomeChannelListC2 = new WebModules.HomeChannelListC(ChannelListAliasD);
            TrainList.DataSource = HomeChannelListC2.CategoryInfo;
            TrainList.DataBind();

            // TEST
            //GridView1.DataSource = HomeChannelListA1.TextInfoL;
            //GridView1.DataBind();
        }
    }
}
