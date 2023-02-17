// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using 数据库悲观并发;

//Console.WriteLine("Hello, World!");
main();




/*
    会发生这种并发的问题的根本在于查和更改不是同时执行的，之间的空隙时间可能别人会卡进来
        悲观并发的方法是把这段空隙给锁起来。好处是绝对不会被人钻空子，坏处就是锁非常容易导致程序锁死。且EFCore是不支持的
        乐观并发的方法是把查和更改的空隙极度压缩，一句话里面既是查也是改。好处就是不会导致程序锁死以及可能会有ABA问题，坏处就是有可能被钻空子。且EFCore是支持的
        乐观RowVersion可以侦听一行的任何一个值发生变化自己版本改变，而且版本不重复，所以可以解决ABA问题，缺点是只有SqlServer支持，其他数据库可以通过Guid来实现
 */
static void main()
{
    Console.WriteLine("开始抢房子,请输入你的名字");

    string name = Console.ReadLine();

    //会出并发问题的
    //Bad(name);

    //悲观并发控制事务锁
    //BeginTransaction(name);

    //乐观并发令牌
    //IsConcurrencyToken(name);

    //乐观RowVerson
    IsRowVersion(name);

}

 
static void Bad(string name)
    {
        using (MyDbContext db = new MyDbContext())
        {
            var h = db.Houses.Single(x => x.Id == 1);
            if (!string.IsNullOrEmpty(h.Owner))
            {
                if (name == h.Owner)
                {
                    Console.WriteLine("您已经抢到过该房子了");

                }
                else
                {
                    Console.WriteLine($"房子已经被{h.Owner}占领了");
                } 
                return;
            }

            h.Owner = name;

            Thread.Sleep(5000);
            Console.WriteLine($"{name}抢到房子了");
            db.SaveChanges();

            Console.ReadKey();
        }
    }


static void BeginTransaction(string name)
{
    using (MyDbContext db = new MyDbContext())
    {
        using (var tx = db.Database.BeginTransaction())
        {
            Console.WriteLine(DateTime.Now+ "准备select for update");
            //加上Update锁    //FromSqlInterpolated是拓展方法命名空间是Microsoft.EntityFrameworkCore
            //var h = db.Houses.FromSqlInterpolated($"select * from houses where Id = 1 for update").Single();//mysql
            var h = db.Houses.FromSqlInterpolated($"select* from houses with(rowlock, UpdLock) where Id= 1").Single();//sqlserver
            
            //var h = db.Houses.Single(x => x.Id == 1);
            Console.WriteLine(DateTime.Now + "结束select for update");
            if (!string.IsNullOrEmpty(h.Owner))
            {
                if (name == h.Owner)
                {
                    Console.WriteLine("您已经抢到过该房子了");
                }
                else
                {
                    Console.WriteLine($"房子已经被{h.Owner}占领了");
                }
                Console.ReadKey();
                return;
            }

            h.Owner = name;

            Thread.Sleep(2000);
            Console.WriteLine($"{name}抢到房子了");
            
            db.SaveChanges();
            tx.Commit();//解Update锁
            
            Console.ReadKey();
    
        }
    }
}


static void IsConcurrencyToken(string name)
{
    using (MyDbContext db = new MyDbContext())
    {
        var h = db.Houses.Single(x => x.Id == 1);
        if (!string.IsNullOrEmpty(h.Owner))
        {
            if (name == h.Owner)
            {
                Console.WriteLine("您已经抢到过该房子了");

            }
            else
            {
                Console.WriteLine($"房子已经被{h.Owner}占领了");
            }
            Console.ReadKey();
            return;
        }

        h.Owner = name;

        Thread.Sleep(2000);

        //主线程捕获不到异步的异常，所以需要处理子线程的异常，全局异常不知道可不可以！
        try
        {
            db.SaveChanges(); //带上之前db.Houses.Single(x => x.Id == 1);结果的Owner值
            Console.WriteLine($"{name}抢到房子了");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var entity = ex.Entries.First();
            string newValue =  entity.GetDatabaseValues().GetValue<string>("Owner");

            Console.WriteLine($"并发冲突，值已经被别人抢先改为{newValue}了");
            
        }
        

        Console.ReadKey();


        /*
             开始抢房子,请输入你的名字
             a
             a抢到房子了
         
             开始抢房子,请输入你的名字
             b
             并发冲突，值已经被别人抢先改为a了
         */
    }

}


static void IsRowVersion(string name)
{
    using (MyDbContext db = new MyDbContext())
    {
        var h = db.Houses.Single(x => x.Id == 1);
        if (!string.IsNullOrEmpty(h.Owner))
        {
            if (name == h.Owner)
            {
                Console.WriteLine("您已经抢到过该房子了");

            }
            else
            {
                Console.WriteLine($"房子已经被{h.Owner}占领了");
            }
            Console.ReadKey();
            return;
        }

        h.Owner = name;

        Thread.Sleep(2000);

        //主线程捕获不到异步的异常，所以需要处理子线程的异常，全局异常不知道可不可以！
        try
        {
            db.SaveChanges();
            Console.WriteLine($"{name}抢到房子了");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var entity = ex.Entries.First();
            string newValue = entity.GetDatabaseValues().GetValue<string>("Owner");

            Console.WriteLine($"并发冲突，值已经被别人抢先改为{newValue}了");

        }


        Console.ReadKey();


        /*
             开始抢房子,请输入你的名字
             a
             a抢到房子了
         
             开始抢房子,请输入你的名字
             b
             并发冲突，值已经被别人抢先改为a了
         */
    }
}