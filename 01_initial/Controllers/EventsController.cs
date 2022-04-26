using _01_initial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_initial.Controllers
{
    [Route("api/[controller]/{email}/{pass}/[action]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        #region GetEvents
        public IActionResult GetEvents(string email, string pass)
        {
            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                List<EventsDTO> events = (from c in context.Events
                                          join ec in context.Cities on c.City.City_Id equals ec.City_Id
                                          select new EventsDTO()
                                          {
                                              Event_Id=c.EventId,
                                              Name = c.Name,
                                              Description = c.Description,
                                              Date = c.Date,
                                              Deadline = c.Deadline,
                                              Address = c.Address,
                                              Capacity = c.Capacity,
                                              isTicket = c.isTicket,
                                              Price = c.Price,
                                              City_Name=c.City.City_Name,
                                              Category_Name=c.Category.Category_Name,
                                          }).ToList();
                return Ok(events);
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }
        #endregion

        #region GetEventsByCity
        [HttpGet("{cityname}")]
        public IActionResult GetEventsByCity(string cityname, string email, string pass)
        {
            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (context.Cities.SingleOrDefault(a => a.City_Name == cityname) != null)
                {
                    if (context.Events.SingleOrDefault(a => a.City.City_Name == cityname) != null)
                    {
                        List<EventsDTO> eventsbycity = (from c in context.Events
                                                        where c.City.City_Name == cityname
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
                        return Ok(eventsbycity);
                    }
                    else
                    {
                        return StatusCode(404, "There is no events in that city!");
                    }
                }
                else
                {
                    return StatusCode(404, "There is no such city in our system!");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }
        #endregion

        #region GetEventsByCategory
        [HttpGet("{eventcate}")]
        public IActionResult GetEventsByCategory(string eventcate, string email, string pass)
        {
            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (context.Categories.SingleOrDefault(a => a.Category_Name == eventcate) != null)
                {
                    if (context.Events.SingleOrDefault(a => a.Category.Category_Name == eventcate) != null)
                    {
                        List<EventsDTO> eventsbycity = (from c in context.Events
                                                        where c.Category.Category_Name == eventcate
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
                        return Ok(eventsbycity);
                    }
                    else
                    {
                        return StatusCode(404, "There is no events in that category!");
                    }
                }
                else
                {
                    return StatusCode(404, "There is no such category in our system!");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }
        #endregion

        #region AddEvent
        [HttpPost]
        public IActionResult AddEvent(string email, string pass, Events ev)
        {
            EgeDbContext context = new EgeDbContext();
            if(context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin ||
                    context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsPromoter)
                {
                    if (ev != null)
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
                            Category = context.Categories.Find(ev.Category.Category_Name)
                        };
                        context.Events.Add(EventAdded);
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(400, "You did not enter any information.");
                    }
                }
                    
                else
                {
                    return StatusCode(401, "You are not an admin or promoter.");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }

        }
        #endregion


        #region RemoveEventAsAdmin
        [HttpDelete("{eventid}")]
        public IActionResult RemoveEventAsAdmin(int eventid, string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
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
                        return StatusCode(404, "There is no such event.");
                    }
                }
                else
                {
                    return StatusCode(401, "You are not an admin.");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }

        }
        #endregion

        #region RemoveEventAsPromoter
        [HttpDelete("{eventid}")]
        public IActionResult RemoveEventAsPromoter(int eventid, string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsPromoter)
                {
                    DeletedEvents del = new DeletedEvents();
                    Events ev = ctx.Events.SingleOrDefault(a => a.EventId == eventid);
                    del.Del_Name = ev.Name;
                    del.Del_Date = DateTime.Now;
                    del.Del_Event_Date = ev.Date;
                    del.Del_Description = ev.Description;
                    var deadlinedate = ev.Deadline.AddDays(-5);
                    var todaydate = DateTime.Now;
                    if (todaydate < deadlinedate)
                    {
                        if (ev != null)
                        {
                            ctx.DeletedEvents.Add(del);
                            ctx.Events.Remove(ev);
                            ctx.SaveChanges();
                            return Ok();
                        }
                        else
                        {
                            return StatusCode(404, "There is no such event.");
                        }
                    }
                    else
                    {
                        return StatusCode(401,"There is less than 5 days until the event. You are not allowed to decline it now!");
                    }
                }
                else
                {
                    return StatusCode(401, "You are not a promoter.");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }
        #endregion

        #region UpdateEventAsPromoter
        [HttpPatch("{eventid}")]
        public IActionResult UpdateEventAsPromoter(int eventid, string email, string pass, Events Ev)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsPromoter)
                {
                    Events original = ctx.Events.SingleOrDefault(a => a.EventId == eventid);
                    var deadlinedate = original.Deadline.AddDays(-5);
                    var todaydate = DateTime.Now;
                    if (todaydate < deadlinedate)
                    {
                        if (original != null)
                        {
                            original.Address = Ev.Address != null ? Ev.Address : original.Address;
                            original.Capacity = Ev.Capacity != null ? Ev.Capacity : original.Capacity;
                            ctx.SaveChanges();
                            return Ok();
                        }
                        else
                        {
                            return StatusCode(404, "There is no such event.");
                        }
                    }
                    else
                    {
                        return StatusCode(401, "There is less than 5 days until the event. You are not allowed to update it now!");
                    }
                }
                else
                {
                    return StatusCode(401, "You are not a promoter.");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }
        #endregion

        #region UpdateEventAsAdmin
        [HttpPatch("{eventid}")]
        public IActionResult UpdateEventAsAdmin(int eventid, string email, string pass, Events Ev)
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
        #endregion

        #region JoinEventAsAttender
        [HttpPatch("{eventid}")]
        public IActionResult JoinEventAsAttender(int eventid, string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAttender)
                {
                    Users user = ctx.Users.SingleOrDefault(a => a.EMail == email);
                    Events ev = ctx.Events.SingleOrDefault(a => a.EventId == eventid);
                    if (ev != null)
                    {
                        if(ev.Capacity > 0)
                        {
                            ev.Attenders.Add(user);
                            ev.Capacity = ev.Capacity - 1;
                            ctx.SaveChanges();
                            return Ok();
                        }
                        else
                        {
                            return StatusCode(400, "We are sorry but capacity of the event you want to join is full. Take another look at the events we have!");
                        }
                    }
                    else
                    {
                        return StatusCode(404, "There is no such event.");
                    }
                }
                else
                {
                    return StatusCode(401, "You are not an attender!");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }
        #endregion

        #region LeaveEventAsAttender
        [HttpPatch("{eventid}")]
        public IActionResult LeaveEventAsAttender(int eventid, string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAttender)
                {
                    Users user = ctx.Users.SingleOrDefault(a => a.EMail == email);
                    Events ev = ctx.Events.SingleOrDefault(a => a.EventId == eventid);
                    if (ev != null)
                    {
                        if(ev.Attenders.SingleOrDefault(a => a.EMail == email).UserId == user.UserId)
                    {
                            ev.Attenders.Remove(user);
                            ev.Capacity = ev.Capacity + 1;
                            ctx.SaveChanges();
                            return Ok();
                        }
                    else
                        {
                            return StatusCode(400,"You're name is not in the list. You can only leave events that you joined, right?");
                        }
                    }
                    else
                    {
                        return StatusCode(404, "There is no such event.");
                    }
                }
                else
                {
                    return StatusCode(401, "You are not an attender!");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");

            }
        }
        #endregion

        #region GetEventsAsAdmin
        [HttpGet]
        public IActionResult GetEventsAsAdmin(string email, string pass)
        {
            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
                {
                    List<Events> events = (from c in context.Events
                                           join ec in context.Cities on c.City.City_Id equals ec.City_Id
                                           select new Events()
                                           {
                                               EventId = c.EventId,
                                               Name = c.Name,
                                               Description = c.Description,
                                               Date = c.Date,
                                               Deadline = c.Deadline,
                                               Address = c.Address,
                                               Capacity = c.Capacity,
                                               isTicket = c.isTicket,
                                               Price = c.Price,
                                               City = c.City,
                                               Category = c.Category,
                                               Attenders = c.Attenders
                                           }).ToList();
                    return Ok(events);
                }
                else
                {
                    return StatusCode(401, "You are not an admin!");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }
        #endregion

        #region GetEventsThatJoined
        [HttpGet]
        public IActionResult GetEventsThatJoined(string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAttender)
                {
                    List<EventsDTO> events = (from c in ctx.Events
                                              join ec in ctx.Cities on c.City.City_Id equals ec.City_Id
                                              where c.Attenders.SingleOrDefault(c => c.EMail == email).EMail == email
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
                                                  Category_Name = c.Category.Category_Name,
                                              }).ToList();
                    return Ok(events);
                }
                else
                {
                    return StatusCode(401, "You are not an attender!");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }

        }
        #endregion

        #region GetEventsThatIDidntJoin
        [HttpGet]
        public IActionResult GetEventsThatIDidntJoin(string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAttender)
                {
                    List<EventsDTO> events = (from c in ctx.Events
                                              join ec in ctx.Cities on c.City.City_Id equals ec.City_Id
                                              where c.Attenders.SingleOrDefault(c => c.EMail == email).EMail != email
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
                                                  Category_Name = c.Category.Category_Name,
                                              }).ToList();
                    return Ok(events);
                }
                else
                {
                    return StatusCode(401, "You are not an attender!");
                }
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }
        #endregion

        #region GetDeletedEvents
        [HttpGet]
        public IActionResult GetDeletedEvents(string email, string pass)
        {
            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                List<DeletedEvents> deletedEvents = (from c in context.DeletedEvents
                                                     select new DeletedEvents()
                                                     {
                                                         Del_Date=c.Del_Date,
                                                         Del_Description=c.Del_Description,
                                                         Del_Event_Date=c.Del_Event_Date,
                                                         Del_Name = c.Del_Name,
                                                     }).ToList();
                return Ok(deletedEvents);
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }
        #endregion








    }
}
