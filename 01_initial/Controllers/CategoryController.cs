using _01_initial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _01_initial.Controllers
{
    [Route("api/[controller]/{email}/{pass}/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public IActionResult GetCategory(string email, string pass)
        {

            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                List<Categories> categories = (from c in context.Categories
                                     select new Categories()
                                     {
                                         Category_Id = c.Category_Id,
                                         Category_Name = c.Category_Name
                                     }).ToList();
                return Ok(categories);
            }
            else
            {
                return StatusCode(301);
            }


        }


        [HttpPost]
        public IActionResult AddCategory(string email, string pass, Categories catt)
        {

            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
            {
                context.Categories.Add(catt);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(301);
            }
        }


        [HttpDelete("{catid}")]
        public IActionResult RemoveCategory(int catid, string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
            {
                Categories cat = ctx.Categories.SingleOrDefault(a => a.Category_Id==catid );
                if (cat != null)
                {
                    ctx.Categories.Remove(cat);
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
