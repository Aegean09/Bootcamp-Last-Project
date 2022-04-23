using System.ComponentModel.DataAnnotations;

namespace _01_initial.Models
{
    public class User
    {
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "You have to enter an email!")]
        public string EMail { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Chk_Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
