using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models.Tournament;

namespace backend.Controllers
{
    public class TournamentController : ApiController
    {
        [HttpGet]
        [Route("api/tournament")]
        public List<Tournament> getTournaments()
        {
            return TournamentRequest.getInstance().getAllTournaments();
        }

        [HttpGet]
        [Route("api/tournament/{tournamentId}")]
        public Tournament getTournamentById(int tournamentId)
        {            
            return TournamentRequest.getInstance().getTournamentById(tournamentId);
        }

        [HttpPost]
        [Route("api/tournament")]
        public TournamentReply registerTournament(Tournament tournament)
        {
            TournamentReply tournamentRep = new TournamentReply();
            TournamentRequest.getInstance().Add(tournament);
            tournamentRep.Name = tournament.Name;
            tournamentRep.TournamentId = tournament.TournamentId;
            tournamentRep.TournamentStatus = "Successful";

            return tournamentRep;
        }

        [HttpPut]
        [Route("api/tournament")]
        public String putTournament(Tournament tournament)
        {
            return TournamentRequest.getInstance().UpdateTournament(tournament);
        }

        [HttpDelete]
        [Route("api/tournament/{tournamentId}")]
        public String deleteTournament(int tournamentId)
        {
            return TournamentRequest.getInstance().Remove(tournamentId);
        }
    }
}
