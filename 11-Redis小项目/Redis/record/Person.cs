using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.record
{
    //全部都是只读属性
    internal record Person(int Id, string Name, string Address);

}
