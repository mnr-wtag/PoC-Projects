using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DotNetMvcDemo.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
        );

        T GetById(Expression<Func<T, bool>> expression, List<string> includes = null);

        void Insert(T entity);

        void BulkInsert(IEnumerable<T> entities);

        void Delete(T entity);

        void Update(T entity);


    }
}
