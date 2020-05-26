using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace ZapiCore
{

    /// <summary>
    /// 中间件捕获异常
    /// </summary>
    public class ExceptionHandlerMiddleWare
    {

        private readonly ILogger _logger;

        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleWare(RequestDelegate next, ILoggerFactory logger)
        {
            this.next = next;
            _logger = logger?.CreateLogger("exceptionlog");
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception != null)
            {
                await WriteExceptionAsync(context, exception).ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        private async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            WriteException(exception);
            ResponseMessage responseMessage = new ResponseMessage();
            if (exception is ZCustomizeException)
            {
                ZCustomizeException ex = exception as ZCustomizeException;
                responseMessage.Code = ex.Code.ToString();
                responseMessage.Message = ex.ErrorMsg;
             
            }
            else if (exception is DbException)
            {
                responseMessage.Code = ResponseCodeDefines.ServiceError;
                responseMessage.Message = "系统错误";
            }
            else if (exception != null)
            {
                responseMessage.Code = ResponseCodeDefines.ServiceError;
                responseMessage.Message = exception.Message;
            }
            string text = JsonHelper.ToJson(responseMessage);
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            context.Response.ContentLength = Encoding.Default.GetByteCount(text);
            await context.Response.WriteAsync(text);
        }

        private void WriteException(Exception exception)
        {
            if (!(exception is ZCustomizeException))
            {
                if (_logger == null)
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]错误:{exception.ToString()}");
                }
                else
                {
                    _logger.LogError(exception.ToString());
                }
            }
        }
    }
}
