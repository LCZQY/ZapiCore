namespace ZapiCore
{
    /// <summary>
    /// 分页(空则查询全部)
    /// </summary>
    public class PageCondition
    {

        /// <summary>
        /// 页数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 行数
        /// </summary>

        public int PageSize { get; set; }

    }
}
