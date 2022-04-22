using System.ComponentModel.DataAnnotations;

namespace _01_initial.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Chk_Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
