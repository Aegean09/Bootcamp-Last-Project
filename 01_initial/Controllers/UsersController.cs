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
        [HttpGet("{email}/{pass}/[action]")]
        public IActionResult GetAdmins(string email, string pass)
        {
            EgeDbContext ctx = new EgeDbContext();
            if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                List<UserDTO> Users = (from c in ctx.Users
                                       where c.IsAdmin == true
                                       select new UserDTO()
                                       {
                                           UserId = c.UserId,
                                           FName = c.FName,
                                           LName = c.LName,
                                           EMail = c.EMail,
                                           IsAdmin = c.IsAdmin,
                                           IsPromoter = c.IsPromoter,
                                           IsAttender = c.IsAttender,
                                       }).ToList();
                return Ok(Users);
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }



        [HttpGet("{email}/{pass}/[action]")]
        public IActionResult GetPromoters(string email, string pass)
        {
            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                List<UserDTO> Users = (from c in context.Users
                                       where c.IsPromoter == true
                                       select new UserDTO()
                                       {
                                           UserId = c.UserId,
                                           FName = c.FName,
                                           LName = c.LName,
                                           EMail = c.EMail,
                                           IsAdmin = c.IsAdmin,
                                           IsPromoter = c.IsPromoter,
                                           IsAttender = c.IsAttender,
                                       }).ToList();
                return Ok(Users);
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }
        }


        [HttpGet("{email}/{pass}/[action]")]
        public IActionResult GetAttenders(string email, string pass)
        {
            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                List<Users> Users = (from c in context.Users
                                     where c.IsAttender == true
                                     select new Users()
                                     {
                                         UserId = c.UserId,
                                         FName = c.FName,
                                         LName = c.LName,
                                         EMail = c.EMail,
                                         Password = c.Password,
                                         IsAdmin = c.IsAdmin,
                                         IsPromoter = c.IsPromoter,
                                         IsAttender = c.IsAttender,
                                     }).ToList();
                return Ok(Users);
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");

            }
        }


        [HttpPost("[action]")]
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
                    return StatusCode(400,"Check your details again!");
                }
            }
            else
            {
                return StatusCode(400, "Your password and and chk_password doesn't match!");
            }


        }


        [HttpDelete("{email}/{pass}/[action]")]
        public IActionResult RemoveUSer (string email, string pass)
        {
            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                Users us = context.Users.SingleOrDefault(a => a.EMail == email);
                context.Users.Remove(us);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(404, "You are not signed up in our system. Check if email and password is correct!");
            }

            
        }


        [HttpPatch("{email}/{pass}/[action]/{id}")]
        public IActionResult UpdateUser(string email, string pass,int id, Users Changing_User)
        {
            EgeDbContext context = new EgeDbContext();
            if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null)
            {
                if (context.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAdmin)
                {
                    Users original = context.Users.SingleOrDefault(a => a.UserId == id);
                    original.LName = Changing_User.LName != null ? Changing_User.LName : original.LName;
                    original.FName = Changing_User.FName != null ? Changing_User.FName : original.FName;
                    original.EMail = Changing_User.EMail != null ? Changing_User.EMail : original.EMail;
                    original.Password = Changing_User.Password != null ? Changing_User.Password : original.Password;
                    original.Chk_Password = Changing_User.Chk_Password != null ? Changing_User.Chk_Password : original.Chk_Password;
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
    }
}
