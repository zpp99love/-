/*using Domain.���ݿ�2;
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
        /// һ��һ
        /// </summary>
        /// <returns></returns>
        [HttpGet("Add1")]
        public string Add1()
        {
                Order2 order = new Order2();
                order.Name = "aa";
                order.Address = "vsaf";        

                //˳����
                Delivery2 delivery = new Delivery2();
                delivery.CompanyName = "ff";
                delivery.Number = "159845";
                delivery.Order2 = order;


                //db.Order.Add(order);û��ΪOrder��������ָ��ֵorder.Delivery = delivery;��˲���ֻ��order��˳����

                //���д��˫��ָ����order.Delivery = delivery��delivery.Order = order;������ô��˭����˳����

                _db.Delivery2.Add(delivery);

                _db.SaveChanges();

            return "OK";
        }







 



    }
}*/