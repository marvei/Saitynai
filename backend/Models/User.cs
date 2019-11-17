using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class User
    {
        //String name;

        //public String Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}

        //int age;

        //public int Age
        //{
        //    get { return age; }
        //    set { age = value; }
        //}

        //string password;

        //public string Password
        //{
        //    get { return password; }
        //    set { password = value; }
        //}

        //string userId;

        //public string UserId
        //{
        //    get { return userId; }
        //    set { userId = value; }
        //}

        public int? id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public int? fk_TeamId { get; set; }

    }
}