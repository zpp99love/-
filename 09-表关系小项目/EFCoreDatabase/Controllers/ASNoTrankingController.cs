using Infrastructure;
using Domain.һ��һ;
using Domain.һ�Զ�;
using Domain.��Զ�;
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
        /// Tranking ʵ�� ����
        /// </summary>
        /// <returns></returns>
        [HttpGet("Status")]
        public string Status()
        {

            Article article = _db.Article.First();
            article.Title = "aa";
            article.Message = "bb";//�ڶ����ٸı䣬֪��Titleû�仯ֻ�ı�Message��ֵ

            Article article1 = new Article { Title = "new",Message="new" };//��_db���κι�ϵ����������

            //_db.Article.Remove(article);��ɾ��״̬���´ε���SaveChanges������ɾ��
            //_db.Article.Add(article1);//�����

            EntityEntry s = _db.Entry(article);
            Console.WriteLine(s.State);//��ȡʵ�嵱ǰ״̬
            Console.WriteLine(s.DebugView.LongView);//��ȡʵ��ǰ��仯����






            _db.SaveChanges();

            return "OK";
        }






        /// <summary>
        /// ASNoTranking �������ݿ�������ֻ�ǲ�ѯ���޸��ٱ������ռ�ڴ�
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

                _db.Comment.Add(item);//���ᱻ��ӣ�Ϊʲô��ȫ������AsNoTracking���ǿ��Ը��¡�ɾ������ӣ�

            }

            
            return "OK";
        }






        /// <summary>
        /// ASNoTrankingForSelect ɧ��������ǰ���Ȳ�ѯSelect��Update������䣬ɧ����ֱ��һ�����Update t set Message = xx where Id = x
        /// </summary>
        /// <returns></returns>
        [HttpGet("ASNoTrankingForSelect")]
        public string ASNoTrankingForSelect()
        {

            var comment = _db.Comment.Where(x=>x.Id==2).Single();//Select
            comment.Message = "cadf";
            _db.SaveChanges();



            //����
            Comment comment1 = new Comment {Id=2,Message="adfad" };//Id����д�����ڶ�λ����������
            var entity = _db.Entry(comment1);
            entity.Property("Message").IsModified = true;
            Console.WriteLine(entity.DebugView.LongView);//��ȡʵ��ǰ��仯����
            _db.SaveChanges();

            //����������ɾ���ȣ�һ��������������ʹ�ã���ά�����ɶ��Բbug�����׷���

            return "OK";
        }













    }
}