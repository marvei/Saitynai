using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Team
{
    public class Team
    {
        String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        string teamId;

        public string TeamId
        {
            get { return teamId; }
            set { teamId = value; }
        }

        List<string> users;

        public List<string> Users
        {
            get { return users; }
            set
            {
                if (users == null)
                {
                    users = new List<string>();
                }
                if (value.Any())
                {
                    users.Add(value.First());
                }
                
            }
        }
    }
}