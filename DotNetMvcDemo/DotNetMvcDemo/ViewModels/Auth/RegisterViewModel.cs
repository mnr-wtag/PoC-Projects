using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.ViewModels.Auth
{
    public class RegisterViewModel
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