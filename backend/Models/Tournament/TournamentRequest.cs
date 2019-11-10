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

        public bool Add(Tournament tournament)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string str = guid.ToString();
                tournament.TournamentId = str;
                tournament.Matches = new List<string>();
                tournamentList.Add(tournament);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Remove(string tournamentId)
        {
            for (int i = 0; i < tournamentList.Count; i++)
            {
                Tournament temp = tournamentList.ElementAt(i);
                if (temp.TournamentId.Equals(tournamentId))
                {
                    tournamentList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public List<Tournament> getAllTournaments()
        {
            return tournamentList;
        }

        public bool UpdateTournament(Tournament tournament)
        {
            for (int i = 0; i < tournamentList.Count; i++)
            {
                Tournament temp = tournamentList.ElementAt(i);
                if (temp.TournamentId.Equals(tournament.TournamentId))
                {
                    tournamentList[i] = tournament; //update user
                    return true;
                }
            }
            return false;
        }

        public Tournament getTournamentById(string id)
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

        public bool addMatch(string id, string tournamentId)
        {
            Tournament myTournament = getTournamentById(tournamentId);
            if (myTournament != null)
            {
                myTournament.Matches.Add(id);
                return true;
            }
            return false;
        }
    }
}