namespace DotNetMvcDemo.Helpers
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