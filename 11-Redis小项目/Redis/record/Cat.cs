using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.record
{
    public record Cat(int Id, string Name)
    {
        public int Age { get; set; }

        //自定义构造函数为age赋值，同时调用默认构造函数为默认字段赋值
        public Cat(int Id, string Name, int Age) : this(Id, Name)
        {
            this.Age = Age;
        }
    }
}
