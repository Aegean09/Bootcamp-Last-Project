using _01_initial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _01_initial.Controllers
{
    [Route("api/[controller]/{email}/{pass}/[action]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        public IActionResult GetEvents(string email,string pass)
        {
            
            EgeDbContext context = new EgeDbContext();
            if(context.Users.SingleOrDefault(a=>a.EMail==email && a.Password==pass)!=null){
                List<Events> events = (from c in context.Events
                                       select new Events()
                                       {
                                           Category = c.Category,
                                           City = c.City,
                                           Name = c.Name,
                                           EventId = c.EventId,
                                       }).ToList();
                return Ok(events);
            }
            else
            {
                return StatusCode(301);
            }

            
        }


        [HttpPost]
        public IActionResult AddEvent(string email,string pass, Events ev)
        {

            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password==pass).IsAdmin)
            {
                context.Events.Add(ev);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(301);
            }
        }


        [HttpDelete("{eventid}")]
        public IActionResult RemoveEvent(int eventid, string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
            {
                Events ev = ctx.Events.SingleOrDefault(a => a.EventId == eventid);
                if (ev != null)
                {
                    ctx.Events.Remove(ev);
                    ctx.SaveChanges();
                    return Ok();
                }
                else
                {
                    return StatusCode(301);
                }

            }
            else
            {
                return StatusCode(301); 
            }

                
        }


        [HttpPatch("{eventid}")]
        public IActionResult UpdateEvent(int eventid, string email, string pass,Events Ev)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
            {
                Events original = ctx.Events.SingleOrDefault(a => a.EventId == eventid);
                if (original != null)
                {
                    original.Name = Ev.Name != null ? Ev.Name : original.Name;
                    original.City = Ev.City != null ? Ev.City : original.City;
                    original.Category = Ev.Category != null ? Ev.Category : original.Category;
                    return Ok();
                }
                else
                {
                    return StatusCode(301);
                }

            }
            else
            {
                return StatusCode(301);
            }
        }
    }
}
