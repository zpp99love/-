using Dome.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.IService
{

    /// <summary>
    /// 定义业务逻辑处理接口
    /// </summary>
    public interface IUserService
    {
        bool Reg(User user);
        bool Login(User user);
        User GetById(int id);
        bool ExistsByEmail(string email);
        List<User> List();

    }
}
