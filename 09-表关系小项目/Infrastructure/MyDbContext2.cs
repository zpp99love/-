using Domain.数据库2;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure
{
    public class MyDbContext2 : DbContext
    {
       //new
       public MyDbContext2() 
       {

       }
        //依赖注入
        public MyDbContext2(DbContextOptions options) : base(options)
        {

        }

        //多数据库
        //public MyDbContext2(DbContextOptions<MyDbContext2> options) : base(options)
        //{

        //}

        //添加服务已配置
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
       { 
            
            builder.UseSqlServer("Server=.;Database=EFCore2;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
            base.OnConfiguring(builder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);       
        }


        public DbSet<Order2> Order2 { get; set; }
        public DbSet<Delivery2> Delivery2 { get; set; }





    }
}