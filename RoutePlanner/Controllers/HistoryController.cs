using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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

        // GET: api/History?id=1
        //[AllowAnonymous]
        [HttpGet("History")]
        public IActionResult GetHistory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var route = _context.Routes.Where(x => x.IdUser == id).Select(c => new { id = c.IdUser, title = c.Title, date = c.DateRoutes });

            return Ok(route);
        }

        // GET: api/Search?id=1"&"name=test4"&"date=2017-01-21T00:00:00
        //[AllowAnonymous]
        [HttpGet("Search")]
        public IActionResult Search(int id, string name = "", DateTime? date = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var route = _context.Routes.Where(x => x.IdUser == id && EF.Functions.Like(x.Title, "%%".Insert(1, name)) && EF.Functions.Like(x.DateRoutes.ToString(), "%%".Insert(1, date.ToString())))
            .Select(c => new { id = c.IdUser, title = c.Title, date = c.DateRoutes });

            return Ok(route);
        }        
    }
}