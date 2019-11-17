using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models;

namespace backend.Controllers
{
    public class UserController : ApiController
    {
        //get all users
        [HttpGet]
        [Route("api/user/")]
        public IHttpActionResult GetUsers()
        {
            List<User> allUsers = DatabaseAccessModel.GetUserDataListFromSql().ToList();

            if (allUsers.Any())
            {
                return Content(HttpStatusCode.OK, allUsers);
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Message = "Users not found.",
                Code = HttpStatusCode.NotFound.ToString()
            });
        }

        [HttpGet]
        [Route("api/user/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            User user = DatabaseAccessModel.GetUserDataListFromSqlById(id);
            if (user.id != null)
            {
                return Ok(user);
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Message = "User with id " + id + " not found.",
                Code = HttpStatusCode.NotFound.ToString()
            });
        }

        //[Route("CreateUser")]
        //[HttpPost]
        //[Route("api/user")]
        //public IHttpActionResult registerUser(User user)
        //{

        //    if (UserRequest.getInstance().Add(user))
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();

        //    //Console.WriteLine("In registerUser");
        //    //UserReply userRep = new UserReply();
        //    //UserRequest.getInstance().Add(user);
        //    //userRep.Name = user.Name;
        //    //userRep.Age = user.Age;
        //    //userRep.UserId = user.UserId;
        //    //userRep.UserStatus = "Successful";

        //    //return teamRep;

        //}

        [HttpPost]
        [Route("api/user/")]
        public IHttpActionResult RegisterUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new Error
                {
                    Message = "Invalid data.",
                    Code = HttpStatusCode.BadRequest.ToString()
                });
            }
            user.Password = SecurePasswordHasher.Hash(user.Password);
            if (DatabaseAccessModel.AddUserToDatabase(user))
            {
                return Ok(User);
            }
            return Content(HttpStatusCode.BadRequest, new Error
            {
                Message = "Invalid user data/could not insert.",
                Code = HttpStatusCode.BadRequest.ToString()
            });
        }

        //[Route("UpdateUser")]
        //[HttpPut]
        //[Route("api/user/")]
        //public IHttpActionResult putUser(User user)
        //{
        //    if (UserRequest.getInstance().UpdateUser(user))
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

        [HttpPut]
        [Route("api/user/{id}")]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, new Error
                {
                    Message = "Invalid data.",
                    Code = HttpStatusCode.BadRequest.ToString()
                });
            }
            if (DatabaseAccessModel.CheckUserExists(id))
            {
                //user.Password = SecurePasswordHasher.Hash(user.Password);
                if (DatabaseAccessModel.UpdateUserToDatabase(user, id))
                {
                    return Ok();
                }
                return Content(HttpStatusCode.BadRequest, new Error
                {
                    Message = "Invalid user data/could not update.",
                    Code = HttpStatusCode.BadRequest.ToString()
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Message = "User with id " + id + " not found.",
                Code = HttpStatusCode.NotFound.ToString()
            });
        }


        //[Route("RemoveUser/{userId}")]
        //[HttpDelete]
        //[Route("api/user/{userId}")]
        //public IHttpActionResult deleteUser(string userId)
        //{
        //    if (UserRequest.getInstance().getUserById(userId) == null)
        //    {
        //        return NotFound();
        //    }
        //    if (UserRequest.getInstance().Remove(userId))
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

        [HttpDelete]
        [Route("api/user/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            if (DatabaseAccessModel.CheckUserExists(id))
            {
                try 
                {
                    if (DatabaseAccessModel.DeleteUser(id))
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
                Message = "User with id " + id + " not found.",
                Code = HttpStatusCode.NotFound.ToString()
            });
        }
    }
}
