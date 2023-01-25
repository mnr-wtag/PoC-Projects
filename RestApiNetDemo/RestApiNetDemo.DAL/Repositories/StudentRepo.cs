using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RestApiNetDemo.DAL.Repositories
{
    public class StudentRepo : IRepository<Student, int>,IDisposable
    {
        private  DotNetMvcDbEntities _dbEntities;
        private IDbSet<Student> _entities;
        private readonly string _errorMessage = string.Empty;


        public DotNetMvcDbEntities Context { get; set; }
        public virtual IQueryable<Student> Table => Entities;

        protected virtual IDbSet<Student> Entities => _entities ?? (_entities = Context.Set<Student>());

        public StudentRepo()
        {
            _dbEntities= new DotNetMvcDbEntities();
        }

        public StudentRepo(DotNetMvcDbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public bool Add(Student entity)
        {
            try
            {
                _dbEntities.Students.Add(entity);
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
                Student student = _dbEntities.Students.FirstOrDefault(s => s.Id == id);
                if (student == null) return false;
                _dbEntities.Students.Remove(student);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Student> GetAll(Expression<Func<Student, bool>> expression = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, List<string> includes = null)
        {
            try
            {
                List<Student> studentList = _dbEntities.Students.ToList();
                return studentList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Student GetById(Expression<Func<Student, bool>> expression, List<string> includes = null)
        {
            try
            {
                IQueryable<Student> query = Table;
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

        public Student GetById(List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(Student entity)
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
