using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RestApiNetDemo.DAL.Repositories
{
    public class EnrollmentRepo : IBulkInsert<Enrollment>
    {
        public bool Add(Enrollment entity)
        {
            throw new NotImplementedException();
        }

        public bool BulkInsert(IEnumerable<Enrollment> entities)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int entity)
        {
            throw new NotImplementedException();
        }

        public List<Enrollment> GetAll(Expression<Func<Enrollment, bool>> expression = null, Func<IQueryable<Enrollment>, IOrderedQueryable<Enrollment>> orderBy = null, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Enrollment GetById(Expression<Func<Enrollment, bool>> expression, List<string> includes = null)
        {
            throw new NotImplementedException();
        }



        public bool Update(Enrollment entity)
        {
            throw new NotImplementedException();
        }
    }
}
