using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using ZapiCore.Attributes;

namespace ZapiCore.Filters
{
    /// <summary>
    /// 根据特性选择数据连接字符串
    /// IResourceFilter  在构造函数执行之前执行
    /// </summary>
    public class DatabaseChooseFilter : IResourceFilter
    {
        private DataBaseConnectionFactory _dataBaseConnectionFactory;
        public IConfiguration Configuration { get; }

        public DatabaseChooseFilter(DataBaseConnectionFactory dataBaseConnectionFactory, IConfiguration configuration)
        {
            _dataBaseConnectionFactory = dataBaseConnectionFactory;
            Configuration = configuration;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 判断使用读库，还是写库
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            var dbWrite = (DatabaseChooseAttribute)controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(DatabaseChooseAttribute), false).FirstOrDefault();
            if (dbWrite != null)
            {
                if (dbWrite.IsWrite)
                {
                    _dataBaseConnectionFactory.SetDatabaseChooseType(DatabaseChooseType.Write);
                }
            }
        }
    }
}
