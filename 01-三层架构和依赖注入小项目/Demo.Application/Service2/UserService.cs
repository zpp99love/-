using Demo.Application.IService;
using Dome.Domain.Entities;
using Dome.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 实现类.Data.NH.Repositories;

namespace Demo.Application.Service2
{
    public class UserService : IUserService
    {

        //获取仓储接口实现类
        private readonly IUserRepository _userRepository = null;


        public UserService()
        {
            _userRepository = new UserRepository();
            Console.WriteLine("我是业务逻辑2");
        }

        public bool Reg(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Email))
            {
                return false;
            }

            user.RegTime = DateTime.Now;
            user.Status = true;

            int count = _userRepository.Add(user);

            return count > 0;
        }



        public bool Login(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return false;
            }

            var target = _userRepository.GetByEmail(user.Email);

            if (target == null)
            {
                return false;
            }

            if (!target.Password.Equals(user.Password))
            {
                return false;
            }

            target.LastLoginTime = DateTime.Now;
            _userRepository.Update(target);

            return true;
        }




        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }



        public bool ExistsByEmail(string email)
        {
            return _userRepository.GetByEmail(email) != null;
        }



        public List<User> List()
        {
            return _userRepository.List();
        }

    }
}
