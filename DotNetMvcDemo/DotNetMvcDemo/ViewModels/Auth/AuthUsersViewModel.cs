namespace DotNetMvcDemo.ViewModels.Auth
{
    public class AuthUsersViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}