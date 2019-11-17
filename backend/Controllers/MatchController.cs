using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models;

namespace backend.Controllers
{
    public class MatchController : ApiController
    {
        //[HttpGet]
        //[Route("api/match")]
        //public IHttpActionResult getUsers()
        //{
        //    List<Match> availableMatches = MatchRequest.getInstance().getAllMatches();
        //    if (availableMatches.Any())
        //    {
        //        return Ok(availableMatches);
        //    }
        //    return NotFound();
        //}

        [HttpGet]
        [Route("api/tournament/{tournamentId:int}/match/")]
        public IHttpActionResult GetMatchesByTournamentId(int tournamentId)
        {
            if (DatabaseAccessModel.CheckTournamentExists(tournamentId))
            {
                List<Match> allMatches = DatabaseAccessModel.GetMatchListByTournamentId(tournamentId).ToList();
                if (allMatches.Any())
                {
                    return Content(HttpStatusCode.OK, allMatches);
                }
                return Content(HttpStatusCode.NotFound, new Error
                {
                    Code = HttpStatusCode.NotFound.ToString(),
                    Message = "Matches for tournament with id " + tournamentId + " not found."
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Code = HttpStatusCode.NotFound.ToString(),
                Message = "Tournament with id " + tournamentId + " not found."
            });
        }

        //[HttpGet]
        //[Route("api/match/{matchId}")]
        //public IHttpActionResult getMatchById(string matchId)
        //{
        //    Match currentMatch = MatchRequest.getInstance().getMatchById(matchId);
        //    if (currentMatch != null)
        //    {
        //        return Ok();
        //    }
        //    return NotFound();
        //}

        [HttpGet]
        [Route("api/tournament/{tournamentId:int}/match/{matchId:int}")]
        public IHttpActionResult GetMatchById(int tournamentId, int matchId)
        {
            //select from match sujungto su tournament kur match>fk_tourId = tourId
            if (DatabaseAccessModel.CheckTournamentExists(tournamentId))
            {
                if (DatabaseAccessModel.CheckMatchInTournamentExists(tournamentId, matchId))
                {
                    Match match = DatabaseAccessModel.GetMatchDataListFromSqlbyId(matchId);
                    if (match.id != null)
                    {
                        return Content(HttpStatusCode.OK, match);
                    }
                    return Content(HttpStatusCode.NotFound, new Error
                    {
                        Code = HttpStatusCode.NotFound.ToString(),
                        Message = "Matches for specified tournament with id " + tournamentId + " not found."
                    });
                }
                return Content(HttpStatusCode.NotFound, new Error
                {
                    Code = HttpStatusCode.NotFound.ToString(),
                    Message = "No match found with id " + matchId + " in tournament with id " + tournamentId + "."
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Code = HttpStatusCode.NotFound.ToString(),
                Message = "Tournament with id " + tournamentId + " not found."
            });
        }

        [HttpPost]
        [Route("api/tournament/{tournamentId:int}/match/")]
        public IHttpActionResult RegisterUser(int tournamentId, Match match)
        {
            //check if tournament by tournamentID exists and create a match with fk_tour = tourId
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new Error
                {
                    Message = "Invalid Data",
                    Code = HttpStatusCode.BadRequest.ToString()
                });
            }
            if (DatabaseAccessModel.CheckTournamentExists(tournamentId))
            {
                if (DatabaseAccessModel.AddNewMatchToDatabaseByTournament(match, tournamentId))
                {
                    return Ok(match);
                }
                return Content(HttpStatusCode.BadRequest, new Error
                {
                    Message = "Invalid team data/could not insert.",
                    Code = HttpStatusCode.BadRequest.ToString()
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Code = HttpStatusCode.NotFound.ToString(),
                Message = "Tournament with id " + tournamentId + " not found."
            });
        }

        [HttpPut]
        [Route("api/tournament/{tournamentId:int}/match/{matchId:int}")]
        public IHttpActionResult PutMatch(int tournamentId, int matchId, Match match)
        {
            //check if tournament with id exists
            //check if match with id exists
            //update match by id
            
            //if (!ModelState.IsValid)
            //{
            //    return Content(HttpStatusCode.BadRequest, new Error
            //    {
            //        Message = "Invalid Data",
            //        Code = HttpStatusCode.BadRequest.ToString()
            //    });
            //}
            if (DatabaseAccessModel.CheckTournamentExists(tournamentId))
            {
                if (DatabaseAccessModel.CheckMatchInTournamentExists(tournamentId, matchId))
                {
                    if (DatabaseAccessModel.UpdateMatchToDatabase(matchId, match))
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.BadRequest, new Error
                    {
                        Code = HttpStatusCode.NotFound.ToString(),
                        Message = "Invalid team data/could not update."
                    });
                }
                return Content(HttpStatusCode.NotFound, new Error
                {
                    Code = HttpStatusCode.NotFound.ToString(),
                    Message = "Match with id " + matchId + " not found in tournament match list."
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Code = HttpStatusCode.NotFound.ToString(),
                Message = "Tournament with id " + tournamentId + " not found."
            });
        }

        [HttpDelete]
        [Route("api/tournament/{tournamentId:int}/match/{matchId:int}")]
        public IHttpActionResult DeleteUser(int tournamentId, int matchId)
        {
            if (DatabaseAccessModel.CheckTournamentExists(tournamentId))
            {
                if (DatabaseAccessModel.CheckMatchInTournamentExists(tournamentId, matchId))
                {
                    try
                    {
                        if (DatabaseAccessModel.DeleteMatchFromTournament(matchId))
                        {
                            return Ok();
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        return Content(HttpStatusCode.NotAcceptable, ex.Message);
                    }
                }
                return Content(HttpStatusCode.NotFound, new Error
                {
                    Code = HttpStatusCode.NotFound.ToString(),
                    Message = "Match with id " + matchId + " not found in tournament match list."
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Code = HttpStatusCode.NotFound.ToString(),
                Message = "Tournament with id " + tournamentId + " not found."
            });
        }

        //[HttpPut]
        //[Route("api/match/{matchId}/{teamId}")]
        //public IHttpActionResult addTeamToMatch(string matchId, string teamId)
        //{
        //    if (TeamRequest.getInstance().getTeamById(teamId) == null)
        //    {
        //        return NotFound();
        //    }
        //    if (MatchRequest.getInstance().getMatchById(matchId) == null)
        //    {
        //        return NotFound();
        //    }
        //    if (MatchRequest.getInstance().addTeam(teamId, matchId))
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}
    }
}
