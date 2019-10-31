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

        [HttpGet("InfoUser")]
        public IActionResult GetUser(int IdUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = _context.Users.FirstOrDefault(x => x.IdUser == IdUser);

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

        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser(int IdUser, Users data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == IdUser);

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