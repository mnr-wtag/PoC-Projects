using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiNetDemo.BLL.Helpers
{
    public enum Response
    {
        Success,
        Error,
        NotFound,
        BadRequest,
        Exists
    }

    public class ServiceResponse
    {
        public Response Response { get; set; }
        public string Message { get; set; }
    }
}
