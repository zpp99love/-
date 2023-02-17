using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.数据库2
{
    public class Delivery2
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
    
        public string Number { get; set; }

        public Order2 Order2 { get; set; }
        public int Order2Id { get; set; }
    
    }
}
