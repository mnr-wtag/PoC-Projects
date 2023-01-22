using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RestApiNetDemo.DAL.Repositories
{
    public class DepartmentRepo : IRepository<Department, int>
    {
        private readonly DotNetMvcDbEntities _dbEntities;
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
                _dbEntities.Departments.Add(entity);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                Department course = _dbEntities.Departments.FirstOrDefault(s => s.Id == id);
                if (course == null) return false;
                _dbEntities.Departments.Remove(course);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public List<Department> GetAll(Expression<Func<Department, bool>> expression = null, Func<IQueryable<Department>, IOrderedQueryable<Department>> orderBy = null, List<string> includes = null)
        {
            try
            {
                var courseList = _dbEntities.Departments.ToList();
                return courseList;
            }
            catch (System.Exception)
            {

                throw;
            }

        }



        public Department GetById(Expression<Func<Department, bool>> expression, List<string> includes = null)
        {
            try
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
            catch (System.Exception)
            {

                throw;
            }

        }


        public bool Update(Department entity)
        {
            try
            {
                _dbEntities.Entry(entity).State = EntityState.Modified;
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (System.Exception)
            {

                throw;
            }


        }
    }
}
