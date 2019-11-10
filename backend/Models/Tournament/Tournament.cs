using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Tournament
{
    public class Tournament
    {
        String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        string tournamentId;

        public string TournamentId
        {
            get { return tournamentId; }
            set { tournamentId = value; }
        }

        List<string> matches;

        public List<string> Matches
        {
            get { return matches; }
            set
            {
                if (matches == null)
                {
                    matches = new List<string>();
                }
                if (value.Any())
                {
                    matches.Add(value.First());
                }
            }
        }
    }
}