using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.record
{
    //Id只让构造赋值，Name其他地方赋值
    public record Student(int Id)
    {
        public string? Name { get; set; }
    }
}
