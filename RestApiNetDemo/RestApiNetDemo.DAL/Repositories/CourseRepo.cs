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
        private readonly IRepository<Cours, int> _repo;

        public CourseRepo()
        {
            _dbEntities = new DotNetMvcDbEntities();
        }

        public CourseRepo(IRepository<Cours, int> repo)
        {
            _repo = repo;
        }

        public bool Add(Cours entity)
        {
            _dbEntities.Courses.Add(entity);
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }

        public bool Delete(int id)
        {
            Cours course = _dbEntities.Courses.FirstOrDefault(s => s.Id == id);
            if (course == null) return false;
            _dbEntities.Courses.Remove(course);
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }

        public List<Cours> GetAll()
        {
            List<Cours> courseList = _dbEntities.Courses.ToList();
            return courseList;
        }

        public Cours GetById(int id)
        {
            Cours course = _dbEntities.Courses.FirstOrDefault(s => s.Id == id);
            return course;
        }

        public bool Update(Cours entity)
        {
            _dbEntities.Entry(entity).State = EntityState.Modified;
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }
    }
}
