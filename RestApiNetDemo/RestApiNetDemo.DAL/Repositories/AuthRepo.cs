using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RestApiNetDemo.DAL.Repositories
{
    public class AuthRepo : IRepository<AuthUser, int>
    {
        public bool Add(AuthUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int entity)
        {
            throw new NotImplementedException();
        }

        public List<AuthUser> GetAll(Expression<Func<AuthUser, bool>> expression = null, Func<IQueryable<AuthUser>, IOrderedQueryable<AuthUser>> orderBy = null, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public AuthUser GetById(Expression<Func<AuthUser, bool>> expression, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(AuthUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
