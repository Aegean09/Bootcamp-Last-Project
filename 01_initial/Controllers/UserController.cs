using _01_initial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _01_initial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IActionResult GetUsers()
        {
            EgeDbContext context = new EgeDbContext();
            List<User> Users = (from c in context.Users
                                  select new User()
                                  {
                                      FName= c.FName,
                                      LName= c.LName,
                                      EMail= c.EMail,
                                      IsAdmin= c.IsAdmin,
                                  }).ToList();
            return Ok(Users);
        }


        [HttpPost]
        public IActionResult AddUser(User user)
        {
            
        }


        [HttpDelete("{id}")]
        public IActionResult RemoveEvent(int id, User User)
        {
            if (User.IsAdmin == true)
            {
                EgeDbContext ctx = new EgeDbContext();
                Event ev = ctx.Events.SingleOrDefault(a => a.EventId == id);
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
