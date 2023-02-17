using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据库悲观并发
{
    public class House
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }

        //SqlServer 独有，行版本
        public byte[] RowVersion { get; set; }
    }
}
