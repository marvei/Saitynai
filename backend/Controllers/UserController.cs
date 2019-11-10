using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models;

namespace backend.Controllers
{
    //[Route("api/[controller]")]
    public class UserController : ApiController
    {
        //[Route("GetUsers")]
        [HttpGet]
        [Route("api/user")]
        public IHttpActionResult getUsers()
        {
            List<User> availableUsers = UserRequest.getInstance().getAllUsers();
            if (availableUsers.Any())
            {
                return Ok(availableUsers);
            }
            return NotFound();
        }

        //[Route("GetUserById/{userId}")]
        [HttpGet]
        [Route("api/user/{userId}")]
        public IHttpActionResult getUserById(string userId)
        {
            User requestedUser = UserRequest.getInstance().getUserById(userId);
            if (requestedUser != null)
            {
                return Ok(requestedUser);
            }
            return NotFound();
        }

        //[Route("CreateUser")]
        [HttpPost]
        [Route("api/user")]
        public IHttpActionResult registerUser(User user)
        {

            if (UserRequest.getInstance().Add(user))
            {
                return Ok();
            }
            return BadRequest();

            //Console.WriteLine("In registerUser");
            //UserReply userRep = new UserReply();
            //UserRequest.getInstance().Add(user);
            //userRep.Name = user.Name;
            //userRep.Age = user.Age;
            //userRep.UserId = user.UserId;
            //userRep.UserStatus = "Successful";

            //return teamRep;

        }

        //[Route("UpdateUser")]
        [HttpPut]
        [Route("api/user")]
        public IHttpActionResult putUser(User user)
        {
            if (UserRequest.getInstance().UpdateUser(user))
            {
                return Ok();
            }
            return BadRequest();
        }


        //[Route("RemoveUser/{userId}")]
        [HttpDelete]
        [Route("api/user/{userId}")]
        public IHttpActionResult deleteUser(string userId)
        {
            if (UserRequest.getInstance().getUserById(userId) == null)
            {
                return NotFound();
            }
            if (UserRequest.getInstance().Remove(userId))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
