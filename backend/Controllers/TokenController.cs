using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    public class TokenController : ApiController
    {
        //prideti vartotojo teises prisijungimo metu, kitu atveju leisti tik getint
        [HttpGet]
        [Authorize]
        [Route("api/jwt/ok")]
        public IHttpActionResult Authenticated() => Ok("Authenticated");

        [HttpGet]
        [Route("api/jwt/notok")]
        public IHttpActionResult NotAuthenticated() => Unauthorized();


        [HttpPost]
        [Route("api/jwt")]
        public IHttpActionResult Authenticate(User user)
        {
            var loginRequest = new User
            {
                Username = user.Username,
                Password = user.Password
            };

            User databaseUser;

            if (user != null)
            {
                databaseUser = DatabaseAccessModel.GetUserByUsername(loginRequest.Username);

                var isUsernamePasswordValid = SecurePasswordHasher.Verify(loginRequest.Password, databaseUser.Password);

                if (isUsernamePasswordValid)
                {
                    //loginRequest.id = databaseUser.id;
                    loginRequest.Role = databaseUser.Role;
                    //var token = CreateToken(loginRequest.Username);
                    var token = CreateToken(loginRequest.Role, loginRequest.Username);
                    return Ok(token);
                }
            }
            return Unauthorized();
        }

        private string CreateToken(string role, string name)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddDays(7);

            var tokenHandler = new JwtSecurityTokenHandler();


            //new Claim(ClaimTypes.Name, name)
            var claimsIdentity = new ClaimsIdentity(new[]
            {                 
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, name)
            });

            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, id));

            const string secretKey = "s1u3p5e8r6s8e5c9r5e6t";

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token =
                (JwtSecurityToken)
                tokenHandler.CreateJwtSecurityToken(
                    issuer: "http://localhost:44346/",
                    audience: "http://localhost:44346/",
                    subject: claimsIdentity,
                    notBefore: issuedAt,
                    expires: expires,
                    signingCredentials: signingCredentials);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
