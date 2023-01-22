using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RestApiNetDemo.DAL.IRepositories
{
    public interface IRepoWithSearch<T, TId, TString> : IRepository<T, TId>
    {
        List<T> Search(TString search);
    }

    public interface IRepository<T, TId>
    {
        bool Add(T entity);
        bool Delete(TId entity);
        bool Update(T entity);
        T GetById(Expression<Func<T, bool>> expression, List<string> includes = null);
        List<T> GetAll(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null);
    }

    public interface IBulkInsert<T>
    {
        bool Add(T entity);
        bool BulkInsert(IEnumerable<T> entities);
        List<T> GetAll(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null);
    }
}
