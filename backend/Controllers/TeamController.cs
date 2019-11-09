using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models.Team;

namespace backend.Controllers
{
    public class TeamController : ApiController
    {
        [HttpGet]
        [Route("api/team")]
        public List<Team> getUsers()
        {
            return TeamRequest.getInstance().getAllTeams();
        }

        [HttpGet]
        [Route("api/team/{teamId}")]
        public Team getUserById(int teamId)
        {
            return TeamRequest.getInstance().getTeamById(teamId);
        }

        [HttpPost]
        [Route("api/team")]
        public TeamReply registerTeam(Team team)
        {
            TeamReply teamRep = new TeamReply();
            TeamRequest.getInstance().Add(team);
            teamRep.Name = team.Name;
            teamRep.TeamId = team.TeamId;
            teamRep.TeamStatus = "Successful";

            return teamRep;
        }

        [HttpPut]
        [Route("api/team")]
        public String putUser(Team team)
        {
            return TeamRequest.getInstance().UpdateTeam(team);
        }

        [HttpDelete]
        [Route("api/team/{teamId}")]
        public String deleteTeam(int teamId)
        {
            return TeamRequest.getInstance().Remove(teamId);
        }
    }
}
