using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MyActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("MyActionFilter前代码");
            //定义异常
            //string str = null;
            //string str1 = str.ToString();
            ActionExecutedContext result =await next();
            //定义异常
            //string str = null;
            //string str1 = str.ToString();
            if (result.Exception != null)
            {
                Console.WriteLine($"MyActionFilter后代码检测到异常，该异常来自上一个过滤器或Action，异常为：{result.Exception}");
            }
            else
            {
                Console.WriteLine("MyActionFilter前代码执行完成");
            }
        }
    }
}
