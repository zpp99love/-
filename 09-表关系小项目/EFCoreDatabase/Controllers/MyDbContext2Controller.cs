/*using Domain.数据库2;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDatabase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyDbContext2Controller : ControllerBase
    {

        private readonly ILogger<MyDbContext2Controller> _logger;
        private readonly MyDbContext2 _db;

        public MyDbContext2Controller(ILogger<MyDbContext2Controller> logger, MyDbContext2 db)
        {
            _logger = logger;
            _db = db;
        }

        /// <summary>
        /// 一对一
        /// </summary>
        /// <returns></returns>
        [HttpGet("Add1")]
        public string Add1()
        {
                Order2 order = new Order2();
                order.Name = "aa";
                order.Address = "vsaf";        

                //顺杆找
                Delivery2 delivery = new Delivery2();
                delivery.CompanyName = "ff";
                delivery.Number = "159845";
                delivery.Order2 = order;


                //db.Order.Add(order);没有为Order导航属性指定值order.Delivery = delivery;因此不能只存order来顺杆爬

                //如果写了双向指定（order.Delivery = delivery，delivery.Order = order;），那么存谁都会顺杆爬

                _db.Delivery2.Add(delivery);

                _db.SaveChanges();

            return "OK";
        }







 



    }
}*/