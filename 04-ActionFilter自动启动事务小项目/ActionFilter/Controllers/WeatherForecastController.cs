using Application;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;

namespace ActionFilter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[NotTransationAttribute]
    public class WeatherForecastController : ControllerBase
    {
        private readonly MyDbContext db;

        public WeatherForecastController(MyDbContext db)
        {
            this.db = db;
        }


        //============================普通使用TransactionScope====================================

        /// <summary>
        /// 默认状态下，一个SaveChanges()即为一个事务。
        /// 该测试虽然书名超过了字段最大长度，但人名没超过，所以人的SaveChanges()可以添加成功！
        /// </summary>
        /// <returns></returns>
        [HttpPost("SaveChanges1")]
        public string Test1()
        {
            db.Persons.Add(new Person { Name = "zyf", Age = 18 });
            db.SaveChanges();
            db.Books.Add(new Book { Name = "梦回金沙湾", Price = 88 });
            db.SaveChanges();
            return "ok";
        }
        /// <summary>
        /// 使用TransactionScope将人和书的添加包裹成一个事务，这样只要一个失败全回滚，说明TransactionScope优先级大于SaveChanges()
        /// </summary>
        /// <returns></returns>
        [HttpPost("Transaction2")]
        public string Test2()
        {
            using (TransactionScope tx = new TransactionScope())
            {
                db.Persons.Add(new Person { Name = "zyf", Age = 18 });
                db.SaveChanges();
                db.Books.Add(new Book { Name = "梦回金沙湾", Price = 88 });
                db.SaveChanges();

                tx.Complete();
                return "ok";
            }
        }









        /// <summary>
        /// 测试Complete()，被TransactionScope包裹的事务虽然可以成功，但是不执行Complete()也会回滚，因为TransactionScope继承了IDisposable
        /// </summary>
        /// <returns></returns>
        [HttpPost("Complete1")]
        public string Test3()
        {
            using (TransactionScope tx = new TransactionScope())
            {
                db.Persons.Add(new Person { Name = "zyf", Age = 18 });
                db.SaveChanges();
                db.Books.Add(new Book { Name = "金沙湾", Price = 88 });
                db.SaveChanges();

                return "ok";
            }
        }
        [HttpPost("Complete2")]
        public string Test4()
        {
            using (TransactionScope tx = new TransactionScope())
            {
                db.Persons.Add(new Person { Name = "zyf", Age = 18 });
                db.SaveChanges();
                db.Books.Add(new Book { Name = "金沙湾", Price = 88 });
                db.SaveChanges();

                tx.Complete();
                return "ok";
            }
        }










        /// <summary>
        /// 在一个存在await的异步方法中，TransactionScope需要在构造函数中传入TransactionScopeAsyncFlowOption，否则报错
        /// A TransactionScope must be disposed on the same thread that it was created.
        /// </summary>
        /// <returns></returns>
        [HttpPost("TransactionAsync1")]
        public async Task<string> Test5()
        {
            using (TransactionScope tx = new TransactionScope())
            {
                db.Persons.Add(new Person { Name = "zyf", Age = 18 });
                await db.SaveChangesAsync();
                db.Books.Add(new Book { Name = "金沙湾", Price = 88 });
                await db.SaveChangesAsync();

                tx.Complete();
                return "ok";
            }
        }
        /// <summary>
        /// 因为在同步状态下TransactionScope内部其实是去找了ThreadLocal(相当于当前线程的全局上下文)，所以可以知晓所有数据变化，作出回滚
        /// 而在异步下，就得去找异步版的ThreadLocal--AsyncLocal，所以TransactionScopeAsyncFlowOption相当于是告诉TransactionScope去激活AsyncLocal
        /// </summary>
        /// <returns></returns>
        [HttpPost("TransactionAsync2")]
        public async Task<string> Test6()
        {
            using (TransactionScope tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                db.Persons.Add(new Person { Name = "zyf", Age = 18 });
                await db.SaveChangesAsync();
                db.Books.Add(new Book { Name = "金沙湾", Price = 88 });
                await db.SaveChangesAsync();
                 
                tx.Complete();
                return "ok";
            }
        }









        //============================通过ActionFilter和Attribute使用TransactionScope====================================

        /// <summary>
        /// 测试TransationScopeFilter
        /// </summary>
        /// <returns></returns>
        [HttpPost("ActionFilterAttribute1")]
        public async Task<string> Test8()
        {

           db.Persons.Add(new Person { Name = "zyf", Age = 18 });
           await db.SaveChangesAsync();
           db.Books.Add(new Book { Name = "梦回金沙湾", Price = 88 });
           await db.SaveChangesAsync();

           return "ok";           
        }
        /// <summary>
        /// 标记NotTransationAttribute不使用TransationScopeFilter
        /// </summary>
        /// <returns></returns>
        [NotTransationAttribute]
        [HttpPost("ActionFilterAttribute2")]
        public async Task<string> Test9()
        {
            db.Persons.Add(new Person { Name = "zyf", Age = 18 });
            await db.SaveChangesAsync();
            db.Books.Add(new Book { Name = "梦回金沙湾", Price = 88 });
            await db.SaveChangesAsync();

            return "ok";
        }











    }
}
