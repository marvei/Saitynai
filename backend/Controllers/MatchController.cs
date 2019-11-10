using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models.Match;
using backend.Models.Team;

namespace backend.Controllers
{
    public class MatchController : ApiController
    {
        [HttpGet]
        [Route("api/match")]
        public IHttpActionResult getUsers()
        {
            List<Match> availableMatches = MatchRequest.getInstance().getAllMatches();
            if (availableMatches.Any())
            {
                return Ok(availableMatches);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/match/{matchId}")]
        public IHttpActionResult getMatchById(string matchId)
        {
            Match currentMatch = MatchRequest.getInstance().getMatchById(matchId);
            if (currentMatch != null)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/match")]
        public IHttpActionResult registerUser(Match match)
        {

            if (MatchRequest.getInstance().Add(match))
            {
                return Ok();
            }
            return BadRequest();

            //Console.WriteLine("In registerUser");
            //MatchReply matchRep = new MatchReply();
            //MatchRequest.getInstance().Add(match);
            //matchRep.Name = match.Name;
            //matchRep.Date = match.Date;
            //matchRep.MatchId = match.MatchId;
            //matchRep.MatchStatus = "Successful";

            //return matchRep;
        }

        [HttpPut]
        [Route("api/match")]
        public IHttpActionResult putMatch(Match match)
        {
            if (MatchRequest.getInstance().UpdateMatch(match))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("api/match/{matchId}")]
        public IHttpActionResult deleteUser(string matchId)
        {
            if (MatchRequest.getInstance().getMatchById(matchId) == null)
            {
                return NotFound();
            }
            if (MatchRequest.getInstance().Remove(matchId))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("api/match/{matchId}/{teamId}")]
        public IHttpActionResult addTeamToMatch(string matchId, string teamId)
        {
            if (TeamRequest.getInstance().getTeamById(teamId) == null)
            {
                return NotFound();
            }
            if (MatchRequest.getInstance().getMatchById(matchId) == null)
            {
                return NotFound();
            }
            if (MatchRequest.getInstance().addTeam(teamId, matchId))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
