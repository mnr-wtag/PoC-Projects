using RestApiNetDemo.BEL.AuthUser;
using System;

namespace RestApiNetDemo.BLL.Services
{
    public class AuthUserService
    {
        public bool Login(string username, string password)
        {
            var user = new AuthUserDTO();
            return true;
        }
    }
}
