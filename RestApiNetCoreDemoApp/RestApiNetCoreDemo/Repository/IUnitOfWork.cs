using Microsoft.EntityFrameworkCore;

namespace RestApiNetCoreDemo.Repository
{
    public interface IUnitOfWork<out TContext> where TContext : DbContext, new()
    {
        TContext Context { get; }
        void Save();
    }
}
