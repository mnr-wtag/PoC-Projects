using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RestApiNetDemo.DAL.Repositories
{
    public class DepartmentRepo : IRepository<Department, int>, IDisposable
    {
        private DotNetMvcDbEntities _dbEntities;
        private IDbSet<Department> _entities;
        private readonly string _errorMessage = string.Empty;

        public DotNetMvcDbEntities Context { get; set; }
        public virtual IQueryable<Department> Table => Entities;

        protected virtual IDbSet<Department> Entities => _entities ?? (_entities = Context.Set<Department>());
        public DepartmentRepo()
        {
            _dbEntities = new DotNetMvcDbEntities();
        }

        public DepartmentRepo(DotNetMvcDbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public bool Add(Department entity)
        {
            try
            {
                using (_dbEntities)
                {
                    _dbEntities.Departments.Add(entity);
                    int result = _dbEntities.SaveChanges();
                    return result != 0;
                }


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
                using (_dbEntities)
                {
                    Department course = _dbEntities.Departments.FirstOrDefault(s => s.Id == id);
                    if (course == null) return false;
                    _dbEntities.Departments.Remove(course);
                    int result = _dbEntities.SaveChanges();
                    return result != 0;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Department> GetAll(Expression<Func<Department, bool>> expression = null, Func<IQueryable<Department>, IOrderedQueryable<Department>> orderBy = null, List<string> includes = null)
        {
            try
            {
                using (_dbEntities)
                {
                    var courseList = _dbEntities.Departments.ToList();
                    return courseList;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }



        public Department GetById(Expression<Func<Department, bool>> expression, List<string> includes = null)
        {
            try
            {
                using (_dbEntities)
                {
                    IQueryable<Department> query = Table;
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

            }
            catch (Exception)
            {

                throw;
            }

        }


        public bool Update(Department entity)
        {
            try
            {
                using (_dbEntities)
                {
                    _dbEntities.Entry(entity).State = EntityState.Modified;
                    int result = _dbEntities.SaveChanges();
                    return result != 0;
                }

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
