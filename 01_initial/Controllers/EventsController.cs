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
            List<Events> events = (from c in context.Events
                                  select new Events()
                                  {
                                      Category=c.Category,
                                      City=c.City,
                                      Name=c.Name,
                                      EventId=c.EventId,
                                  }).ToList();
            return Ok(events);
        }


        [HttpPost("{id}")]
        public IActionResult AddEvent(int id, Events ev)
        {
            EgeDbContext context = new EgeDbContext();
            context.Events.Add(ev);
            context.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult RemoveEvent(int id, Users User)
        {
            if (User.IsAdmin == true)
            {
                EgeDbContext ctx = new EgeDbContext();
                Events ev = ctx.Events.SingleOrDefault(a=>a.EventId == id);
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
        public IActionResult UpdateEvent(int id, Events Ev)
        {
            EgeDbContext context = new EgeDbContext();
            Events original = context.Events.SingleOrDefault(a => a.EventId == id);
            original.Name = Ev.Name != null ? Ev.Name : original.Name;
            original.City = Ev.City != null ? Ev.City : original.City;
            original.Category = Ev.Category != null ? Ev.Category : original.Category;

            return Ok();
        }
    }
}
