using Domain.一对一;
using Domain.一对多;
using Domain.多对多;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure
{
    public class MyDbContext : DbContext
    {
        //new
        public MyDbContext()
        {

        }

        //依赖注入
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }

        //多数据库
        //public MyDbContext(DbContextOptions<MyDbContext2> options) : base(options)
        //{

        //}

        //添加服务已配置(但是如果要使用using (MyDbContext db = new MyDbContext())则必须要)
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            builder.UseSqlServer("Server=.;Database=EFCore1;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
            base.OnConfiguring(builder);
        }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //this.GetType().Assembly:获得当前对象所属的类所在的程序集
            //GetExecutingAssembly():是指获取调用此方法(GetExecutingAssembly)的方法所在的程序集。
            //GetCallingAssembly():是指获取调用此方法(GetCallingAssembly)所在方法的方法的程序集。
            
        }

        public DbSet<Article> Article { get; set; }
        public DbSet<Comment> Comment { get; set; }   
        public DbSet<Order> Order { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }

    }
}