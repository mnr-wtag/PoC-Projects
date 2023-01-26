using System.ComponentModel.DataAnnotations;

namespace RestApiNetDemo.BEL.AuthUser
{
    public class LoginDTO
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