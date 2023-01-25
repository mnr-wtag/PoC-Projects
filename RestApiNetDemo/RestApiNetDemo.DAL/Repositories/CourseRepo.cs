using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RestApiNetDemo.DAL.Repositories
{
    public class CourseRepo : IRepository<Cours, int>,IDisposable
    {
        private  DotNetMvcDbEntities _dbEntities;
        private IDbSet<Cours> _entities;
        private readonly string _errorMessage = string.Empty;

        public DotNetMvcDbEntities Context { get; set; }
        public virtual IQueryable<Cours> Table => Entities;

        protected virtual IDbSet<Cours> Entities => _entities ?? (_entities = Context.Set<Cours>());
        public CourseRepo()
        {
            _dbEntities = new DotNetMvcDbEntities();
        }

        public CourseRepo(DotNetMvcDbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public bool Add(Cours entity)
        {
            try
            {
                _dbEntities.Courses.Add(entity);
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
                Cours course = _dbEntities.Courses.FirstOrDefault(s => s.Id == id);
                if (course == null) return false;
                _dbEntities.Courses.Remove(course);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Cours> GetAll(Expression<Func<Cours, bool>> expression = null, Func<IQueryable<Cours>, IOrderedQueryable<Cours>> orderBy = null, List<string> includes = null)
        {
            try
            {
                var courseList = _dbEntities.Courses.ToList();
                return courseList;
            }
            catch (Exception)
            {

                throw;
            }

        }



        public Cours GetById(Expression<Func<Cours, bool>> expression, List<string> includes = null)
        {
            try
            {
                IQueryable<Cours> query = Table;
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


        public bool Update(Cours entity)
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
