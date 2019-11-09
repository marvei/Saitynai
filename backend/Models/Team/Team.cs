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

        int teamId;

        public int TeamId
        {
            get { return teamId; }
            set { teamId = value; }
        }
    }
}