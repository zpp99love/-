using Dome.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dome.Domain.IRepositories
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IUserRepository
    {
        int Add(User user);
        int Update(User user);
        User GetById(int id);
        User GetByEmail(string email);
        List<User> List();

    }
}
