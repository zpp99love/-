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
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCoreDatabase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ASNoTrankingController : ControllerBase
    {

        private readonly ILogger<MyDbContextController> _logger;
        private readonly MyDbContext _db;

        public ASNoTrankingController(ILogger<MyDbContextController> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        /// <summary>
        /// Tranking 实体 快照
        /// </summary>
        /// <returns></returns>
        [HttpGet("Status")]
        public string Status()
        {

            Article article = _db.Article.First();
            article.Title = "aa";
            article.Message = "bb";//第二次再改变，知道Title没变化只改变Message的值

            Article article1 = new Article { Title = "new",Message="new" };//与_db无任何关系，不被跟踪

            //_db.Article.Remove(article);已删除状态，下次调用SaveChanges便真正删除
            //_db.Article.Add(article1);//已添加

            EntityEntry s = _db.Entry(article);
            Console.WriteLine(s.State);//获取实体当前状态
            Console.WriteLine(s.DebugView.LongView);//获取实体前后变化详情






            _db.SaveChanges();

            return "OK";
        }






        /// <summary>
        /// ASNoTranking 对于数据库量大且只是查询用无跟踪避免快照占内存
        /// </summary>
        /// <returns></returns>
        [HttpGet("ASNoTranking")]
        public string ASNoTranking()
        {

            var comment = _db.Comment.AsNoTracking();
            foreach (var item in comment)
            {
                Console.WriteLine(item.Message);
                Console.WriteLine(_db.Entry(item).State);//Detached

                _db.Comment.Add(item);//不会被添加，为什么我全部都是AsNoTracking还是可以更新、删除、添加？

            }

            
            return "OK";
        }






        /// <summary>
        /// ASNoTrankingForSelect 骚操作，以前都先查询Select再Update两步语句，骚操作直接一步语句Update t set Message = xx where Id = x
        /// </summary>
        /// <returns></returns>
        [HttpGet("ASNoTrankingForSelect")]
        public string ASNoTrankingForSelect()
        {

            var comment = _db.Comment.Where(x=>x.Id==2).Single();//Select
            comment.Message = "cadf";
            _db.SaveChanges();



            //妙用
            Comment comment1 = new Comment {Id=2,Message="adfad" };//Id必须写，用于定位改哪条数据
            var entity = _db.Entry(comment1);
            entity.Property("Message").IsModified = true;
            Console.WriteLine(entity.DebugView.LongView);//获取实体前后变化详情
            _db.SaveChanges();

            //还可以用于删除等，一句解决，但不建议使用，难维护，可读性差，bug不容易发现

            return "OK";
        }













    }
}