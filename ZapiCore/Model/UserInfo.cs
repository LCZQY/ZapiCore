using ZapiCore.Filters;

namespace ZapiCore.Model
{
    public class UserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名(登录帐号)
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 所属组织ID
        /// </summary>
        public string OrganizationId { get; set; }
        /// <summary>
        /// 所属公司ID(所属经纪公司ID)
        /// </summary>
        public string FilialeId { get; set; }
        /// <summary>
        /// 所属集团ID
        /// </summary>
        public string BlocId { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 所属组织名称
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        /// 所属公司名称(所属经纪公司名称)
        /// </summary>
        public string FilialeName { get; set; }
        /// <summary>
        /// 所属集团名称
        /// </summary>
        public string BlocName { get; set; }

        /// <summary>
        /// 用户类型（1-经纪人，2-开发商, 3-员工端用户 ... ）
        /// </summary>
        [SwaggerIgnore]
        public string Application { get; set; }
        /// <summary>
        /// 用户Code，同一用户唯一标识
        /// </summary>
        /// <value></value>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户开放标识
        /// </summary>
        /// <value></value>
        public string OpenId { get; set; }

        /// <summary>
        /// 用户登录凭证
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [SwaggerIgnore]
        public string Token { get; set; }
    }
}
