using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RestApiNetDemo.DAL.Repositories
{
    public class TeacherRepo : IRepository<Teacher, int>, IDisposable
    {
        private DotNetMvcDbEntities _dbEntities;
        private IDbSet<Teacher> _entities;
        private readonly string _errorMessage = string.Empty;

        public DotNetMvcDbEntities Context { get; set; }
        public virtual IQueryable<Teacher> Table => Entities;

        protected virtual IDbSet<Teacher> Entities => _entities ?? (_entities = Context.Set<Teacher>());

        public TeacherRepo(DotNetMvcDbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public TeacherRepo()
        {
            _dbEntities = new DotNetMvcDbEntities();
        }

        public bool Add(Teacher entity)
        {
            try
            {
                _dbEntities.Teachers.Add(entity);
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
                Teacher teacher = _dbEntities.Teachers.FirstOrDefault(s => s.Id == id);
                if (teacher == null) return false;
                _dbEntities.Teachers.Remove(teacher);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetAll(Expression<Func<Teacher, bool>> expression = null, Func<IQueryable<Teacher>, IOrderedQueryable<Teacher>> orderBy = null, List<string> includes = null)
        {
            try
            {
                var teacherList = _dbEntities.Teachers.ToList();
                return teacherList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Teacher GetById(Expression<Func<Teacher, bool>> expression, List<string> includes = null)
        {
            try
            {
                IQueryable<Teacher> query = Table;
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

        public bool Update(Teacher entity)
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