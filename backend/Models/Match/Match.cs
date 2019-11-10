using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Match
{
    public class Match
    {
        String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        int date;

        public int Date
        {
            get { return date; }
            set { date = value; }
        }

        string matchId;

        public string MatchId
        {
            get { return matchId; }
            set { matchId = value; }
        }

        List<string> teams;

        public List<string> Teams
        {
            get { return teams; }
            set 
            {
                if (teams == null)
                {
                    teams = new List<string>();
                }
                if (value.Any())
                {
                    teams.Add(value.First());
                }
            }
        }
    }
}