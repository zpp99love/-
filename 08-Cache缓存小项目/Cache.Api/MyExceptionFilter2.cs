using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cache.Api
{
    public class MyExceptionFilter2 : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {

            context.Result = new ObjectResult(new 
            {
                code = 500,
                message = "我先处理"
            });

            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }   
    }
}