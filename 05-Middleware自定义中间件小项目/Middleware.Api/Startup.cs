using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middleware.Api.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });


            //自定义中间件，https://localhost:44391/test 请求结果为：
            /*
                1 start
                2 start
                Run
                2 end
                1 end              
             */
            //按照顺序执行每个Use的前逻辑，遇到Run之后再反向执行每个Use的后逻辑
            //Run相当于一个终结点，Run之后的中间件不会被执行，因为它只有一个RequestDelegate类型参数不可以调用next.Invoke();
            app.Map("/test", async pipeBuilder => {
                pipeBuilder.Use(async (context, next) => {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("1 start<br/>");
                    await next.Invoke();
                    await context.Response.WriteAsync("1 end<br/>");
                });
                pipeBuilder.Use(async (context, next) => {
                    await context.Response.WriteAsync("2 start<br/>");
                    await next.Invoke();
                    await context.Response.WriteAsync("2 end<br/>");
                });
                pipeBuilder.Run(async context => {
                    await context.Response.WriteAsync("Run<br/>");
                });
                pipeBuilder.Use(async (context, next) => {
                    await context.Response.WriteAsync("3 我在Run后面还能被执行吗？<br/>");
                    await next.Invoke();
                    await context.Response.WriteAsync("3 end<br/>");
                });
            });








            //自定义中间件类：将上面写在app.Map中的Use剥离出来实现，好处是可以复用此类代码、依赖注入服务
            //https://localhost:44391/Test1Middleware 请求结果为：
            /*
                1 Test1MiddlewareStart
                Run
                1 Test1MiddlewareEnd
             */
            app.Map("/Test1Middleware", async pipeBuilder => {

                pipeBuilder.UseMiddleware<Test1Middleware>();
                
                pipeBuilder.Run(async context => {
                    await context.Response.WriteAsync("Run<br/>");
                });
            });








            //自定义中间件类2：拦截每次请求体，检测出包含Json字符串则转化为Dynamic类型，并传递给其他中间件
            //https://localhost:44391/DjsonMiddleware 请求结果为：
            /*
                401
             */
            //https://localhost:44391/DjsonMiddleware/ 请求结果为：
            /*
                {
                    "name":"zyf",
                    "age":"18"
                } 
                +Run<br/>
             */
            app.Map("/DynamicMiddleware", async pipeBuilder => {

                pipeBuilder.UseMiddleware<DynamicMiddleware>();

                pipeBuilder.Run(async context => {
                    dynamic? obj = context.Items["BodyDynamic"];
                    if (obj != null)
                    {
                        await context.Response.WriteAsync($"{obj} +Run<br/>");
                    }
                });
            });










        }
    }
}
