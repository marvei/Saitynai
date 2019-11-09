using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models.Match;

namespace backend.Controllers
{
    public class MatchController : ApiController
    {
        [HttpGet]
        [Route("api/match")]
        public List<Match> getUsers()
        {
            return MatchRequest.getInstance().getAllMatches();
        }

        [HttpGet]
        [Route("api/match/{matchId}")]
        public Match getMatchById(int matchId)
        {
            return MatchRequest.getInstance().getMatchById(matchId);
        }

        [HttpPost]
        [Route("api/match")]
        public MatchReply registerUser(Match match)
        {
            Console.WriteLine("In registerUser");
            MatchReply matchRep = new MatchReply();
            MatchRequest.getInstance().Add(match);
            matchRep.Name = match.Name;
            matchRep.Date = match.Date;
            matchRep.MatchId = match.MatchId;
            matchRep.MatchStatus = "Successful";

            return matchRep;
        }

        [HttpPut]
        [Route("api/match")]
        public String putMatch(Match match)
        {
            return MatchRequest.getInstance().UpdateMatch(match);
        }

        [HttpDelete]
        [Route("api/match/{matchId}")]
        public String deleteUser(int matchId)
        {
            return MatchRequest.getInstance().Remove(matchId);
        }
    }
}
