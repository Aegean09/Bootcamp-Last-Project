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
        [HttpGet("[action]")]
        public IActionResult GetAdmins()
        {
            EgeDbContext context = new EgeDbContext();
            List<UserDTO> Users = (from c in context.Users
                                   where c.IsAdmin == true
                                   select new UserDTO()
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

        [HttpGet("[action]")]
        public IActionResult GetPromoters()
        {
            EgeDbContext context = new EgeDbContext();
            List<UserDTO> Users = (from c in context.Users
                                   where c.IsPromoter == true
                                   select new UserDTO()
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
        [HttpGet("[action]")]
        public IActionResult GetAttenders()
        {
            EgeDbContext context = new EgeDbContext();
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

        //[HttpPatch("{email}/{pass}/[action]/{eventid}")]
        //public IActionResult JoinEventAsAttender(int eventid, string email, string pass)
        //{
        //    EgeDbContext ctx = new EgeDbContext();
        //    if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null && ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAttender)
        //    {
        //        Users user = ctx.Users.SingleOrDefault(a => a.EMail == email);
        //        Events ev = ctx.Events.SingleOrDefault(a => a.EventId == eventid);
        //        if (ev != null && ev.Capacity > 0 )
        //        {
        //            user.Events_I_Attend.Add(ev);
        //            ev.Capacity = ev.Capacity - 1;
        //            ctx.SaveChanges();
        //            return Ok();
        //        }
        //        else
        //        {
        //            return StatusCode(301);
        //        }
        //    }
        //    return StatusCode(301);
        //}


        //[HttpPatch("{email}/{pass}/[action]/{eventid}")]
        //public IActionResult LeaveEventAsAttender(int eventid, string email, string pass)
        //{
        //    EgeDbContext ctx = new EgeDbContext();
        //    if (ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass) != null && ctx.Users.SingleOrDefault(a => a.EMail == email && a.Password == pass).IsAttender)
        //    {
        //        Users user = ctx.Users.SingleOrDefault(a => a.EMail == email);
        //        Events ev = ctx.Events.SingleOrDefault(a => a.EventId == eventid);
        //        if (ev != null && user.Events_I_Attend.SingleOrDefault(a => a.EventId == eventid).EventId == eventid)
        //        {
        //            user.Events_I_Attend.Remove(ev);
        //            ev.Capacity = ev.Capacity + 1;
        //            ctx.SaveChanges();
        //            return Ok();
        //        }
        //        else
        //        {
        //            return StatusCode(301);
        //        }
        //    }
        //    return StatusCode(301);
        //}
    }
}
