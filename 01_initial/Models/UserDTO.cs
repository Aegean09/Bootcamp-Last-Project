using System.ComponentModel.DataAnnotations;

namespace _01_initial.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        [MaxLength(255)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                            + "@"
                            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "You have to submit a valid email.")]
        public string EMail { get; set; }
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password has to include only numbers and letters as well as minumun 8 character lenght!")]
        public bool IsAdmin { get; set; }
        public bool IsPromoter { get; set; }
        public bool IsAttender { get; set; }
    }
}
