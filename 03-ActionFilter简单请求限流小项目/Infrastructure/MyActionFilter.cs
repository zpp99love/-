using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MyActionFilter : IAsyncActionFilter
    {
        private readonly IMemoryCache memoryCache;

        public MyActionFilter(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string cacheKey = ip;
            long? lastTime = memoryCache.Get<long?>(cacheKey);

            //判断是否1秒内多次请求
            if (lastTime == null || Environment.TickCount64-lastTime>1000)
            {
                memoryCache.Set(cacheKey, Environment.TickCount64,TimeSpan.FromSeconds(10));
                await next();         
            }
            else
            {
                ObjectResult result = new ObjectResult("你访问太频繁了") { StatusCode = 429 };
                context.Result = result;
            }
        }
    }
}
