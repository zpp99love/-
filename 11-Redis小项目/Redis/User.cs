using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    public class User
    {
        //必须要有一个类似主键的字段，用于在Hash表中添加ids记录，以便快速定位urn中的该user
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }



        //自定义ToString()，不然默认只输出类名
/*        public override string ToString()
        {
            return $"Id = {Id},Name={Name},Address = {Address}";  //base.ToString();
        }*/
    }
}
