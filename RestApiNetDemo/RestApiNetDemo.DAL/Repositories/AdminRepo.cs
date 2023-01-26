using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RestApiNetDemo.DAL.Repositories
{
    public class AdminRepo : IRepository<Admin, int>, IDisposable
    {
        private DotNetMvcDbEntities _dbEntities;
        private IDbSet<Admin> _entities;
        private readonly string _errorMessage = string.Empty;

        public DotNetMvcDbEntities Context { get; set; }
        public virtual IQueryable<Admin> Table => Entities;

        protected virtual IDbSet<Admin> Entities => _entities ?? (_entities = Context.Set<Admin>());

        public AdminRepo(DotNetMvcDbEntities dbEntities) => _dbEntities = dbEntities;

        public bool Add(Admin entity)
        {
            try
            {
                _dbEntities.Admins.Add(entity);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Admin admin = _dbEntities.Admins.FirstOrDefault(s => s.Id == id);
                if (admin == null) return false;
                _dbEntities.Admins.Remove(admin);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Admin> GetAll(Expression<Func<Admin, bool>> expression = null, Func<IQueryable<Admin>, IOrderedQueryable<Admin>> orderBy = null, List<string> includes = null)
        {
            try
            {
                List<Admin> adminList = _dbEntities.Admins.ToList();
                return adminList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Admin GetById(Expression<Func<Admin, bool>> expression, List<string> includes = null)
        {
            try
            {
                IQueryable<Admin> query = Table;
                if (expression != null)
                {
                    query = query.Where(expression);
                }
                if (includes != null)
                {
                    query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                }

                var data = query.FirstOrDefault();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(Admin entity)
        {
            try
            {
                _dbEntities.Entry(entity).State = EntityState.Modified;
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbEntities != null)
                {
                    _dbEntities.Dispose();
                    _dbEntities = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}