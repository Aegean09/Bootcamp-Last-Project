using _01_initial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _01_initial.Controllers
{
    [Route("api/[controller]/{email}/{pass}/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public IActionResult GetCity(string email, string pass)
        {

            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                List<Cities> cities = (from c in context.Cities
                                       select new Cities()
                                       {
                                           City_Id = c.City_Id,
                                           City_Name = c.City_Name
                                       }).ToList();
                return Ok(cities);
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }


        }


        [HttpPost]
        public IActionResult AddCity(string email, string pass, Cities cit)
        {

            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
                {
                    context.Cities.Add(cit);
                    context.SaveChanges();
                    return Ok();
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


        [HttpDelete("{cityname}")]
        public IActionResult RemoveCity(string cityname, string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
                {
                    Cities cit = ctx.Cities.SingleOrDefault(a => a.City_Name == cityname);
                    if (cit != null)
                    {
                        ctx.Cities.Remove(cit);
                        ctx.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(400, "City  does not exists! Check if city name is correct.");
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
        [HttpPatch("{cityname}")]
        public IActionResult UpdateCity(string cityname, string email, string pass, Cities cit)
        {
            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
                {
                    Cities original = context.Cities.SingleOrDefault(a => a.City_Name == cityname);
                    if (original != null)
                    {
                        original.City_Name = cit.City_Name != null ? cit.City_Name : original.City_Name;
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(400, "City does not exists! Check if city name is correct.");
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
    }
}
