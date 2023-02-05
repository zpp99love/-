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


            //�Զ����м����https://localhost:44391/test ������Ϊ��
            /*
                1 start
                2 start
                Run
                2 end
                1 end              
             */
            //����˳��ִ��ÿ��Use��ǰ�߼�������Run֮���ٷ���ִ��ÿ��Use�ĺ��߼�
            //Run�൱��һ���ս�㣬Run֮����м�����ᱻִ�У���Ϊ��ֻ��һ��RequestDelegate���Ͳ��������Ե���next.Invoke();
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
                    await context.Response.WriteAsync("3 ����Run���滹�ܱ�ִ����<br/>");
                    await next.Invoke();
                    await context.Response.WriteAsync("3 end<br/>");
                });
            });








            //�Զ����м���ࣺ������д��app.Map�е�Use�������ʵ�֣��ô��ǿ��Ը��ô�����롢����ע�����
            //https://localhost:44391/Test1Middleware ������Ϊ��
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








            //�Զ����м����2������ÿ�������壬��������Json�ַ�����ת��ΪDynamic���ͣ������ݸ������м��
            //https://localhost:44391/DjsonMiddleware ������Ϊ��
            /*
                401
             */
            //https://localhost:44391/DjsonMiddleware/ ������Ϊ��
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
