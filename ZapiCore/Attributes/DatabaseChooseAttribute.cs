using System;

namespace ZapiCore.Attributes
{
    /// <summary>
    /// 是否写【
    /// true：  使用writeDB，
    /// false ：使用readDB】
    /// </summary>
    public class DatabaseChooseAttribute : Attribute
    {
        /// <summary>
        /// 是否写【
        /// true：  使用writeDB，
        /// false ：使用readDB】
        /// </summary>
        public bool IsWrite { get; set; }

        public DatabaseChooseAttribute(bool isWrite)
        {
            IsWrite = isWrite;
        }
    }
}
