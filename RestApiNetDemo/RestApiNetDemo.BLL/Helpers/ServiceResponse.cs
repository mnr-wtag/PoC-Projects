using System.Net;

namespace RestApiNetDemo.BLL.Helpers
{
    public sealed class ServiceResponse
    {
        public ServiceResponse(HttpStatusCode statusCode, string message, object data = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }
    }
}