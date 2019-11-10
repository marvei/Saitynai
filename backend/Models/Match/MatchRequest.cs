using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Match
{
    public class MatchRequest
    {
        List<Match> matchList;
        static MatchRequest matchReq = null;

        private MatchRequest()
        {
            matchList = new List<Match>();
        }

        public static MatchRequest getInstance()
        {
            if (matchReq == null)
            {
                matchReq = new MatchRequest();
                return matchReq;
            }
            else
            {
                return matchReq;
            }
        }

        public bool Add(Match match)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string str = guid.ToString();
                match.MatchId = str;
                match.Teams = new List<string>();
                matchList.Add(match);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public bool Remove(string matchId)
        {
            for (int i = 0; i < matchList.Count; i++)
            {
                Match temp = matchList.ElementAt(i);
                if (temp.MatchId.Equals(matchId))
                {
                    matchList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public List<Match> getAllMatches()
        {
            return matchList;
        }

        public bool UpdateMatch(Match match)
        {
            for (int i = 0; i < matchList.Count; i++)
            {
                Match temp = matchList.ElementAt(i);
                if (temp.MatchId.Equals(match.MatchId))
                {
                    matchList[i] = match; //update user
                    return true;
                }
            }
            return false;
        }

        public Match getMatchById(string id)
        {
            for (int i = 0; i < matchList.Count; i++)
            {
                Match temp = matchList.ElementAt(i);
                if (temp.MatchId.Equals(id))
                {
                    return temp;
                }
            }
            return null;
        }

        public bool addTeam(string id, string matchId)
        {
            Match myMatch = getMatchById(matchId);
            if (myMatch != null)
            {
                myMatch.Teams.Add(id);
                return true;
            }
            return false;
        }
    }
}