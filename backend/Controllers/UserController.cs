using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Controllers
{
    public class UserController : ApiController
    {
        //get all users
        [AllowAnonymous]
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

        [Authorize]
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

        [AllowAnonymous]
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

        [Authorize]
        [HttpPut]
        [Route("api/user/{id}")]
        public IHttpActionResult PutUser(int id, User user)
        {
            //var a = Thread.CurrentPrincipal.Identity.Name;
            if (DatabaseAccessModel.GetUserByUsername(Thread.CurrentPrincipal.Identity.Name).id != id && !Thread.CurrentPrincipal.IsInRole(UserRoles.Admin))
            {
                return Content(HttpStatusCode.Forbidden, new Error
                {
                    Message = "Your id doesn't match with this user.",
                    Code = HttpStatusCode.Forbidden.ToString()
                });
            }
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

        //[Authorize(Roles = UserRoles.Admin + ";" + UserRoles.Registered)]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Registered)]
        [HttpDelete]
        [Route("api/user/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            if (DatabaseAccessModel.GetUserByUsername(Thread.CurrentPrincipal.Identity.Name).id != id && !Thread.CurrentPrincipal.IsInRole(UserRoles.Admin))
            {
                return Content(HttpStatusCode.Forbidden, new Error
                {
                    Message = "Your id doesn't match with this user.",
                    Code = HttpStatusCode.Forbidden.ToString()
                });
            }
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

        [Authorize]
        [HttpPost]
        [Route("api/user/{userId}/team")]
        public IHttpActionResult UserTeamCreate(int userId, Team team)
        {
            if (DatabaseAccessModel.CheckUserExists(userId))
            {
                if (DatabaseAccessModel.AddTeamToDatabaseByUser(team, userId))
                {
                    return Ok(team);
                }
                return Content(HttpStatusCode.BadRequest, new Error
                {
                    Code = HttpStatusCode.BadRequest.ToString(),
                    Message = "Invalid team data/could not insert."
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Code = HttpStatusCode.NotFound.ToString(),
                Message = "User with id " + userId + " not found."
            });
        }

        [Authorize]
        [HttpGet]
        [Route("api/user/{userId}/team")]
        public IHttpActionResult getUserTeam(int userId)
        {
            if (DatabaseAccessModel.CheckUserExists(userId))
            {
                Team userTeam = DatabaseAccessModel.GetTeamByOwner(userId);
                if (userTeam.id != null)
                {
                    return Ok(userTeam);
                }
                return Content(HttpStatusCode.NotFound, new Error
                {
                    Code = HttpStatusCode.NotFound.ToString(),
                    Message = "No team ownership of user with id " + userId + " found."
                });
            }
            return Content(HttpStatusCode.NotFound, new Error
            {
                Code = HttpStatusCode.NotFound.ToString(),
                Message = "User with id " + userId + " not found."
            });
        }
    }
}
