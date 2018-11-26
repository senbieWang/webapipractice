using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiStandardTest.Filter
{
    /// <summary>
    /// 判断是否是一个格式正确的请求
    /// </summary>
    public class ModelValidactionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// action执行前执行此过滤器
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState); // returns 400 with error
            }          
        }
    }
}
