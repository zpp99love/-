using Dome.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dome.Domain.DbContexts
{
    public class DbContext
    {
        //定义为static为了存入的数据持久化
        public static List<User> Users = new List<User>
        {
            new User(){ Id=1001,Email="zhangsan@qq.com",Password="123",Username="张三",RegTime=DateTime.Now,LastLoginTime=DateTime.Now,Status=true},
            new User(){ Id=1002,Email="lisi@qq.com",Password="123",Username="李四",RegTime=DateTime.Now,LastLoginTime=DateTime.Now,Status=false},
            new User(){ Id=1003,Email="wangwu@qq.com",Password="123",Username="王五",RegTime=DateTime.Now,LastLoginTime=DateTime.Now,Status=true}
        };

    }
}
