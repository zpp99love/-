using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.一对一
{
    public class Delivery
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
    
        public string Number { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }
    
    }
}
