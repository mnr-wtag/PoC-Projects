namespace RestApiNetDemo.DAL.IRepositories
{
    public interface IAuth<out T> where T : class
    {
        T Authenticate(string email, string password);
    }
}
