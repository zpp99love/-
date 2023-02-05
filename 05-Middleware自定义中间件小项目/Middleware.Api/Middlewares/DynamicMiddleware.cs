using Dynamic.Json;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api.Middlewares
{
    public class DynamicMiddleware
    {
        private readonly RequestDelegate next;

        public DynamicMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string pwd = context.Request.Query["password"];

            if (pwd == "123")
            {
                if (context.Request.HasJsonContentType())
                {
                    //将请求体转为流
                    var stream = context.Request.BodyReader.AsStream();
                    //安装Dynamic.Json包将Json转化为Dynamic类型
                    dynamic obj = await DJson.ParseAsync(stream);
                    context.Items["BodyDynamic"] = obj;
                }

                await next.Invoke(context);
            }
            else 
            {
                context.Response.StatusCode = 401;
            }







        }
    }
}
