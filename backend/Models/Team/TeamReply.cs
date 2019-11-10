using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Team
{
    public class TeamReply
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

        String teamStatus;

        public String TeamStatus
        {
            get { return teamStatus; }
            set { teamStatus = value; }
        }
    }
}