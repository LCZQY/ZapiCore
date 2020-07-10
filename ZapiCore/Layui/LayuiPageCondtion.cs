using System;
using System.Collections.Generic;
using System.Text;

namespace ZapiCore.Layui
{
    /// <summary>
    /// Layui 表格分页请求体
    /// </summary>
    public class LayuiPageCondtion
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int Limit { get; set; }

    }
}
