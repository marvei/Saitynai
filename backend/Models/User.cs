using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class User
    {
        public int? id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public int? fk_TeamId { get; set; }
        public string Role { get; set; }
    }
}