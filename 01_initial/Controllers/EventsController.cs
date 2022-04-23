using _01_initial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _01_initial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        public IActionResult GetEvents()
        {
            EgeDbContext context = new EgeDbContext();
            List<Event> events = (from c in context.Events
                                  select new Event()
                                  {
                                      Category=c.Category,
                                      City=c.City,
                                      Name=c.Name,
                                      EventId=c.EventId,
                                  }).ToList();
            return Ok(events);
        }


        [HttpPost]
        public IActionResult AddEvent(Event Event, User user)
        {
            if (user.IsAdmin == true)
            {
                EgeDbContext context = new EgeDbContext();
                context.Events.Add(Event);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult RemoveEvent(int id, User User)
        {
            if (User.IsAdmin == true)
            {
                EgeDbContext ctx = new EgeDbContext();
                Event ev = ctx.Events.SingleOrDefault(a=>a.EventId == id);
                ctx.Events.Remove(ev);
                ctx.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPatch("{id}")]
        public IActionResult UpdateEvent(int id, Event Ev, User User)
        {
            if (User.IsAdmin == true)
            {
                EgeDbContext context = new EgeDbContext();
                Event original = context.Events.SingleOrDefault(a => a.EventId == id);
                original.Name = Ev.Name != null ? Ev.Name : original.Name;
                original.City = Ev.City != null ? Ev.City : original.City;
                original.Category = Ev.Category != null ? Ev.Category : original.Category;

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
