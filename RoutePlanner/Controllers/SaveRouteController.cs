using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutePlanner;
using Microsoft.AspNetCore.Authorization;

namespace RoutePlanner.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class SaveRouteController : ControllerBase
    {
        private readonly RouteplannerContext _context;

        public SaveRouteController(RouteplannerContext context)
        {
            _context = context;
        }

        // GET: /api/Route?id=1
        //[AllowAnonymous]
        [HttpGet("Route")]
        public IActionResult GetRoute(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var route = _context.Routes.Where(x => x.IdRoutes == id)
            .Select(c => new
            {
                title = c.Title,
                date = c.DateRoutes,
                stage = c.Stage.Where(a => a.IdRoutes == id)
            .Select(b => new
            {
                place = b.Place,
                date = b.DateStage,
                comments = b.Comments.Where(d => d.IdStage == b.IdStage)
            .Select(e => new
            {
                note = e.Note, date = e.DateNote })
            })
            });

            if (route == null)
            {
                return BadRequest(ModelState);
            }
            return Ok(route);
        }

        //Пока не работает
        //[AllowAnonymous]
        //[HttpPost("Save")]
        //public IActionResult SaveRoute(int IdUser, [FromBody] Create data)
        //{            
        //    if (!ModelState.IsValid)
        //    {
        //        return StatusCode(415);
        //    }
        //    return Ok(1);
            //dataRoutes.IdUser = IdUser;
            //_context.Routes.Add(dataRoutes);
            //_context.SaveChanges();



            //dataStage.IdRoutes = dataRoutes.IdRoutes;
            //_context.Stage.Add(dataStage);
            //_context.SaveChanges();
            //Stage stage = _context.Stage.Where(c => c.IdRoutes == IdUser)
            //    .FirstOrDefault();
            //if (stage == null)
            //{ return StatusCode(416); }

            //_context.Stage.Load();

            //data.IdRoutes = IdUser;
            //_context.Stage.Add(data);
            //_context.SaveChanges();



            //     Customer ivan = context.Customers
            //.Where(c => c.LastName == "Иванов")
            //.FirstOrDefault();

            //     // Создаем заказ
            //     Order order = new Order
            //     {
            //         ProductName = "Яблоки",
            //         Quantity = 5,
            //         PurchaseDate = DateTime.Now,
            //         // Ссылка на покупателя в навигационном свойстве
            //         Customer = ivan
            //     };

            //     context.Orders.Add(order);

            //     context.SaveChanges();



            //Routes validate = _context.Routes.FirstOrDefault(x => x.IdUser == idUser && x.Title == data.Route.Title && x.DateRoutes == data.Route.DateRoutes);
            ////Users user = _context.Users.FirstOrDefault(x => x.Login == data.Login && x.Pass == data.Pass);
            //if (validate != null)
            //{
            //    foreach (var i in data.Stage)
            //    {
            //        i.IdRoutes = validate.IdRoutes;
            //        _context.Stage.Add(i);
            //    }
            //    _context.SaveChanges();

            //    //List<Stage> validateStage = _context.Stage.FirstOrDefault(x => x.IdRoutes == validate.IdRoutes && x.Title == data.Route.Title && x.DateRoutes == data.Route.DateRoutes);

            //    //foreach (var i in data.Comment)
            //    //{
            //    //    i.IdStage = validate.IdRoutes;
            //    //    _context.Stage.Add(i);
            //    //}
            //    //_context.SaveChanges();

            //}

        //    return Ok();
        //}





        // GET: api/SaveRoute/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetRoutes([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var routes = await _context.Routes.FindAsync(id);

        //    if (routes == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(routes);
        //}

        //// PUT: api/SaveRoute/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRoutes([FromRoute] int id, [FromBody] Routes routes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != routes.IdRoutes)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(routes).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RoutesExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/SaveRoute
        //[HttpPost]
        //public async Task<IActionResult> PostRoutes([FromBody] Routes routes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Routes.Add(routes);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRoutes", new { id = routes.IdRoutes }, routes);
        //}

        //// DELETE: api/SaveRoute/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRoutes([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var routes = await _context.Routes.FindAsync(id);
        //    if (routes == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Routes.Remove(routes);
        //    await _context.SaveChangesAsync();

        //    return Ok(routes);
        //}

        //private bool RoutesExists(int id)
        //{
        //    return _context.Routes.Any(e => e.IdRoutes == id);
        //}
    }
}