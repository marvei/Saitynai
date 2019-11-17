using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models;

namespace backend.Controllers
{
    public class TeamController : ApiController
    {
        //[HttpGet]
        //[Route("api/team")]
        //public IHttpActionResult getUsers()
        //{
        //    List<Team> availableTeams = TeamRequest.getInstance().getAllTeams();
        //    if (availableTeams.Any())
        //    {
        //        return Ok(availableTeams);
        //    }
        //    return NotFound();
        //}

        [HttpGet]
        [Route("api/tournament/{tournamentId:int}/match/{matchId:int}/team/")]
        public IHttpActionResult GetTeams(int tournamentId, int matchId)
        {
            if (DatabaseAccessModel.CheckTournamentExists(tournamentId))
            {
                if (DatabaseAccessModel.CheckMatchInTournamentExists(tournamentId, matchId))
                {
                    List<Team> allTeams = DatabaseAccessModel.GetTeamListByMatchId(matchId).ToList();
                    if (allTeams.Any())
                    {
                        return Content(HttpStatusCode.OK, allTeams);
                    }
                    return Content(HttpStatusCode.NotFound, new Error
                    {
                        Code = HttpStatusCode.NotFound.ToString(),
                        Message = "Teams for specific tournament and match not found."
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

        //[HttpGet]
        //[Route("api/team/{teamId}")]
        //public IHttpActionResult getUserById(string teamId)
        //{
        //    Team currentTeam = TeamRequest.getInstance().getTeamById(teamId);
        //    if (currentTeam != null)
        //    {
        //        return Ok(currentTeam);
        //    }
        //    return NotFound();
        //}

        [HttpGet]
        [Route("api/tournament/{tournamentId:int}/match/{matchId:int}/team/{teamId:int}")]
        public IHttpActionResult GetTeamById(int tournamentId, int matchId, int teamId)
        {
            if (DatabaseAccessModel.CheckTournamentExists(tournamentId))
            {
                if (DatabaseAccessModel.CheckMatchInTournamentExists(tournamentId, matchId))
                {
                    if (DatabaseAccessModel.CheckTeamInMatchExists(matchId, teamId))
                    {
                        Team team = DatabaseAccessModel.GetTeamDataListFromSqlbyId(teamId);
                        if (team.id != null)
                        {
                            return Content(HttpStatusCode.OK, team);
                        }
                        return Content(HttpStatusCode.NotFound, new Error
                        {
                            Code = HttpStatusCode.NotFound.ToString(),
                            Message = "Teams for specific tournament and match not found."
                        });
                    }
                    return Content(HttpStatusCode.NotFound, new Error
                    {
                        Code = HttpStatusCode.NotFound.ToString(),
                        Message = "Team with id " + teamId + " not found in match list."
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

        //public TeamReply registerTeam(Team team)
        [HttpPost]
        [Route("api/tournament/{tournamentId:int}/match/{matchId:int}/team/")]
        public IHttpActionResult AddTeam( int tournamentId, int matchId, Team team)
        {
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
                if (DatabaseAccessModel.CheckMatchInTournamentExists(tournamentId, matchId))
                {
                    if (DatabaseAccessModel.AddTeamToDatabaseByMatch(team, matchId))
                    {
                        return Ok(team);
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
                    Message = "Match with id " + matchId + " not found in tournament match list."
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Code = HttpStatusCode.NotFound.ToString(),
                Message = "Tournament with id " + tournamentId + " not found."
            });
        }

        [HttpPut]
        [Route("api/tournament/{tournamentId:int}/match/{matchId:int}/team/{teamId:int}")]
        public IHttpActionResult PutUser(int tournamentId, int matchId, int teamId, Team team)
        {
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
                    if (DatabaseAccessModel.CheckTeamInMatchExists(matchId, teamId))
                    {
                        if (DatabaseAccessModel.UpdateTeamToDatabase(teamId, team))
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
                        Message = "Team with id " + teamId + " not found in match " + matchId + " team list."
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
        [Route("api/tournament/{tournamentId:int}/match/{matchId:int}/team/{teamId:int}")]
        public IHttpActionResult DeleteUser(int tournamentId, int matchId, int teamId)
        {
            if (DatabaseAccessModel.CheckTournamentExists(tournamentId))
            {
                if (DatabaseAccessModel.CheckMatchInTournamentExists(tournamentId, matchId))
                {
                    if (DatabaseAccessModel.CheckTeamInMatchExists(matchId, teamId))
                    {
                        try
                        {
                            if (DatabaseAccessModel.DeleteTeam(teamId))
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
                        Message = "Team with id " + teamId + " not found in match " + matchId + " team list."
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

        //[HttpPut]
        //[Route("api/team/{teamId}/{userId}")]
        //public IHttpActionResult addUserToTeam(string teamId, string userId)
        //{
        //    if (UserRequest.getInstance().getUserById(userId) == null)
        //    {
        //        return NotFound();
        //    }
        //    if (TeamRequest.getInstance().getTeamById(teamId) == null)
        //    {               
        //        return NotFound();
        //    }
        //    if (TeamRequest.getInstance().addUser(userId, teamId))
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}
    }
}
