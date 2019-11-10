using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models.Team;
using backend.Models.Match;
using backend.Models;

namespace backend.Controllers
{
    public class TeamController : ApiController
    {
        [HttpGet]
        [Route("api/team")]
        public IHttpActionResult getUsers()
        {
            List<Team> availableTeams = TeamRequest.getInstance().getAllTeams();
            if (availableTeams.Any())
            {
                return Ok(availableTeams);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/team/{teamId}")]
        public IHttpActionResult getUserById(string teamId)
        {
            Team currentTeam = TeamRequest.getInstance().getTeamById(teamId);
            if (currentTeam != null)
            {
                return Ok(currentTeam);
            }
            return NotFound();
        }


        //public TeamReply registerTeam(Team team)
        [HttpPost]
        [Route("api/team")]
        public IHttpActionResult registerTeam(Team team)
        {
            if (TeamRequest.getInstance().Add(team))
            {
                return Ok();
            }
            return BadRequest();

            //TeamReply teamRep = new TeamReply();
            //TeamRequest.getInstance().Add(team);
            //teamRep.Name = team.Name;
            //teamRep.TeamId = team.TeamId;
            //teamRep.TeamStatus = "Successful";

            //return teamRep;
        }

        [HttpPut]
        [Route("api/team")]
        public IHttpActionResult putUser(Team team)
        {
            if (TeamRequest.getInstance().UpdateTeam(team))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("api/team/{teamId}")]
        public IHttpActionResult deleteTeam(string teamId)
        {
            if (TeamRequest.getInstance().getTeamById(teamId) == null)
            {
                return NotFound();
            }
            if (TeamRequest.getInstance().Remove(teamId))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("api/team/{teamId}/{userId}")]
        public IHttpActionResult addUserToTeam(string teamId, string userId)
        {
            if (UserRequest.getInstance().getUserById(userId) == null)
            {
                return NotFound();
            }
            if (TeamRequest.getInstance().getTeamById(teamId) == null)
            {               
                return NotFound();
            }
            if (TeamRequest.getInstance().addUser(userId, teamId))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
