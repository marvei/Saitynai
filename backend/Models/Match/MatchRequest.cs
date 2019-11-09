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

        public void Add(Match match)
        {
            matchList.Add(match);
        }

        public String Remove(int matchId)
        {
            for (int i = 0; i < matchList.Count; i++)
            {
                Match temp = matchList.ElementAt(i);
                if (temp.MatchId.Equals(matchId))
                {
                    matchList.RemoveAt(i);
                    return "Delete successful";
                }
            }
            return "Delete unsuccessful";
        }

        public List<Match> getAllMatches()
        {
            return matchList;
        }

        public String UpdateMatch(Match match)
        {
            for (int i = 0; i < matchList.Count; i++)
            {
                Match temp = matchList.ElementAt(i);
                if (temp.MatchId.Equals(match.MatchId))
                {
                    matchList[i] = match; //update user
                    return "Update successful";
                }
            }
            return "Update unsuccessful";
        }

        public Match getMatchById(int id)
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
    }
}