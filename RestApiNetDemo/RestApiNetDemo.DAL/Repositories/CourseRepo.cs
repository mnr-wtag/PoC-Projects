using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RestApiNetDemo.DAL.Repositories
{
    public class CourseRepo : IRepository<Cours, int>
    {
        private readonly DotNetMvcDbEntities _dbEntities;

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
            catch (System.Exception)
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
            catch (System.Exception)
            {

                throw;
            }

        }

        public List<Cours> GetAll()
        {
            try
            {
                var courseList = _dbEntities.Courses.ToList();
                return courseList;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public Cours GetById(int id)
        {
            try
            {
                Cours course = _dbEntities.Courses.FirstOrDefault(s => s.Id == id);
                return course;
            }
            catch (System.Exception)
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
            catch (System.Exception)
            {

                throw;
            }


        }
    }
}
