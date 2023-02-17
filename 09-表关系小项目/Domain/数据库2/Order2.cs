using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Domain.数据库2
{
    public class Order2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public Delivery2 Delivery2 { get; set; }


    }
}
