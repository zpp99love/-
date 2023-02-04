using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using 实现类.Data.EF.Repositories;

namespace 依赖注入.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "依赖注入.Api", Version = "v1" });
            });

            //添加服务
            //services.AddSingleton<IUserService, UserService>();
            //services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //以下注释均为.NET Core3.0以前版本写法

                //使用Autofac
                //var builder = new ContainerBuilder();
            //builder.RegisterType<UserService>().As<IUserService>();
            //builder.RegisterType<UserRepository>().As<IUserRepository>();
                //接管原有其他地方的services服务
                //builder.Populate(services);
                //构建服务容器
                //var container = builder.Build();
                //返回Autofac服务提供程序
                //return new AutofacServiceProvider(container);

            //使用ApiModule添加各自项目的模块化服务集合
            builder.RegisterModule<ApiModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "依赖注入.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
