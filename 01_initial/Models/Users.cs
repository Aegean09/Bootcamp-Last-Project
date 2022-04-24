using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _01_initial.Models
{
    //[Index(nameof(EMail), IsUnique = true)]
    public class Users
    {
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        [MaxLength(255)]
        public string EMail { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
        public string Chk_Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
