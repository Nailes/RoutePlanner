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
    public class SaveRouteController : ControllerBase
    {
        private readonly RouteplannerContext _context;

        public SaveRouteController(RouteplannerContext context)
        {
            _context = context;
        }

        [HttpGet("Route")]
        public IActionResult GetRoute(int IdRoute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var route = _context.Routes.Where(x => x.IdRoutes == IdRoute)
            .Select(c => new
            {
                title = c.Title,
                date = c.DateRoutes,
                stage = c.Stage.Where(a => a.IdRoutes == IdRoute)
            .Select(b => new
            {
                id = b.IdStage,
                place = b.Place,
                date = b.DateStage,
                comments = b.Comments.Where(d => d.IdStage == b.IdStage)
            .Select(e => new
            {
                id = e.IdComments,
                note = e.Note,
                date = e.DateNote })
            })
            });

            if (route == null)
            {
                return BadRequest(ModelState);
            }
            return Ok(route);
        }

        [HttpDelete("DeleteStage")]
        public async Task<IActionResult> DeleteStage(int IdStage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stage = await _context.Stage.FindAsync(IdStage);

            if (stage == null)
            {
                return NotFound();
            }

            _context.Stage.Remove(stage);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("DeleteComments")]
        public async Task<IActionResult> DeleteComments(int IdComments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.Comments.FindAsync(IdComments);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Save")]
        public IActionResult SaveRoute(int IdUser, Routes route)
        {
            Routes dataRoute = new Routes
            {
                IdUser = IdUser,
                Title = route.Title,
                DateRoutes = route.DateRoutes
            };
            _context.Routes.Add(dataRoute);

            foreach (var i in route.Stage)
            {
                Stage dataStage = new Stage
                {
                    IdRoutes = dataRoute.IdRoutes,
                    Place = i.Place,
                    DateStage = i.DateStage
                };
                _context.Stage.Add(dataStage);

                foreach (var j in i.Comments)
                {
                    Comments dataComments = new Comments
                    {
                        IdStage = dataStage.IdStage,
                        Note = j.Note,
                        DateNote = j.DateNote
                    };
                    _context.Comments.Add(dataComments);
                }
            }

            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("EditRoute")]
        public async Task<IActionResult> EditRoute(int IdRoute, Routes data)
        {
            Routes route = await _context.Routes.FirstOrDefaultAsync(x => x.IdRoutes == IdRoute);

            if (route == null)
            {
                return BadRequest(ModelState);
            }

            if (data.Title != null && data.Title != "")
            {
                route.Title = data.Title;
            }
            if (data.DateRoutes != null)
            {
                route.DateRoutes = data.DateRoutes;
            }

            _context.Stage.Where(p => p.IdRoutes == route.IdRoutes).Load();
            int countI = 0;            
            foreach (var i in route.Stage)
            {
                if (i.IdRoutes == IdRoute)
                {
                    int countP = 0;
                    int countQ = 0;
                    foreach (var p in data.Stage)
                    {                        
                        if (countI == countP)
                        {
                            if (p.Place != null && p.Place != "")
                            {
                                i.Place = p.Place;
                            }
                            if (p.DateStage != null)
                            {
                                i.DateStage = p.DateStage;
                            }
                            _context.Comments.Where(x => x.IdStage == i.IdStage).Load();
                            int countJ = 0;

                            foreach (var j in i.Comments)
                            {
                                if (j.IdStage == i.IdStage)
                                {
                                    foreach (var q in p.Comments)
                                    {
                                        if (countJ == countQ)
                                        {
                                            if (q.Note != null && q.Note != "")
                                            {
                                                j.Note = q.Note;
                                            }
                                            if (q.DateNote != null)
                                            {
                                                j.DateNote = q.DateNote;
                                            }
                                            break;
                                        }
                                        countQ++;
                                    }
                                }
                                countJ++;
                            }
                            break;
                        }
                        countP++;
                    }
                    countI++;
                }
            }
            await _context.SaveChangesAsync();
            return Ok(data);
        }
    }
}