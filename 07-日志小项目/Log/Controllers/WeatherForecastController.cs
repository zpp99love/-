using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<string> _logger;

        public WeatherForecastController(ILogger<string> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void ILogger() 
        {

            //ILogger
            //_logger.LogInformation("f");
            _logger.LogDebug("连接数据库成功");
            _logger.LogDebug("开始查找数据");
            _logger.LogWarning("查找失败..重试第一次");
            _logger.LogWarning("查找失败..重试第二次");
            _logger.LogError("失败");
            // _logger.LogCritical("byby");
            try
            {
                System.IO.File.ReadAllText("G:/f.txt");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"异常对象");
                _logger.LogWarning(ex, "异常对象");
            }


            
            //Serilog
            User user = new User { Name = "admin", Email = "111qq.com" };
            _logger.LogDebug("注册一个用户{@persopn}",user);
            _logger.LogDebug("用户名：" + user.Name, "邮箱：" + user.Email);
        }



        class User
        { 
            public string Name { get; set; }

            public string Email { get; set; }
        }
      




    }
}
