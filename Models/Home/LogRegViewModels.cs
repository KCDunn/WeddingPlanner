using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class RegisterUser
    {
        [Display(Name ="First Name")]
        [Required]
        [MinLength(2)]
        public string first_name { get; set; }
        
        [Display(Name ="Last Name")]
        [Required]
        [MinLength(2)]
        public string last_name { get; set; }

        [Display(Name ="Email Address")]
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name ="Password")]
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name ="Confirm Password")]
        [Compare("password", ErrorMessage = "Password and Confirmation must match.")]
        [DataType(DataType.Password)]
        public string confirm { get; set; }
    }



    public class LoginUser
    {
        [Display(Name="Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string logEmail {get;set;}

        [Display(Name="Password")]
        [Required]
        [DataType(DataType.Password)]
        public string logPassword {get;set;}
    }

     public class LogRegUsers
    {
        public RegisterUser Register { get; set; }
        public LoginUser Login { get; set; }
    }
}