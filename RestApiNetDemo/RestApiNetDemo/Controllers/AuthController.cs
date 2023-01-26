using RestApiNetDemo.BLL.IServices;
using System.Web.Http;

namespace RestApiNetDemo.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IAuthUserService _authUserService;

        public AuthController(IAuthUserService authUserService)
        {
            _authUserService = authUserService;
        }
    }
}