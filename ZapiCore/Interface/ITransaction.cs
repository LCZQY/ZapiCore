using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace ZapiCore
{
    /// <summary>
    /// 事务处理
    /// </summary>
    /// <typeparam name="TContext">每个插件或者模块自己的DbContext</typeparam>
    public interface ITransaction<TContext> where TContext : DbContext
    {
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransaction();
    }
}
