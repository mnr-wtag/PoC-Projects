using System.Data.Entity;

namespace RestApiNetDemo.DAL.UnitOfWork
{
    public interface IUnitOfWork<out TContext> where TContext : DbContext, new()
    {
        TContext Context { get; }
        void Save();
    }
}
