using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Tournament
{
    public class TournamentRequest
    {
        List<Tournament> tournamentList;
        static TournamentRequest tournamentReq = null;

        private TournamentRequest()
        {
            tournamentList = new List<Tournament>();
        }

        public static TournamentRequest getInstance()
        {
            if (tournamentReq == null)
            {
                tournamentReq = new TournamentRequest();
                return tournamentReq;
            }
            else
            {
                return tournamentReq;
            }
        }

        public void Add(Tournament tournament)
        {
            tournamentList.Add(tournament);
        }

        public String Remove(int tournamentId)
        {
            for (int i = 0; i < tournamentList.Count; i++)
            {
                Tournament temp = tournamentList.ElementAt(i);
                if (temp.TournamentId.Equals(tournamentId))
                {
                    tournamentList.RemoveAt(i);
                    return "Delete successful";
                }
            }
            return "Delete unsuccessful";
        }

        public List<Tournament> getAllTournaments()
        {
            return tournamentList;
        }

        public String UpdateTournament(Tournament tournament)
        {
            for (int i = 0; i < tournamentList.Count; i++)
            {
                Tournament temp = tournamentList.ElementAt(i);
                if (temp.TournamentId.Equals(tournament.TournamentId))
                {
                    tournamentList[i] = tournament; //update user
                    return "Update successful";
                }
            }
            return "Update unsuccessful";
        }

        public Tournament getTournamentById(int id)
        {
            for (int i = 0; i < tournamentList.Count; i++)
            {
                Tournament temp = tournamentList.ElementAt(i);
                if (temp.TournamentId.Equals(id))
                {
                    return temp;
                }
            }
            return null;
        }
    }
}