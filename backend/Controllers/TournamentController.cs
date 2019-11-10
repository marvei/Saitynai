using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models.Tournament;
using backend.Models.Match;

namespace backend.Controllers
{
    public class TournamentController : ApiController
    {
        [HttpGet]
        [Route("api/tournament")]
        public IHttpActionResult getTournaments()
        {
            List<Tournament> availableTournaments = TournamentRequest.getInstance().getAllTournaments();
            if (availableTournaments.Any())
            {
                return Ok(availableTournaments);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/tournament/{tournamentId}")]
        public IHttpActionResult getTournamentById(string tournamentId)
        {
            Tournament currentTournament = TournamentRequest.getInstance().getTournamentById(tournamentId);
            if (currentTournament != null)
            {
                return Ok(currentTournament);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/tournament")]
        public IHttpActionResult registerTournament(Tournament tournament)
        {
            if (TournamentRequest.getInstance().Add(tournament))
            {
                return Ok();
            }
            return BadRequest();

            //TournamentReply tournamentRep = new TournamentReply();
            //TournamentRequest.getInstance().Add(tournament);
            //tournamentRep.Name = tournament.Name;
            //tournamentRep.TournamentId = tournament.TournamentId;
            //tournamentRep.TournamentStatus = "Successful";

            //return tournamentRep;
        }

        [HttpPut]
        [Route("api/tournament")]
        public IHttpActionResult putTournament(Tournament tournament)
        {
            if (TournamentRequest.getInstance().UpdateTournament(tournament))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("api/tournament/{tournamentId}")]
        public IHttpActionResult deleteTournament(string tournamentId)
        {
            if (TournamentRequest.getInstance().getTournamentById(tournamentId) == null)
            {
                return NotFound();
            }
            if (TournamentRequest.getInstance().Remove(tournamentId))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("api/tournament/{tournamentId}/{matchId}")]
        public IHttpActionResult addMatchToTournament(string tournamentId, string matchId)
        {
            if (MatchRequest.getInstance().getMatchById(matchId) == null)
            {
                return NotFound();
            }
            if (TournamentRequest.getInstance().getTournamentById(tournamentId) == null)
            {
                return NotFound();
            }
            if (TournamentRequest.getInstance().addMatch(matchId, tournamentId))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
