using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dome.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public DateTime RegTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool Status { get; set; }

    }
}
