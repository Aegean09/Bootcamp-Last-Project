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
        public IActionResult GetEvents(string email, string pass)
        {

            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                List<EventsDTO> events = (from c in context.Events
                                          join ed in context.Cities on c.City.City_Id equals ed.City_Id
                                          select new EventsDTO()
                                          {
                                              Event_Id = c.EventId,
                                              Name = c.Name,
                                              Description = c.Description,
                                              Date = c.Date,
                                              Deadline = c.Deadline,
                                              Address = c.Address,
                                              Capacity = c.Capacity,
                                              isTicket = c.isTicket,
                                              Price = c.Price,
                                              City_Name = c.City.City_Name,
                                              Category_Name = c.Category.Category_Name
                                          }).ToList();
                return Ok(events);
            }
            else
            {
                return StatusCode(301);
            }


        }


        [HttpPost]
        public IActionResult AddEvent(string email, string pass, Events ev)
        {

            EgeDbContext context = new EgeDbContext();
            if ((context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null && context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin) ||
                (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null && context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsPromoter))
            {
                Events EventAdded = new Events()
                {
                    Name = ev.Name,
                    Description = ev.Description,
                    Date = ev.Date,
                    Deadline = ev.Deadline,
                    Address = ev.Address,
                    Capacity = ev.Capacity,
                    isTicket = ev.isTicket,
                    Price = ev.Price,
                    City = context.Cities.Find(ev.City.City_Id),
                    Category = context.Categories.Find(ev.Category.Category_Id)
                };
                context.Events.Add(EventAdded);
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
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null && ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
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
        public IActionResult UpdateEvent(int eventid, string email, string pass, Events Ev)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null && ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
            {
                Events original = ctx.Events.SingleOrDefault(a => a.EventId == eventid);
                if (original != null)
                {
                    original.Name = Ev.Name != null ? Ev.Name : original.Name;
                    original.Address = Ev.Address != null ? Ev.Address : original.Address;
                    original.isTicket = Ev.isTicket != null ? Ev.isTicket : original.isTicket;
                    original.Capacity = Ev.Capacity != null ? Ev.Capacity : original.Capacity;
                    original.Deadline = Ev.Deadline != null ? Ev.Deadline : original.Deadline;
                    original.Description = Ev.Description != null ? Ev.Description : original.Description;
                    original.Price = Ev.Price != null ? Ev.Price : original.Price;
                    original.Date = Ev.Date != null ? Ev.Date : original.Date;
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
    }
}
