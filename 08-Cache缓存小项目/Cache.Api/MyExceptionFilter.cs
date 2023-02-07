using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cache.Api
{
    public class MyExceptionFilter : IAsyncExceptionFilter
    {
        //获取当前环境
        private readonly IWebHostEnvironment _hostEnvironment;

        public MyExceptionFilter(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {

            string msg;
            if(_hostEnvironment.IsDevelopment())
            {
                msg = context.Exception.ToString();
            }
            else
            {
                msg = "服务器端发生未处理异常";
            }


            ObjectResult objectResult = new ObjectResult(new {code = 500,message = msg});
            context.Result = objectResult;
            
            //这个设置true，后面再细说
            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }
    }
}