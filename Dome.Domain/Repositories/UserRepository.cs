using Dome.Domain.DbContexts;
using Dome.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dome.Domain.Repositories
{
    public class UserRepository
    {
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
