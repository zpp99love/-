using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cache.Api
{
    public class MyExceptionFilter3 : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {

            context.Result = new ObjectResult(new 
            {
                code = 500,
                message = "我为什么还要处理啊！"
            });

            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }   
    }
}