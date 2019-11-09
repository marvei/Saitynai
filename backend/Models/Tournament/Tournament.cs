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

        int tournamentId;

        public int TournamentId
        {
            get { return tournamentId; }
            set { tournamentId = value; }
        }
    }
}