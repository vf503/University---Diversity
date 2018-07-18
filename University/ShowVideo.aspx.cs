using System;
using System.IO;
using System.Web.UI;

namespace colleges
{
    public partial class ShowVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Login OP
            string sTokenEx = "";
            if (this.Context.Request.Cookies["USSUserID"] == null)
            {
                this.Context.Response.Redirect("index.aspx?showlogin=1");
            }
            else
            {
                sTokenEx = this.Context.Request.Cookies["USSToken"].Value + "@" + this.Context.Request.UserHostAddress;
                try
                {
                    DLLUserService.User USSUser = new DLLUserService.User();
                    USSUser.Authenticate(sTokenEx, 0);
                }
                catch (Exception ex)
                {
                    Context.Response.Redirect("index.aspx");
                }
            }
            //Login ED
            if (string.IsNullOrEmpty(Request.QueryString["ID"])) return;
            string sArticleGUID = Request.QueryString["ID"];
            string indexPath = new DAL.Article().GetArticlePath(sArticleGUID);
            string strFilePath = System.Configuration.ConfigurationManager.AppSettings["FileRootPath"];

            string[] pathList = strFilePath.Split('|');
            for (int i = 0; i < pathList.Length; i++)
            {
                if (string.IsNullOrEmpty(pathList[i])) continue;
                string sPath = pathList[i];
                if (!sPath.EndsWith("\\")) sPath = sPath + "\\";
                if (File.Exists(sPath.Split(',')[1] + indexPath))
                {
                    indexPath = sPath.Split(',')[0] + "/" + indexPath;
                    break;
                }
            }
            if (string.IsNullOrEmpty(indexPath))
                vframe.InnerHtml = "很抱歉，视频文件不存在！";
            else
            {
                this.VideoShowFrame.Attributes["src"] = indexPath;
                //Response.Redirect(indexPath);
            }
        }
    }
}
