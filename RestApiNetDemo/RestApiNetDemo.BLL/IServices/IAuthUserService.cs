using RestApiNetDemo.BLL.Helpers;

namespace RestApiNetDemo.BLL.IServices
{
    public interface IAuthUserService
    {
        ServiceResponse Login(string username, string password);
    }
}
