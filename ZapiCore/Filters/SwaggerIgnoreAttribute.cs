using System;
using System.Collections.Generic;
using System.Text;

namespace ZapiCore.Filters
{
    /// <summary>
    /// Swagger中生成文档时忽略指定字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerIgnoreAttribute : Attribute { }
}
