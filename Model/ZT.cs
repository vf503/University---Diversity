using System;
using System.Data;
namespace Model
{
    /// <summary>
    /// 专题枚举类型
    /// </summary>
    public enum ZTType : byte
    {
        None = 0,
        FirstOne = 1,
        Recommend = 2,
        Hot = 3,
        Famouse = 4,
        Important = 5,
        News = 6
    }
    /// <summary>
    /// 专题
    /// </summary>   
    /// 
    [Serializable]
    public class ZT
    {
        public ZT()
        {
            GUID = string.Empty;
            Name = string.Empty;
            Summary = string.Empty;
            Picture = string.Empty;
            Type = ZTType.None;
        }

        /// <summary>
        /// 专题GUID
        /// </summary>
        public String GUID
        {
            set;
            get;
        }
        /// <summary>
        /// 专题名称
        /// </summary>
        public string Name
        {
            set;
            get;
        }
        /// <summary>
        /// 专题简介
        /// </summary>
        public string Summary
        {
            set;
            get;
        }
        /// <summary>
        /// 专题图片
        /// </summary>
        public string Picture
        {
            set;
            get;
        }
        /// <summary>
        /// 专题类型
        /// </summary>
        public ZTType Type
        {
            set;
            get;
        }
    }
}

