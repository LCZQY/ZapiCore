using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZapiCore.Model
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class BaseField
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }


        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }
}
