using System.Collections.Generic;

namespace RestApiNetDemo.DAL.IRepositories
{
    public interface IRepository<T, TId, TString>
    {
        T GetById(TId id);
        List<T> GetAll();
        bool Add(T entity);
        bool Delete(TId entity);
        bool Update(T entity);
        List<T> Search(TString search);
    }

    public interface IRepository<T, TId>
    {
        bool Add(T entity);
        bool Delete(TId entity);
        bool Update(T entity);
        T GetById(TId id);
        List<T> GetAll();
    }
}
