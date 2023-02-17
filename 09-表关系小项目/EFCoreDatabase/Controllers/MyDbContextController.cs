using Infrastructure;
using Domain.一对一;
using Domain.一对多;
using Domain.多对多;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EFCoreDatabase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyDbContextController : ControllerBase
    {

        private readonly ILogger<MyDbContextController> _logger;
        private readonly MyDbContext _db;

        public MyDbContextController(ILogger<MyDbContextController> logger, MyDbContext db)
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
            using (MyDbContext db = new MyDbContext())
            {
                Order order = new Order();
                order.Name = "aa";

                //顺杆找
                Delivery delivery = new Delivery();
                delivery.CompanyName = "ff";
                delivery.Number = "159845";
                delivery.Order = order;


                //db.Order.Add(order);没有为Order导航属性指定值order.Delivery = delivery;因此不能只存order来顺杆爬

                //如果写了双向指定（order.Delivery = delivery，delivery.Order = order;），那么存谁都会顺杆爬

                db.Delivery.Add(delivery);


                db.SaveChanges();


            }

            return "OK";
        }







        /// <summary>
        /// 一对多
        /// </summary>
        /// <returns></returns>
        [HttpGet("Add2")]
        public string Add2()
        {
            using (MyDbContext db = new MyDbContext())
            {
                Article article = new Article();
                article.Title = "aa";
                article.Message= "bb";
                
                //顺杆找
                Comment comment1 = new Comment() { Message = "cc",TheArticle = article };
                Comment comment2 = new Comment() { Message = "dd",TheArticle = article };
                //Comment comment1 = new Comment() { Message = "cc",TheArticle = article };
                //Comment comment2 = new Comment() { Message = "dd",TheArticle = article };

                article.Comments.Add(comment1);
                article.Comments.Add(comment2);

                db.Article.Add(article);
                //顺杆找
                //db.Comment.Add(comment1);
                //db.Comment.Add(comment2);

                db.SaveChanges();


            }

            return "OK";
        }








        /// <summary>
        /// 多对多
        /// </summary>
        /// <returns></returns>
        [HttpGet("Add3")]
        public string Add3()
        {
            using (MyDbContext db = new MyDbContext())
            {
                Student s1 = new Student() { Name = "s1"};
                Student s2 = new Student() { Name = "s2" };
                Student s3 = new Student() { Name = "s3" };
                //顺杆找
                Teacher t1 = new Teacher() { Name = "t1" };
                Teacher t2 = new Teacher() { Name = "t2" };
                Teacher t3 = new Teacher() { Name = "t3" };


                s1.Teachers.Add(t1);
                s1.Teachers.Add(t2);

                s2.Teachers.Add(t2);
                s2.Teachers.Add(t3);

                s3.Teachers.Add(t1);
                s3.Teachers.Add(t2);
                s3.Teachers.Add(t3);
                

                //不写双向绑定，很乱，但是可以两个都加
                db.Student.Add(s1);
                db.Student.Add(s2);
                db.Student.Add(s3);

                db.Teacher.Add(t1);
                db.Teacher.Add(t2);
                db.Teacher.Add(t3);


                db.SaveChanges();




                var teachers = db.Teacher.Include(x => x.Students);
                foreach (var item in teachers)
                {
                    Console.WriteLine(item.Name);
                    foreach (var s in item.Students)
                    {
                        Console.WriteLine(s.Name);
                    }
                }





            }

            return "OK";
        }







        /// <summary>
        /// IQueryableIEnumerable
        /// </summary>
        /// <returns></returns>
        [HttpGet("IQueryableIEnumerable")]
        public void IQueryableIEnumerable()
        {
            /*
                1、DbSet先继承IQueryable后继承IEnumerable，所以既可以使用IQueryable类型接收也可以IEnumerable接收，但优先默认为IQueryable               
             */

            //Executed DbCommand(145ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //   SELECT[c].[Id], [c].[ArticleId], [c].[Message]
            //   FROM[CommentSet] AS[c]
            //客户端评论
            IEnumerable<Comment> cmt1 = _db.Comment;
            IEnumerable<Comment>  cmt1temp = cmt1.Where(x => x.Message.Contains("微软"));
            foreach (var item in cmt1temp)
            {
                Console.WriteLine(item.Message);
            }


            //Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
            //   SELECT [c].[Id], [c].[ArticleId], [c].[Message]
            //   FROM [CommentSet] AS [c]
            //   WHERE [c].[Message] LIKE N'%微软%'
            //服务端评估1
            IQueryable<Comment> cmt2 = _db.Comment;
            IEnumerable<Comment> cmt2temp = cmt2.Where(x => x.Message.Contains("微软"));
            foreach (var item in cmt2temp)
            {
                Console.WriteLine(item.Message);
            }
            //服务端评估2
            IEnumerable<Comment> cmt3 = _db.Comment.Where(x => x.Message.Contains("微软"));
            foreach (var item in cmt3)
            {
                Console.WriteLine(item.Message);
            }
            //服务端评估3
            IQueryable<Comment> cmt4 = _db.Comment.Where(x => x.Message.Contains("微软"));
            foreach (var item in cmt4)
            {
                Console.WriteLine(item.Message);
            }
            //默认服务端评论
            var cmt5 = _db.Comment.Where(x => x.Message.Contains("微软"));
            foreach (var item in cmt5)
            {
                Console.WriteLine(item.Message);
            }









            /*
               2、如果查询语句中包含非常多繁琐的操作，这些操作的时间不仅慢而且服务端还不能很好的兼容，那就应该放在客户端进行               
            */

            //Executed DbCommand(6ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //SELECT COALESCE(REPLACE(SUBSTRING([c].[Message], 0 + 1, 2), N'a', N'b'), N'') +N'.....' AS[Msg]
            //FROM[CommentSet] AS[c]
            var cmt6 = _db.Comment.Select(x => new { Msg = x.Message.Substring(0, 2).Replace("a", "b") + "....." });
            foreach (var item in cmt6)
            {
                Console.WriteLine(item.Msg);
            }
            //Executed DbCommand(7ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //  SELECT[c].[Id], [c].[ArticleId], [c].[Message]
            //  FROM[CommentSet] AS[c]
            var cmt7 = ((IEnumerable<Comment>)_db.Comment).Select(x => new { Msg = x.Message.Substring(0, 2).Replace("a", "b") + "....." });
            foreach (var item in cmt7)
            {
                Console.WriteLine(item.Msg);
            }










            /*
               3、延迟执行，able：有能力执行的，还没有执行。对于IQueryable、IEnumerable执行终结方法才会立即查询，非终结方法不会。
                常见的终结方法有：Foreach、ToArray()、ToList()、Min()、Count()等等
                常见非终结方法有：GroupBy()、OrderBy()、Include()、Skip()等
                一般返回值是IQueryable类型的就是非终结方法。只有真正要干点什么如Count()才属于终结。
            */

            //开始Where
            //结束Where，开始Foreach
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(9ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT[c].[Id], [c].[ArticleId], [c].[Message]
            //      FROM[CommentSet] AS[c]
            //      WHERE[c].[Message] LIKE N'%微软%'
            //微软找不到工作
            Console.WriteLine("开始Where");
            IQueryable<Comment> cmt8 = _db.Comment.Where(x => x.Message.Contains("微软"));
            Console.WriteLine("结束Where，开始Foreach");
            foreach (var item in cmt8)
            {
                Console.WriteLine(item.Message);
            }
            Console.WriteLine("结束Foreach");


            /*
                延时加载的追加好处就是可以真正执行查询之前链式追加条件或者根据条件动态动态拼接
                前者适用于只要考虑某些条件唯一结果，后者适用于考虑某些条件的某些结果
            */
            //前者
            IQueryable<Comment> ctx10 = _db.Comment.Where(x => x.Id > 1).OrderBy(x => x.Id).Skip(3);

            //后者 ，通常先把条件唯一全部放在最前面
            IQueryable<Comment> ctx11 = _db.Comment.Where(x => x.Message.Contains("微软"));
            if (5 - 1 < 2)
            {
                ctx10 = ctx10.Where(x => x.Id < 2);
            }
            else
            {
                ctx10 = ctx10.Where(x => x.Message.Contains("aaaa"));
            }
            if (2 > 5)
            {
                ctx10 = ctx10.Where(x => x.Message.StartsWith("sss"));
            }
            else
            {
                ctx10 = ctx10.Where(x => x.ArticleId == 1);
            }
            foreach (var item in ctx10)
            {
                Console.WriteLine(item.Message);
            }

            /*
                延时加载的复用好处就是对某个延迟结果调用不同的终结方法，且不影响最初的延迟结果
            */    
              //SELECT COUNT(*)
              //FROM[CommentSet] AS[c]
              //WHERE[c].[Message] LIKE N'%微软%'
              //长度：1

              //Executed DbCommand(1ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
              //SELECT[c].[Id], [c].[ArticleId], [c].[Message]
              //FROM[CommentSet] AS[c]
              //WHERE[c].[Message] LIKE N'%微软%'
              //集合：System.Collections.Generic.List`1[Domain.一对多.Comment]

              //Executed DbCommand(5ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
              //SELECT MAX([c].[Id])
              //FROM[CommentSet] AS[c]
              //WHERE[c].[Message] LIKE N'%微软%'
              //Id：2
            IQueryable<Comment> ctx12 = _db.Comment.Where(x => x.Message.Contains("微软"));
            Console.WriteLine("长度："+ctx12.Count());
            Console.WriteLine("集合："+ctx12.ToList());
            Console.WriteLine("Id："+ctx12.Max(x => x.Id));



            /*
                终结方法会导致执行SELECT语句，但是前提必须是IQueryable(IEnumerable)+终结( _db.Comment.Count())的查询格式
                如果是其他类型+终结的格式则不会触发SELECT语句
            */

            //Executed DbCommand(0ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //   SELECT[c].[Id], [c].[ArticleId], [c].[Message]
            //   FROM[CommentSet] AS[c]
            //   WHERE[c].[Message] LIKE N'%微软%'
            // 长度：1
            IQueryable<Comment> ctx9 = _db.Comment.Where(x => x.Message.Contains("微软"));
            var a = ctx9.ToList();//IQueryable+终结格式
            var b = a.Count();//其他类型+终结的格式
            Console.WriteLine("长度："+b);




            /*
                 IQueryable从数据库获取数据是类似yeild一样分批拿，数据多霸占数据库连接的时间就长，性能不好，若是中途数据库服务关闭就报错。
                 可以先执行终结方法ToList()等拿到所有数据再做其他操作
            */

            foreach (var item in _db.Article)
            {
                Console.WriteLine(item.Message);
                //Thread.Sleep(1000);
            }



            /*
                多个lQueryable/IEnumerable的遍历嵌套情况下，由于很多数据库的ADO.NET Core Provider默认是不支持多个DataReader同时执行的，所以会报错
                System.InvalidOperationException:“There is already an open DataReader associated with this Connection which must be closed first.”
                目前只有SqlServer可以在连接字符串中加上MultipleActiveResultSets=true允许多个DataReader同时存在，因为它是SqlServer提供的，并不是ADO.Net提供的
                或者只能使用终结方法了
             */
            foreach (var A in _db.Article)
            {
                foreach (var C in _db.Comment)
                {
                    Console.WriteLine(A.Message, C.Message);
                }
            }
            foreach (var A in (IEnumerable<Article>)_db.Article)
            {
                foreach (var C in (IEnumerable<Comment>)_db.Comment)
                {
                    Console.WriteLine(A.Message, C.Message);
                }
            }
            //终结方法，只使用数据不大情况
            foreach (var A in _db.Article.ToList())
            {
                foreach (var C in _db.Comment)
                {
                    Console.WriteLine(A.Message, C.Message);
                }
            }


            /*
                如果方法需要返回查询结果，并且在方法里销毁DhCantext的话，是不能返回lQueryable/IEnumerable的，必须一次性加载返回，否则报错
                System.ObjectDisposedException:“Cannot access a disposed context instance. A common cause of this error is disposing a context instance that was resolved from dependency injection and then later trying to use the same context instance elsewhere in your application. This may occur if you are calling 'Dispose' on the context instance, or wrapping it in a using statement. If you are using dependency injection, you should let the dependency injection container take care of disposing context instances.
                ObjectDisposed_ObjectName_Name”
             */
            foreach (var item in GetArticles())
            {
                Console.WriteLine(item.Message);
            }


            /*             
                4、IEnumerable和IQueryble都是延迟加载机制，用到了才去数据库取，区别是IEnumerable过滤是在客户端而已
             */

        }


        //private IQueryable<Article> GetArticles()
        private IEnumerable<Article> GetArticles()
        {
            using (MyDbContext db = new MyDbContext())
            {
                //return db.Article;
                return db.Article.ToArray();
            }

            //return _db.Article;依赖注入的却没问题！！！难道不会被释放，不对，因为生命周期所以释放在是这次请求结束？
        }


















    }
}