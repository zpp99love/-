using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api.Middlewares
{
    public class Test1Middleware
    {
        private readonly RequestDelegate next;

        public Test1Middleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("1 Test1MiddlewareStart<br/>");
            await next.Invoke(context);
            await context.Response.WriteAsync("1 Test1MiddlewareEnd<br/>");
        }
    }
}
