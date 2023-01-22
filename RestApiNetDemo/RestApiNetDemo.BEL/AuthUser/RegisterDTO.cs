using RestApiNetDemo.BEL.AuditableEntities;
using System.ComponentModel.DataAnnotations;

namespace RestApiNetDemo.BEL.AuthUser
{
    public class RegisterDTO : DateAndAuthorDTO
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords Not Matched")]
        public string ConfirmPassword { get; set; }
    }
}
