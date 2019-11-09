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
        public List<User> getUsers()
        {
            return UserRequest.getInstance().getAllUsers();
        }

        //[Route("GetUserById/{userId}")]
        [HttpGet]
        [Route("api/user/{userId}")]
        public User getUserById(int userId)
        {
            return UserRequest.getInstance().getUserById(userId);
        }

        //[Route("CreateUser")]
        [HttpPost]
        [Route("api/user")]
        public UserReply registerUser(User user)
        {
            Console.WriteLine("In registerUser");
            UserReply userRep = new UserReply();
            UserRequest.getInstance().Add(user);
            userRep.Name = user.Name;
            userRep.Age = user.Age;
            userRep.UserId = user.UserId;
            userRep.UserStatus = "Successful";

            return userRep;
        }

        //[Route("UpdateUser")]
        [HttpPut]
        [Route("api/user")]
        public String putUser(User user)
        {
            return UserRequest.getInstance().UpdateUser(user);
        }


        //[Route("RemoveUser/{userId}")]
        [HttpDelete]
        [Route("api/user/{userId}")]
        public String deleteUser(int userId)
        {
            return UserRequest.getInstance().Remove(userId);
        }
    }
}
