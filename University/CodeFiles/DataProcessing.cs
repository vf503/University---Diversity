using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace colleges
{
    public class DataProcessing
    {
        // 专题链接
        public static string GetSubjectLink(string SubjectID,string SubjectTitle)
        {
            string SubjectLink = "SpecialAttention.aspx?ID=" + SubjectID + "&Title=" + HttpUtility.UrlEncode(SubjectTitle);
            return SubjectLink;
        }
        public static string GetSubjectLiteLink(string SubjectID, string SubjectTitle)
        {
            string SubjectLink = "SpecialAttentionLite.aspx?ID=" + SubjectID + "&Title=" + HttpUtility.UrlEncode(SubjectTitle);
            return SubjectLink;
        }
        //// 截取
        //public static string SubstringText(string original, int length)
        //{
        //    if (original.Length >= length)
        //    {
        //        original = original.Substring(0, length - 1) + "…";
        //        return original;
        //    }
        //    else return original;
        //}
        /// <summary>
        /// 截取等宽中英文字符串
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="length">要截取的中文字符长度</param>
        /// <param name="appendStr">截取后后追加的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string SubstringText(string str, int length)
        {
            string appendStr = "…";
            if (str == null) return string.Empty;

            int len = (length-1) * 2;
            //aequilateLength为中英文等宽长度,cutLength为要截取的字符串长度
            int aequilateLength = 0, cutLength = 0;
            Encoding encoding = Encoding.GetEncoding("GB2312");

            string cutStr = str;
            int strLength = cutStr.Length;
            byte[] bytes;
            for (int i = 0; i < strLength; i++)
            {
                bytes = encoding.GetBytes(cutStr.Substring(i, 1));
                if (bytes.Length == 2)//不是英文
                    aequilateLength += 2;
                else
                    aequilateLength++;

                if (aequilateLength <= len) cutLength += 1;

                if (aequilateLength > len)
                    return cutStr.Substring(0, cutLength) + appendStr;
            }
            return cutStr;
        }

        public static string SubstringTextNoHtml(string str, int length)
        {
            string appendStr = "…";
            if (str == null) return string.Empty;
            str = System.Text.RegularExpressions.Regex.Replace(str, "<[^>]*>", "");
            int len = (length - 1) * 2;
            //aequilateLength为中英文等宽长度,cutLength为要截取的字符串长度
            int aequilateLength = 0, cutLength = 0;
            Encoding encoding = Encoding.GetEncoding("GB2312");

            string cutStr = str;
            int strLength = cutStr.Length;
            byte[] bytes;
            for (int i = 0; i < strLength; i++)
            {
                bytes = encoding.GetBytes(cutStr.Substring(i, 1));
                if (bytes.Length == 2)//不是英文
                    aequilateLength += 2;
                else
                    aequilateLength++;

                if (aequilateLength <= len) cutLength += 1;

                if (aequilateLength > len)
                    return cutStr.Substring(0, cutLength) + appendStr;
            }
            return cutStr;
        }

        //
        public static string StringCut(string source,char DelimiterChar, int ResultOrder)
        {
            //char DelimiterChars = {DelimiterChar};
            string[] str = source.Split(DelimiterChar);
            string Result = str[1];
            return Result;
        }
        // 排行Guid
        public static string CutHotListUrl(string url)
        {
            string guid;
            string[] str = url.Split(new char[] { '=' });
            guid = str[1].ToString();
            return guid;
        }
        // 删除Html标签
        //public static string NoHTML(string Htmlstring)
        //{
        //    //删除脚本
        //    Htmlstring = Htmlstring.Replace("\r\n", "");
        //    Htmlstring = Regex.Replace(Htmlstring, @"<script.*?</script>", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"<style.*?</style>", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"<.*?>", "", RegexOptions.IgnoreCase);
        //    //删除HTML
        //    Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
        //    Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
        //    Htmlstring = Htmlstring.Replace("<", "");
        //    Htmlstring = Htmlstring.Replace(">", "");
        //    Htmlstring = Htmlstring.Replace("\r\n", "");
        //    Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
        //    return Htmlstring;
        //}
        /// <summary>
        /// 清除文本中Html的标签
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string NoHTML(string Content)
        {
            Content = Zxj_ReplaceHtml("&#[^>]*;", "", Content);
            Content = Zxj_ReplaceHtml("</?marquee[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?object[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?param[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?embed[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?table[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("&nbsp;", "", Content);
            Content = Zxj_ReplaceHtml("</?tr[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?th[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?p[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?a[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?img[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?tbody[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?li[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?span[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?div[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?th[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?td[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?script[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("(javascript|jscript|vbscript|vbs):", "", Content);
            Content = Zxj_ReplaceHtml("on(mouse|exit|error|click|key)", "", Content);
            Content = Zxj_ReplaceHtml("<\\?xml[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("<\\/?[a-z]+:[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?font[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?b[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?u[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?i[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?strong[^>]*>", "", Content);
            string clearHtml = Content;
            return clearHtml;
        }

        /// <summary>
        /// 清除文本中的Html标签
        /// </summary>
        /// <param name="patrn">要替换的标签正则表达式</param>
        /// <param name="strRep">替换为的内容</param>
        /// <param name="content">要替换的内容</param>
        /// <returns></returns>
        private static string Zxj_ReplaceHtml(string patrn, string strRep, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                content = "";
            }
            Regex rgEx = new Regex(patrn, RegexOptions.IgnoreCase);
            string strTxt = rgEx.Replace(content, strRep);
            return strTxt;
        }

        //
        public static DataTable ImportCSV(string filePath)
        {
            DataTable ds = new DataTable();
            using (StreamReader sw = new StreamReader(filePath, Encoding.Default))
            {

                string str = sw.ReadLine();
                string[] columns = str.Split(',');
                foreach (string name in columns)
                {
                    ds.Columns.Add(name);
                }

                string line = sw.ReadLine();
                while (line != null)
                {
                    string[] data = line.Split(',');
                    ds.Rows.Add(data);
                    line = sw.ReadLine();
                }

                sw.Close();
            }

            return ds;
        }

        public static DataTable getCsvData(string pCsvpath, string pCsvname)
        {

                DataSet dsCsvData = new DataSet();

                OleDbConnection OleCon = new OleDbConnection();
                OleDbCommand OleCmd = new OleDbCommand();
                OleDbDataAdapter OleDa = new OleDbDataAdapter();

                OleCon.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pCsvpath + ";Extended Properties='Text;FMT=Delimited(,);HDR=YES;IMEX=1';";
                OleCon.Open();
                DataTable dts1 = OleCon.GetSchema("Tables");
                DataTable dts2 = OleCon.GetSchema("Columns");
                OleCmd.Connection = OleCon;
                OleCmd.CommandText = "select * from [" + pCsvname + "] where 1=1";
                OleDa.SelectCommand = OleCmd;
                OleDa.Fill(dsCsvData, "Csv");
                OleCon.Close();
                return dsCsvData.Tables[0];
        }
        public static string UrlEnCode(string src)
        {
            string str = HttpUtility.UrlEncode(src, Encoding.GetEncoding("utf-8"));
            return str;
        }
    }
}
