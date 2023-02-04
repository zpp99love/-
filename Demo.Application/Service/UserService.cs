using Demo.Application.IService;
using Dome.Domain.Entities;
using Dome.Domain.IRepositories;
using System;
using System.Collections.Generic;
//using 实现类.Data.EF.Repositories;
//using 实现类.Data.NH.Repositories;


namespace Demo.Application.Service
{
    /// <summary>
    /// 实现业务逻辑处理接口
    /// </summary>
    public class UserService : IUserService
    {

        //获取仓储接口实现类
        private readonly IUserRepository _userRepository = null;

        public UserService(IUserRepository userRepository)
        {
            //_userRepository = new UserRepository();

            //并且可以取消对 实现类.Data.EF、实现类.Data.NH的引用，将这种引用关系转化为Api层对它们的引用，也进一步证明应该将仓储实现放在基础设施层！
            _userRepository = userRepository;
            Console.WriteLine("我是业务逻辑1");
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
