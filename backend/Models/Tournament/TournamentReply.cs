using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Tournament
{
    public class TournamentReply
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

        String tournamentStatus;

        public String TournamentStatus
        {
            get { return tournamentStatus; }
            set { tournamentStatus = value; }
        }
    }
}