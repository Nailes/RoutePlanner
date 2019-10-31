using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace RoutePlanner.Controllers
{
    [Authorize]
    [Route("api")]
    public class AccountController : ControllerBase
    {
        private readonly RouteplannerContext _context;

        public AccountController(RouteplannerContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("Signin")]
        public async Task Login(Users data)
        {
            Users user = _context.Users.FirstOrDefault(x => x.Login == data.Login && x.Pass == data.Pass);

            var identity = GetIdentity(user);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                login = identity.Name,
                id = user.IdUser
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [AllowAnonymous]
        [HttpPost("Signup")]
        public async Task<IActionResult> Register(Users data)
        {
            Users users = _context.Users.FirstOrDefault(x => x.Login == data.Login && x.Email == data.Email);
            if (users == null)
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400);
                }

                _context.Users.Add(data);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return StatusCode(400);
        }

        private ClaimsIdentity GetIdentity(Users user)
        {
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Email)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}