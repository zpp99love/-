using Demo.Application.IService;
//using Demo.Application.Service;
//using Demo.Application.Service2;
using Dome.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 依赖注入.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        //获取业务逻辑接口实现类
        private readonly IUserService _userService = null;

        public UserController(IUserService userService)
        {
            //_userService = new UserService();
            _userService = userService;
        }


        [HttpGet("GetUserById")]
        public IActionResult Info(int Id)
        {
            var user = _userService.GetById(Id);
            return Json(user);
        }



        [HttpPost("Login")]
        public IActionResult Login(User user)
        {
            var result = _userService.Login(user);

            return Json(result);
        }


        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            var result = _userService.Reg(user);

            return Json(result);
        }


        [HttpGet("List")]
        public IActionResult List()
        {
            var list = _userService.List();

            return Json(list);
        }


        [HttpGet("ExistsByEmail")]
        public IActionResult ExistsByEmail(string email)
        {
            var result = _userService.ExistsByEmail(email);
            return Json(result);
        }


    }
}
