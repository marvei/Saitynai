using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class UserReply
    {
        String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        string userId;
        public string UserId
        {
            get { return userId;  }
            set { userId = value; }
        }

        String userStatus;

        public String UserStatus
        {
            get { return userStatus; }
            set { userStatus = value; }
        }
    }
}