using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;


namespace ZapiCore
{
    /// <summary>
    /// 事务处理
    /// </summary>
    /// <typeparam name="TContext">每个插件或者模块自己的DbContext</typeparam>
    public class Transaction<TContext> : ITransaction<TContext> where TContext : DbContext
    {
        private readonly TContext dbContext;
        public Transaction(TContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await dbContext.Database.BeginTransactionAsync();
        }


    }
}
