using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.Dapper
{
    public class PageInput
    {
        /// <summary>
        /// 当前索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页多少条
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 偏移量（PageIndex*PageSize）
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string OrderStr { get; set; }
    }
}
