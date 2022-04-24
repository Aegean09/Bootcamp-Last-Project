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
                List<City> cities = (from c in context.City
                                     select new City()
                                     {
                                         City_Id = c.City_Id,
                                         City_Name = c.City_Name
                                     }).ToList();
                return Ok(cities);
            }
            else
            {
                return StatusCode(301);
            }


        }


        [HttpPost]
        public IActionResult AddCity(string email, string pass, City cit)
        {

            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
            {
                context.City.Add(cit);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(301);
            }
        }


        [HttpDelete("{cityid}")]
        public IActionResult RemoveCity(int cityid, string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
            {
                City cit= ctx.City.SingleOrDefault(a => a.City_Id== cityid);
                if (cit != null)
                {
                    ctx.City.Remove(cit);
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
