using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace colleges
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        //public override string GetVaryByCustomString(HttpContext context, string arg)
        //{
        //    if (arg.ToLower() == "sessionid")
        //    {
        //        HttpCookie cookie =
        //         context.Request.Cookies["ASP.NET_SessionId"];
        //        if (cookie != null)
        //            return cookie.Value;
        //    }
        //    return base.GetVaryByCustomString(context, arg);
        //} 

        public override string GetVaryByCustomString(System.Web.HttpContext context, string custom)
        {
            if (custom == "USSUserID")
            {
                if (context.Request.Cookies["USSUserID"] == null)
                    return string.Empty;
                return context.Request.Cookies["USSUserID"].Value;
            }
            else
            {
                return base.GetVaryByCustomString(context, custom);
            }
        }
    }
}