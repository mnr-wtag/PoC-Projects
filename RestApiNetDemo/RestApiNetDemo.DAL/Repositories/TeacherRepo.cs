using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RestApiNetDemo.DAL.Repositories
{
    internal class TeacherRepo : IRepository<Teacher, int>
    {
        private readonly DotNetMvcDbEntities _dbEntities;
        public TeacherRepo(DotNetMvcDbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public bool Add(Teacher entity)
        {
            _dbEntities.Teachers.Add(entity);
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }

        public bool Delete(int id)
        {
            Teacher teacher = _dbEntities.Teachers.FirstOrDefault(s => s.Id == id);
            if (teacher == null) return false;
            _dbEntities.Teachers.Remove(teacher);
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }

        public List<Teacher> GetAll()
        {
            List<Teacher> teacherList = _dbEntities.Teachers.ToList();
            return teacherList;
        }

        public Teacher GetById(int id)
        {
            Teacher teacher = _dbEntities.Teachers.FirstOrDefault(s => s.Id == id);
            return teacher;
        }

        public bool Update(Teacher entity)
        {
            _dbEntities.Entry(entity).State = EntityState.Modified;
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }
    }
}
