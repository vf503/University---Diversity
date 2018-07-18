using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 专题枚举类型
    /// </summary>
    public enum ArticleType : byte
    {
        Vedio = 0,
        HTML = 1
    }

    [Serializable]
    public class Article
    {
        public Article()
        {
            GUID = string.Empty;
            Title = string.Empty;
            Summary = string.Empty;
            Picture = string.Empty;
            Speaker = string.Empty;
            SpeakerInfo = string.Empty;
            IndexFile = string.Empty;
            Filename = string.Empty;
            HaveAttachment = false;
            CreateTime = DateTime.MinValue;
            Type = ArticleType.Vedio;
        }

        /// <summary>
        /// 课件GUID
        /// </summary>
        public String GUID
        {
            set;
            get;
        }
        /// <summary>
        /// 课件标题
        /// </summary>
        public string Title
        {
            set;
            get;
        }
        /// <summary>
        /// 课件简介
        /// </summary>
        public string Summary
        {
            set;
            get;
        }
        /// <summary>
        /// 课件图片
        /// </summary>
        public string Picture
        {
            set;
            get;
        }
        /// <summary>
        /// 课件类型
        /// </summary>
        public ArticleType Type
        {
            set;
            get;
        }
        public string Speaker { set; get; }
        public string SpeakerInfo { set; get; }
        public int Duration { set; get; }
        public string IndexFile { set; get; }
        public DateTime CreateTime { set; get; }
        public string Filename { set; get; }
        public bool HaveAttachment { set; get; }
    }
}

