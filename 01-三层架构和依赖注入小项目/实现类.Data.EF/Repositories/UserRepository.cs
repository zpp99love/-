using Dome.Domain.DbContexts;
using Dome.Domain.Entities;
using Dome.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 实现类.Data.EF.Repositories
{
    /// <summary>
    /// 仓储实现类，
    /// 这里单独放在一个项目中只是为了将来仓储万一实现更改为其他框架如depper时，
    /// 可以在实现类名称相同情况只通过改变using命名空间和项目引用来达到切换仓储实现的效果。
    /// 当然，目前我能想到更好的方法是为多个仓储实现自定义各自命名空间。
    /// </summary>
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            Console.WriteLine("现在使用的是EF仓储");
        }

        public int Add(User user)
        {
            DbContext.Users.Add(user);
            return 1;
        }

        public int Update(User user)
        {
            var target = DbContext.Users.Single(x => x.Id == user.Id);
            target.Password = user.Password;
            target.Status = user.Status;
            target.LastLoginTime = user.LastLoginTime;
            target.Username = user.Username;

            return 1;
        }

        public User GetById(int id)
        {
            return DbContext.Users.SingleOrDefault(x => x.Id == id);
        }

        public User GetByEmail(string email)
        {
            return DbContext.Users.SingleOrDefault(x => x.Email.Equals(email,StringComparison.OrdinalIgnoreCase));
        }

        public List<User> List()
        {
            return DbContext.Users;
        }
    }
}
