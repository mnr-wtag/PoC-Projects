using RestApiNetDemo.BLL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
