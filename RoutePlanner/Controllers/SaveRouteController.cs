using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutePlanner;

namespace RoutePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveRouteController : ControllerBase
    {
        private readonly RouteplannerContext _context;

        public SaveRouteController(RouteplannerContext context)
        {
            _context = context;
        }

        // GET: api/SaveRoute
        [HttpGet]
        public IEnumerable<Routes> GetRoutes()
        {
            return _context.Routes;
        }

        // GET: api/SaveRoute/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoutes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routes = await _context.Routes.FindAsync(id);

            if (routes == null)
            {
                return NotFound();
            }

            return Ok(routes);
        }

        // PUT: api/SaveRoute/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoutes([FromRoute] int id, [FromBody] Routes routes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != routes.IdRoutes)
            {
                return BadRequest();
            }

            _context.Entry(routes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoutesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SaveRoute
        [HttpPost]
        public async Task<IActionResult> PostRoutes([FromBody] Routes routes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Routes.Add(routes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoutes", new { id = routes.IdRoutes }, routes);
        }

        // DELETE: api/SaveRoute/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoutes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routes = await _context.Routes.FindAsync(id);
            if (routes == null)
            {
                return NotFound();
            }

            _context.Routes.Remove(routes);
            await _context.SaveChangesAsync();

            return Ok(routes);
        }

        private bool RoutesExists(int id)
        {
            return _context.Routes.Any(e => e.IdRoutes == id);
        }
    }
}