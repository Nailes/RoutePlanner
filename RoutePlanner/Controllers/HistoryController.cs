using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;


namespace RoutePlanner.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly RouteplannerContext _context;

        public HistoryController(RouteplannerContext context)
        {
            _context = context;
        }

        [HttpGet("History")]
        public IActionResult GetHistory(int IdUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var route = _context.Routes.Where(x => x.IdUser == IdUser).Select(c => new { id = c.IdRoutes, title = c.Title, date = c.DateRoutes });

            return Ok(route);
        }

        [HttpGet("Search")]
        public IActionResult Search(int IdUser, string name = "", DateTime? date = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var route = _context.Routes.Where(x => x.IdUser == IdUser && EF.Functions.Like(x.Title, "%%".Insert(1, name)) && EF.Functions.Like(x.DateRoutes.ToString(), "%%".Insert(1, date.ToString())))
            .Select(c => new { id = c.IdRoutes, title = c.Title, date = c.DateRoutes });

            return Ok(route);
        }

        [HttpDelete("DeleteRoute")]
        public async Task<IActionResult> DeleteRoute(int IdUser, int IdRoute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routes = await _context.Routes.FindAsync(IdRoute);
            if (routes == null)
            {
                return NotFound();
            }

            _context.Routes.Remove(routes);
            await _context.SaveChangesAsync();

            var route =  _context.Routes.Where(x => x.IdUser == IdUser).Select(c => new { id = c.IdRoutes, title = c.Title, date = c.DateRoutes });

            return Ok(route);
        }
    }
}