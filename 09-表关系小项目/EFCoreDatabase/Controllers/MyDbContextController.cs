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
        /// һ��һ
        /// </summary>
        /// <returns></returns>
        [HttpGet("Add1")]
        public string Add1()
        {
            using (MyDbContext db = new MyDbContext())
            {
                Order order = new Order();
                order.Name = "aa";

                //˳����
                Delivery delivery = new Delivery();
                delivery.CompanyName = "ff";
                delivery.Number = "159845";
                delivery.Order = order;


                //db.Order.Add(order);û��ΪOrder��������ָ��ֵorder.Delivery = delivery;��˲���ֻ��order��˳����

                //���д��˫��ָ����order.Delivery = delivery��delivery.Order = order;������ô��˭����˳����

                db.Delivery.Add(delivery);


                db.SaveChanges();


            }

            return "OK";
        }







        /// <summary>
        /// һ�Զ�
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
                
                //˳����
                Comment comment1 = new Comment() { Message = "cc",TheArticle = article };
                Comment comment2 = new Comment() { Message = "dd",TheArticle = article };
                //Comment comment1 = new Comment() { Message = "cc",TheArticle = article };
                //Comment comment2 = new Comment() { Message = "dd",TheArticle = article };

                article.Comments.Add(comment1);
                article.Comments.Add(comment2);

                db.Article.Add(article);
                //˳����
                //db.Comment.Add(comment1);
                //db.Comment.Add(comment2);

                db.SaveChanges();


            }

            return "OK";
        }








        /// <summary>
        /// ��Զ�
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
                //˳����
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
                

                //��д˫��󶨣����ң����ǿ�����������
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
                1��DbSet�ȼ̳�IQueryable��̳�IEnumerable�����Լȿ���ʹ��IQueryable���ͽ���Ҳ����IEnumerable���գ�������Ĭ��ΪIQueryable               
             */

            //Executed DbCommand(145ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //   SELECT[c].[Id], [c].[ArticleId], [c].[Message]
            //   FROM[CommentSet] AS[c]
            //�ͻ�������
            IEnumerable<Comment> cmt1 = _db.Comment;
            IEnumerable<Comment>  cmt1temp = cmt1.Where(x => x.Message.Contains("΢��"));
            foreach (var item in cmt1temp)
            {
                Console.WriteLine(item.Message);
            }


            //Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
            //   SELECT [c].[Id], [c].[ArticleId], [c].[Message]
            //   FROM [CommentSet] AS [c]
            //   WHERE [c].[Message] LIKE N'%΢��%'
            //���������1
            IQueryable<Comment> cmt2 = _db.Comment;
            IEnumerable<Comment> cmt2temp = cmt2.Where(x => x.Message.Contains("΢��"));
            foreach (var item in cmt2temp)
            {
                Console.WriteLine(item.Message);
            }
            //���������2
            IEnumerable<Comment> cmt3 = _db.Comment.Where(x => x.Message.Contains("΢��"));
            foreach (var item in cmt3)
            {
                Console.WriteLine(item.Message);
            }
            //���������3
            IQueryable<Comment> cmt4 = _db.Comment.Where(x => x.Message.Contains("΢��"));
            foreach (var item in cmt4)
            {
                Console.WriteLine(item.Message);
            }
            //Ĭ�Ϸ��������
            var cmt5 = _db.Comment.Where(x => x.Message.Contains("΢��"));
            foreach (var item in cmt5)
            {
                Console.WriteLine(item.Message);
            }









            /*
               2�������ѯ����а����ǳ��෱���Ĳ�������Щ������ʱ�䲻�������ҷ���˻����ܺܺõļ��ݣ��Ǿ�Ӧ�÷��ڿͻ��˽���               
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
               3���ӳ�ִ�У�able��������ִ�еģ���û��ִ�С�����IQueryable��IEnumerableִ���ս᷽���Ż�������ѯ�����ս᷽�����ᡣ
                �������ս᷽���У�Foreach��ToArray()��ToList()��Min()��Count()�ȵ�
                �������ս᷽���У�GroupBy()��OrderBy()��Include()��Skip()��
                һ�㷵��ֵ��IQueryable���͵ľ��Ƿ��ս᷽����ֻ������Ҫ�ɵ�ʲô��Count()�������սᡣ
            */

            //��ʼWhere
            //����Where����ʼForeach
            //info: Microsoft.EntityFrameworkCore.Database.Command[20101]
            //      Executed DbCommand(9ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //      SELECT[c].[Id], [c].[ArticleId], [c].[Message]
            //      FROM[CommentSet] AS[c]
            //      WHERE[c].[Message] LIKE N'%΢��%'
            //΢���Ҳ�������
            Console.WriteLine("��ʼWhere");
            IQueryable<Comment> cmt8 = _db.Comment.Where(x => x.Message.Contains("΢��"));
            Console.WriteLine("����Where����ʼForeach");
            foreach (var item in cmt8)
            {
                Console.WriteLine(item.Message);
            }
            Console.WriteLine("����Foreach");


            /*
                ��ʱ���ص�׷�Ӻô����ǿ�������ִ�в�ѯ֮ǰ��ʽ׷���������߸���������̬��̬ƴ��
                ǰ��������ֻҪ����ĳЩ����Ψһ��������������ڿ���ĳЩ������ĳЩ���
            */
            //ǰ��
            IQueryable<Comment> ctx10 = _db.Comment.Where(x => x.Id > 1).OrderBy(x => x.Id).Skip(3);

            //���� ��ͨ���Ȱ�����Ψһȫ��������ǰ��
            IQueryable<Comment> ctx11 = _db.Comment.Where(x => x.Message.Contains("΢��"));
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
                ��ʱ���صĸ��úô����Ƕ�ĳ���ӳٽ�����ò�ͬ���ս᷽�����Ҳ�Ӱ��������ӳٽ��
            */    
              //SELECT COUNT(*)
              //FROM[CommentSet] AS[c]
              //WHERE[c].[Message] LIKE N'%΢��%'
              //���ȣ�1

              //Executed DbCommand(1ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
              //SELECT[c].[Id], [c].[ArticleId], [c].[Message]
              //FROM[CommentSet] AS[c]
              //WHERE[c].[Message] LIKE N'%΢��%'
              //���ϣ�System.Collections.Generic.List`1[Domain.һ�Զ�.Comment]

              //Executed DbCommand(5ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
              //SELECT MAX([c].[Id])
              //FROM[CommentSet] AS[c]
              //WHERE[c].[Message] LIKE N'%΢��%'
              //Id��2
            IQueryable<Comment> ctx12 = _db.Comment.Where(x => x.Message.Contains("΢��"));
            Console.WriteLine("���ȣ�"+ctx12.Count());
            Console.WriteLine("���ϣ�"+ctx12.ToList());
            Console.WriteLine("Id��"+ctx12.Max(x => x.Id));



            /*
                �ս᷽���ᵼ��ִ��SELECT��䣬����ǰ�������IQueryable(IEnumerable)+�ս�( _db.Comment.Count())�Ĳ�ѯ��ʽ
                �������������+�ս�ĸ�ʽ�򲻻ᴥ��SELECT���
            */

            //Executed DbCommand(0ms) [Parameters=[], CommandType = 'Text', CommandTimeout = '30']
            //   SELECT[c].[Id], [c].[ArticleId], [c].[Message]
            //   FROM[CommentSet] AS[c]
            //   WHERE[c].[Message] LIKE N'%΢��%'
            // ���ȣ�1
            IQueryable<Comment> ctx9 = _db.Comment.Where(x => x.Message.Contains("΢��"));
            var a = ctx9.ToList();//IQueryable+�ս��ʽ
            var b = a.Count();//��������+�ս�ĸ�ʽ
            Console.WriteLine("���ȣ�"+b);




            /*
                 IQueryable�����ݿ��ȡ����������yeildһ�������ã����ݶ��ռ���ݿ����ӵ�ʱ��ͳ������ܲ��ã�������;���ݿ����رվͱ���
                 ������ִ���ս᷽��ToList()���õ���������������������
            */

            foreach (var item in _db.Article)
            {
                Console.WriteLine(item.Message);
                //Thread.Sleep(1000);
            }



            /*
                ���lQueryable/IEnumerable�ı���Ƕ������£����ںܶ����ݿ��ADO.NET Core ProviderĬ���ǲ�֧�ֶ��DataReaderͬʱִ�еģ����Իᱨ��
                System.InvalidOperationException:��There is already an open DataReader associated with this Connection which must be closed first.��
                Ŀǰֻ��SqlServer�����������ַ����м���MultipleActiveResultSets=true������DataReaderͬʱ���ڣ���Ϊ����SqlServer�ṩ�ģ�������ADO.Net�ṩ��
                ����ֻ��ʹ���ս᷽����
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
            //�ս᷽����ֻʹ�����ݲ������
            foreach (var A in _db.Article.ToList())
            {
                foreach (var C in _db.Comment)
                {
                    Console.WriteLine(A.Message, C.Message);
                }
            }


            /*
                ���������Ҫ���ز�ѯ����������ڷ���������DhCantext�Ļ����ǲ��ܷ���lQueryable/IEnumerable�ģ�����һ���Լ��ط��أ����򱨴�
                System.ObjectDisposedException:��Cannot access a disposed context instance. A common cause of this error is disposing a context instance that was resolved from dependency injection and then later trying to use the same context instance elsewhere in your application. This may occur if you are calling 'Dispose' on the context instance, or wrapping it in a using statement. If you are using dependency injection, you should let the dependency injection container take care of disposing context instances.
                ObjectDisposed_ObjectName_Name��
             */
            foreach (var item in GetArticles())
            {
                Console.WriteLine(item.Message);
            }


            /*             
                4��IEnumerable��IQueryble�����ӳټ��ػ��ƣ��õ��˲�ȥ���ݿ�ȡ��������IEnumerable�������ڿͻ��˶���
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

            //return _db.Article;����ע���ȴû���⣡�����ѵ����ᱻ�ͷţ����ԣ���Ϊ�������������ͷ�����������������
        }


















    }
}