using RestApiNetDemo.BEL.AuthUser;
using RestApiNetDemo.BLL.Helpers;
using RestApiNetDemo.BLL.IServices;

namespace RestApiNetDemo.BLL.Services
{
    public class AuthUserService : IAuthUserService
    {
        public bool Login(string username, string password)
        {
            var user = new AuthUserDTO();
            return true;
        }

        ServiceResponse IAuthUserService.Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}