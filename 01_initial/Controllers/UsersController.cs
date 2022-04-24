using _01_initial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _01_initial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IActionResult GetUsers()
        {
            EgeDbContext context = new EgeDbContext();
            List<Users> Users = (from c in context.Users
                                 select new Users()
                                 {
                                     UserId = c.UserId,
                                     FName = c.FName,
                                     LName = c.LName,
                                     EMail = c.EMail,
                                     Password = c.Password,
                                     Chk_Password = c.Chk_Password,
                                     IsAdmin = c.IsAdmin,
                                 }).ToList();
            return Ok(Users);
        }


        [HttpPost]
        public IActionResult AddUser(Users User1)
        {
            EgeDbContext context = new EgeDbContext();
            if (User1.Password == User1.Chk_Password)
            {
                context.Users.Add(User1);
                try
                {
                    context.SaveChanges();
                    return Ok();
                }
                catch
                {
                    return StatusCode(301);
                }
            }
            else
            {
                return StatusCode(301);
            }
            

        }


        [HttpDelete("{id}")]
        public IActionResult RemoveUSer(int id)
        {
            EgeDbContext ctx = new EgeDbContext();
            Users us = ctx.Users.SingleOrDefault(a => a.UserId == id);
            ctx.Users.Remove(us);
            ctx.SaveChanges();
            return Ok();
        }


        [HttpPatch("{id}")]
        public IActionResult UpdateUser(int id, Users Changing_User)
        {
            EgeDbContext context = new EgeDbContext();
            Users original = context.Users.SingleOrDefault(a => a.UserId == id);
            original.LName = Changing_User.LName != null ? Changing_User.LName : original.LName;
            original.FName = Changing_User.FName != null ? Changing_User.FName : original.FName;
            original.EMail = Changing_User.EMail != null ? Changing_User.EMail : original.EMail;
            original.Password = Changing_User.Password != null ? Changing_User.Password : original.Password;
            original.Chk_Password = Changing_User.Chk_Password != null ? Changing_User.Chk_Password : original.Chk_Password;

            return Ok();
        }
    }
}
