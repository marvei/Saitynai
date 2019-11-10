using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models;
using backend.Models.Team;

namespace backend.Controllers
{
    public class TeamPlayerController : ApiController
    {
        //[HttpPut]
        //[Route("api/team/player")]
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
