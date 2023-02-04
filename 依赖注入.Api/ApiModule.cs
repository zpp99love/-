using Autofac;
using Demo.Application.IService;
using Demo.Application.Service;
using Dome.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using 实现类.Data.EF.Repositories;

namespace 依赖注入.Api
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //为此项目模块化注册服务
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();


        }
    }
}
