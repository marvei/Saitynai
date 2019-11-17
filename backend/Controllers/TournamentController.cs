using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models;

namespace backend.Controllers
{
    public class TournamentController : ApiController
    {

        //get tournament list (ALL)
        [HttpGet]
        [Route("api/tournament/")]
        public IHttpActionResult GetTournaments()
        {
            List<Tournament> allTournaments = DatabaseAccessModel.GetTournamentListFromSql().ToList();
            if (allTournaments.Any())
            {
                return Content(HttpStatusCode.OK, allTournaments);
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Message = "Tournaments not found.",
                Code = HttpStatusCode.NotFound.ToString()
            });
        }

        //get tournament by ID
        [HttpGet]
        [Route("api/tournament/{id}")]
        public IHttpActionResult GetTournamentById(int id)
        {
            Tournament tournament = DatabaseAccessModel.GetTournamentListFromSqlbyId(id);
            if (tournament.id != null)
            {
                return Ok(tournament);
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Message = "Tournament with id " + id + " not found.",
                Code = HttpStatusCode.NotFound.ToString()
            });
        }

        //Add tournament to database
        [HttpPost]
        [Route("api/tournament/")]
        public IHttpActionResult AddTournament(Tournament tournament)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new Error
                {
                    Message = "Invalid data.",
                    Code = HttpStatusCode.BadRequest.ToString()
                });
            }
            if (DatabaseAccessModel.AddTournamentToDatabase(tournament))
            {
                return Ok(tournament);
            }
            return Content(HttpStatusCode.BadRequest, new Error
            {
                Message = "Invalid tournament data/could not insert.",
                Code = HttpStatusCode.BadRequest.ToString()
            });
        }

        //update tournament
        [HttpPut]
        [Route("api/tournament/{id}")]
        public IHttpActionResult putTournament(Tournament tournament, int id)
        {
            if (DatabaseAccessModel.CheckTournamentExists(id))
            {
                if (DatabaseAccessModel.UpdateTournamentToDatabase(id, tournament))
                {
                    return Ok();
                }
                return Content(HttpStatusCode.BadRequest, new Error
                {
                    Message = "Invalid tournament data/could not update.",
                    Code = HttpStatusCode.BadRequest.ToString()
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Message = "Tournament with id  " + id + " not found.",
                Code = HttpStatusCode.NotFound.ToString()
            });
        }

        //delete tournament
        [HttpDelete]
        [Route("api/tournament/{tournamentId}")]
        public IHttpActionResult DeleteTournament(int tournamentId)
        {
            if (DatabaseAccessModel.CheckTournamentExists(tournamentId))
            {
                try
                {
                    if (DatabaseAccessModel.DeleteTournament(tournamentId))
                    {
                        return Ok();
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    return Content(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }
            return Ok();
        }

        //[HttpPut]
        //[Route("api/tournament")]
        //public IHttpActionResult addMatchToTournament(string tournamentId, string matchId)
        //{
        //    //change match by id fk_tournamentid to tournamentId
        //    return Ok();
        //}
    }
}
