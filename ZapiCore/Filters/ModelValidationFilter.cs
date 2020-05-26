using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ZapiCore.Filters
{
    /// <summary>
    /// 模型验证过滤器
    /// </summary>
    public class ModelValidationFilter: IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                string msg = string.Empty;
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        msg += error.ErrorMessage + ",";
                    }
                }
                var result = new ResponseMessage()
                {
                    Code = ResponseCodeDefines.ModelStateInvalid,
                    Message = msg,

                };
                context.Result = new JsonResult(result);
            }
        }

    }
}
