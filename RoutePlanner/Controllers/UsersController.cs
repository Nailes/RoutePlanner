using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace RoutePlanner.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RouteplannerContext _context;

        public UsersController(RouteplannerContext context)
        {
            _context = context;
        }

        // GET: api/Users
        //[AllowAnonymous]
        [HttpGet("InfoUser")]
        public IActionResult GetUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = _context.Users.FirstOrDefault(x => x.IdUser == id);

            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var response = new
            {
                login = user.Login,
                email = user.Email,
                password = user.Pass
            };

            return Ok(response);
        }

        // PUT: api/EditUser?id=1 -Body(@{Login = "user1"; Pass = "123"; Email = "sdcsd@dsd.com";})
        //[AllowAnonymous]
        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser(int id, [FromBody] Users data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);

            if (user == null)
            {
                return BadRequest(ModelState);
            }
            if (data.Login != null)
            {
                user.Login = data.Login;
            }
            if (data.Pass != null)
            {
                user.Pass = data.Pass;
            }
            if (data.Email != null)
            {
                user.Email = data.Email;
            }

            await _context.SaveChangesAsync();

            return Ok(data);
        }
    }
}