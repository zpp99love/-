using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MyDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }


        public MyDbContext(DbContextOptions<MyDbContext> opt): base(opt)
        { 
            
        }
    }
}
