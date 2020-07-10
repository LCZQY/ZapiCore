using Microsoft.AspNetCore.Authorization;
using System;

namespace ZapiCore.Attributes
{
    /// <summary>
    /// 解释token权限的验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizationLocalAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 权限项名称
        /// </summary>
        public string Permission { get; set; }

        /// <summary>
        /// 是否检查权限
        /// </summary>
        public bool IsCheck { get; set; }

        public AuthorizationLocalAttribute(string permission, bool IsCheck = false)
        {
            Permission = permission;
        }

    }
}
