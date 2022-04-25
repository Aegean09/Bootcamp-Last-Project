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


        [HttpDelete("{catname}")]
        public IActionResult RemoveCategory(string catname, string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
            {
                Categories cat = ctx.Categories.SingleOrDefault(a => a.Category_Name==catname );
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

        //[HttpPatch("{catname}")]
        //public IActionResult UpdateCategory(string catname, string email, string pass, Categories cit)
        //{
        //    EgeDbContext context = new EgeDbContext();
        //    if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null && context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
        //    {
        //        Categories original = context.Categories.SingleOrDefault(a => a.Category_Name == catname);
        //        if (original != null)
        //        {
        //            original.Category_Name = cit.Category_Name != null ? cit.Category_Name : original.Category_Name;
        //            context.SaveChanges();
        //            return Ok();
        //        }
        //        else
        //        {
        //            return StatusCode(301);
        //        }
        //    }
        //    else
        //    {
        //        return StatusCode(301);
        //    }
        //}
    }
}
