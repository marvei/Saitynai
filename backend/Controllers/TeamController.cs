using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using backend.Models;

namespace backend.Controllers
{
    public class TeamController : ApiController
    {
        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("api/tournament/{tournamentId:int}/match/{matchId:int}/team/")]
        public IHttpActionResult AddTeam(int tournamentId, int matchId, Team team)
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

        [Authorize(Roles = UserRoles.Admin)]
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

        [Authorize(Roles = UserRoles.Admin)]
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

        [Authorize]
        [HttpPut]
        [Route("api/team/{teamId}/user/{userId}")]
        public IHttpActionResult AddUserToTeam(int userId, int teamId)
        {
            //teamid owner - principal user id
            if (DatabaseAccessModel.GetUserByUsername(Thread.CurrentPrincipal.Identity.Name).id != (DatabaseAccessModel.GetTeamById(teamId)).ownerId && !Thread.CurrentPrincipal.IsInRole(UserRoles.Admin))
            {
                return Content(HttpStatusCode.Forbidden, new Error
                {
                    Message = "Your id doesn't match with this user.",
                    Code = HttpStatusCode.Forbidden.ToString()
                });
            }
            if (DatabaseAccessModel.CheckTeamExists(teamId))
            {
                if (DatabaseAccessModel.CheckUserExists(userId))
                {
                    if (DatabaseAccessModel.AddUserToTeam(userId, teamId))
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.BadRequest, new Error
                    {
                        Code = HttpStatusCode.BadRequest.ToString(),
                        Message = "Bad data/could not update."
                    });
                }
                return Content(HttpStatusCode.NotFound, new Error
                {
                    Code = HttpStatusCode.NotFound.ToString(),
                    Message = "User with id " + userId + " not found."
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Code = HttpStatusCode.NotFound.ToString(),
                Message = "Team with id " + teamId + " not found."
            });
        }
    }
}
