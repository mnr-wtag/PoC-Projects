using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}